using JFS_Task.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using ServiceStack.Text;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace JFS_Task
{
    public class IndexController : Controller
    {
        [ViewData]
        public List<TurnoverBalance> TurnoverReport { get; set; }

        private const string BalancesListName = "balance";
        private const string PaymentsListName = "";

        private readonly IDataAccessProvider _dataAccessProvider;

        public IndexController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpPost("GetBalances")]
        public ActionResult GetBalances(    // Lots of parameters here
            IFormFile balance,
            IFormFile payment,
            int accountId,
            FileFormat format,
            Period reportPeriod)
        {
            // Clearing data
            _dataAccessProvider.ClearAllData();

            // Parsing files & filling tables
            List<Balance>? balances = JsonSerializerHelper.DeserializeObjectsList<Balance>(BalancesListName, balance);
            if (balances == null)
            {
                TempData["Message"] = "There seems to be a problem with balances file.";
                return Redirect("~/");
            }
            _dataAccessProvider.AddBalanceBulk(balances);
            balances.Clear();

            List<Payment>? payments = JsonSerializerHelper.DeserializeObjectsList<Payment>(PaymentsListName, payment);
            if (payments == null)
            {
                TempData["Message"] = "There seems to be a problem with payments file.";
                return Redirect("~/");
            }
            _dataAccessProvider.AddPaymentBulk(payments);
            payments.Clear();

            // Compiling report from balances and payments lists ordered by a period date
            balances = _dataAccessProvider.GetBalances(accountId);
            payments = _dataAccessProvider.GetPayments(accountId);

            TurnoverReport = new();

            // Here we will keep track of the values
            TurnoverBalance? currentTurnover = null;

            // We will skip the set amount of records using this pointer. Alternatively we could remove them on the spot to improve RAM usage
            // We could also use deferred execution to read the DB in chunks to improve RAM usage further, however this should be done on
            // data access framework level and implemented for all DB tables. Probably something like that already exists, but I won't bother with it for now.

            int monthsPassed = 0;
            DateTime? periodEnd = null;
            
            balances.ForEach(b =>
            {
                if (currentTurnover == null)
                {
                    // It means that we are at the new period
                    currentTurnover = new()
                    {
                        Period = b.Period,
                        StartingBalance = b.InBalance,
                        Accrued = 0,
                        Paid = 0
                    };

                    switch (reportPeriod)
                    {
                        case Period.Month:
                            periodEnd = b.Period.AddMonths(1);                                                      // Set to next month's 1st day
                            break;
                        case Period.Quarter:
                            int quarterNumber = (b.Period.Month - 1) / 3 + 1;
                            periodEnd = new DateTime(b.Period.Year, (quarterNumber - 1) * 3 + 1, 1).AddMonths(3);   // Set to next quarter's 1st day
                            break;
                        case Period.Year:
                            periodEnd = new DateTime(b.Period.Year + 1, 1, 1);                                      // Set to next year's 1st day
                            break;
                        default:
                            throw new NotSupportedException("This report does not support the " + reportPeriod + " period type");
                    }

                    monthsPassed = 0;
                }

                // Adding accrued amount for current month
                currentTurnover.Accrued += b.Calculation;

                // Adding all payments for current month
                foreach (Payment p in payments
                    .SkipWhile(p => p.Date < currentTurnover.Period)                // Not interested in payments outside period
                    .TakeWhile(p => p.Date < currentTurnover.Period.AddMonths(1)))  // Each minor period is 1 month
                {
                    currentTurnover.Paid += p.Sum;
                }

                monthsPassed++;

                // When we reach the end of a period, a new 
                if (currentTurnover.Period.AddMonths(monthsPassed) == periodEnd)
                {
                    currentTurnover.CalculateEndingBalance();

                    TurnoverReport.Add(currentTurnover);
                    currentTurnover = null;
                    monthsPassed = 0;
                }
            });

            if (format == FileFormat.XML)
            {
                // Generating and returning XML file
                XElement xmlElements = new XElement("Report", TurnoverReport.Select(l => new XElement("Line",
                    new XElement("Period", l.Period),
                    new XElement("StartingBalance", l.StartingBalance),
                    new XElement("Accrued", l.Accrued),
                    new XElement("Paid", l.Paid),
                    new XElement("EndingBalance", l.EndingBalance))));

                string fileName = "report.xml";
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "tmp\\" + fileName;
                string fileType = "text/xml";
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "tmp\\");
                System.IO.File.Delete(filePath);
                using (var stream = System.IO.File.Create(filePath))
                {
                    xmlElements.Save(stream);
                }

                return PhysicalFile(filePath, fileType, fileName);
            }
            else if (format == FileFormat.CSV)
            {
                // Generating and returning CSV file
                string csv = CsvSerializer.SerializeToCsv(TurnoverReport);

                string fileName = "report.csv";
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "tmp\\" + fileName;
                string fileType = "text/csv";
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "tmp\\");
                System.IO.File.Delete(filePath);
                using (var stream = System.IO.File.Create(filePath))
                {
                    stream.Write(new UTF8Encoding(true).GetBytes(csv));
                }

                return PhysicalFile(filePath, fileType, fileName);
            }
            else
            {
                return View(TurnoverReport);
            }
        }
    }
}

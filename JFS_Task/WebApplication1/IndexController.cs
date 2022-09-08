using JFS_Task.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace JFS_Task
{
    public class IndexController : Controller
    {
        private const string BalancesListName = "balance";
        private const string PaymentsListName = "";

        private readonly IDataAccessProvider _dataAccessProvider;

        public IndexController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        // GET: IndexController
        public ActionResult Index()
        {
            return View();
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

            // Compiling report
            balances = _dataAccessProvider.GetBalances(accountId);
            payments = _dataAccessProvider.GetPayments(accountId);

            List<TurnoverBalance> turnoverReport = new();
            balances.ForEach(b =>
            {
                // report?
            });

            TempData["Message"] = "Controller executed; accountID: " + accountId;

            return Redirect("~/"); ;

        }
    }
}

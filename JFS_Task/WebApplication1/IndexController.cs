using JFS_Task.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace JFS_Task
{
    public class IndexController : Controller
    {
        private readonly string BalancesListName = "balance";
        private readonly string PaymentsListName = "";

        // GET: IndexController
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost("GetBalances")]
        public ActionResult GetBalances(    // Lots of parameters here.
            IFormFile balance,
            IFormFile payment,
            string accountId,
            FileFormat format,
            Period reportPeriod)
        {
            // Parsing files
            List<Balance>? balances = JsonSerializerHelper.DeserializeObjectsList<Balance>(BalancesListName, balance);
            if (balances == null)
            {
                TempData["Message"] = "There seems to be a problem with balances file.";
                return Redirect("~/");
            }

            List<Payment>? payments = JsonSerializerHelper.DeserializeObjectsList<Payment>(PaymentsListName, payment);
            if (payments == null)
            {
                TempData["Message"] = "There seems to be a problem with payments file.";
                return Redirect("~/");
            }

            TempData["Message"] = "Controller executed; accountID: " + accountId;

            return Redirect("~/"); ;

        }
    }
}

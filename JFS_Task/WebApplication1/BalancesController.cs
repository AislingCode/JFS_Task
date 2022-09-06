using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace JFS_Task
{
    [Route("[controller]")]
    public class BalancesController : Controller
    {
        private readonly string BalancesListName = "balance";
        private readonly string PaymentsListName = "";

        // POST <BalancesController>/GetBalances
        [HttpPost("GetBalances")]
        public ActionResult GetBalances(IFormFile balance, IFormFile payment, string accountId, FileFormat format)
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

            return Redirect("~/");
        }
    }
}

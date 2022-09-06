using Newtonsoft.Json;
using System.Globalization;

namespace JFS_Task
{
    public class Payment
    {
        // Hardcoding this here, since the app will only handle the proposed file format.
        private const string DATETIMEPATTERN = "yyyy-MM-dd HH:mm:ss";

        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public double Sum { get; set; }
        public Guid PaymentGuid { get; set; }

        /// <summary>
        /// This is to account for a custom JSON formatting for the object.
        /// </summary>
        [JsonConstructor]
        public Payment(
            int account_id,
            string date,
            double sum,
            string payment_guid)
        {
            AccountId = account_id;
            Date = DateTime.ParseExact(date, DATETIMEPATTERN, CultureInfo.InvariantCulture);
            Sum = sum;
            PaymentGuid = new Guid(payment_guid);
        }

    }
}

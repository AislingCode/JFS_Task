using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace JFS_Task
{
    [Table("Payment")]
    public class Payment
    {
        // Hardcoding this here, since the app will only handle the proposed file format.
        private const string DateTimePattern = "yyyy-MM-dd HH:mm:ss";

        [Key]
        public int RecId { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; } // It must be Universal Time, but I didn't implement any validations in setter
        public double Sum { get; set; }
        public Guid PaymentGuid { get; set; }

        public Payment()
        {
            // Default parameterless constructor for EntityFramework to use
        }

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
            Date = DateTime.ParseExact(date, DateTimePattern, CultureInfo.InvariantCulture).ToUniversalTime();
            Sum = sum;
            PaymentGuid = new Guid(payment_guid);
        }

    }
}

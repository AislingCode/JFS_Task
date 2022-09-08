using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace JFS_Task
{
    [Table("Balance")]
    public class Balance
    {
        // Hardcoding this here, since the app will only handle the proposed file format.
        private const string DATETIMEPATTERN = "yyyyMM";

        [Key]
        public int RecId { get; set; }
        public int AccountId { get; set; }
        public DateTime Period { get; set; }
        public double InBalance { get; set; }
        public double Calculation { get; set; }

        public Balance()
        {
            // Default parameterless constructor for EntityFramework to use
        }

        /// <summary>
        /// This is to account for a custom JSON formatting for the object.
        /// </summary>
        [JsonConstructor]
        public Balance(
            int account_id,
            string period,
            double in_balance,
            double calculation)
        {
            AccountId = account_id;
            Period = DateTime.SpecifyKind(DateTime.ParseExact(period, DATETIMEPATTERN, CultureInfo.InvariantCulture), DateTimeKind.Utc);
            InBalance = in_balance;
            Calculation = calculation;
        }
    }
}

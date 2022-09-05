using Newtonsoft.Json;

namespace JFS_Task
{
    public class Balance
    {
        int AccountId { get; set; }
        int Period { get; set; }
        double InBalance { get; set; }
        double Calculation { get; set; }

        /// <summary>
        /// This is to account for a custom JSON formatting for the object.
        /// </summary>
        [JsonConstructor]
        public Balance(
            int account_id,
            int period,
            double in_balance,
            double calculation)
        {
            AccountId = account_id;
            Period = period;
            InBalance = in_balance;
            Calculation = calculation;
        }
    }
}

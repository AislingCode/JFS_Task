using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JFS_Task
{
    [Table("TurnoverBalance")]
    public class TurnoverBalance
    {
        [Key]
        public DateTime Period { get; set; }
        public double StartingBalance { get; set; }
        public double Accrued { get; set; }
        public double Paid { get; set; }
        public double EndingBalance { get; set; }

        public void CalculateEndingBalance()
        {
            EndingBalance = StartingBalance + Accrued - Paid;
        }
    }
}

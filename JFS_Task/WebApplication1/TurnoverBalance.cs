using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JFS_Task
{
    [Table("TurnoverBalance")]
    public class TurnoverBalance
    {
        [Key]
        public DateTime Period { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double StartingBalance { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double Accrued { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double Paid { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double EndingBalance { get; set; }

        public void CalculateEndingBalance()
        {
            EndingBalance = StartingBalance + Accrued - Paid;
        }
    }
}

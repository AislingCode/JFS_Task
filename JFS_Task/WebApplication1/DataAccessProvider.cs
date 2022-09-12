using Microsoft.EntityFrameworkCore;

namespace JFS_Task
{
    /// <summary>
    /// This is a general data access provider class, which handles everything related to the DB.
    /// </summary>
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly DomainModelPostgreSqlContext _context;

        public DataAccessProvider(DomainModelPostgreSqlContext context)
        {
            _context = context;
        }

        public void AddBalanceBulk(List<Balance> balances)
        {
            _context.Balances.AddRange(balances.ToArray());
            _context.SaveChanges();
        }

        public void AddPaymentBulk(List<Payment> payments)
        {
            _context.Payments.AddRange(payments.ToArray());
            _context.SaveChanges();
        }

        public void ClearAllData()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Balance\"");
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Payment\"");
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"TurnoverBalance\"");
        }

        public List<Balance> GetBalances(int AccountId)
        {
            return _context.Balances.Where(e => e.AccountId == AccountId).OrderBy(e => e.Period).ToList();
        }

        public List<Payment> GetPayments(int AccountId)
        {
            return _context.Payments.Where(e => e.AccountId == AccountId).OrderBy(e => e.Date).ToList();
        }
    }
}

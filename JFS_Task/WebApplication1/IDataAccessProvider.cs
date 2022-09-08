namespace JFS_Task
{
    public interface IDataAccessProvider
    {
        void AddBalanceBulk(List<Balance> balances);
        void AddPaymentBulk(List<Payment> payments);

        void ClearAllData();
        List<Balance> GetBalances(int AccountId);
        List<Payment> GetPayments(int AccountId);
    }
}

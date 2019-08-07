namespace Core.Model
{
    public class Balance : Transaction
    {
        public Balance(int customerId) : base(customerId)
        {
        }

        // ServiceResult
        public string BalanceAmount { get; set; }
    }
}
namespace Core.Model
{
    public class Payment : Transaction
    {
        public Payment(int customerId) : base(customerId)
        {
        }

        /// مبلغ
        public string Amount { get; set; }

        public int UnitId { get; set; }
    }
}


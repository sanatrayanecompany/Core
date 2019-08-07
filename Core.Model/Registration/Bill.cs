namespace Core.Model
{
    public class Bill : Transaction
    {
        public Bill(int customerId) : base(customerId)
        {
        }

        /// مبلغ
        public string Amount { get; set; }

        // شناسه قبض
        public string BillId { get; set; }

        // شناسه پرداخت
        public string PaymentId { get; set; }
    }
}


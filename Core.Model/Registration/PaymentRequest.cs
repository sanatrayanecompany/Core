namespace Core.Model
{
    public class PaymentRequest : Request
    {
        public PaymentRequest(int customerId) : base(customerId)
        {
        }

        /// مبلغ
        public string Amount { get; set; }
        public int UnitId { get; set; }
    }
}


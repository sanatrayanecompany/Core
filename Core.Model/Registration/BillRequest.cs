namespace Core.Model
{
    public class BillRequest : Request
    {
        public BillRequest(int customerId) : base(customerId)
        {
        }

        // مبلغ
        public string Amount { get; set; }
        
        // شناسه قبض
        public string BillId { get; set; }
      
        // شناسه پرداخت
        public string PaymentId { get; set; }
    }
}


namespace Core.Model
{
    public class VoucherRequest : Request
    {
        public VoucherRequest(int customerId) : base(customerId)
        {
        }

        // مبلغ
        public string Amount { get; set; }

        // اپراتور
        public string Operator { get; set; }
    }
}


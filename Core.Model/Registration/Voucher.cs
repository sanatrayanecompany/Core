namespace Core.Model
{
    public class Voucher : Transaction
    {
        public Voucher(int customerId) : base(customerId)
        {
        }

        // مبلغ
        public string Amount { get; set; }

        // اپراتور
        public string Operator { get; set; }
    }
}


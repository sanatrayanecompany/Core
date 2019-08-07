namespace Core.Model
{
    public class FundTransferRequest : Request
    {
        public FundTransferRequest(int customerId) : base(customerId)
        {
        }

        // مبلغ
        public string Amount { get; set; }

        // شماره کارت مقصد   
        public string DestPAN { get; set; }
    }
}
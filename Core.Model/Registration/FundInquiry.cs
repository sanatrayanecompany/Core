namespace Core.Model
{
    public class FundInquiry : Transaction
    {
        public FundInquiry(int customerId) : base(customerId)
        {
        }

        // نام
        public string Fullname { get; set; }

        // شماره کارت مقصد   
        public string DestPAN { get; set; }
    }
}


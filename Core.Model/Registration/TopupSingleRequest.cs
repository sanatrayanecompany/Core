namespace Core.Model
{
    public class TopupSingleRequest : Request
    {
        public TopupSingleRequest(int customerId) : base(customerId)
        {
        }

        // مبلغ
        public string Amount { get; set; }
        
        // موبایل
        public string Mobile { get; set; }

        // طرح
        public string profileId { get; set; }
    }
}


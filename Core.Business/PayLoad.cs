using Core.Base;

namespace Core.Business
{
    public class PayLoad : IPayLoad
    {
        public long UserId { get; set; }

        public int[] Roles { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public int BranchId { get; set; }

    }
}

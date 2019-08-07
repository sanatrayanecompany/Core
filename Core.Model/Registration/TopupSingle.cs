using System;

namespace Core.Model
{
    public class TopupSingle : Transaction
    {
        public TopupSingle(int customerId) : base(customerId)
        {
        }

        // مبلغ
        public string Amount { get; set; }

        // موبایل
        public string Mobile { get; set; }
    }
}


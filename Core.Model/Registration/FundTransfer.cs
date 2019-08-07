using System;
using System.Security.AccessControl;

namespace Core.Model
{
    public class FundTransfer : Transaction
    {
        public FundTransfer(int customerId) : base(customerId)
        {
        }

        // مبلغ
        public string Amount { get; set; }

        // شماره کارت مقصد   
        public string DestPAN { get; set; }

        // داده دریافتی از استعلام کارت به کارت بانک کشاورزی
        public string DualData { get; set; }

        // در کارت به کارت بانک کشاورزی استفاده میشود. در درخواست کارت به کارت بانک کشاورزی، یک عدد پیامک میشود که می بایست در این فیلد وارد شود
        public string VerificationCod { get; set; }

    }
}


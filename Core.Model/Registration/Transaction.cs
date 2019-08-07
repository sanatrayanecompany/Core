using System;

namespace Core.Model
{
    public abstract class Transaction: Request
    {
        public Transaction(int CustomerId):base(CustomerId)
        {
            
        }

        /// اطالعات احراز هویت )این فیلد از نوع کالس میباشد که دارای فیلدهای password, username میباشد.(
        public AuthInfo AuthInfo { get; set; }
        
        /// شماره موبایل
        public string CustMobile { get; set; }
        
        /// تاریخ خرید(MMDD -(میالدی
        public string Date { get; set; }
        
        /// کد پذیرنده
        public string MerchantID { get; set; }

        /// (شماره پیگیری )عدد حداکثر تا 9 رقم
        public string STAN { get; set; }

        /// زمان درخواست(HH24MMSS(
        public string Time { get; set; }

        /// شماره فاکتور )اختیاری(
        public string InvoiceNo { get; set; }
        
        /// نسخه برنامه موبایلی
        public int Version { get; set; }
        
        // ServiceResult
        public string RefNo { get; set; } //کد تایید
        public int ResponseCode { get; set; } //کد خطا
        public bool Success { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class RequestCP
    {
        /// <summary>
        /// اطالعات احراز هویت )این فیلد از نوع کالس میباشد که دارای فیلدهای password, username میباشد.(
        /// </summary>
        public AuthInfo authInfo { get; set; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string custMobile { get; set; }

        /// <summary>
        /// تاریخ خرید(MMDD -(میالدی
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// شماره IP دستگاه
        /// </summary>
        public string localIP { get; set; }

        /// <summary>
        /// کد پذیرنده
        /// </summary>
        public string merchantID { get; set; }

        /// <summary>
        /// (شماره پیگیری )عدد حداکثر تا 9 رقم
        /// </summary>
        public string STAN { get; set; }

        /// <summary>
        /// سریال ترمینال
        /// </summary>
        public string serial { get; set; }

        /// <summary>
        /// کد ترمینال
        /// </summary>
        public string terminalID { get; set; }

        /// <summary>
        /// زمان درخواست(HH24MMSS(
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// مبلغ خرید
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// شماره فاکتور )اختیاری(
        /// </summary>
        public string invoiceNo { get; set; }

        /// <summary>
        /// شماره کارت )فقط اعداد کارت بدون خط تیره و فاصله(
        /// </summary>
        public string PAN { get; set; }

        /// <summary>
        /// پین رمز شده
        /// </summary>
        public string PINBlock { get; set; }

        /// <summary>
        /// نسخه برنامه موبایلی
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// track2 کارت
        /// </summary>
        public string track2Ciphered { get; set; }


        // ServiceResult
        public bool Success { get; set; }
        public string refNo { get; set; }//کد تایید
        public int ResponseCode { get; set; } //کد خطا
    }


    public class StockCP
    {
        /// <summary>
        /// اطالعات احراز هویت )این فیلد از نوع کالس میباشد که دارای فیلدهای password, username میباشد.(
        /// </summary>
        public AuthInfo authInfo { get; set; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string custMobile { get; set; }

        /// <summary>
        /// تاریخ خرید(MMDD -(میالدی
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// شماره IP دستگاه
        /// </summary>
        public string localIP { get; set; }

        /// <summary>
        /// کد پذیرنده
        /// </summary>
        public string merchantID { get; set; }

        /// <summary>
        /// (شماره پیگیری )عدد حداکثر تا 9 رقم
        /// </summary>
        public string STAN { get; set; }

        /// <summary>
        /// سریال ترمینال
        /// </summary>
        public string serial { get; set; }

        /// <summary>
        /// کد ترمینال
        /// </summary>
        public string terminalID { get; set; }

        /// <summary>
        /// زمان درخواست(HH24MMSS(
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// مبلغ خرید
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// شماره فاکتور )اختیاری(
        /// </summary>
        public string invoiceNo { get; set; }

        /// <summary>
        /// شماره کارت )فقط اعداد کارت بدون خط تیره و فاصله(
        /// </summary>
        public string PAN { get; set; }

        /// <summary>
        /// پین رمز شده
        /// </summary>
        public string PINBlock { get; set; }

        /// <summary>
        /// نسخه برنامه موبایلی
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// track2 کارت
        /// </summary>
        public string track2Ciphered { get; set; }


        // ServiceResult
        public bool Success { get; set; }
        public string refNo { get; set; }//کد تایید
        public int ResponseCode { get; set; } //کد خطا
    }
    public class RequestPay
    {
       
        /// <summary>
        /// شماره IP دستگاه
        /// </summary>
        public string localIP { get; set; }

        /// <summary>
        /// سریال ترمینال
        /// </summary>
        public string serial { get; set; }

        /// <summary>
        /// کد ترمینال
        /// </summary>
        public string terminalID { get; set; }

      
        /// <summary>
        /// مبلغ خرید
        /// </summary>
        public string amount { get; set; }
        
        /// <summary>
        /// شماره کارت )فقط اعداد کارت بدون خط تیره و فاصله(
        /// </summary>
        public string PAN { get; set; }

        /// <summary>
        /// پین رمز شده
        /// </summary>
        public string PINBlock { get; set; }

       
        /// <summary>
        /// track2 کارت
        /// </summary>
        public string track2Ciphered { get; set; }
    }
    
}


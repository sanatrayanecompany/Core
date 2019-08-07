using Core.WebApi;
using System;
using System.Linq;

namespace Core.Model
{
    public class Request
    {
        private MerchantInfo merchantInfo = new MerchantInfo();

        public Request(Int32 customerID)
        {
            using (var context = new DbContextDataContext())
            {
                merchantInfo = context.MerchantInfos.Where(m => m.CustomerId == customerID).SingleOrDefault();
                LocalIP = merchantInfo.localIP.Trim();
                Serial = merchantInfo.Serial.Trim();
                MerchantID = merchantInfo.merchantID.Trim();
                TerminalID = merchantInfo.terminalID.Trim();
                Mobile = merchantInfo.Mobile.Trim();
                CustomerId = customerID;
                InvoiceNo = "1001";
            }
        }

        public Int32 CustomerId { private set; get; }

        public static AuthInfo AuthInfo
        {
            get
            {
                return new AuthInfo()
                {
                    Username = "FANAVA",
                    Password = "123"
                };
            }
        }

        public static string LocalIP { private set; get; }

        public static string Serial { private set; get; }

        public static string InvoiceNo { set; get; }

        public static string STAN
        {
            get
            {
                Random generator = new Random();
                String value = generator.Next(1, 99999).ToString("D5");
                return value;
            }
        }

        public static string MerchantID { private set; get; }

        public static string TerminalID { private set; get; }

        public static string Mobile { private set; get; }

        public static string Date
        {
            get { return DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0'); }
        }

        public static string Time
        {
            get
            {
                return DateTime.Now.Hour.ToString() +
                       DateTime.Now.Minute.ToString() +
                       DateTime.Now.Second.ToString();
            }
        }

        /// شماره کارت )فقط اعداد کارت بدون خط تیره و فاصله(
        public string PAN { get; set; }

        /// پین رمز شده
        public string PINBlock { get; set; }
        
        /// track2 کارت
        public string Track2Ciphered { get; set; }
    }
}


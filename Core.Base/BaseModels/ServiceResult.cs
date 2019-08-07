using System.Collections.Generic;

namespace Core.Base
{
    public class ServiceResultClient
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string RefNo { get; set; } //کد تایید
        public string ClientRefNo { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }

    public class ServiceResult<T>
    {
        public ServiceResult()
        {

        }

        public List<string> Message { get; set; } = new List<string>();

        public bool Error
        {
            get { return Error; }
            set { Success = !Error; }
        }

        public bool Success
        {
            get { return Success; }
            set { Error = !Success; }
        }

        public List<T> Data { get; set; }
    }

    public enum TransactionStatus
    {
        SuccessfulTransaction = 0, // تراکنش موفق
        TransactionFaild = 1, // تراکنش ناموفق
        LackOfEnoughAmount = 2, // عدم موجودی کافی
        InvalidPassword = 3 // رمز نامعتبر است
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{

    public class ServiceResultClient
    {
        public string status { get; set; }
        public string message { get; set; }
        public string refNo { get; set; }//کد تایید
        public string clientRefNo { get; set; }
        public string date { get; set; }
        public string time { get; set; }



    }

    public class ServiceResult<T>
    {
        public ServiceResult()
        {

        }
        public List<string> Message { get; set; } = new List<string>();
        public bool Error
        {
            get
            {
                return Error;
            }
            set
            {
                Success = !Error;
            }
        }
        public bool Success
        {
            get
            {
                return Success;
            }
            set
            {
                Error = !Success;
            }
        }
        public List<T> Data { get; set; }

    }

    public enum StatusTransaction
    {
        SuccessfulTransaction = 0,// تراکنش موفق
        TransactionFaild = 1,// تراکنش ناموفق
        LackOfEnoughAmount = 2,// عدم موجودی کافی
        InvalidPassword = 3// رمز نامعتبر است

    }

}

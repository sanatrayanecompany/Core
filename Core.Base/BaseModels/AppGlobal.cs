using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public class AppGlobal
    {
        public static ServiceResult<object> ServiceResultInstance(bool error, string message, object data = null)
        {

            var serviceResultInstance = new ServiceResult<object>() { Error = error, Message = new List<string>() { message }, Data = new List<object>() { data } };
            return serviceResultInstance;
        }
        public static ServiceResultClient ServiceResultClientInstance(bool error, string message)
        {
            return new ServiceResultClient()
            {
                status = !error ? StatusTransaction.SuccessfulTransaction.ToString() : StatusTransaction.TransactionFaild.ToString(),
                message = message,
            };
        }
    }

}

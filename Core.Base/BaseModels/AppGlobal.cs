using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public class AppGlobal
    {
        public static ServiceResultClient ServiceResultClientInstance(bool error, string message)
        {
            return new ServiceResultClient()
            {
                Status = !error ? TransactionStatus.SuccessfulTransaction.ToString() : TransactionStatus.TransactionFaild.ToString(),
                Message = message,
            };
        }
    }

}

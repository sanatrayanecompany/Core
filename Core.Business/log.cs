using System.Linq;
using Core.Model;
using Core.WebApi;
using Core.Business.FanAvaServices_Mobile;
using OfficeOpenXml.FormulaParsing;

namespace Core.Business
{
    public class log
    {
        public static void InsertLog(Payment model, string transactionResponseId)
        {
            using (var context = new WebApi.DbContextDataContext())
            {
                var merchanetinfo= context.MerchantInfos.Where(m => m.CustomerId == model.CustomerId).Single();
                var request = new WebApi.Request()
                {
                    Success = model.Success,
                    RefNo = model.RefNo,
                    PAN = model.PAN,
                    STAN = model.STAN,
                    TransactionResponseId = transactionResponseId,
                    MerchantInfoId = merchanetinfo.Id,
                };
                context.Requests.InsertOnSubmit(request);
                context.SubmitChanges();

                context.PaymentTransactions.InsertOnSubmit(new PaymentTransaction()
                {
                    Amount = model.Amount,
                    UnitId =1,
                    RequestId = request.Id
                });

                context.SubmitChanges();
            }
        }

    }
}

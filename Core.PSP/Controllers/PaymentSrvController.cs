using Core.Business;
using Core.Model;
using Core.Base;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using System;
using System.IO;

namespace Core.PSP.Controllers
{


    public class Token
    {
        public string token { get; set; }
    }
    [RoutePrefix("api/PaymentSrv")]
    public class PaymentSrvController : _Controller
    {
        public PaymentBiz _PaymentBiz { get; set; }
        public PaymentSrvController(PaymentBiz paymentBiz)
        {
            _PaymentBiz = paymentBiz;
        }

        

        [HttpPost]
        [Route("PaymentRequest")]
        public async Task<ServiceResultClient> PaymentRequest(RequestPay model)
        {
            var resultPay = await _PaymentBiz.PaymentRequest(model);
            return resultPay;
        }










        //var result = new ServiceResultClient()
        //{
        //    refNo = "151502150",
        //    clientRefNo = "1050",
        //    date = "1397/12/21",
        //    time = "09:42",
        //    message = "تراکنش موفق",
        //    status = StatusTransaction.SuccessfulTransaction.ToString()
        //};
        [HttpGet]
        [Route("test")]
        //[AuthorizeUser((int)Roles.Admin, (int)Roles.User)]
        public string test()
        {
            return "Success";
        }



    }
}

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


    [RoutePrefix("api/StockSrv")]
    public class StockSrvController : _Controller
    {
        public StockBiz _StockBiz { get; set; }
        public StockSrvController(StockBiz stockBiz)
        {
            _StockBiz = stockBiz;
        }


        [HttpPost]
        [Route("stock")]
        public async Task<ServiceResultClient> StockRequest(RequestPay model)
        {
            var resultPay = await _StockBiz.StockRequest(model);
            return resultPay;
        }
        
    }
}

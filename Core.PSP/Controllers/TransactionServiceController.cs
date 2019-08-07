using Core.Business;
using Core.Model;
using Core.Base;
using System.Web.Http;
using System.Threading.Tasks;

namespace Core.PSP.Controllers
{
    [RoutePrefix("api/TransactionService")]
    public class TransactionServiceController : _Controller
    {
        public TransactionService TransactionService { get; set; }
        public TransactionServiceController(TransactionService transactionService)
        {
            TransactionService = transactionService;
        }
        
        [HttpPost]
        [Route("PaymentRequest")]
        public async Task<ServiceResultClient> PaymentRequest(PaymentRequest model)
        {
            return await TransactionService.PaymentRequestAsync(model);
        }

        [HttpPost]
        [Route("BalanceRequest")]
        public async Task<ServiceResultClient> BalanceRequest(BalanceRequest model)
        {
            return await TransactionService.BlanceRequestAsync(model);
        }

        [HttpPost]
        [Route("FundInquiryRequest")]
        public async Task<ServiceResultClient> FundInquiryRequestRequest(FundTransferRequest model)
        {
            return await TransactionService.FundInquiryRequestAsync(model);
        }

        [HttpPost]
        [Route("FundTransferRequest")]
        public async Task<ServiceResultClient> FundTransferRequest(FundTransferRequest model)
        {
            return await TransactionService.FundTransferRequestAsync(model);
        }

        [HttpPost]
        [Route("TopupRequest")]
        public async Task<ServiceResultClient> TopupRequest(TopupSingleRequest model)
        {
            return await TransactionService.TopupSingleRequestAsync(model);
        }

        [HttpPost]
        [Route("VoucherRequest")]
        public async Task<ServiceResultClient> VoucherRequest(VoucherRequest model)
        {
            return await TransactionService.VoucherRequestAsync(model);
        }

        [HttpPost]
        [Route("BillRequest")]
        public async Task<ServiceResultClient> BillRequest(BillRequest model)
        {
            return await TransactionService.BillRequestAsync(model);
        }
        
        [HttpGet]
        [Route("test")]
        public string test()
        {
            return "Success";
        }
    }
}
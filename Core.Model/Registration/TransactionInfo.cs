using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Registration
{

    public class TransactionInfo
    {
        public string Track2Card { get; set; }

        public string Pan { get; set; }

        public string Pinblock { get; set; }

        public bool Confirm { get; set; }
        
        public PaymentRequest PaymentRequest { get; set; }

        public BalanceRequest BalanceRequest { get; set; }

        public FundTransferRequest FundTransferRequest { get; set; }

        public BillRequest BillRequest { get; set; }


        //public TopupRequest TopupRequest { get; set; }

        public VoucherRequest VoucherRequest { get; set; }

    }
}

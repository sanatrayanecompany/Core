using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;
using Core.Base;
using Core.Business.FanAvaServices_Mobile;
using Request = Core.Model.Request;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using Core.Model.Registration;

namespace Core.Business
{
    public class TransactionService : _Service
    {
        private ServiceResultClient TransactionMessage(string responseCode, string responseMessage)
        {
            switch (responseCode)
            {
                case "00":
                case "08":
                case "16":
                    return AppGlobal.ServiceResultClientInstance(false, responseMessage ?? "عملیات موفق");

                case "01":
                case "19":
                case "34":
                case "90":
                case "1":
                    return AppGlobal.ServiceResultClientInstance(true, responseMessage ?? "عملیات ناموفق");

                case "10":
                case "02":
                case "05":
                case "12":
                case "14":
                case "23":
                case "30":
                case "31":
                case "38":
                    return AppGlobal.ServiceResultClientInstance(true, responseMessage ?? "تراکنش نامعتبر است");

                case "03":
                case "57":
                case "58":
                case "61":
                case "62":
                case "65":
                case "75":
                    return AppGlobal.ServiceResultClientInstance(true, responseMessage ?? "تراکنش نامعتبر است");

                case "51":
                    return AppGlobal.ServiceResultClientInstance(true, responseMessage ?? "عدم موجودي کافي");

                case "63":
                    return AppGlobal.ServiceResultClientInstance(true, responseMessage ?? "تراکنش ناموفق");

                case "55":
                    return AppGlobal.ServiceResultClientInstance(true, responseMessage ?? "رمز نامعتبر است");

                case "15":
                case "33":
                case "39":
                case "41":
                case "42":
                case "44":
                case "52":
                case "53":
                case "54":
                case "56":
                    return AppGlobal.ServiceResultClientInstance(true, responseMessage ?? "کارت نامعتبر است");
                    
                default:
                    return AppGlobal.ServiceResultClientInstance(true, responseMessage ?? "خطای نامشخص");
            }
        }

        /// <summary>
        /// عملیات تست اتصال 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> TestConnection()
        {
            var requestCpIn = new requestCPIn();
            SoapMobileClient s = new SoapMobileClient();

            var resultRequestCP = s.requestCP(requestCpIn);
            var resultRequestCPMessage = TransactionMessage(resultRequestCP.responseCode, resultRequestCP.responseMsg);

            return resultRequestCPMessage != null ? true : false;
        }

        /// <summary>
        /// گرفتن اطلاعات مشتری 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Client> FindClientInfo(int customerId)
        {
            try
            {
                using (var context = new WebApi.DbContextDataContext())
                {
                    var client = context.ClientInfos.Where(m => m.CustomerId == customerId).Single();

                    if (client != null)
                    {
                        return new Client()
                        {
                            CustomerName = client.CompanyName,
                            CustomerTell = client.BusinessTell,
                            HasEppKeyboard = client.HasEppKeyboard ?? false
                        };
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// عملیات خرید 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResultClient> PaymentRequestAsync(PaymentRequest model)
        {
            try
            {
                Payment payment = new Payment(model.CustomerId);

                var requestCpIn = new requestCPIn();
                requestCpIn.authInfo = new authInfo()
                {
                    userName = Request.AuthInfo.Username,
                    password = Request.AuthInfo.Password
                };
                payment.MerchantID = requestCpIn.merchantID = Request.MerchantID;
                requestCpIn.terminalID = Request.TerminalID;
                payment.Amount = requestCpIn.amount = model.Amount.ToString();
                payment.Date = requestCpIn.date = Request.Date;
                payment.Time = requestCpIn.time = Request.Time;
                requestCpIn.localIP = Request.LocalIP;
                payment.PAN = requestCpIn.PAN = model.PAN;
                requestCpIn.PINBlock = model.PINBlock;
                requestCpIn.serial = Request.Serial;
                payment.STAN = requestCpIn.STAN = Request.STAN;
                requestCpIn.track2Ciphered = model.Track2Ciphered;
                requestCpIn.custMobile = Request.Mobile;
                payment.InvoiceNo = requestCpIn.invoiceNo = Request.InvoiceNo;

                SoapMobileClient s = new SoapMobileClient();

                var resultRequestCP = s.requestCP(requestCpIn);
                log.InsertLog(payment, resultRequestCP.responseCode);

                var resultRequestCPMessage = TransactionMessage(resultRequestCP.responseCode, resultRequestCP.responseMsg);
                if (resultRequestCPMessage.Status != TransactionStatus.SuccessfulTransaction.ToString())
                {
                    payment.Success = false;
                    payment.RefNo = resultRequestCP.refNo;
                    return resultRequestCPMessage;
                }
                else
                {
                    var settleReverseIn = new settleReverseIn()
                    {
                        authInfo = requestCpIn.authInfo,
                        custMobile = requestCpIn.custMobile,
                        date = requestCpIn.date,
                        localIP = requestCpIn.localIP,
                        merchantID = requestCpIn.merchantID,
                        orgRefNo = resultRequestCP.refNo,
                        terminalID = requestCpIn.terminalID,
                        serial = requestCpIn.serial,
                        time = requestCpIn.time,
                        orgAmount = requestCpIn.amount,
                        transType = "paymentrequest",
                        STAN = requestCpIn.STAN,
                        version = requestCpIn.version
                    };
                    var resultSettlement = s.settlement(settleReverseIn);
                    var resultSettlementMessage = TransactionMessage(resultSettlement.responseCode, resultSettlement.responseMsg);

                    resultSettlementMessage.ClientRefNo = settleReverseIn.STAN;
                    resultSettlementMessage.Date = settleReverseIn.date;
                    resultSettlementMessage.RefNo = settleReverseIn.orgRefNo;
                    resultSettlementMessage.Time = settleReverseIn.time;

                    payment.Success =
                        resultSettlementMessage.Status == TransactionStatus.SuccessfulTransaction.ToString()
                            ? true
                            : false;

                    return resultSettlementMessage;
                }
            }
            catch (Exception ex)
            {
                return AppGlobal.ServiceResultClientInstance(true, "عملیات نا موفق");
            }
        }
        
        /// <summary>
        /// موجودی
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResultClient> BlanceRequestAsync(BalanceRequest model)
        {
            try
            {
                Balance balance = new Balance(model.CustomerId);
                var requestCpIn = new requestCPIn()
                {
                    authInfo = new authInfo()
                    {
                        userName = Request.AuthInfo.Username,
                        password = Request.AuthInfo.Password
                    }
                };

                balance.MerchantID = requestCpIn.merchantID = Request.MerchantID;
                requestCpIn.terminalID = Request.TerminalID;
                requestCpIn.amount = "0";
                balance.Date = requestCpIn.date = Request.Date;
                balance.Time = requestCpIn.time = Request.Time;
                requestCpIn.localIP = Request.LocalIP;
                balance.PAN = requestCpIn.PAN = model.PAN;
                requestCpIn.PINBlock = model.PINBlock;
                requestCpIn.serial = Request.Serial;
                balance.STAN = requestCpIn.STAN = Request.STAN;
                requestCpIn.track2Ciphered = model.Track2Ciphered;
                requestCpIn.custMobile = Request.Mobile;
                balance.InvoiceNo = requestCpIn.invoiceNo = Request.InvoiceNo;

                SoapMobileClient soapMobileClient = new SoapMobileClient();

                var resultBalanceCP = soapMobileClient.balanceCP(requestCpIn);
                var resultRequestCPMessage = TransactionMessage(resultBalanceCP.responseCode, resultBalanceCP.responseMsg);
                if (resultRequestCPMessage.Status != TransactionStatus.SuccessfulTransaction.ToString())
                {
                    balance.Success = false;
                    //var resultLog = await InsertLog(balance);
                    balance.RefNo = resultBalanceCP.refNo;
                    balance.BalanceAmount = resultBalanceCP.responseMsg;
                    return resultRequestCPMessage;
                }
                else
                {
                    var settleReverseIn = new settleReverseIn()
                    {
                        authInfo = requestCpIn.authInfo,
                        custMobile = requestCpIn.custMobile,
                        date = requestCpIn.date,
                        localIP = requestCpIn.localIP,
                        merchantID = requestCpIn.merchantID,
                        orgRefNo = resultBalanceCP.refNo,
                        terminalID = requestCpIn.terminalID,
                        serial = requestCpIn.serial,
                        time = requestCpIn.time,
                        orgAmount = requestCpIn.amount,
                        transType = "balance",
                        STAN = requestCpIn.STAN,
                        version = requestCpIn.version
                    };
                    var resultSettlement = soapMobileClient.settlement(settleReverseIn);
                    var resultSettlementMessage = TransactionMessage(resultSettlement.responseCode, resultSettlement.responseMsg);

                    resultSettlementMessage.ClientRefNo = settleReverseIn.STAN;
                    resultSettlementMessage.Date = settleReverseIn.date;
                    resultSettlementMessage.RefNo = settleReverseIn.orgRefNo;
                    resultSettlementMessage.Time = settleReverseIn.time;

                    balance.Success = true;
                    //var resultLog = await InsertLog(balance);

                    return resultSettlementMessage;
                }
            }
            catch (Exception ex)
            {
                return AppGlobal.ServiceResultClientInstance(true, "عملیات نا موفق");
            }
        }

        /// <summary>
        /// استعلام کارت مقصد در عملبات کارت به کارت
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResultClient> FundInquiryRequestAsync(FundTransferRequest model)
        {
            try
            {
                var fundInquiry = new FundInquiry(model.CustomerId);
                var fundInquiryIn = new fundInquiryIn()
                {
                    authInfo = new authInfo()
                    {
                        userName = Request.AuthInfo.Username,
                        password = Request.AuthInfo.Password
                    }
                };

                fundInquiryIn.merchantID = Request.MerchantID;
                fundInquiryIn.terminalID = Request.TerminalID;
                fundInquiryIn.date = Request.Date;
                fundInquiryIn.time = Request.Time;
                fundInquiryIn.localIP = Request.LocalIP;
                fundInquiryIn.PAN = model.PAN;
                fundInquiryIn.PINBlock = model.PINBlock;
                fundInquiryIn.serial = Request.Serial;
                fundInquiryIn.STAN = Request.STAN;
                fundInquiryIn.custMobile = Request.Mobile;
                fundInquiryIn.invoiceNo = Request.InvoiceNo;
                fundInquiryIn.amount = model.Amount;
                fundInquiryIn.destPAN = model.DestPAN;

                SoapMobileClient soapMobileClient = new SoapMobileClient();

                var resultFundTransfer = soapMobileClient.fundInquiry(fundInquiryIn);
                var resultRequestCPMessage =
                    TransactionMessage(resultFundTransfer.responseCode, resultFundTransfer.responseMsg);
                if (resultRequestCPMessage.Status != TransactionStatus.SuccessfulTransaction.ToString())
                {
                    fundInquiry.Success = false;
                    fundInquiry.RefNo = resultFundTransfer.refNo;
                    fundInquiry.Fullname = string.Empty;
                    return resultRequestCPMessage;
                }
                else
                {
                    fundInquiry.Success = true;
                   return new ServiceResultClient()
                   {
                       Status = TransactionStatus.SuccessfulTransaction.ToString(),
                       RefNo = fundInquiry.RefNo,
                       Date = fundInquiry.Date,
                       Message = resultFundTransfer.firstName + " " + resultFundTransfer.lastName,
                       ClientRefNo = fundInquiryIn.STAN
                   };
                }
            }
            catch (Exception ex)
            {
                return AppGlobal.ServiceResultClientInstance(true, "عملیات نا موفق");
            }
        }

        /// <summary>
        /// عملبات کارت به کارت
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResultClient> FundTransferRequestAsync(FundTransferRequest model)
        {
            try
            {
                var fundTransfer = new FundTransfer(model.CustomerId);

                var fundTransferIn = new fundTransferIn()
                {
                    authInfo = new authInfo()
                    {
                        userName = Request.AuthInfo.Username,
                        password = Request.AuthInfo.Password
                    }
                };

                fundTransfer.MerchantID = fundTransferIn.merchantID = Request.MerchantID;
                fundTransferIn.terminalID = Request.TerminalID;
                fundTransfer.Date = fundTransferIn.date = Request.Date;
                fundTransfer.Time = fundTransferIn.time = Request.Time;
                fundTransferIn.localIP = Request.LocalIP;
                fundTransfer.PAN = fundTransferIn.PAN = model.PAN;
                fundTransferIn.PINBlock = model.PINBlock;
                fundTransferIn.serial = Request.Serial;
                fundTransfer.STAN = fundTransferIn.STAN = Request.STAN;
                fundTransfer.Track2Ciphered = model.Track2Ciphered;
                fundTransferIn.custMobile = Request.Mobile;
                fundTransfer.InvoiceNo = fundTransferIn.invoiceNo = Request.InvoiceNo;
                fundTransfer.Amount = fundTransferIn.amount = model.Amount;
                fundTransfer.DestPAN = fundTransferIn.destPAN = model.DestPAN;
                fundTransfer.DualData = fundTransferIn.dualData;
                fundTransfer.VerificationCod = fundTransferIn.verificationCode;

                SoapMobileClient soapMobileClient = new SoapMobileClient();

                var resultFundTransfer = soapMobileClient.fundTransfer(fundTransferIn);
                var resultRequestCPMessage = TransactionMessage(resultFundTransfer.responseCode, resultFundTransfer.responseMsg);
                if (resultRequestCPMessage.Status != TransactionStatus.SuccessfulTransaction.ToString())
                {
                    fundTransfer.Success = false;
                    //var resultLog = await InsertLog(fundTransfer);
                    fundTransfer.RefNo = resultFundTransfer.refNo;
                    fundTransfer.Amount = resultFundTransfer.responseMsg;
                    return resultRequestCPMessage;
                }
                else
                {
                    var settleReverseIn = new settleReverseIn()
                    {
                        authInfo = fundTransferIn.authInfo,
                        custMobile = fundTransferIn.custMobile,
                        date = fundTransferIn.date,
                        localIP = fundTransferIn.localIP,
                        merchantID = fundTransferIn.merchantID,
                        orgRefNo = resultFundTransfer.refNo,
                        terminalID = fundTransferIn.terminalID,
                        serial = fundTransferIn.serial,
                        time = fundTransferIn.time,
                        orgAmount = fundTransferIn.amount,
                        transType = "request",
                        STAN = fundTransferIn.STAN,
                        version = fundTransferIn.version
                    };
                    var resultSettlement = soapMobileClient.settlement(settleReverseIn);
                    var resultSettlementMessage = TransactionMessage(resultSettlement.responseCode, resultSettlement.responseMsg);

                    resultSettlementMessage.ClientRefNo = settleReverseIn.STAN;
                    resultSettlementMessage.Date = settleReverseIn.date;
                    resultSettlementMessage.RefNo = settleReverseIn.orgRefNo;
                    resultSettlementMessage.Time = settleReverseIn.time;

                    fundTransfer.Success = true;
                    //var resultLog = await InsertLog(fundTransfer);

                    return resultSettlementMessage;
                }
            }
            catch (Exception)
            {
                return AppGlobal.ServiceResultClientInstance(true, "عملیات نا موفق");
            }
        }

        /// <summary>
        /// شارژ مستقیم
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResultClient> TopupSingleRequestAsync(TopupSingleRequest model)
        {
            try
            {
                var topupSingle = new TopupSingle(model.CustomerId);

                var topupCpIn = new topupCPIn()
                {
                    authInfo = new authInfo()
                    {
                        userName = Request.AuthInfo.Username,
                        password = Request.AuthInfo.Password
                    }
                };

                topupSingle.MerchantID = topupCpIn.merchantID = Request.MerchantID;
                topupCpIn.terminalID = Request.TerminalID;
                topupSingle.Date = topupCpIn.date = Request.Date;
                topupSingle.Time = topupCpIn.time = Request.Time;
                topupCpIn.localIP = Request.LocalIP;
                topupSingle.PAN = topupCpIn.PAN = model.PAN;
                topupCpIn.PINBlock = model.PINBlock;
                topupCpIn.serial = Request.Serial;
                topupSingle.STAN = topupCpIn.STAN = Request.STAN;
                topupCpIn.track2Ciphered = model.Track2Ciphered;
                topupCpIn.custMobile = Request.Mobile;
                topupSingle.InvoiceNo = topupCpIn.invoiceNo = Request.InvoiceNo;
                topupSingle.Amount = topupCpIn.amount = model.Amount;
                topupCpIn.profileId= model.profileId;
                topupCpIn.mobile= topupSingle.Mobile = model.Mobile;
                //topupCpIn.OrderType = 1;

                SoapMobileClient soapMobileClient = new SoapMobileClient();

                topupOut topupMTNCpSingle = null;
                if (new[]{ "19", "20" }.Contains(model.profileId))
                {
                    topupMTNCpSingle = soapMobileClient.topupMTNCPSingle(topupCpIn);
                }
                else if (new[] { "3", "4", "5", "6", "8", "41", "42" }.Contains(model.profileId))
                {
                    topupMTNCpSingle = soapMobileClient.topupSetavalCPSingle(topupCpIn);
                }
                var resultRequestCPMessage = TransactionMessage(topupMTNCpSingle.responseCode, topupMTNCpSingle.responseMsg);
                if (resultRequestCPMessage.Status != TransactionStatus.SuccessfulTransaction.ToString())
                {
                    topupSingle.Success = false;
                    //var resultLog = await InsertLog(topupSingle);
                    topupSingle.RefNo = topupMTNCpSingle.refNo;
                    topupSingle.Amount = topupMTNCpSingle.responseMsg;
                    return resultRequestCPMessage;
                }
                else
                {
                    var settleReverseIn = new settleReverseIn()
                    {
                        authInfo = topupCpIn.authInfo,
                        custMobile = topupCpIn.custMobile,
                        date = topupCpIn.date,
                        localIP = topupCpIn.localIP,
                        merchantID = topupCpIn.merchantID,
                        orgRefNo = topupMTNCpSingle.refNo,
                        terminalID = topupCpIn.terminalID,
                        serial = topupCpIn.serial,
                        time = topupCpIn.time,
                        orgAmount = topupCpIn.amount,
                        STAN = topupCpIn.STAN,
                        version = topupCpIn.version
                    };

                    if (new[] { "19", "20" }.Contains(model.profileId))
                    {
                        settleReverseIn.transType = "topupmtn";
                    }
                    else if (new[] { "3", "4", "5", "6", "8", "41", "42" }.Contains(model.profileId))
                    {
                        settleReverseIn.transType = "topupsetaval";
                    }

                    var resultSettlement = soapMobileClient.settlement(settleReverseIn);
                    var resultSettlementMessage = TransactionMessage(resultSettlement.responseCode, resultSettlement.responseMsg);

                    resultSettlementMessage.ClientRefNo = settleReverseIn.STAN;
                    resultSettlementMessage.Date = settleReverseIn.date;
                    resultSettlementMessage.RefNo = settleReverseIn.orgRefNo;
                    resultSettlementMessage.Time = settleReverseIn.time;

                    topupSingle.Success = true;
                    //var resultLog = await InsertLog(topupSingle);

                    return resultSettlementMessage;
                }
            }
            catch (Exception)
            {
                return AppGlobal.ServiceResultClientInstance(true, "عملیات نا موفق");
            }
        }

        /// <summary>
        /// خرید کارت شارژ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResultClient> VoucherRequestAsync(VoucherRequest model)
        {
            return null;

            //try
            //{
            //    var voucher = new Voucher();

            // var voucherCpIn = new voucherCpIn()
            //    {
            //        authInfo = new authInfo()
            //        {
            //            userName = Request.AuthInfo.Username,
            //            password = Request.AuthInfo.Password
            //        }
            //    };

            //    voucher.MerchantID = voucherCpIn.MerchantID = Request.MerchantID;
            //    voucher.TerminalID = voucherCpIn.TerminalID = Request.TerminalID;
            //    voucher.Date = voucherCpIn.Date = Request.Date;
            //    voucher.Time = voucherCpIn.Time = Request.Time;
            //    voucher.LocalIP = voucherCpIn.localIP = Request.LocalIP;
            //    voucher.PAN = voucherCpIn.PAN = model.PAN;
            //    voucherCpIn.PINBlock = model.PINBlock;
            //    voucher.Serial = voucherCpIn.Serial = Request.Serial;
            //    voucher.STAN = voucherCpIn.STAN = Request.STAN;
            //    voucherCpIn.track2Ciphered = model.Track2Ciphered;
            //    voucherCpIn.custMobile = Request.Mobile;
            //    voucher.InvoiceNo = voucherCpIn.invoiceNo = Request.InvoiceNo;
            //    voucher.Amount = voucherCpIn.amount = model.Amount;
            //    voucherCpIn.profileId = model.profileId;
            //    voucherCpIn.OrderType = 1;

            //    SoapMobileClient soapMobileClient = new SoapMobileClient();

            //    voucher voucherMTNCpSingle = null;
            //    if (new[] { "19", "20" }.Contains(model.profileId))
            //    {
            //        voucherMTNCpSingle = soapMobileClient.voucherMTNCPSingle(voucherCpIn);
            //    }
            //    else if (new[] { "3", "4", "5", "6", "8", "41", "42" }.Contains(model.profileId))
            //    {
            //        voucherMTNCpSingle = soapMobileClient.voucherSetavalCPSingle(voucherCpIn);
            //    }
            //    var resultRequestCPMessage = TransactionMessage(voucherMTNCpSingle.responseCode, voucherMTNCpSingle.responseMsg);
            //    if (resultRequestCPMessage.Status != TransactionStatus.SuccessfulTransaction.ToString())
            //    {
            //        voucher.Success = false;
            //        var resultLog = await InsertLog(voucher);
            //        voucher.RefNo = voucherMTNCpSingle.RefNo;
            //        voucher.Amount = voucherMTNCpSingle.responseMsg;
            //        return resultRequestCPMessage;
            //    }
            //    else
            //    {
            //        var settleReverseIn = new settleReverseIn()
            //        {
            //            authInfo = voucherCpIn.authInfo,
            //            custMobile = voucherCpIn.custMobile,
            //            Date = voucherCpIn.Date,
            //            localIP = voucherCpIn.localIP,
            //            MerchantID = voucherCpIn.MerchantID,
            //            orgRefNo = voucherMTNCpSingle.RefNo,
            //            TerminalID = voucherCpIn.TerminalID,
            //            Serial = voucherCpIn.Serial,
            //            Time = voucherCpIn.Time,
            //            orgAmount = voucherCpIn.amount,
            //            STAN = voucherCpIn.STAN,
            //            version = voucherCpIn.version
            //        };

            //        if (new[] { "19", "20" }.Contains(model.profileId))
            //        {
            //            settleReverseIn.transType = "vouchermtn";
            //        }
            //        else if (new[] { "3", "4", "5", "6", "8", "41", "42" }.Contains(model.profileId))
            //        {
            //            settleReverseIn.transType = "vouchersetaval";
            //        }

            //        var resultSettlement = soapMobileClient.settlement(settleReverseIn);
            //        var resultSettlementMessage = TransactionMessage(resultSettlement.responseCode, resultSettlement.responseMsg);

            //        resultSettlementMessage.ClientRefNo = settleReverseIn.STAN;
            //        resultSettlementMessage.Date = settleReverseIn.Date;
            //        resultSettlementMessage.RefNo = settleReverseIn.orgRefNo;
            //        resultSettlementMessage.Time = settleReverseIn.Time;

            //        voucher.Success = true;
            //        var resultLog = await InsertLog(voucher);

            //        return resultSettlementMessage;
            //    }
            //}
            //catch (Exception)
            //{
            //    return AppGlobal.ServiceResultClientInstance(true, "عملیات نا موفق");
            //}
        }

        /// <summary>
        /// پرداخت قبض
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResultClient> BillRequestAsync(BillRequest model)
        {
            try
            {
                var bill = new Bill(model.CustomerId);

                var billCpIn = new billCPIn();

                billCpIn.authInfo = new authInfo()
                {
                    userName = Request.AuthInfo.Username,
                    password = Request.AuthInfo.Password
                };
                bill.MerchantID = billCpIn.merchantID = Request.MerchantID;
                billCpIn.terminalID = Request.TerminalID;
                bill.Amount = billCpIn.amount = model.Amount.ToString();
                bill.Date = billCpIn.date = Request.Date;
                bill.Time = billCpIn.time = Request.Time;
                billCpIn.localIP = Request.LocalIP;
                bill.PAN = billCpIn.PAN = model.PAN;
                billCpIn.PINBlock = model.PINBlock;
                billCpIn.serial = Request.Serial;
                bill.STAN = billCpIn.STAN = Request.STAN;
                billCpIn.track2Ciphered = model.Track2Ciphered;
                billCpIn.custMobile = Request.Mobile;
                bill.InvoiceNo = billCpIn.invoiceNo = Request.InvoiceNo;
                bill.PaymentId = billCpIn.paymentID = model.PaymentId;
                bill.BillId = billCpIn.billID = model.BillId;

                SoapMobileClient s = new SoapMobileClient();

                var resultBillRequestCp = s.billRequestCP(billCpIn);
                var resultRequestCPMessage = TransactionMessage(resultBillRequestCp.responseCode, resultBillRequestCp.responseMsg);
                if (resultRequestCPMessage.Status != TransactionStatus.SuccessfulTransaction.ToString())
                {
                    bill.Success = false;
                    //var resultLog = await InsertLog(bill);
                    bill.RefNo = resultBillRequestCp.refNo;
                    return resultRequestCPMessage;
                }
                else
                {
                    var settleReverseIn = new settleReverseIn()
                    {
                        authInfo = billCpIn.authInfo,
                        custMobile = billCpIn.custMobile,
                        date = billCpIn.date,
                        localIP = billCpIn.localIP,
                        merchantID = billCpIn.merchantID,
                        orgRefNo = resultBillRequestCp.refNo,
                        terminalID = billCpIn.terminalID,
                        serial = billCpIn.serial,
                        time = billCpIn.time,
                        orgAmount = billCpIn.amount,
                        transType = "bill",
                        STAN = billCpIn.STAN,
                        version = billCpIn.version
                    };
                    var resultSettlement = s.settlement(settleReverseIn);
                    var resultSettlementMessage = TransactionMessage(resultSettlement.responseCode, resultSettlement.responseMsg);

                    resultSettlementMessage.ClientRefNo = settleReverseIn.STAN;
                    resultSettlementMessage.Date = settleReverseIn.date;
                    resultSettlementMessage.RefNo = settleReverseIn.orgRefNo;
                    resultSettlementMessage.Time = settleReverseIn.time;

                    bill.Success =
                        resultSettlementMessage.Status == TransactionStatus.SuccessfulTransaction.ToString()
                            ? true
                            : false;
                    //var resultLog = await InsertLog(bill);

                    return resultSettlementMessage;
                }
            }
            catch (Exception ex)
            {
                return AppGlobal.ServiceResultClientInstance(true, "عملیات نا موفق");
            }
        }
    }
}

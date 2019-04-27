using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Core.Model;
using Core.Base;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Core.Business
{
    public class StockBiz : _Service
    {

        public async Task<Json> InsertLog(StockCP model)
        {
            Json result;
            try
            {
                var multiResult = await base.Data.QueryMultipleAsync
                       (
                           "[dbo].[SP_PaymentTran_InsertLog]",

                           new
                           {
                               model.PAN,
                               model.refNo,
                               model.localIP,
                               model.merchantID,
                               model.ResponseCode,
                               model.STAN,
                               model.terminalID,
                               model.invoiceNo,
                               model.Success
                           },

                           commandType: CommandType.StoredProcedure
                       );
                var message = multiResult.Read<Message>().FirstOrDefault();
                var msgs = new List<string>() { $" {message.Text} , {message.Title} " };

                return new Json(null, message.Success, !message.Success ? msgs : null, null, message.Success ? msgs : null);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در ذخیره اطلاعات");
            }
        }

        private ServiceResultClient TransactionMessage(string responseCode)
        {
            ServiceResultClient serviceResult;

            switch (responseCode)
            {
                case "00":
                case "08":
                case "16":
                    serviceResult = AppGlobal.ServiceResultClientInstance(false, "عملیات موفق");
                    break;

                case "01":
                case "19":
                case "34":
                case "90":
                case "1":
                    serviceResult = AppGlobal.ServiceResultClientInstance(true, "عملیات ناموفق");
                    break;


                case "10":
                case "02":
                case "05":
                case "12":
                case "14":
                case "23":
                case "30":
                case "31":
                case "38":

                    serviceResult = AppGlobal.ServiceResultClientInstance(true, "تراکنش نامعتبر است");
                    break;

                case "03":
                case "57":
                case "58":
                case "61":
                case "62":
                case "65":
                case "75":
                    serviceResult = AppGlobal.ServiceResultClientInstance(true, "تراکنش نامعتبر است");
                    break;


                case "51":
                    serviceResult = AppGlobal.ServiceResultClientInstance(true, "عدم موجودي کافي");
                    break;

                case "63":
                    serviceResult = AppGlobal.ServiceResultClientInstance(true, "تراکنش ناموفق");
                    break;

                case "55":
                    serviceResult = AppGlobal.ServiceResultClientInstance(true, "رمز نامعتبر است");
                    break;



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

                    serviceResult = AppGlobal.ServiceResultClientInstance(true, "کارت نامعتبر است");
                    break;



                default:
                    serviceResult = AppGlobal.ServiceResultClientInstance(true, "خطای نامشخص");
                    break;
            }

            return serviceResult;
        }
        public async Task<ServiceResultClient> StockRequest(RequestPay model)
        {
            try
            {
                ServiceResultClient serviceResult;
                StockCP StockCP = new StockCP();

                //FanAvaServices_Mobile.requestCPIn reqCP = new FanAvaServices_Mobile.requestCPIn();
                  FanAvaServices_Mobile.requestCPIn reqCP = new FanAvaServices_Mobile.requestCPIn();
                reqCP.authInfo = new FanAvaServices_Mobile.authInfo() { userName = Constants.AuthInfo.username, password = Constants.AuthInfo.password };
                StockCP.merchantID = reqCP.merchantID = Constants.merchantID;
                StockCP.terminalID = reqCP.terminalID = Constants.terminalID;
                StockCP.amount = reqCP.amount = model.amount.ToString();
                StockCP.date = reqCP.date = Constants.date;
                StockCP.time = reqCP.time = Constants.time;
                StockCP.localIP = reqCP.localIP = Constants.LocalIP;
                StockCP.PAN = reqCP.PAN = model.PAN;
                reqCP.PINBlock = model.PINBlock;
                StockCP.serial = reqCP.serial = Constants.serial;
                StockCP.STAN = reqCP.STAN = Constants.STAN;
                reqCP.track2Ciphered = model.track2Ciphered;
                reqCP.custMobile = Constants.mobile;
                StockCP.invoiceNo = reqCP.invoiceNo = Constants.invoiceNo;


                FanAvaServices_Mobile.SoapMobileClient s = new FanAvaServices_Mobile.SoapMobileClient();


                var resultRequestCP = s.balanceCP(reqCP);
                var resultRequestCPMessage = TransactionMessage(resultRequestCP.responseCode);
                if (resultRequestCPMessage.status != StatusTransaction.SuccessfulTransaction.ToString())
                {
                    StockCP.Success = false;
                    var resultLog = await InsertLog(StockCP);
                    StockCP.refNo = resultRequestCP.refNo;
    

                    return resultRequestCPMessage;
                }

                return new ServiceResultClient();
            }
            catch (Exception e)
            {
                return AppGlobal.ServiceResultClientInstance(true, "عملیات نا موفق");
            }
        }

    }
}
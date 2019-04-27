using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Core.Model;
using Core.Base;
using System.Collections.Generic;

namespace Core.Business
{
    public class TestItemBiz : _Service
    {
        public async Task<Json> Insert(TestItemView model)
        {

            Json result;
            try
            {

                DateTime HoldingDate = model.PersianHoldingDate.PersianDateToMiladi().Date;
                DateTime RegistrationDeadline = model.PersianRegistrationDeadline.PersianDateToMiladi().Date;
                DateTime DeadlineCancelling = model.PersianDeadlineCancelling.PersianDateToMiladi().Date;

                //var province = await base.Data.QueryFirstOrDefaultAsync<TestItem>
                var multiResult = await base.Data.QueryMultipleAsync
                      (
                           "[Test].[SP_TestItem_Insert]",

                               new
                               {
                                   model.TestGroupId,
                                   model.BranchId,
                                   model.UserId,
                                   model.Code,
                                   //model.Title,
                                   HoldingDate,
                                   model.HoldingTime,
                                   model.Capacity,
                                   model.EventPlace,
                                   model.Cost,
                                   model.PercentEmployer,
                                   RegistrationDeadline,
                                   DeadlineCancelling,
                                   model.Description
                               },

                               commandType: CommandType.StoredProcedure
                           );
                var message = multiResult.Read<Message>().FirstOrDefault();
                return new Json(null, message.Success, null, null, new List<string>() { $" {message.Text} , {message.Title} " });
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در ذخیره اطلاعات");
            }
        }

        public async Task<Json> Update(TestItemView model)
        {

            Json result;
            try
            {

                DateTime HoldingDate = model.PersianHoldingDate.PersianDateToMiladi().Date;
                DateTime RegistrationDeadline = model.PersianRegistrationDeadline.PersianDateToMiladi().Date;
                DateTime DeadlineCancelling = model.PersianDeadlineCancelling.PersianDateToMiladi().Date;

                //var province = await base.Data.QueryFirstOrDefaultAsync<TestItem>
                var multiResult = await base.Data.QueryMultipleAsync
                      (
                           "[Test].[SP_TestItem_Update]",

                               new
                               {
                                   model.Id,
                                   model.TestGroupId,
                                   model.BranchId,
                                   model.UserId,
                                   model.Code,
                                   HoldingDate,
                                   model.HoldingTime,
                                   model.Capacity,
                                   model.EventPlace,
                                   model.Cost,
                                   model.PercentEmployer,
                                   RegistrationDeadline,
                                   DeadlineCancelling,
                                   model.Description
                               },

                               commandType: CommandType.StoredProcedure
                           );
                var message = multiResult.Read<Message>().FirstOrDefault();
                return new Json(null, message.Success, null, null, new List<string>() { $" {message.Text} , {message.Title} " });
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در ذخیره اطلاعات");
            }
        }


        public async Task<Json> UpdateStatusAccept(long Id, long UserId)
        {
            try
            {
                int TestItemStatusId = (int)TestItemStatus.Accept;
                var multiResult = await base.Data.QueryMultipleAsync
                       (
                           "[Test].[SP_TestItem_UpdateForAccept]",

                           new
                           {
                               Id = Id,
                               UserId,
                               TestItemStatusId
                           },

                           commandType: CommandType.StoredProcedure
                       );

                var message = multiResult.Read<Message>().FirstOrDefault();
                return new Json(null, message.Success, null, null, new List<string>() { $" {message.Text} , {message.Title} " });
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در ذخیره اطلاعات");
            }
        }
        public async Task<Json> Delete(long Id)
        {
            try
            {
                var multiResult = await base.Data.QueryMultipleAsync
                       (
                           "[Test].[SP_TestItem_Delete]",

                           new { Id = Id },

                           commandType: CommandType.StoredProcedure
                       );

                var message = multiResult.Read<Message>().FirstOrDefault();
                return new Json(null, message.Success, null, null, new List<string>() { $" {message.Text} , {message.Title} " });
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در ذخیره اطلاعات");
            }
        }


        public async Task<Json> GetAllView(long UserId, TestItemStatus? Status = null)
        {
            try
            {
                int? TestItemStatus = null;
                if (Status != null)
                    TestItemStatus = (int)Status;


                IEnumerable<TestItemView> TestItemViewList = await base.Data.QueryAsync<TestItemView>
                    (
                        "[Test].[SP_TestItem_GetAllView]",

                        new
                        {
                            UserId,
                            TestItemStatus
                        },

                        commandType: CommandType.StoredProcedure
                    );

                TestItemViewList.ToList().ForEach(tg =>
                {
                    tg.PercentEmployerStr = $"{(tg.PercentEmployer * 100).ToString()}%";
                    tg.PersianRegistrationDeadline = tg.RegistrationDeadline.ToPersianShortDate();
                    tg.PersianDeadlineCancelling = tg.DeadlineCancelling.ToPersianShortDate();
                    tg.PersianHoldingDate = tg.HoldingDate.ToPersianShortDate();
                    tg.CostSeparate = tg.Cost.ToString("#,###,###");
                });
                return new Json(TestItemViewList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }

        public async Task<Json> GetHoldingDateByTestGroupId(int TestGroupId, int? ProvinceId, int? CityId)
        {
            try
            {


                IEnumerable<TestItemView> TestItemViewList = await base.Data.QueryAsync<TestItemView>
                    (
                        "[Test].[SP_HoldingDate_GetByTestGroupId]",

                        new
                        {
                            TestGroupId,
                            ProvinceId,
                            CityId
                        },

                        commandType: CommandType.StoredProcedure
                    );

                TestItemViewList.ToList().ForEach(f =>
                {
                    f.PersianHoldingDate = f.HoldingDate.ToPersianShortDate();
                });


                return new Json(TestItemViewList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }
        public async Task<Json> GetForRegister(int TestGroupId, int? ProvinceId, int? CityId, string PersianHoldingDate)
        {
            try
            {
                DateTime? HoldingDate = null;
                if (PersianHoldingDate != null)
                {
                    HoldingDate = PersianHoldingDate.PersianDateToMiladi();
                }

                IEnumerable<TestItemView> TestItemViewList = await base.Data.QueryAsync<TestItemView>
                    (
                        "[Test].[SP_TestItem_GetForRegister]",

                        new
                        {
                            TestGroupId,
                            HoldingDate = HoldingDate != null ? HoldingDate.Value : HoldingDate,
                            ProvinceId = ProvinceId != null ? ProvinceId.Value : ProvinceId,
                            CityId = CityId != null ? CityId.Value : CityId
                        },

                        commandType: CommandType.StoredProcedure
                    );

                TestItemViewList.ToList().ForEach(f =>
                {
                    f.PersianHoldingDate = f.HoldingDate.ToPersianShortDate();
                    f.PersianDeadlineCancelling = f.DeadlineCancelling.ToPersianShortDate();
                    f.PersianRegistrationDeadline = f.RegistrationDeadline.ToPersianShortDate();
                    f.CostSeparate = f.Cost.ToString("#,###,###");
                });


                return new Json(TestItemViewList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }





    }
}
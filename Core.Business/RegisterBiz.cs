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
    public class RegisterBiz : _Service
    {
        public async Task<tbUser> Insert(tbUser model)
        {
            try
            {
                model.PasswordEncript =AppHelper.GetMD5HashData(model.Password);
                IEnumerable<tbUser> user = await base.Data.QueryAsync<tbUser>
                       (
                           "[Test].[SP_User_Insert]",

                            new
                            {
                                Username = model.Username,
                                PasswordEncript = model.PasswordEncript,
                                Firstname = model.Firstname,
                                Lastname = model.Lastname,
                                IsAdmin = model.IsAdmin,
                                Mobile = model.Mobile,
                                IsActive = model.IsActive,
                                Email = model.Email,
                                CreateDate = DateTime.Now,
                                BranchId = model.BranchId
                            },

                           commandType: CommandType.StoredProcedure
                       );

                return user.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        //public async Task<Json> Delete(long Id)
        //{
        //    try
        //    {
        //        await base.Data.ExecuteAsync
        //               (
        //                   "[Test].[SP_Meeting_Delete]",

        //                   new { Id = Id },

        //                   commandType: CommandType.StoredProcedure
        //               );
        //        return new Json(true, "با موفقیت حذف شد");
        //    }
        //    catch
        //    {
        //        return new Json(false, "خطا در حذف اطلاعات");
        //    }
        //}

        //public async Task<Json> Cancel(MeetingCancel model)
        //{
        //    try
        //    {
        //        await base.Data.ExecuteAsync
        //               (
        //                   "[Test].[SP_Meeting_Cancel]",

        //                   new { Id = model.Id, CancelText = model.CancelText },

        //                   commandType: CommandType.StoredProcedure
        //               );
        //        return new Json(true, "با موفقیت کنسل شد");
        //    }
        //    catch
        //    {
        //        return new Json(false, "خطا در ذخیره اطلاعات");
        //    }
        //}

        //public async Task<Json> Select(long UserId)
        //{
        //    try
        //    {
        //        List<MeetingView> MeetingViewList = new List<MeetingView>();

        //        IEnumerable<MeetingView> meetingList = await base.Data.QueryAsync<MeetingView>
        //            (
        //                "[Test].[SP_Meeting_Select]",

        //                new { UserId = UserId },

        //                commandType: CommandType.StoredProcedure
        //            );

        //        MeetingMemberBiz meetingMemberBiz = new MeetingMemberBiz();

        //        foreach (MeetingView metting in meetingList)
        //        {
        //            metting.Members = new List<MeetingMember>();

        //            var t = await meetingMemberBiz.SelectAll(metting.Id);
        //            metting.Members = t.AsList();

        //            MeetingViewList.Add(metting);
        //        }
        //        return new Json(MeetingViewList);
        //    }
        //    catch(Exception e)
        //    {
        //        return new Json(false, "خطا در واکشی اطلاعات");
        //    }
        //}
    }
}
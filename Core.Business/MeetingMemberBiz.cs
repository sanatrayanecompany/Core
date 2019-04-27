using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Core.Model;
using Core.Base;

namespace Core.Business
{
    public class MeetingMemberBiz : _Service
    {
        public async Task<int> Insert(MeetingMemberInsert model)
        {
            try
            {
                return await base.Data.ExecuteAsync
                        (
                            "[Test].[SP_MeetingMember_Insert]",

                            new { MeetingId = model.MeetingId, UserId = model.UserId },

                            commandType: CommandType.StoredProcedure
                        );
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> Delete(long Id)
        {
            try
            {
                return await base.Data.ExecuteAsync
                        (
                            "[Test].[SP_MeetingMember_Delete]",

                            new { Id = Id },

                            commandType: CommandType.StoredProcedure
                        );
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> Confirm(MeetingMemberConfirm model)
        {
            try
            {
                return await base.Data.ExecuteAsync
                        (
                            "[Test].[SP_MeetingMember_Confirm]",

                            new { Id = model.Id, IsConfirm = model.IsConfirm },

                            commandType: CommandType.StoredProcedure
                        );
            }
            catch
            {
                return -1;
            }
        }

        public async Task<IEnumerable<MeetingMember>> SelectAll(long MeetingId)
        {
            try
            {
                return await base.Data.QueryAsync<MeetingMember>
                      (
                          "[Test].[SP_MeetingMember_SelectAll]",

                          new { MeetingId = MeetingId },

                          commandType: CommandType.StoredProcedure
                      );
            }
            catch
            {
                return new List<MeetingMember>();
            }
        }
    }
}
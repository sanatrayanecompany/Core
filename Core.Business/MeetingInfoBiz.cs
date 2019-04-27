using System.Data;
using System.Threading.Tasks;
using Dapper;
using Core.Model;
using Core.Base;

namespace Core.Business
{
    public class MeetingInfoBiz : _Service
    {
        public async Task<int> Insert(MeetingInfoInsert model)
        {
            try
            {
                return await base.Data.ExecuteAsync
                    (
                        "[Test].[SP_MeetingInfo_InsertOrUpdate]",

                        new
                        {
                            MeetingId = model.MeetingId,
                            Title = model.Title,
                            Address = model.Address,
                            Description = model.Description,
                            DateTime = model.DateTime,
                            ShowMember = model.ShowMember,
                            Latitude = model.Latitude,
                            Longitude = model.Longitude,
                        },

                        commandType: CommandType.StoredProcedure
                    );
            }
            catch
            {
                return -1;
            }
        }
    }
}
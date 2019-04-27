using Core.Business;
using Core.Model;
using Core.Base;
using System.Web.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Core.PSP.Controllers
{
    [RoutePrefix("api/userRole")]
    public class UserRoleController : _Controller
    {
        private UserRoleBiz _UserRoleBiz;

        public UserRoleController(UserRoleBiz userRoleBiz)
        {
            _UserRoleBiz = userRoleBiz;
        }

        [HttpPost]
        [Route("add")]
        //[ValidationFilter()]
        [AuthorizeUser((int)Roles.Admin)]
        public async Task<Json> Add(List<UserRoleModel> modelList)
        {
            //PayLoad pl = base.GetCurrentUser<PayLoad>();
            
            var result = await _UserRoleBiz.Insert(modelList);

            return result;//new Json(true, "اضافه شد");
        }



        [HttpGet]
        [Route("getAllView")]
        //[OutputCache(1)]
        public async Task<Json> GetAllView()
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();

            return await _UserRoleBiz.GetAllView();
        }

        [HttpDelete]
        [Route("remove")]
        //[ValidationFilter()]
        //[AuthorizeUser((int)Roles.Admin)]
        public async Task<Json> Delete(int Id)//DeleteModel model)
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();
            var result = await _UserRoleBiz.Delete(Id);

            return result;
        }


    }
}

using Core.Business;
using Core.Model;
using Core.Base;
using System.Web.Http;
using System.Threading.Tasks;
using System;

namespace Core.PSP.Controllers
{
    [RoutePrefix("api/role")]
    public class RoleController : _Controller
    {
        private RoleBiz _RoleBiz;

        public RoleController(RoleBiz roleBiz)
        {
            _RoleBiz = roleBiz;
        }

        [HttpPost]
        [Route("add")]
        [ValidationFilter()]
        [AuthorizeUser((int)Roles.Admin)]
        //public async Task<Json> Insert(tbUser model)
        //{
        //    PayLoad pl = base.GetCurrentUser<PayLoad>();
        //    var result = await _RoleBiz.Insert(model);
            
        //    return new Json(result);
        //}

        [HttpGet]
        [Route("getAll")]
        //[OutputCache(1)]
        public async Task<Json> GetAll()
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();

            return await _RoleBiz.GetAll();
        }


        

    }
}

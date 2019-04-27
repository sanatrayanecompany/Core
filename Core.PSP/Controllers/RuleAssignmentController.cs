using Core.Business;
using Core.Model;
using Core.Base;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using System;
using System.Collections.Generic;

namespace Core.PSP.Controllers
{

    [RoutePrefix("api/ruleAssignment")]



    public class RuleAssignmentController : _Controller
    {
        public RuleAssignmentBiz _RuleAssignBiz { get; set; }
        public RuleAssignmentController(RuleAssignmentBiz ruleAssignBiz)
        {
            _RuleAssignBiz = ruleAssignBiz;
        }

        [HttpGet]
        [Route("getAllView")]
        //[OutputCache(1)]
        public async Task<Json> GetAllRuleAssignmentView()
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();

            return await _RuleAssignBiz.GetAllRuleAssignmentView();
        }

        [HttpPost]
        [Route("add")]
        public async Task<Json> Add(List<RuleAssignment> modelList)
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();
            //set UserId
            foreach (var item in modelList)
            {
                item.UserId = pl.UserId;
            }
            var result = await _RuleAssignBiz.Insert(modelList);

            return result;//new Json(true, "اضافه شد");
        }

        [HttpDelete]
        [Route("remove")]
        //[ValidationFilter()]
        //[AuthorizeUser((int)Roles.Admin)]
        public async Task<Json> Delete(int Id)//DeleteModel model)
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();
            var result = await _RuleAssignBiz.Delete(Id);

            return result;
        }

        [HttpGet]
        [Route("test")]
        //[AuthorizeUser((int)Roles.Admin, (int)Roles.User)]
        public string test()
        {
            return "Success";
        }
    }
}

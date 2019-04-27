using System.Web.Http;
using System.Threading.Tasks;
using Core.Base;
using Core.Business;
using Core.Model;

namespace Core.PSP.Controllers
{
    [AuthorizeUser()]
    [RoutePrefix("api/contact")]
    public class ContactController : _Controller
    {
        private ContactBiz _ContactBiz;

        public ContactController(ContactBiz contactBiz)
        {
            _ContactBiz = contactBiz;
        }

        [HttpPost]
        [Route("insert")]
        [ValidationFilter()]
        public async Task<Json> Insert(ContactInsert model)
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();
            model.UserId = pl.UserId;

            return await _ContactBiz.Insert(model);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<Json> GetAll()
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();

            return await _ContactBiz.SelectAllContact(pl.UserId);
        }

        [HttpGet]
        [Route("getallnew")]
        public async Task<Json> GetAllNew()
        {
            PayLoad pl = base.GetCurrentUser<PayLoad>();

            return await _ContactBiz.SelectAllNew(pl.UserId);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<Json> Delete(long Id)
        {
            return await _ContactBiz.Delete(Id);
        }

        [HttpPut]
        [Route("confirm")]
        [ValidationFilter()]
        public async Task<Json> Confirm(ContactConfirm model)
        {
            return await _ContactBiz.Confirm(model);
        }

        [HttpGet]
        [Route("search")]
        public async Task<Json> Search(string Username)
        {
            if (string.IsNullOrEmpty(Username))
            {
                return new Json(false, "نام کاربری را وارد کنید");
            }

            if (Username.Length < 3)
            {
                return new Json(false, "حداقل 3 کاراکتر وارد کنید");
            }

            PayLoad pl = base.GetCurrentUser<PayLoad>();

            return await _ContactBiz.Search(new ContactSearch()
            {
                UserId = pl.UserId,
                Username = Username
            });
        }

        protected override void Dispose(bool disposing)
        {
            this._ContactBiz.Dispose();
        }
    }
}
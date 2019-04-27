using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Core.Base
{
    public class AuthorizeUser : AuthorizationFilterAttribute
    {
        private IToken _token;

        private readonly int[] _roles;

        public AuthorizeUser(params int[] role)
        {
            _token = _token = new Token();// Resolver.GetType<IToken>();

            _roles = role;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Host.Config.Token.Enable && !hasPermission())
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new ObjectContent<Json>(new Json(false, "امکان اجرای درخواست برای شما وجود ندارد"), new JsonMediaTypeFormatter())
                };
                throw new HttpResponseException(message);
            }
            base.OnAuthorization(actionContext);
        }

        private bool hasPermission()
        {
            UserRole payload = _token.Decode<UserRole>();

            if (payload == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            bool permission = false;

            if (_roles.Any())
            {
                foreach (int i in _roles)
                {
                    if (permission)
                    {
                        break;
                    }
                    else
                    {
                        foreach (int r in payload.Roles)
                        {
                            if (i == r)
                            {
                                permission = true;

                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                permission = true;
            }
            return permission;
        }

        private class UserRole : IPayLoad
        {
            public int[] Roles { get; set; }
        }
    }
}
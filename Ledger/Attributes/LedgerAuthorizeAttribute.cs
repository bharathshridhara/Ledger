using System;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using System.Web.Http.Controllers;

namespace Ledger.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LedgerAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var authHeader = actionContext.Request.Headers.Authorization;
                if (authHeader != null)
                {
                    if (authHeader.Scheme == "Basic")
                    {
                        var header = authHeader.Parameter;

                        var encoding = Encoding.GetEncoding("iso-8859-1");
                        var credentials = encoding.GetString(Convert.FromBase64String(header));

                        int separator = credentials.IndexOf(':');
                        string name = credentials.Substring(0, separator);
                        string password = credentials.Substring(separator + 1);

                        //TODO: Authenticate user
                        var identity = new GenericIdentity(name);
                        Thread.CurrentPrincipal = new GenericPrincipal(identity, null);
                        if (actionContext.RequestContext != null)
                        {
                            actionContext.RequestContext.Principal = Thread.CurrentPrincipal;
                        }

                    }
                }
                base.OnAuthorization(actionContext);
            }
            catch(FormatException ex)
            {
                actionContext.Response.StatusCode = HttpStatusCode.Unauthorized;
            }
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            OnAuthorization(actionContext);
            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }
    }
}

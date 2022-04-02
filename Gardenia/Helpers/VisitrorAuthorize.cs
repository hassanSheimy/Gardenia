using Gardenia.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gardenia.Helpers
{
    public class VisitrorAuthorize:ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            if(token == null)
            {
                context.Result = new UnauthorizedResult();
                return base.OnActionExecutionAsync(context, next);
            }
            token = token.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var decodedValue = handler.ReadJwtToken(token);
                var clamins = decodedValue.Claims;
                int visitorId;
                bool isVisitor = false;
                foreach (var claim in clamins)
                {
                    if (claim.Type == "userid")
                    {
                        visitorId = int.Parse(claim.Value.ToString());
                    }
                    if (claim.Type == "IsVisitor")
                    {
                        isVisitor = bool.Parse(claim.Value);
                    }
                }
                if (!isVisitor)
                {
                    context.Result = new UnauthorizedResult();
                    return base.OnActionExecutionAsync(context, next);
                }
            }
            catch (System.Exception)
            {
                context.Result = new UnauthorizedResult();
                return base.OnActionExecutionAsync(context, next);
            }
            
            return base.OnActionExecutionAsync(context, next);
        }
    }
}

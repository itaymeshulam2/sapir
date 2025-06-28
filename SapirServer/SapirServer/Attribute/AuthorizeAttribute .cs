using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        return;
        var s = context.HttpContext.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(s))
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }
        if (AuthenticationHeaderValue.TryParse(s, out var headerValue))
        {
            var scheme = headerValue.Scheme;
            var parameter = headerValue.Parameter;

            var stream = parameter;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var validationJWT = IsUserValid(tokenS);
            if (!validationJWT.Res)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized, Value = new { Message = validationJWT.Message } };
                return;
            }
            var user = tokenS.Claims.FirstOrDefault(a => a.Type == "sub")?.Value;
            if(string.IsNullOrEmpty(user))
            {
                user = tokenS.Claims.FirstOrDefault(a => a.Type == "Sid")?.Value;
            }
            context.HttpContext.Request.Headers["JWTSub"] = user;
        }
    }

    private (bool Res, string Message) IsUserValid(JwtSecurityToken tokenS)
    {
        var expirationJWT = DateTimeOffset.FromUnixTimeSeconds(int.Parse(tokenS.Payload.FirstOrDefault(a => a.Key == "exp").Value.ToString())).UtcDateTime;
        if (expirationJWT < DateTime.UtcNow)
        {
            return (false, "Login Expired"); ;
        }
        return (true, "");
    }
}
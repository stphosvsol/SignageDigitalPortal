using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Infrastructure.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class JsonValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {


        private void ValidateRequestHeader(HttpRequestBase request)
        {
            var tokenValue = request.Headers["RequestVerificationToken"];
            var cookiesValue = request.Cookies["__RequestVerificationToken"];

            var cookieValue = cookiesValue == null ? String.Empty : cookiesValue.Value;


            if (!String.IsNullOrEmpty(tokenValue) && !String.IsNullOrEmpty(cookieValue))
            {
                AntiForgery.Validate(cookieValue, tokenValue);
            }
            else
            {
                AntiForgery.Validate();
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                ValidateRequestHeader(filterContext.HttpContext.Request);
            }
            catch (HttpAntiForgeryException)
            {
                throw new HttpAntiForgeryException("Error de validación");
            }
        }
    } 
}

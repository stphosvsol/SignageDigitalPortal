using System;
using System.Web.Mvc;
using SignageRepository.Database;

namespace SignageDigitalPortal.Controllers
{
    public class BaseController : Controller
    {
        protected readonly SignageDigitalEntities Db = new SignageDigitalEntities();

        protected string UrlApp
        {
            get
            {
                if (HttpContext.Request.Url != null)
                    return HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority +
                           HttpContext.Request.ApplicationPath;
                return String.Empty;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    Db.Dispose();
                }
                catch (Exception)
                {
                    return;
                }
            }

            base.Dispose(disposing);
        }
    }
}

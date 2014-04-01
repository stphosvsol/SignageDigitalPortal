using System;
using System.Web.Mvc;
using SignageRepository.Database;

namespace SignageDigitalPortal.Controllers
{
    public class BaseController : Controller
    {
        protected readonly SignageDigitalEntities Db = new SignageDigitalEntities();


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

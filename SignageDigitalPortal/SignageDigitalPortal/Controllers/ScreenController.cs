using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SignageDigitalPortal.Services.Async;

namespace SignageDigitalPortal.Controllers
{
    public class ScreenController : BaseController
    {
        //
        // GET: /Management/
        public async Task<ActionResult> Index()
        {
            try
            {
                var serviceJs = new CatSerializeService();
                ViewBag.ScreenSizes = await serviceJs.GetScreenSize(Db);
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
	}
}
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SignageDigitalPortal.Services.Async;

namespace SignageDigitalPortal.Controllers
{
    public class ManagementController : BaseController
    {
        //
        // GET: /Management/
        public async Task<ActionResult> Index()
        {
            try
            {
                var serviceJs = new CatSerializeService();
                ViewBag.ScreenSizes = await serviceJs.GetScreenSizeAsync(Db);
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
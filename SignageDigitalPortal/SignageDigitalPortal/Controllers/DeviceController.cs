using System;
using System.Linq;
using System.Web.Mvc;
using Infrastructure.JqGrid.Model;
using SignageRepository.Database;
using SignageRepository.Log;
using SignageRepository.Models.Constants;
using SignageRepository.Shared;

namespace SignageDigitalPortal.Controllers
{
    [Authorize(Roles = RolesConstants.ROLE_MANAGER)]
    public class DeviceController : BaseController
    {
        //
        // GET: /Device/
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                SharedLogger.LogError(ex);
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult List(JqGridFilterModel opts)
        {
            var result = new GenericRepository<VwDevice>(Db).JqGridFindBy(opts, VwDeviceJson.Key, VwDeviceJson.Columns);
            return Json(result);
        }
        
        public ActionResult Upsert(int? id)
        {

            try
            {
                var model = id.HasValue ? Db.Device.Single(e => e.DeviceId == id) : new Device();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        /*
        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public ActionResult DoUpsert(Screen model)
        {
            try
            {
                var service = new ScreenService();
                model.UserId = User.Identity.GetUserId();

                if (service.FillInfoScreen(model) == false)
                {
                    return Json(new ResponseMessageModel
                    {
                        HasError = false,
                        Title = ResShared.TITLE_REGISTER_FAILED,
                        Message = ResMediaRep.ERROR_NOMEDIA_INFO
                    });
                }

                ModelState.Clear();
                ValidateModel(model);
                if (ModelState.IsValid == false)
                {
                    return Json(new ResponseMessageModel
                    {
                        HasError = true,
                        Title = ResShared.TITLE_REGISTER_FAILED,
                        Message = ResShared.ERROR_INVALID_MODEL
                    });
                }

                var respMsg = service.DoUpsert(Db, model);
                respMsg.UrlToGo = Url.Action("Index", "Device", new {area = ""});
                return Json(respMsg);

            }
            catch (Exception ex)
            {
                SharedLogger.LogError(ex, model.Name, model.CatScreenSizeId, model.UserId);
                return Json(new ResponseMessageModel
                {
                    HasError = true,
                    Title = ResShared.TITLE_REGISTER_FAILED,
                    Message = ResShared.ERROR_UNKOWN
                });
            }
        }

        [HttpPost]
        public ActionResult DoObsolete(int id)
        {
            try
            {
                var model = Db.Screen.Single(e => e.ScreenId == id);
                if (model == null)
                {
                    return Json(new ResponseMessageModel
                    {
                        HasError = false,
                        Title = ResShared.TITLE_OBSOLETE_FAILED,
                        Message = ResShared.ERROR_MODEL_NOTFOUND
                    });
                }

                Db.ScreenSchedule.RemoveRange(model.ScreenSchedule);
                Db.Screen.Remove(model);
                Db.SaveChanges();

                return Json(new ResponseMessageModel
                {
                    HasError = false,
                    Title = ResShared.TITLE_OBSOLETE_SUCCESS,
                    Message = ResShared.INFO_REGISTER_DELETED
                });

            }
            catch (Exception ex)
            {
                SharedLogger.LogError(ex, id);
                return Json(new ResponseMessageModel
                {
                    HasError = true,
                    Title = ResShared.TITLE_OBSOLETE_FAILED,
                    Message = ResShared.ERROR_REGISTER_CANNOT_DELETE
                });
            }
        }*/
    }
}
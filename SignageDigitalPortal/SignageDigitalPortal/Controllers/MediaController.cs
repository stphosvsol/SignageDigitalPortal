using System;
using System.Net;
using System.Web.Mvc;
using Infrastructure.JqGrid.Model;
using SignageDigitalPortal.Resources;
using SignageDigitalPortal.Services.Upload;
using SignageRepository.Database;
using SignageRepository.Log;
using SignageRepository.Models.Constants;
using SignageRepository.Models.Shared;
using SignageRepository.Shared;

namespace SignageDigitalPortal.Controllers
{
        [Authorize(Roles=RolesConstants.ROLE_MANAGER)]
    public class MediaController : BaseController
    {
        //
        // GET: /Media/

        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult List(JqGridFilterModel opts)
        {
            var result = new GenericRepository<VwMedia>(Db).JqGridFindBy(opts, VwMediaJson.Key, VwMediaJson.Columns);
            return Json(result);
        }


        [HttpPost]
        public ActionResult Upsert(int? id)
        {
            Media model;

            try
            {
                if (id.HasValue)
                {
                    model = new GenericRepository<Media>(Db).FindById(id);
                    ViewBag.Preview = UrlApp + UploadMediaService.MediaRelativePath + model.RealName;
                }
                else
                {
                    model = new Media { MediaId = EntityConstants.NULL_VALUE };
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoUpsert(Media model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return Json(new ResponseMessageModel
                    {
                        HasError = true,
                        Title = ResShared.TITLE_REGISTER_FAILED,
                        Message = ResShared.ERROR_INVALID_MODEL
                    });
                }

                var repository = new GenericRepository<Media>(Db);

                if (model.MediaId == EntityConstants.NULL_VALUE)
                {
                    repository.Add(model);
                }
                else
                {
                    repository.Update(model);
                }

                return Json(new ResponseMessageModel
                {
                    HasError = false,
                    Title = ResShared.TITLE_REGISTER_SUCCESS,
                    Message = ResShared.INFO_REGISTER_SAVED
                });

            }
            catch (Exception ex)
            {
                SharedLogger.LogError(ex, model.RealName, model.MediaId, model.UserId);
                return Json(new ResponseMessageModel
                {
                    HasError = true,
                    Title = ResShared.TITLE_REGISTER_FAILED,
                    Message = ResShared.ERROR_UNKOWN
                });
            }
        }
    }
}
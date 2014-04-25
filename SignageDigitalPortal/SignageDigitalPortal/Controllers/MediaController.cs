using System;
using System.Net;
using System.Web.Mvc;
using Infrastructure.JqGrid.Model;
using Microsoft.AspNet.Identity;
using SignageDigitalPortal.Resources;
using SignageDigitalPortal.Services.Catalogs;
using SignageDigitalPortal.Services.Upload;
using SignageRepository.Database;
using SignageRepository.Log;
using SignageRepository.Models.Constants;
using SignageRepository.Models.Shared;
using SignageRepository.Resources;
using SignageRepository.Shared;

namespace SignageDigitalPortal.Controllers
{
    [Authorize(Roles = RolesConstants.ROLE_MANAGER)]
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
                SharedLogger.LogError(ex);
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
            catch (Exception ex)
            {
                SharedLogger.LogError(ex);
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
                var service = new MediaService();
                model.UserId = User.Identity.GetUserId();

                if (service.FillInfoMedia(model) == false)
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
                return Json(respMsg);

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

        [HttpPost]
        public ActionResult DoObsolete(int id)
        {
            try
            {
                var repository = new GenericRepository<Media>(Db);
                var model = repository.FindById(id);
                if (model == null)
                {
                    return Json(new ResponseMessageModel
                    {
                        HasError = false,
                        Title = ResShared.TITLE_OBSOLETE_FAILED,
                        Message = ResShared.ERROR_MODEL_NOTFOUND
                    });
                }

                repository.Delete(model);

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
        }
    }
}


using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Infrastructure.JqGrid.Model;
using Microsoft.AspNet.Identity;
using SignageDigitalPortal.Resources;
using SignageDigitalPortal.Services.Catalogs;
using SignageRepository.Database;
using SignageRepository.Log;
using SignageRepository.Models.Constants;
using SignageRepository.Models.Shared;
using SignageRepository.Repository.Catalogs;
using SignageRepository.Shared;
using WebGrease.Css.Extensions;

namespace SignageDigitalPortal.Controllers
{
    [Authorize(Roles = RolesConstants.ROLE_MANAGER)]
    public class ChannelController : BaseController
    {
        //
        // GET: /Channel/
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
            var result = new GenericRepository<VwChannel>(Db).JqGridFindBy(opts, VwChannelJson.Key, VwChannelJson.Columns);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Upsert(int? id)
        {
            Channel model;

            try
            {
                var lstMedias = CatSharedRepository.GetLstMedias(Db);
                if (id.HasValue)
                {
                    model = Db.Channel.Single(e => e.ChannelId == id.Value);
                    model.MediaId = model.ChannelSchedule.First().Media.MediaId;
                }
                else
                {
                    model  = new Channel
                    {
                        ChannelId = EntityConstants.NULL_VALUE,
                        MediaId = lstMedias.Select(e => e.IdLg).First()
                    };
                }

                ViewBag.LstMedias = new SelectList(lstMedias, "IdLg", "Value", model.MediaId);

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
        public ActionResult DoUpsert(Channel model)
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

                var service = new ChannelService(Db);
                model.UserId = User.Identity.GetUserId();
                var respMsg = service.Upsert(model);
                return Json(respMsg);
            }
            catch (Exception ex)
            {
                SharedLogger.LogError(ex, model.Name, model.ChannelId, model.UserId);
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
                var model = Db.Channel.Single(e => e.ChannelId == id);
                if (model == null)
                {
                    return Json(new ResponseMessageModel
                    {
                        HasError = false,
                        Title = ResShared.TITLE_OBSOLETE_FAILED,
                        Message = ResShared.ERROR_MODEL_NOTFOUND
                    });
                }

                Db.ChannelSchedule.RemoveRange(model.ChannelSchedule);
                Db.Channel.Remove(model);
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
        }
	}
}
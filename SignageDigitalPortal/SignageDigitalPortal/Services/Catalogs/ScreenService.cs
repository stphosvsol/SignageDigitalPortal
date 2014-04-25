using System;
using System.Linq;
using SignageDigitalPortal.Resources;
using SignageRepository.Database;
using SignageRepository.Models.Shared;

namespace SignageDigitalPortal.Services.Catalogs
{
    public class ScreenService
    {
        public bool FillInfoScreen(Screen model)
        {
            model.Timestamp = DateTime.Now;

            foreach (var screenSchedule in model.ScreenSchedule)
            {
                screenSchedule.HasClose = false;
                screenSchedule.IsFullScreen = false;
                screenSchedule.StartTime = null;
                screenSchedule.EndTime = null;
                screenSchedule.Day = null;
                screenSchedule.Year = null;
                screenSchedule.UserId = model.UserId;
            }
            return true;
        }

        public ResponseMessageModel DoUpsert(SignageDigitalEntities db, Screen model)
        {
            if (model.ScreenId > 0)
            {
                var modelOld = db.Screen.Single(e => e.ScreenId == model.ScreenId);
                modelOld.Name = model.Name;
                modelOld.CatScreenSizeId = model.CatScreenSizeId;
                modelOld.UserId = model.UserId;
                modelOld.Timestamp = DateTime.Now;

                for (var i = modelOld.ScreenSchedule.Count-1; i >= 0; i--)
                {
                    var screenOld = modelOld.ScreenSchedule.ElementAt(i);
                    var screenNew = model.ScreenSchedule.FirstOrDefault(e => e.ScreenId == screenOld.ScreenId);

                    if (screenNew == null)
                    {
                        db.ScreenSchedule.Remove(screenOld);
                    }
                    else
                    {
                        screenOld.CopyValues(screenNew);
                        model.ScreenSchedule.Remove(screenNew);
                    }
                }

                foreach (var screenSchedule in model.ScreenSchedule)
                {
                    modelOld.ScreenSchedule.Add(screenSchedule);
                }

            }
            else
            {
                db.Screen.Add(model);
            }

            db.SaveChanges();

            return new ResponseMessageModel
            {
                HasError = false,
                Title = ResShared.TITLE_REGISTER_SUCCESS,
                Message = ResShared.INFO_REGISTER_SAVED,
            };
        }
    }
}
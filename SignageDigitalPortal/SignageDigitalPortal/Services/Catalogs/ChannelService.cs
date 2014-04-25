using System;
using System.Collections.Generic;
using System.Linq;
using SignageDigitalPortal.Resources;
using SignageRepository.Database;
using SignageRepository.Log;
using SignageRepository.Models.Shared;

namespace SignageDigitalPortal.Services.Catalogs
{
    public class ChannelService
    {
        private SignageDigitalEntities _db;
        public ChannelService(SignageDigitalEntities db)
        {
            _db = db;
        }

        public bool FillInfoChannel(Channel model)
        {
            try
            {
                model.ChannelSchedule = new List<ChannelSchedule>
                {
                    new ChannelSchedule
                    {
                        MediaId = model.MediaId,
                        StartTime = null,
                        EndTime = null,
                        Day = null,
                        Year = null,
                        Timestamp = DateTime.Now
                    }
                };

                model.Timestamp = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                SharedLogger.LogError(ex, model.Name, model.ChannelId, model.UserId);
                return false;
            }
        }

        public ResponseMessageModel Upsert(Channel model)
        {
            if (model.ChannelId > 0)
            {
                var modelOld = _db.Channel.First(e => e.ChannelId == model.ChannelId);
                var chSched = modelOld.ChannelSchedule.First();
                modelOld.Name = model.Name;
                modelOld.Timestamp = DateTime.Now;
                chSched.MediaId = model.MediaId;
                chSched.Timestamp = DateTime.Now;
            }
            else
            {
                if (FillInfoChannel(model) == false)
                {
                    return new ResponseMessageModel
                    {
                        HasError = true,
                        Title = ResShared.TITLE_REGISTER_FAILED,
                        Message = ResShared.ERROR_INVALID_MODEL
                    };
                } 
                _db.Channel.Add(model);
            }

            _db.SaveChanges();

            return new ResponseMessageModel
            {
                HasError = false,
                Title = ResShared.TITLE_REGISTER_SUCCESS,
                Message = ResShared.INFO_REGISTER_SAVED
            };
        }
    }
}
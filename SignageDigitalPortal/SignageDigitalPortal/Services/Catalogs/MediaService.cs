using System;
using System.IO;
using System.Linq;
using System.Web;
using Infrastructure.Extensions;
using SignageDigitalPortal.Resources;
using SignageDigitalPortal.Services.Upload;
using SignageRepository.Database;
using SignageRepository.Log;
using SignageRepository.Models.Constants;
using SignageRepository.Models.Shared;
using SignageRepository.Resources;
using SignageRepository.Shared;

namespace SignageDigitalPortal.Services.Catalogs
{
    public class MediaService
    {
        public ResponseMessageModel DoUpsert(SignageDigitalEntities db, Media model)
        {
            var repository = new GenericRepository<Media>(db);

            if (model.MediaId == EntityConstants.NULL_VALUE)
            {
                repository.Add(model);
            }
            else
            {
                repository.Update(model);
            }

            return new ResponseMessageModel
            {
                HasError = false,
                Title = ResShared.TITLE_REGISTER_SUCCESS,
                Message = ResShared.INFO_REGISTER_SAVED
            };
        }

        public bool FillInfoMedia(Media model)
        {
            try
            {
                //Revisar primero si es local o externo
                switch (model.SourceType)
                {
                    case SharedConstants.MEDIA_TYPE_LOCAL:
                        var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + UploadMediaService.MediaRelativePath), model.RealName);
                        using (var fileInfo = File.OpenRead(filePath))
                        {
                            model.Size = fileInfo.Length;                            
                        }
                        model.Length = 0;
                        model.Url = model.RealName;
                        
                        var extFile = model.RealName.FileExtensionWoDot();
                        model.CatMimeId = UploadMediaService.DicMimeExtensions.Where(e => e.Value.LstMimes.Contains(extFile)).Select(e => e.Value.CatMimeId).First();
                        
                        break;

                    case SharedConstants.MEDIA_TYPE_EXTERNAL:
                        model.Length = 0;
                        model.LogicName = model.Url;
                        model.RealName = model.Url;
                        model.Size = 0;
                        model.CatMimeId = UploadMediaService.DicMimeExtensions.Where(e => e.Value.LstMimes.Contains(SharedConstants.MEDIA_EXTENSION_EXTERNAL)).Select(e => e.Value.CatMimeId).First();
                        
                        break;
                }


                model.Timestamp = DateTime.Now;
                model.Type = "NA";
            }
            catch (Exception ex)
            {
                SharedLogger.LogError(ex);
                return false;
            }

            return true;
        }
    }
}
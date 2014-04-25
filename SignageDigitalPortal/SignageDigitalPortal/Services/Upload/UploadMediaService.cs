using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Infrastructure.Extensions;
using Microsoft.AspNet.Identity;
using SignageDigitalPortal.Models;
using SignageDigitalPortal.Resources;
using SignageRepository.Models.Catalogs;
using SignageRepository.Models.Constants;
using SignageRepository.Repository;
using SignageRepository.Repository.Catalogs;
using SignageRepository.Resources;

namespace SignageDigitalPortal.Services.Upload
{
    public class UploadMediaService
    {


        public const int LOGO_MAX_SIZE = 1024 * 1204 * 1204;  //1GB
        private readonly string _imgStorage;

        private readonly string _urlApp;
        public static readonly Dictionary<string, MimeListModel> DicMimeExtensions;

        public static string MediaRelativePath = "Content/Images/Medias/";
        public static string ImageRelativePath = "Content/Images/";

        public static string LogoRelativePath = "Content/Images/Logos/";
        private readonly string _pathToSave;
        public long FileSize { get; set; }


        private delegate bool ActionUpload(HttpContext hContext, ResponseMediaFileMessageModel responseMsg);
        private readonly ActionUpload _lstOfActions;

        static UploadMediaService()
        {
            DicMimeExtensions = CatSharedRepository.GetMimeTypes();
        }

        public UploadMediaService(string sMediaStorage, string sUrlApp, string sPathToSave)
        {
            _imgStorage = sMediaStorage;
            _urlApp = sUrlApp;
            _pathToSave = sPathToSave;
            FileSize = LOGO_MAX_SIZE;
            _lstOfActions += ValidateUser;
            _lstOfActions += ValidateNumberOfFiles;
            _lstOfActions += ValidateSizeAndExtension;
            _lstOfActions += SaveMediaToContent;
        }

        private bool ValidateUser(HttpContext hContext, ResponseMediaFileMessageModel responseMsg)
        {
            try
            {
                var userId = hContext.User.Identity.GetUserId();

                if (userId == null)
                {
                    responseMsg.HasError = false;
                    responseMsg.MsgError = ResShared.ERROR_NOTAUTHENTICATED_USER;
                    return false;
                }

                if(AccountRepository.IsUserInRole(userId, new[] {RolesConstants.ROLE_MANAGER}))
                    return true;

                responseMsg.HasError = false;
                responseMsg.MsgError = ResShared.ERROR_NOTAUTHENTICATED_USER;
                return false;
            }
            catch (Exception)
            {
                responseMsg.HasError = false;
                responseMsg.MsgError = ResShared.ERROR_NOTAUTHENTICATED_USER;
                return false;
            }
            
        }
        
        private bool ValidateSizeAndExtension(HttpContext hcontext, ResponseMediaFileMessageModel responseMsg)
        {
            var file = hcontext.Request.Files[0];

            if (file.ContentLength > FileSize)
            {
                responseMsg.HasError = true;
                responseMsg.MsgError = string.Format(ResMediaRep.ERROR_MEDIA_SIZE_TOOLONG, FileSize);
                return false;
            }

            var extFile = file.FileName.FileExtensionWoDot();

            if (DicMimeExtensions.Any(e => e.Value.LstMimes.Contains(extFile)))
                return true;

            responseMsg.HasError = true;
            responseMsg.MsgError = ResMediaRep.ERROR_NOT_MEDIAFILE;
            return false;
        }

        private bool ValidateNumberOfFiles(HttpContext hcontext, ResponseMediaFileMessageModel responseMsg)
        {
            try
            {
                if (hcontext.Request.Files.Count == 1)
                {
                    return true;
                }

                responseMsg.HasError = true;
                responseMsg.MsgError = ResMediaRep.ERROR_UPLOAD_NOSINGLEMEDIA;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool SaveMediaToContent(HttpContext hContext, ResponseMediaFileMessageModel responseMsg)
        {

            try
            {
                var file = hContext.Request.Files[0];
                var ext = Path.GetExtension(file.FileName);
                var extWoDot = ext.Replace(".", String.Empty);
                var fileNameToSave = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fullPath = _imgStorage + fileNameToSave;
                file.SaveAs(fullPath);

                var bIsVideo = DicMimeExtensions.Any(e => e.Value.LstMimes.Contains(extWoDot) && e.Key == SharedConstants.MEDIA_VIDEO);

                responseMsg.Preview = bIsVideo ? _urlApp + ImageRelativePath + SharedConstants.MEDIA_VIDEO_IMG_FILE : _urlApp + _pathToSave + fileNameToSave;
                responseMsg.Media = fileNameToSave;
                responseMsg.FileName = file.FileName;

            }
            catch (Exception)
            {
                responseMsg.HasError = false;
                responseMsg.MsgError = ResShared.ERROR_UNKOWN;
                return false;
            }

            return true;

        }


        public ResponseMediaFileMessageModel ValidateAndSaveMedia(HttpContext hContext)
        {

            var responseMsg = new ResponseMediaFileMessageModel { HasError = false };

            try
            {
                if (_lstOfActions.GetInvocationList().Cast<ActionUpload>().Any(actionToDo => actionToDo.Invoke(hContext, responseMsg) == false))
                {
                    return responseMsg;
                }
                responseMsg.MsgError = ResMediaRep.INFO_UPLOADMEDIA_SUCCESSFUL;
                responseMsg.HasError = false;
            }
            catch (Exception)
            {
                responseMsg.HasError = true;
                responseMsg.MsgError = ResMediaRep.ERROR_UPLOADMEDIA_UNKNOW_ERROR;
            }
            return responseMsg;
        }


        public void CreateDirectory(string chunkPath)
        {
            Directory.CreateDirectory(chunkPath);
        }
    }
}


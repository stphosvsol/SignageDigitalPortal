using System;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SignageRepository.Database;
using SignageRepository.Repository;
using SignageRepository.Repository.Catalogs;

namespace SignageDigitalPortal.Services.Async
{
    public class CatSerializeService
    {
        private readonly JavaScriptSerializer _jSerializer;
        public CatSerializeService()
        {
            _jSerializer = new JavaScriptSerializer();
        }

        public async Task<string> GetScreenSizeAsync(SignageDigitalEntities db)
        {
            var lstInfo = await CatSharedRepository.GetScreenSizeAsync(db);
            return _jSerializer.Serialize(lstInfo);
        }

        public string GetScreenSize(SignageDigitalEntities db)
        {
            var lstInfo = CatSharedRepository.GetScreenSize(db);
            return _jSerializer.Serialize(lstInfo);
        }

        public string GetChannels(SignageDigitalEntities db)
        {
            var lstInfo = CatSharedRepository.GetChannels(db);
            return _jSerializer.Serialize(lstInfo);
        }
    }
}

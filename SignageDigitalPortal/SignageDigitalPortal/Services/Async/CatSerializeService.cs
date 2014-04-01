using System;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SignageRepository.Database;
using SignageRepository.Repository;

namespace SignageDigitalPortal.Services.Async
{
    public class CatSerializeService
    {
        private readonly JavaScriptSerializer _jSerializer;
        public CatSerializeService()
        {
            _jSerializer = new JavaScriptSerializer();
        }

        public async Task<string> GetScreenSize(SignageDigitalEntities db)
        {
            var lstInfo = await CatSharedRepository.GetScreenSize(db);
            return _jSerializer.Serialize(lstInfo);
        }
    }
}

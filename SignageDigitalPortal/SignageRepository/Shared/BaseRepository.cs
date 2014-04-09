using System;
using SignageRepository.Database;

namespace SignageRepository.Shared
{
    public class BaseRepository : IDisposable
    {
        protected SignageDigitalEntities Db;

        public BaseRepository(SignageDigitalEntities db)
        {
            Db = db;
        }

        public void Dispose()
        {
            try
            {
                if (Db != null)
                    Db.Dispose();
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                Db = null;
            }
        }
    }
}
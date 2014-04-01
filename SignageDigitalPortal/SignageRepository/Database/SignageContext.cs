using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace SignageRepository.Database
{
    public partial class SignageDigitalEntities
    {
        public SignageDigitalEntities(DbConnection db, bool bConextOwnsConnection):base(db, bConextOwnsConnection)
        {
            Database.Log = (e => Debug.WriteLine(e));
        }

        public void Detach(Object entity)
        {
            var context = (IObjectContextAdapter) this;
            context.ObjectContext.Detach(entity);
        }
    }
}

using System.Linq;
using SignageRepository.Database;

namespace SignageRepository.Repository
{
    public class AccountRepository
    {
        public static bool IsUserInRole(string userId, string[] roles)
        {
            using (var db = new SignageDigitalEntities())
            {
                return db.AspNetUsers.Any(e => e.Id == userId && e.AspNetRoles.Any(i => roles.Contains(i.Name)));
            }
        }
    }
}

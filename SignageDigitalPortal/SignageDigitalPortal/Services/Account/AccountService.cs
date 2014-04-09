using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SignageDigitalPortal.Models;
using SignageRepository.Models.Constants;
using SignageRepository.Models.Shared;

namespace SignageDigitalPortal.Services.Account
{
    public class AccountService
    {
        public string ProccessRequestByRole(ApplicationUser appUser, UserManager<ApplicationUser> userManager, Controller controller)
        {
            var userRole = appUser.Roles.Select(e => e.Role.Name).FirstOrDefault();
            switch (userRole)
            {
                case RolesConstants.ROLE_MANAGER:
                    return ProcessManagerRole(controller);
                default:
                    return ProcessNoUserRole(controller);
            }
        }

        private string ProcessNoUserRole(Controller controller)
        {
            return controller.Url.Action("Index", "Home", new { area = "" });
        }


        private string ProcessManagerRole(Controller controller)
        {
            return controller.Url.Action("Index", "Home", new { area = "" });
        }

    }
}

using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using DriverTracker.Models;

namespace DriverTracker.Authorization
{
    public class UserInfoPermissionHandler : AuthorizationHandler<SameDriverRequirement, Driver>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserInfoPermissionHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SameDriverRequirement requirement,
            Driver resource)
        {
            if (context.User.IsInRole("Admin") 
                || context.User.IsInRole("Analyst")
                || (requirement.PickupRequestSystemAllowed && context.User.IsInRole("PickupRequestSystem"))
                || _userManager.GetUserId(context.User) == resource.UserIDString)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class SameDriverRequirement : IAuthorizationRequirement {
        public SameDriverRequirement(bool pickupRequestAllowed = false)
        {
            PickupRequestSystemAllowed = pickupRequestAllowed;
        }

        public bool PickupRequestSystemAllowed { get; set; }
    }
}

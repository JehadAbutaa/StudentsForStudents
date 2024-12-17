using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SmartLineSystem.Helper
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredUserType;

        public CustomAuthorizeAttribute(string requiredUserType)
        {
            _requiredUserType = requiredUserType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userType = context.HttpContext.User.FindFirst("UserType")?.Value;
            if (userType != _requiredUserType)
            {
                context.Result = new ForbidResult(); // Or any other result you want to return
                return;
            }
            // Check if user is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ChallengeResult();
                return;
            }

            // Check if user's UserTypee matches the required type

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Models.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoedBezigWebApp.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        private readonly IUserRepository _userRepository;

        public UserFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Get current user
            var user = context.HttpContext.User.Identity.IsAuthenticated
                ? _userRepository.GetBy(context.HttpContext.User.Identity.Name)
                : null;

            var controller = context.Controller as Controller;

            if (controller == null) return;

            if (user != null)
            {
                context.ActionArguments["user"] = user;
                controller.ViewData["User"] = new UserViewModel(user);
            }
            else
            {
                context.Result = controller.RedirectToAction("Login", "Account");
            }
        }
    }
}

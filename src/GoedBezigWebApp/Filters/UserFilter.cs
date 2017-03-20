using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models.Repositories;
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
            context.ActionArguments["user"] = context.HttpContext.User.Identity.IsAuthenticated
                ? _userRepository.GetBy(context.HttpContext.User.Identity.Name)
                : null;
            base.OnActionExecuting(context);
        }
    }
}

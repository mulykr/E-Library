using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LiBook.Services.Extensions.Identity
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }

            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }

            return user.Identity.Name;
        }

        //public static string GetUserRole(this ClaimsPrincipal user)
        //{
        //    if (!user.Identity.IsAuthenticated)
        //    {
        //        return null;
        //    }

        //    string role = "";

        //    if (user.IsInRole(Enum.GetName(typeof(Roles), Roles.Admin)))
        //    {
        //        role = "Admin";
        //    }
        //    else if (user.IsInRole(Enum.GetName(typeof(Roles), Roles.User)))
        //    {
        //        role = "User";
        //    }

        //    return role;
        //}
    }
}

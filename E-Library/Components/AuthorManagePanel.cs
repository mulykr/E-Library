using LiBook.Models;
using LiBook.Services.Extensions.Identity;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiBook.Components
{
    public class AuthorManagePanel : ViewComponent
    {
        public IViewComponentResult Invoke(AuthorViewModel model)
        {
            var isAdmin = User != null && User.IsInRole("Admin");
            //var isOwner = User != null && (User as ClaimsPrincipal).GetUserId() == ;
            var likeBtn = "";
            if (model.AuthorLike.Any(i => i.UserProfileId == (User as ClaimsPrincipal).GetUserId()))
            {
                likeBtn = $"<a href=\"/Author/Like/{model.Id}\" class=\"fa fa-thumbs-up\" style=\"font-size: 1.4em;\"> {model.AuthorLike.Count} </a>";
            }
            else
            {
                likeBtn = $"<a href=\"/Author/Like/{model.Id}\" class=\"fa fa-thumbs-o-up\" style=\"font-size: 1.4em;\"> {model.AuthorLike.Count} </a>";
            }

            return new HtmlContentViewComponentResult(
                new HtmlString(likeBtn));
        }
    }
}

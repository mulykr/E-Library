using System.Linq;
using System.Security.Claims;
using LiBook.Models;
using LiBook.Services.Extensions.Identity;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace LiBook.Components
{
    public class CommentManagePanel : ViewComponent
    {
        public IViewComponentResult Invoke(CommentViewModel model)
        {
            var isAdmin = User != null && User.IsInRole("Admin");
            var isOwner = User != null && (User as ClaimsPrincipal).GetUserId() == model.UserId;
            var likeBtn = "";
            if (model.CommentLikes.Any(i => i.UserProfileId == (User as ClaimsPrincipal).GetUserId()))
            {
                likeBtn = $"<a href=\"/Comment/Like/{model.Id}\" class=\"fa fa-thumbs-up\" style=\"font-size: 1.4em;\"> {model.CommentLikes.Count} </a>";
            }
            else
            {
                likeBtn = $"<a href=\"/Comment/Like/{model.Id}\" class=\"fa fa-thumbs-o-up\" style=\"font-size: 1.4em;\"> {model.CommentLikes.Count} </a>";
            }

            if (isAdmin || isOwner)
            {
               
                return new HtmlContentViewComponentResult(
                    new HtmlString(likeBtn + "\t" +
                                   $"<a href=\"/Comment/Delete/{model.Id}\" class=\"btn btn-danger\">Remove</a>"));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString(likeBtn));
        }
    }
}

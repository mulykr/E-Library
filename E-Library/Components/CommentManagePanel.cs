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
            if (isAdmin || isOwner)
            {
                return new HtmlContentViewComponentResult(
                    new HtmlString($"<a href=\"/Comment/Delete/{model.Id}\" class=\"btn btn-danger\">Remove</a>"));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString($""));
        }
    }
}

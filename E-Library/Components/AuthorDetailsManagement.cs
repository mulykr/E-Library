using LiBook.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace LiBook.Components
{
    public class AuthorDetailsManagement: ViewComponent
    {
        public IViewComponentResult Invoke(AuthorViewModel model)
        {
            var isAdmin = User != null && User.IsInRole("Admin");
            if (isAdmin)
            {
                return new HtmlContentViewComponentResult(
                    new HtmlString($"<a href=\"/authors/Index/{model.Id}\" class=\"btn btn-info fa fa-arrow-left\"> </a> " +
                    $"<a href=\"/authors/Edit/{model.Id}\" class=\"btn btn-success fa fa-pencil\"> </a> "));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString($"<a href=\"/authors/Index/{model.Id}\" class=\"btn btn-info fa fa-arrow-left\"> </a>"));
        }
    }
}

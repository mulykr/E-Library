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
                    new HtmlString($"<a href=\"/authors/Index/{model.Id}\" class=\"btn btn-info\">Back To List</a> " +
                    $"<a href=\"/authors/Edit/{model.Id}\" class=\"btn btn-info\">Edit</a> "));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString($"<a href=\"/authors/Index/{model.Id}\" class=\"btn btn-success\">Back To List</a>"));
        }
    }
}

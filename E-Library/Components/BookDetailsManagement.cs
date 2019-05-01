using LiBook.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace LiBook.Components
{
    public class BookDetailsManagement: ViewComponent
    {
        public IViewComponentResult Invoke(BookViewModel model)
        {
            var isAdmin = User != null && User.IsInRole("Admin");
            if (isAdmin)
            {
                return new HtmlContentViewComponentResult(
                    new HtmlString($"<a href=\"/books/Index/{model.Id}\" class=\"btn btn-info fa fa-arrow-left\"> </a> " +
                    $"<a href=\"/books/Edit/{model.Id}\" class=\"btn btn-success fa fa-pencil\"> </a> "));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString($"<a href=\"/books/Index/{model.Id}\" class=\"btn btn-success fa fa-arrow-left\"> </a>"));
        }
    }
}

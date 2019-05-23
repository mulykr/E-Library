using LiBook.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace LiBook.Components
{
    public class BookTabActions : ViewComponent
    {
        public IViewComponentResult Invoke(BookViewModel model)
        {
            var isAdmin = User != null && User.IsInRole("Admin");
            if (isAdmin)
            {
                return new HtmlContentViewComponentResult(
                    new HtmlString($"<a href=\"/books/details/{model.Id}\" class=\"btn btn-success fa fa-eye\"> </a> " +
                    $"<a href=\"/books/edit/{model.Id}\" class=\"btn btn-info fa fa-pencil\"> </a> " +
                    $"<a href=\"/books/delete/{model.Id}\" class=\"btn btn-danger fa fa-trash-o\"> </a>"));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString($"<a href=\"/books/details/{model.Id}\" class=\"btn btn-success\">Open</a>"));
        }
    }
}

using LiBook.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace LiBook.Components
{
    public class AuthorTabActions : ViewComponent
    {
        public IViewComponentResult Invoke(AuthorViewModel model)
        {
            var isAdmin = User != null && User.IsInRole("Admin");
            if (isAdmin)
            {
                return new HtmlContentViewComponentResult(
                    new HtmlString($"<a href=\"/authors/details/{model.Id}\" class=\"btn btn-success fa fa-eye\"> </a> " +
                                   $"<a href=\"/authors/edit/{model.Id}\" class=\"btn btn-info fa fa-pencil\"> </a> " +
                                   $"<a href=\"/authors/delete/{model.Id}\" class=\"btn btn-danger fa fa-trash-o\"> </a>"));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString($"<a href=\"/authors/details/{model.Id}\" class=\"btn btn-success\">Open</a>"));
        }
    }
}

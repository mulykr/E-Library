using LiBook.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace LiBook.Components
{
    public class DownloadBook : ViewComponent
    {
        public IViewComponentResult Invoke(BookViewModel model)
        {
            var isAuth = User.Identity.IsAuthenticated;
            if (isAuth && model.PdfFilePath != null)
            {
                return new HtmlContentViewComponentResult(
                    new HtmlString($"<a href=\"/pdf/{model.PdfFilePath}\" class=\"btn btn-warning fa fa-cloud-download\"> </a> "));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString($""));
        }
    }
}

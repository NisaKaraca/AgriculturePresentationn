using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentationn.ViewComponents
{
    public class _NavbarPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
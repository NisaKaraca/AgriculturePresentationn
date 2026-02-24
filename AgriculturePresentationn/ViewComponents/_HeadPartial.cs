using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentationn.ViewComponents
{
    public class _HeadPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
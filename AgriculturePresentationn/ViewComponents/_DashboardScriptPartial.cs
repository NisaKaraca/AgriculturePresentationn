using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentationn.ViewComponents
{
    public class _DashboardScriptPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
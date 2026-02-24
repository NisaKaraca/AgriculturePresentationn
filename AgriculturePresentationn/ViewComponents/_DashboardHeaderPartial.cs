using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentationn.ViewComponents
{
    public class _DashboardHeaderPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
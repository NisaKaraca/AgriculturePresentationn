using DataAccessLayer.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AgriculturePresentationn.ViewComponents
{
    public class _DashboardChartPartial : ViewComponent
    {
        AgricultureContext c = new AgricultureContext();

        public IViewComponentResult Invoke()
        {
            var products = c.Products
                .OrderByDescending(x => x.Value)
                .Take(5)
                .Select(x => new { x.Name, x.Value })
                .ToList();

            ViewBag.ProductNames = products.Select(x => x.Name).ToList();
            ViewBag.ProductValues = products.Select(x => x.Value).ToList();

            return View();
        }
    }
}
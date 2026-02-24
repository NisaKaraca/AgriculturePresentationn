using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Contexts;
using AgriculturePresentationn.Models;

namespace AgriculturePresentationn.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AgricultureContext c = new AgricultureContext();

        public IActionResult Index()
        {
            var now = DateTime.Now;

            var model = new DashboardViewModel
            {
                Today = now.ToString("dd.MM.yyyy"),

                TeamCount = c.Teams.Count(),
                ServiceCount = c.Services.Count(),
                MessageCount = c.Contacts.Count(),

                CurrentMonthMessageCount = c.Contacts
                    .Count(x => x.Date.Month == now.Month && x.Date.Year == now.Year),

                Last4Messages = c.Contacts
                    .OrderByDescending(x => x.Date)
                    .Take(4)
                    .Select(x => new LastMessageDto
                    {
                        ContactId = x.ContactID,
                        Name = x.Name,
                        Mail = x.Mail,
                        MessagePreview = x.Message.Length > 60 ? x.Message.Substring(0, 60) + "..." : x.Message,
                        Date = x.Date
                    })
                    .ToList(),

                ProductChart = c.Products
                    .OrderByDescending(x => x.Value)
                    .Take(5)
                    .Select(x => new ProductChartDto
                    {
                        ProductName = x.Name,
                        ProductValue = x.Value
                    })
                    .ToList()
            };

            return View(model);
        }
    }
}
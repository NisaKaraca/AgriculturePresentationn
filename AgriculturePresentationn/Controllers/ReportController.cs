using AgriculturePresentationn.Models;
using ClosedXML.Excel;
using DataAccessLayer.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AgriculturePresentationn.Controllers
{
    public class ReportController : Controller
    {
        private readonly AgricultureContext _context;

        public ReportController()
        {
            _context = new AgricultureContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashboardReport()
        {
            using var workBook = new XLWorkbook();
            var ws = workBook.Worksheets.Add("Dashboard Raporu");

           
            ws.Cell(1, 1).Value = "Başlık";
            ws.Cell(1, 2).Value = "Değer";

           
            var teamCount = _context.Teams.Count();
            var serviceCount = _context.Services.Count();
            var messageCount = _context.Contacts.Count();
            var currentMonthMessageCount = _context.Contacts
                .Count(x => x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);

            ws.Cell(2, 1).Value = "Çalışan Sayısı";
            ws.Cell(2, 2).Value = teamCount;

            ws.Cell(3, 1).Value = "Hizmet Sayısı";
            ws.Cell(3, 2).Value = serviceCount;

            ws.Cell(4, 1).Value = "Toplam Mesaj";
            ws.Cell(4, 2).Value = messageCount;

            ws.Cell(5, 1).Value = "Bu Ay Gelen Mesaj";
            ws.Cell(5, 2).Value = currentMonthMessageCount;

            ws.Cell(6, 1).Value = "Rapor Tarihi";
            ws.Cell(6, 2).Value = DateTime.Now.ToString("dd.MM.yyyy");

            StyleHeader(ws.Range("A1:B1"));
            StyleTable(ws.Range("A1:B6"));
            ws.Columns().AdjustToContents();

            return CreateExcelFile(workBook, $"DashboardRapor_{DateTime.Now:ddMMyyyy}.xlsx");
        }

 
        public IActionResult DashboardSummary()
        {
            var teamCount = _context.Teams.Count();
            var serviceCount = _context.Services.Count();
            var messageCount = _context.Contacts.Count();

            var text =
                $"Dashboard Özeti - {DateTime.Now:dd.MM.yyyy}\n\n" +
                $"Çalışan Sayısı: {teamCount}\n" +
                $"Hizmet Sayısı: {serviceCount}\n" +
                $"Toplam Mesaj: {messageCount}\n";

            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            return File(bytes, "text/plain", $"DashboardOzet_{DateTime.Now:ddMMyyyy}.txt");
        }

        public IActionResult StaticReport()
        {
            using var workBook = new XLWorkbook();
            var ws = workBook.Worksheets.Add("Statik Rapor");

            ws.Cell(1, 1).Value = "Ürün Adı";
            ws.Cell(1, 2).Value = "Ürün Kategorisi";
            ws.Cell(1, 3).Value = "Ürün Stok";

            ws.Cell(2, 1).Value = "Mercimek";
            ws.Cell(2, 2).Value = "Bakliyat";
            ws.Cell(2, 3).Value = "785 Kg";

            ws.Cell(3, 1).Value = "Buğday";
            ws.Cell(3, 2).Value = "Tahıl";
            ws.Cell(3, 3).Value = "1.986 Kg";

            ws.Cell(4, 1).Value = "Havuç";
            ws.Cell(4, 2).Value = "Sebze";
            ws.Cell(4, 3).Value = "167 Kg";

            StyleHeader(ws.Range("A1:C1"));
            StyleTable(ws.Range("A1:C4"));
            ws.Columns().AdjustToContents();

            return CreateExcelFile(workBook, $"StatikRapor_{DateTime.Now:ddMMyyyy}.xlsx");
        }


        private List<ContactModel> ContactList()
        {
            return _context.Contacts.Select(x => new ContactModel
            {
                ContactID = x.ContactID,
                ContactName = x.Name,
                ContactDate = x.Date,
                ContactMail = x.Mail,
                ContactMessage = x.Message
            }).ToList();
        }

        public IActionResult ContactReport()
        {
            using var workBook = new XLWorkbook();
            var ws = workBook.Worksheets.Add("Mesaj Listesi");

            ws.Cell(1, 1).Value = "Mesaj ID";
            ws.Cell(1, 2).Value = "Mesaj Gönderen";
            ws.Cell(1, 3).Value = "Mail Adresi";
            ws.Cell(1, 4).Value = "Mesaj İçeriği";
            ws.Cell(1, 5).Value = "Mesaj Tarihi";

            int row = 2;
            foreach (var item in ContactList())
            {
                ws.Cell(row, 1).Value = item.ContactID;
                ws.Cell(row, 2).Value = item.ContactName;
                ws.Cell(row, 3).Value = item.ContactMail;
                ws.Cell(row, 4).Value = item.ContactMessage;
                ws.Cell(row, 5).Value = item.ContactDate.ToString("dd.MM.yyyy HH:mm");
                row++;
            }

            StyleHeader(ws.Range("A1:E1"));
            StyleTable(ws.Range($"A1:E{row - 1}"));

            ws.Columns().AdjustToContents();
            ws.Column(4).Width = 55;

            return CreateExcelFile(workBook, $"MesajRapor_{DateTime.Now:ddMMyyyy}.xlsx");
        }

        private List<AnnouncementModel> AnnouncementList()
        {
            return _context.Announcements.Select(x => new AnnouncementModel
            {
                Id = x.AnnouncementID,
                Title = x.Title,
                Date = x.Date,
                Description = x.Descripe,
                Status = x.Status
            }).ToList();
        }

        public IActionResult AnnouncementReport()
        {
            using var workBook = new XLWorkbook();
            var ws = workBook.Worksheets.Add("Duyuru Listesi");

            ws.Cell(1, 1).Value = "Duyuru ID";
            ws.Cell(1, 2).Value = "Duyuru Başlığı";
            ws.Cell(1, 3).Value = "Duyuru Tarihi";
            ws.Cell(1, 4).Value = "Duyuru İçeriği";
            ws.Cell(1, 5).Value = "Durum";

            int row = 2;
            foreach (var item in AnnouncementList())
            {
                ws.Cell(row, 1).Value = item.Id;
                ws.Cell(row, 2).Value = item.Title;
                ws.Cell(row, 3).Value = item.Date.ToString("dd.MM.yyyy");
                ws.Cell(row, 4).Value = item.Description;
                ws.Cell(row, 5).Value = item.Status ? "Aktif" : "Pasif";
                row++;
            }

            StyleHeader(ws.Range("A1:E1"));
            StyleTable(ws.Range($"A1:E{row - 1}"));

            ws.Columns().AdjustToContents();
            ws.Column(4).Width = 55;

            return CreateExcelFile(workBook, $"DuyuruRapor_{DateTime.Now:ddMMyyyy}.xlsx");
        }

        private List<ProductClass> ProductList()
        {
            return _context.Products
                .OrderByDescending(x => x.ProductId)
                .Select(x => new ProductClass
                {
                    productname = x.Name,
                    productvalue = x.Value
                })
                .ToList();
        }

        public IActionResult ProductReport()
        {
            using var workBook = new XLWorkbook();
            var ws = workBook.Worksheets.Add("Ürün Listesi");

            ws.Cell(1, 1).Value = "Ürün";
            ws.Cell(1, 2).Value = "Değer";

            int row = 2;
            foreach (var item in ProductList())
            {
                ws.Cell(row, 1).Value = item.productname;
                ws.Cell(row, 2).Value = item.productvalue;
                row++;
            }

            StyleHeader(ws.Range("A1:B1"));
            StyleTable(ws.Range($"A1:B{row - 1}"));

            ws.Columns().AdjustToContents();

            return CreateExcelFile(workBook, $"UrunRapor_{DateTime.Now:ddMMyyyy}.xlsx");
        }

        private static void StyleHeader(IXLRange headerRange)
        {
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        private static void StyleTable(IXLRange tableRange)
        {
            tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            tableRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        private static FileResult CreateExcelFile(XLWorkbook workbook, string fileName)
        {
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return new FileContentResult(content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = fileName
            };
        }
    }
}
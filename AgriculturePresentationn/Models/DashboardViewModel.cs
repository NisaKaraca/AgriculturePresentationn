using System;
using System.Collections.Generic;

namespace AgriculturePresentationn.Models
{
    public class DashboardViewModel
    {
        public string Today { get; set; }

        public int TeamCount { get; set; }
        public int ServiceCount { get; set; }
        public int MessageCount { get; set; }
        public int CurrentMonthMessageCount { get; set; }

        public List<LastMessageDto> Last4Messages { get; set; } = new();
        public List<ProductChartDto> ProductChart { get; set; } = new();
    }

    public class LastMessageDto
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string MessagePreview { get; set; }
        public DateTime Date { get; set; }
    }

    public class ProductChartDto
    {
        public string ProductName { get; set; }
        public int ProductValue { get; set; }
    }
}
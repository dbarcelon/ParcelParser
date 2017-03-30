using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParcelCalculator.API.Models
{
    public class PackagePriceDTO
    {
        public string Message { get; set; }
        public string PackageType { get; set; }
        public double Cost { get; set; }
        public double TotalPrice { get; set; }
    }
}
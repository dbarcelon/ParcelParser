using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParcelCalculator.API.Models
{
    public class PackageDTO
    {
        public double Lenght { get; set; }
        public double Breadth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int NoOfPackage { get; set; }
    }
}
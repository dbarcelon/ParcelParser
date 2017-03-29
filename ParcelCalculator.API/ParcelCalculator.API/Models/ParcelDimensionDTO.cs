using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParcelCalculator.API.Models
{
    public class ParcelDimensionDTO
    {
        public int Id { get; set; }
        public string PackageType { get; set; }
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double Height { get; set; }
        public double Cost { get; set; }
    }
}
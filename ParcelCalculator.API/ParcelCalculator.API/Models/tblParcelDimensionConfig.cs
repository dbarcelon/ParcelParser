using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParcelCalculator.API.Models
{
    public class tblParcelDimensionConfig
    {
        public int Id { get; set; }
        [Required]
        public string PackageType { get; set; }
        [Required]
        public double Length { get; set; }
        [Required]
        public double Breadth { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Cost { get; set; }
    }
}
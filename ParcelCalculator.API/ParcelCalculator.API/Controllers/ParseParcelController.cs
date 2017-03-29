using ParcelCalculator.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ParcelCalculator.API.Controllers
{
    
    public class ParseParcelController : ApiController
    {
        private ParcelCalculatorAPIContext db = new ParcelCalculatorAPIContext();

        public PackagePriceDTO GetParcelPrice(PackageDTO packageDTO)
        {
            var packagePriceDTO = new PackagePriceDTO(); 
            var withinWeightLimit = db.tblWeightConfigs.Where(r => r.Weight >= packageDTO.Weight).FirstOrDefault();
            var weightLimit = db.tblWeightConfigs.FirstOrDefault();
            if (withinWeightLimit == null)
            {
                packagePriceDTO.Message = string.Format("The Parcel Weight exceeded the maximum weight of {0}kg.", weightLimit.Weight);
                return packagePriceDTO;
            }
            var cubicDimension = packageDTO.Lenght * packageDTO.Breadth * packageDTO.Height;
            var packageTypeDimension = db.tblParcelDimensionConfigs.Where(r => (r.Length * r.Breadth * r.Height) >= cubicDimension).FirstOrDefault();
            if (packageTypeDimension==null)
            {
                packagePriceDTO.Message = "The Parcel dimension exceeded the maximum dimension of the service package.";
                return packagePriceDTO; 
            }

            packagePriceDTO.Message = "OK";
            packagePriceDTO.PackageType = packageTypeDimension.PackageType;
            packagePriceDTO.Price = packageTypeDimension.Cost;
            return packagePriceDTO;         
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }

}

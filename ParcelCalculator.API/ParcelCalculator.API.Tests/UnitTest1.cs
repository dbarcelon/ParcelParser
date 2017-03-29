using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParcelCalculator.API.Controllers;
using System.Web.Http;
using ParcelCalculator.API.Models;

namespace ParcelCalculator.API.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckPackageType()
        {
            var controller = new ParseParcelController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var packageDTO = new PackageDTO();
            packageDTO.Length = 190;
            packageDTO.Breadth = 260;
            packageDTO.Height = 150;
            packageDTO.Weight = 27;
            var _response = controller.GetParcelPrice(packageDTO);
            Assert.AreEqual("The Parcel Weight exceeded the maximum weight of 25kg.", _response.Message);  
        }
        [TestMethod]
        public void SmallPackageType()
        {
            var controller = new ParseParcelController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var packageDTO = new PackageDTO();
            packageDTO.Length = 190;
            packageDTO.Breadth = 260;
            packageDTO.Height = 150;
            packageDTO.Weight = 25;
            var _response = controller.GetParcelPrice(packageDTO);
            Assert.AreEqual("Small Package", _response.PackageType);
        }

        [TestMethod]
        public void MediumPackageType()
        {
            var controller = new ParseParcelController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var packageDTO = new PackageDTO();
            packageDTO.Length = 220;
            packageDTO.Breadth = 290;
            packageDTO.Height = 150;
            packageDTO.Weight = 25;
            var _response = controller.GetParcelPrice(packageDTO);
            Assert.AreEqual("Medium Package", _response.PackageType);
        }

        [TestMethod]
        public void LargePackageType()
        {
            var controller = new ParseParcelController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var packageDTO = new PackageDTO();
            packageDTO.Length = 300;
            packageDTO.Breadth = 550;
            packageDTO.Height = 180;
            packageDTO.Weight = 25;
            var _response = controller.GetParcelPrice(packageDTO);
            Assert.AreEqual("Large Package", _response.PackageType);
        }

        [TestMethod]
        public void ExceedDimension()
        {
            var controller = new ParseParcelController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var packageDTO = new PackageDTO();
            packageDTO.Length = 400;
            packageDTO.Breadth = 550;
            packageDTO.Height = 200;
            packageDTO.Weight = 25;
            var _response = controller.GetParcelPrice(packageDTO);
            Assert.AreEqual("The Parcel dimension exceeded the maximum dimension of the service package.", _response.Message);
        }
    }
}

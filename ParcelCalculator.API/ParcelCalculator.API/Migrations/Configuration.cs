namespace ParcelCalculator.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ParcelCalculator.API.Models.ParcelCalculatorAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ParcelCalculator.API.Models.ParcelCalculatorAPIContext context)
        {
            context.tblParcelDimensionConfigs.AddOrUpdate(x => x.Id,
                new Models.tblParcelDimensionConfig() { Id = 1, PackageType = "Small Package", Length = 210, Breadth = 280, Height = 130, Cost = 5 },
                new Models.tblParcelDimensionConfig() { Id = 2, PackageType = "Medium Package", Length = 280, Breadth = 390, Height = 180, Cost = 7.5 },
                new Models.tblParcelDimensionConfig() { Id = 3, PackageType = "Large Package", Length = 380, Breadth = 550, Height = 200, Cost = 8.5 }
                );

            context.tblWeightConfigs.AddOrUpdate(x => x.Id,
                new Models.tblWeightConfig() { Id = 1, Weight = 25 });      
        }
    }
}

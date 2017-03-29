namespace ParcelCalculator.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblParcelDimensionConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageType = c.String(nullable: false),
                        Length = c.Double(nullable: false),
                        Breadth = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblWeightConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Weight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblWeightConfigs");
            DropTable("dbo.tblParcelDimensionConfigs");
        }
    }
}

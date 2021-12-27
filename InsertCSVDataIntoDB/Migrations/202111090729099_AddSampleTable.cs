namespace InsertCSVDataIntoDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSampleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Samples",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Region = c.String(),
                        Country = c.String(),
                        ItemType = c.String(),
                        Sales_Channel = c.String(),
                        Order_Priority = c.String(),
                        Order_Date = c.DateTime(nullable: false),
                        Order_ID = c.String(),
                        Ship_Date = c.DateTime(nullable: false),
                        Units_Sold = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Samples");
        }
    }
}

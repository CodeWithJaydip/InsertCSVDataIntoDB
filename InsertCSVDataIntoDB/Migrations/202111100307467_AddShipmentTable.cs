namespace InsertCSVDataIntoDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShipmentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        shipment_id = c.String(),
                        carrier = c.String(),
                        destination_city = c.String(),
                        destination_name = c.String(),
                        pickup_start_datetime = c.DateTime(),
                        insert_datetime = c.DateTime(nullable: false),
                        insert_by = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shipments");
        }
    }
}

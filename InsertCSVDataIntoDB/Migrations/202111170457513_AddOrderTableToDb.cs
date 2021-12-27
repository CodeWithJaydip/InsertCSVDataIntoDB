namespace InsertCSVDataIntoDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderTableToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        order_status_id = c.Int(nullable: false),
                        customer_id = c.Int(),
                        ship_datetime = c.DateTime(),
                        ship_by = c.String(),
                        insert_datetime = c.DateTime(nullable: false),
                        insert_by = c.String(),
                        order_datetime = c.DateTime(),
                        shipment_id = c.String(),
                        carrier = c.String(),
                        destination_city = c.String(),
                        destination_name = c.String(),
                        tender_datetime = c.DateTime(),
                        pickup_start_datetime = c.DateTime(),
                        IsInternational = c.Boolean(nullable: false),
                        IsShipmentNotificationSent = c.Boolean(nullable: false),
                        LastShipmentNotification = c.DateTime(),
                        CancellationReason = c.String(),
                        UserId = c.String(),
                        release_datetime = c.DateTime(),
                        cancelled_datetime = c.DateTime(),
                        cancelled_by = c.String(),
                        OrderStatus = c.String(),
                        TruckId = c.Int(),
                        manifest_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.orders");
        }
    }
}

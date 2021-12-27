using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsertCSVDataIntoDB.Models
{
    public class order
    {
        public int id { get; set; }
        public int order_status_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public Nullable<System.DateTime> ship_datetime { get; set; }
        public string ship_by { get; set; }
        public System.DateTime insert_datetime { get; set; }
        public string insert_by { get; set; }
        public Nullable<System.DateTime> order_datetime { get; set; }
        public string shipment_id { get; set; }
        public string carrier { get; set; }
        public string destination_city { get; set; }
        public string destination_name { get; set; }
        public Nullable<System.DateTime> tender_datetime { get; set; }
        public Nullable<System.DateTime> pickup_start_datetime { get; set; }
        public bool IsInternational { get; set; }
        public bool IsShipmentNotificationSent { get; set; }
        public Nullable<System.DateTime> LastShipmentNotification { get; set; }
        public string CancellationReason { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> release_datetime { get; set; }
        public Nullable<System.DateTime> cancelled_datetime { get; set; }
        public string cancelled_by { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<int> TruckId { get; set; }
        public Nullable<int> manifest_id { get; set; }
    }
}
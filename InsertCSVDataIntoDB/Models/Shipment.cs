using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsertCSVDataIntoDB.Models
{
    public class Shipment
    {
        public int id { get; set; }
        public string shipment_id { get; set; }
        public string carrier { get; set; }
        public string destination_city { get; set; }
        public string destination_name { get; set; }
        public Nullable<System.DateTime> pickup_start_datetime { get; set; }
        public System.DateTime insert_datetime { get; set; }
        public string insert_by { get; set; }
    }
}
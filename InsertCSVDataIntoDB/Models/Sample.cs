using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsertCSVDataIntoDB.Models
{
    public class Sample
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string ItemType { get; set; }
        public string Sales_Channel { get; set; }
        public string Order_Priority { get; set; }
        public DateTime Order_Date { get; set; }
        public string Order_ID { get; set; }
        public DateTime Ship_Date { get; set; }
        public string Units_Sold { get; set; }
    }
}
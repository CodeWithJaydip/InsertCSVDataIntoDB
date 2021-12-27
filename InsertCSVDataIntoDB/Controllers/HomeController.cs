using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using InsertCSVDataIntoDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UAParser;

namespace InsertCSVDataIntoDB.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();


        public ActionResult Index()
        {
            try
            {
                var dateTime1 = (DateTime.Now - DateTime.Now.AddDays(-60)).Ticks;
                var dateTimeString = (DateTime.Now - DateTime.Now.AddDays(-60)).ToString();
                var dateTime2 = Convert.ToDateTime((DateTime.Now.Date - DateTime.Now.Date.AddDays(-60)).ToString());
                var QA = "";
            }
            catch(Exception e)
            {

            }
            
            string dt1 = DateTime.Now.ToString("MMM/dd/yyyy");
            DateTime dt = DateTime.ParseExact("12/06/2021", "MMM/dd/yyyy", CultureInfo.InvariantCulture);
            string date = dt.ToString("MM/dd/yyyy");
            DateTime date1 = Convert.ToDateTime(date);
            //string IpAdress = GetIp();
          //  string UserAgent = getUserAgent();

             string description = "SB #: 1451; Warehouse: Indianapolis; Category: Hardware";
            Regex regex = new Regex(":.+?;");
            var v = regex.Match(description);
            string s = v.Groups[0].ToString();
            string result = s.Substring(1, s.Length - 2).Trim();
           
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime currentTime = DateTime.Now;
            string currentTime1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime con = Convert.ToDateTime(currentTime1);

            string currentTime2 = DateTime.Now.ToString("yyyy-MM-dd");

            
           var dateTime = DateTime.ParseExact(currentTime2, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            currentTime = DateTime.ParseExact(currentTime1, "yyyy-MM-dd HH:mm:ss",provider);
            DateTime DT2 = new DateTime(2019, 05, 09, 09, 15, 00);

            DateTime changedtimeUTC = TimeZoneInfo.ConvertTimeToUtc(DT2, TimeZoneInfo.Local);
            DateTime get24hours = currentTime.AddHours(24);
            return View();
        }

        public string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ClientIp="";
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }

            if (!string.IsNullOrEmpty(ip))
            {
               ClientIp  = ip.Split(',')[0];
            }

            return ClientIp;
        }

        public void ExportCSV_Employee()
        {
            var sb = new StringBuilder();
            // You can write sql query according your need  

            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", "First Name", "Last Name", "Address ", "BirdthDate", "City", "Salry", Environment.NewLine);
            
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", "Jaydip", "Mer", "Test", "02-03-200", "Ahmedabad", "NA", Environment.NewLine);
            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", "Jaydip", "Mer", "Test", "02-03-200", "Ahmedabad", "NA", Environment.NewLine);

            //Get Current Response  
            var response = System.Web.HttpContext.Current.Response;
            response.BufferOutput = true;
            response.Clear();
            response.ClearHeaders();
            response.ContentEncoding = Encoding.Unicode;
            response.AddHeader("content-disposition", "attachment;filename=Employee.CSV ");
            response.ContentType = "text/plain";
            response.Write(sb.ToString());
            response.End();
        }

        //private string getUserAgent()
        //{
        //    var test = HttpContext.Request.UserHostAddress;
        //    string demo=httpRequest.RequestContext.HttpContext.Request.Headers["User-Agent"].ToString();
        //    string userAgent = Request.UserAgent.ToString();
        //    var uaParser = Parser.GetDefault();
        //    ClientInfo cinfo = uaParser.Parse(userAgent);
        //    string jsonClientInfo = JsonConvert.SerializeObject(cinfo);
        //    var data = (JObject)JsonConvert.DeserializeObject(jsonClientInfo);

        //    var device= data["Device"]["Family"].Value<string>();
        //    var OS= data["OS"]["Family"].Value<string>() + " " + data["OS"]["Major"].Value<string>();
        //    var Browser= data["UA"]["Family"].Value<string>();
        //    var BrowserVersion = data["UserAgent"]["Major"].Value<string>();

        //    return userAgent;
           
        //}

        public ActionResult About()
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            DataTable dt = new DataTable();
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\jaydip mer\shipping.2021.11.08 - Copy.csv"))
                {
                    string[] headers = sr.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header);
                    }
                    while (!sr.EndOfStream)
                    {
                        string[] rows = sr.ReadLine().Split(',');
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                        }
                        dt.Rows.Add(dr);
                    }

                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow drm = dt.Rows[i];
                    var address1 = drm["SHP_DEST_ADDRESS1"].ToString();
                    var demo1 = drm["SHP_SHIP_DATE"].ToString();
                    var date = Convert.ToDateTime(drm["SHP_SHIP_DATE"]);
                    var demo = DateTime.ParseExact(drm["SHP_SHIP_DATE"].ToString(), "dd/M/yyyy", CultureInfo.InvariantCulture);
                    context.shipments.Add(new Shipment()
                    {
                        id = i + 1,
                        shipment_id = drm["SHP_PRO_NUM"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SHP_PRO_NUM"]),
                        carrier = drm["SHP_CAR_SCAC"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SHP_CAR_SCAC"]),
                        destination_city = drm["SHP_DEST_CITY"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SHP_DEST_CITY"]),
                        destination_name = drm["SHP_DEST_NAME"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SHP_DEST_NAME"]),
                        //pickup_start_datetime = drm["SHP_SHIP_DATE"] == DBNull.Value ? DateTime.ParseExact("2000-01-01 00:00","dd/MM/yyyy",null) : DateTime.ParseExact(drm["SHP_SHIP_DATE"].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture),
                        pickup_start_datetime = DateTime.Now,
                        insert_by = "System",
                        insert_datetime = DateTime.Now
                    }); context.SaveChanges();
                }

            }
            catch (Exception e)
            {

            }
            //try
            //{
            //    using (StreamReader sr = new StreamReader(@"C:\Users\jaydip mer\Downloads\100000-Sales-Records\100000 Sales Records.csv"))
            //    {
            //        string[] headers = sr.ReadLine().Split(',');
            //        foreach (string header in headers)
            //        {
            //            dt.Columns.Add(header);
            //        }
            //        while (!sr.EndOfStream)
            //        {
            //            string[] rows = sr.ReadLine().Split(',');
            //            DataRow dr = dt.NewRow();
            //            for (int i = 0; i < headers.Length; i++)
            //            {
            //                dr[i] = rows[i];
            //            }
            //            dt.Rows.Add(dr);
            //        }

            //    }


            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow dr = dt.Rows[i];
            //        context.samples.Add(new Sample()
            //        {
            //            Id = i + 1,
            //            Region = dr["Region"].ToString(),
            //            Country = dr["Country"].ToString(),
            //            ItemType = dr["Item Type"].ToString(),
            //            Sales_Channel = dr["Sales Channel"].ToString(),
            //            Order_Priority = dr["Order Priority"].ToString(),
            //            Order_Date = DateTime.Now,
            //            Order_ID = dr["Order ID"].ToString(),
            //            Ship_Date = DateTime.Now,
            //            Units_Sold = dr["Units Sold"].ToString()
            //        }); context.SaveChanges();
            //    }

            //}
            //catch (Exception e)
            //{

            //}

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var dt = loadDataTable(@"C:\Users\jaydip mer\shipping.2021.11.08 - Copy.csv");
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow drm = dt.Rows[i];
               

                // Process record
                
                var manifestId = 0;

                int warehouseId = drm["SHP_WHS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(drm["SHP_WHS_ID"]);
                string sbNumber = drm["SCN_SBNUM"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SCN_SBNUM"]);

               
              

               

                // Get the order record
               
                int newShipmentId = 0;
               
                    try
                    {
                        // Add shipment record if not exists in the db
                        //newShipmentId = CreateShipment(manifestId, drm, (order.order_datetime ?? order.insert_datetime));
                        using (SqlCommand cmd = new SqlCommand("InsertShipment"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            
                            cmd.Parameters.AddWithValue("@ShipmentId", drm["SHP_PRO_NUM"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SHP_PRO_NUM"]));
                            cmd.Parameters.AddWithValue("@Carrier", drm["SHP_CAR_SCAC"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SHP_CAR_SCAC"]));
                            cmd.Parameters.AddWithValue("@DestinationCity", drm["SHP_DEST_CITY"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SHP_DEST_CITY"]));
                            cmd.Parameters.AddWithValue("@DestinationName", drm["SHP_DEST_NAME"] == DBNull.Value ? string.Empty : Convert.ToString(drm["SHP_DEST_NAME"]));
                            cmd.Parameters.AddWithValue("@TenderDateTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@PickupStartDateTime", drm["SHP_SHIP_DATE"] == DBNull.Value ? Convert.ToDateTime("2000-01-01 00:00") : Convert.ToDateTime(drm["SHP_SHIP_DATE"]));
                            cmd.Parameters.AddWithValue("@InsertDateTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@InsertBy", "System");
                            cmd.Connection = con;
                            cmd.CommandTimeout = 1000000;


                            object obj = cmd.ExecuteScalar();
                            newShipmentId = Convert.ToInt32(obj);
                        }
                    }
                    catch (Exception e)
                    {
                        con.Close();
                    }

                
               

            }


            ViewBag.Message = "Your contact page.";

            return View();
        }


        private static DataTable loadDataTable(string inputpath)
        {
            DataTable csvdt = new DataTable();
            //csvdt.TableName = tableName;

            string Fulltext;
                using (StreamReader sr = new StreamReader(inputpath))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString();//read full content
                        string[] rows = Fulltext.Split('\n');//split file content to get the rows
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            var regex = new Regex("\\\"(.*?)\\\"");
                            var output = regex.Replace(rows[i], m => m.Value.Replace(",", "\\c"));//replace commas inside quotes
                            string[] rowValues = output.Split(',');//split rows with comma',' to get the column values
                            {
                                if (i == 0)
                                {
                                    for (int j = 0; j < rowValues.Count(); j++)
                                    {
                                        csvdt.Columns.Add(rowValues[j].Replace("\\c", ","));//headers
                                    }

                                }
                                else
                                {
                                    try
                                    {
                                        DataRow dr = csvdt.NewRow();
                                        for (int k = 0; k < rowValues.Count(); k++)
                                        {
                                            if (k >= dr.Table.Columns.Count)// more columns may exist
                                            {
                                                csvdt.Columns.Add("clmn" + k);
                                                dr = csvdt.NewRow();
                                            }
                                            dr[k] = rowValues[k].Replace("\\c", ",");

                                        }
                                        csvdt.Rows.Add(dr);//add other rows
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("error");
                                        throw e;
                                    }
                                }
                            }
                        }
                    }
                }
            

            return csvdt;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections;
using Aeriksa.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;
using Aeriksa.Data;
namespace Aeriksa.Controllers
{
    public class HomeController : Controller
    {
       ServiceBAL serviceBAL = new ServiceBAL();

        
        public ActionResult Index()
        {
          //  serviceBAL.getDataFromService();
            
            return View();
        }

        public JsonResult Piechart(string type)
        {
            TempData["location"] = type;
           // ViewBag.type = type;
            IList collection = null;
                            JObject jsonObject;
                            JObject jsonObject1 = null; 
                using (HttpClient client = new HttpClient())
                {
                    switch (type)
                    {
                        case "Delhi":
                            client.BaseAddress = new System.Uri(System.Configuration.ConfigurationManager.AppSettings["Delhi"].ToString());
                            break;
                        case "Bangalore":
                            client.BaseAddress = new System.Uri(System.Configuration.ConfigurationManager.AppSettings["Bangalore"].ToString());
                            break;
                        case "Mumbai":
                            client.BaseAddress = new System.Uri(System.Configuration.ConfigurationManager.AppSettings["Mumbai"].ToString());
                            break;
                        default:
                            client.BaseAddress = new System.Uri(System.Configuration.ConfigurationManager.AppSettings["Bangalore"].ToString());
                            break;
                    }

                  //  client.BaseAddress = new System.Uri(System.Configuration.ConfigurationManager.AppSettings["Bangalore"].ToString()); 
                //    var url = "studies?userId=" + app.user.Id;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                  //  client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", app.user.AccessToken);
                    HttpResponseMessage response =  client.GetAsync(client.BaseAddress).Result;
                    //response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {                       
                        var data = response.Content.ReadAsStringAsync();
                        
                        try
                        {
                            jsonObject = JObject.Parse(data.Result.ToString());

                            
                           
                        //    JSONArray FoodArray = response.getJSONArray("foods");

                        //    for (int i = 0; i < data.length(); i++) {
                        //    String character = FoodArray.getJSONObject(i).getString("char");
                        //    System.out.println("*****character****************"+character);
                        //    JSONArray FoodNameArray = new JSONArray(FoodArray.getJSONObject(i).getString("content"));
                        //    System.out.println("====================///////////"+FoodNameArray);

                        //    for (int j = 0; j <FoodNameArray.length(); j++) {
                        //        String Foodname = FoodArray.getJSONObject(j).getString("food_name");
                        //        System.out.println("@@@@@@@@@@@@@"+Foodname);
                        //    }
                        //}


                          //  JsonResult test = (JsonResult)jsonObject.Property("with");

                            JArray with = jsonObject.Value<JArray>("with");

                            List<ContentModel> contentModel = new List<ContentModel>();

                            foreach (JObject item in with)
                            {
                                ContentModel content = new ContentModel();
                                jsonObject1 = item.Value<JObject>("content");
                                content.ContentDate = jsonObject1.Value<string>("date");
                                content.CO2 = jsonObject1.Value<int>("CO2");
                                content.PM10 = jsonObject1.Value<int>("PM10");
                                content.PM2 = jsonObject1.Value<int>("PM2.5");
                                content.Temp_high = jsonObject1.Value<string>("Temperature_High");
                                contentModel.Add(content);
                                TempData["CO2"] = content.CO2;
                                TempData["PM10"] = content.PM10;

                            }

                            ContentModel[] test3345 = contentModel.ToArray();                            
                            var name = jsonObject.Property("with");
                            if (name != null)
                            {
                                //var result = (from tags in name
                                //              orderby ta.Title ascending
                                //              select new { tags.Title, tags.Credits }).ToList();
                                //return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

                               var jsontext = name.Value.ToString();
                                jsontext = jsontext.Replace("[", "").Replace("]", "").Split(',')[0];
                            }
                            return Json(test3345, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception)
                        {
                            jsonObject = null;
                        }

                        if (jsonObject != null)
                        {

                        }
                         collection = (IList)jsonObject;
                       
                       
                    }

                    
                }

               // return Content(JsonConvert.SerializeObject(collection));
              //  return Json(collection, JsonRequestBehavior.AllowGet);
                return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graph()
        {
            string[] _xval = { "vulpes", "Sp Nayak", "Krishna", "Datta Kharad", "Prabhu Raja" };
            string[] _yval = { "235", "130", "30", "25", "15" };
            //here the chart is going on
            var bytes = new Chart(width: 600, height: 300)
            .AddSeries(
            chartType: "line", legend: "Mindcracker Current Month Runner up",
             xValue: _xval,
             yValues: _yval)
            .GetBytes("png");
            return File(bytes, "image/png");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult test()
        {
            return View();

        }


        [HttpPost]
        public ActionResult test(login test)
        {

            using (var _db = new AriskaEntities())
            {
                var rows = (from myRow in _db.logins
                            select new loginModel
                            {
                                userid = myRow.userid,
                                password = myRow.password,

                            }).ToList();
            }
            return View();

        }


        public ActionResult LineChartDateTimeAxis()
        {
            return View(DateTimeXAxisChartData.GetLineChartData());
        }

        public ActionResult LineChart() 
        {

            return View();
        }

        public ActionResult WeatherForecast()
        {
            return View();
        }

        public ActionResult Weather()
        {
            return View();
        }

        public ActionResult LogIntest1()
        {
           // string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

           // string queryString = "Select * FROM dbo.login"; //update as you feel fit of course for insert/update etc

           // using (SqlConnection connection = new SqlConnection(cs))
           // {
           //     connection.Open();
           //     SqlDataAdapter adapter = new SqlDataAdapter();
           //     SqlCommand command = new SqlCommand(queryString, connection);
           //     SqlDataAdapter adp = new SqlDataAdapter();
           //     command.ExecuteNonQuery();
           //     DataTable dt = new DataTable();
           //     adp.SelectCommand = command;
           ////  var data = new DataSet();
           //     adp.Fill(dt);
                

           //     connection.Close();
           // }
            return View();
        }
        [HttpPost]
        public ActionResult LogIntest1(loginModel loginmodel)
        {
            using (var _db = new AriskaEntities())
            {


                var user = (from myRow in _db.logins
                            where ((loginmodel.userid == myRow.userid) && (loginmodel.password == myRow.password) )
                            select new loginModel
                            {
                                userid = myRow.userid,
                                password = myRow.password,

                            }).SingleOrDefault();

                if (user != null)
                {
                    TempData["UserId"] = user.userid;
                    return RedirectToAction("DashBoard");
                }
                if(loginmodel.userid != null || loginmodel.password != null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult DashBoard()
        {


            return View();
        }

        public ActionResult AQI()
        {


            return View();
        }

        public ActionResult AirQuality()
        {
            ContentModel contentModel = new ContentModel();
            if (TempData["location"] == null)
            {
                TempData["location"] = "Bangalore";
            }
            contentModel.type = TempData["location"] as string;
            return View(contentModel);
        }

        public ActionResult firebase()
        {
            //var firebaseClient = new Firebase("https://developer-api.nest.com", _accessToken);
            //var response = firebaseClient.GetStreaming("devices",
            //        changed: (s, e) =>
            //        {
            //            if (e.Path.Contains("ambient_temperature_f"))
            //                Console.WriteLine("Current temperature has been updated to: {0}.", e.Data);
            //        });
            return View();
        }

        public ActionResult LoginTest()
        {
            return View();
        }

        public ActionResult AQIDashboard(string username)
        {
            Session["UserName"] = username;

            return View();
        }



        //public JsonResult GetData()
        //{
        //    using (var db = new ContosoUniversityEntities())
        //    {
        //        var result = (from tags in db.Courses
        //                      orderby tags.Title ascending
        //                      select new { tags.Title, tags.Credits }).ToList();
        //        return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        //        //return Content(JsonConvert.SerializeObject(_dataPoints), "application/json");
        //    }
        //}
    }
}
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ServiceBAL
    {
        public void getDataFromService()
        {
            try
            {
                JObject jsonObject; 
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress =new System.Uri(System.Configuration.ConfigurationManager.AppSettings["ImageRootPath"].ToString()); 
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
                        }
                        catch (Exception)
                        {
                            jsonObject = null;
                        }

                        if (jsonObject != null)
                        {

                        }
                       
                       
                    }

                    
                }
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Message ::::::: ", ex);

            }
        }

        private void GetData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56851/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/User").Result;

            if (response.IsSuccessStatusCode)
            {


            }
            else
            {

            }
        }
    }



}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientAPI.DataProvider
{
   
    public class ProfessionalAPIProvider
    {
        private static string BASE_URI = "http://localhost:5912/api/professionnel";

        public static async Task<List<Domain>> getDomains(string token)
        {
            List<Domain> domainList = new List<Domain>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization
                          = new AuthenticationHeaderValue("Bearer", token);

                using (var response = await httpClient.GetAsync(BASE_URI+"/domains"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(apiResponse);
                    domainList = JsonConvert.DeserializeObject<List<Domain>>(apiResponse);
                    
                }
            }
            return domainList;
        }


        public async Task<Domain> getDomain(string domainName, string token)
        {
            //   Domain domain = getDomains(token).(domain => { }); ;
            return null ;
        }

        public async Task<List<Intervention>> geInterventionsAsync()
        {
            List<Intervention> interventionList = new List<Intervention>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(BASE_URI))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    interventionList = JsonConvert.DeserializeObject<List<Intervention>>(apiResponse);
                }
            }
            return interventionList;
        }

        public async Task<Intervention> geInterventionAsync()
        {
            Intervention intervention = new Intervention();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(BASE_URI))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    intervention = JsonConvert.DeserializeObject<Intervention>(apiResponse);
                }
            }
            return intervention;
        }


    }
}

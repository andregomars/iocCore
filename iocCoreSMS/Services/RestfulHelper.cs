using iocCoreSMS.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace iocCoreSMS.Services
{
    public static class RestfulHelper
    {
        private static HttpClient client = new HttpClient();
        public static async Task<List<SMSMessage>> GetSMSMessageAsync(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string responseString = await GetMethodAsync(url);
            var inboxResponse = JsonConvert.DeserializeObject<List<SMSMessage>>(responseString);
            
            return inboxResponse;
        }
        
        private static async Task<string> GetMethodAsync(string url)
        {
            string responseString = null;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString; 
        }
    }
}
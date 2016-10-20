using iocCoreSMS.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace iocCoreSMS.Services
{
    public class RestfulHelper
    {
        private HttpClient client;

        public RestfulHelper()
        {
            this.client = new HttpClient();
        }

        public async Task<List<SMSMessage>> GetSMSMessageAsync(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string responseString = await GetMethodAsync(url);
            var inboxResponse = JsonConvert.DeserializeObject<List<SMSMessage>>(responseString);
            
            return inboxResponse;
        }
       public async Task<bool> UpdateSMSMessageAsync(string url, SMSMessage msg)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string payload = JsonConvert.SerializeObject(msg);
            return await PutMethodAsync(url, payload);
        }
        public async Task<SMSMessage> AddSMSMessageAsync(string url, SMSMessage msg)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string payload = JsonConvert.SerializeObject(msg);
            string responseString = await PostMethodAsync(url, payload);
            var response = JsonConvert.DeserializeObject<SMSMessage>(responseString);
            
            return response;
        }
 
        
        public async Task<OutboundSMSResponseWrapper> SendSMSAsync(string url, OutboundSMSRequestWrapper obSMSReqWrapper)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer BF-ACSI~5~20161019040217~HDujbVO0IvN05jKCkKbpdKadyL3FCBws");

            string payload = JsonConvert.SerializeObject(obSMSReqWrapper);
            string responseString = await PostMethodAsync(url, payload);
            var response = JsonConvert.DeserializeObject<OutboundSMSResponseWrapper>(responseString);
            
            return response;
        }
        public async Task<InboundSmsMessageListWrapper> ReceiveSMSAsync(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer BF-ACSI~5~20161019040217~HDujbVO0IvN05jKCkKbpdKadyL3FCBws");

            string responseString = await GetMethodAsync(url);
            var response = JsonConvert.DeserializeObject<InboundSmsMessageListWrapper>(responseString);
            
            return response;
        }

        public async Task<string> GetSMSClientToken(string url)
        {
            return null;
        }
        
        //general helpers
        private async Task<string> GetMethodAsync(string url)
        {
            string responseString = null;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString; 
        }
        
        private async Task<string> PostMethodAsync(string url, string payload)
        {
            string responseString = null;
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString; 
        }
        private async Task<bool> PutMethodAsync(string url, string payload)
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }



    }
}
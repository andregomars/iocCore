using iocCoreSMS.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System;

namespace iocCoreSMS.Services
{
    public class RestfulHelper
    {
        public const string CONTENTTYPE_JSON = "application/json";
        public const string CONTENTTYPE_FORM = "application/x-www-form-urlencoded";
        private HttpClient client;

        public RestfulHelper()
        {
            this.client = new HttpClient();
        }

#region message helpers
        public async Task<List<SMSMessage>> GetSMSMessageAsync(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string responseString = await GetMethodAsync(url);
            if (String.IsNullOrEmpty(responseString)) return null;
            var inboxResponse = JsonConvert.DeserializeObject<List<SMSMessage>>(responseString);
            
            return inboxResponse;
        }
       public async Task<bool> UpdateSMSMessageAsync(string url, SMSMessage msg)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string payload = JsonConvert.SerializeObject(msg);
            return await PutMethodAsync(url, payload, CONTENTTYPE_JSON);
        }
        public async Task<SMSMessage> AddSMSMessageAsync(string url, SMSMessage msg)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string payload = JsonConvert.SerializeObject(msg);
            string responseString = await PostMethodAsync(url, payload, CONTENTTYPE_JSON);
            var response = JsonConvert.DeserializeObject<SMSMessage>(responseString);
            
            return response;
        }
#endregion message helpers 
        
#region SMS helpers
        public async Task<OutboundSMSResponseWrapper> SendSMSAsync(string url, string accessToken, 
             OutboundSMSRequestWrapper obSMSReqWrapper)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", accessToken);

            object requestObj = Common.OutboundSMSRequestAdapter(obSMSReqWrapper);
            string payload = JsonConvert.SerializeObject(requestObj);
            string responseString = await PostMethodAsync(url, payload, CONTENTTYPE_JSON);
            if (String.IsNullOrEmpty(responseString)) return null;
            var response = JsonConvert.DeserializeObject<OutboundSMSResponseWrapper>(responseString);
            
            return response;
        }

        public async Task<DeliveryInfoListWrapper> GetSMSSendStatusAsync(string url, string accessToken)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", accessToken);

            string responseString = await GetMethodAsync(url);
            if (String.IsNullOrEmpty(responseString)) return null;
            var response = JsonConvert.DeserializeObject<DeliveryInfoListWrapper>(responseString);
            
            return response;
        }

        public async Task<InboundSmsMessageListWrapper> ReceiveSMSAsync(string url, string accessToken)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", accessToken);

            string responseString = await GetMethodAsync(url);
            if (String.IsNullOrEmpty(responseString)) return null;
            var response = JsonConvert.DeserializeObject<InboundSmsMessageListWrapper>(responseString);
            
            return response;
        }

        public async Task<AccessTokenResponse> GetNewSMSClientToken(string url, string appKey, string appSecret,
            string appScope)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string payload = $"client_id={appKey}&client_secret={appSecret}&grant_type=client_credentials&scope={appScope}";
            string responseString = await PostMethodAsync(url, payload, CONTENTTYPE_FORM);
            if (String.IsNullOrEmpty(responseString)) return null;
            var response = JsonConvert.DeserializeObject<AccessTokenResponse>(responseString);
            
            return response;
        }
        public async Task<AccessTokenResponse> RefreshSMSClientToken(string url, string appKey, string appSecret,
            string refreshToken)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string payload = $"client_id={appKey}&client_secret={appSecret}&grant_type=refresh_token&refresh_token={refreshToken}";
            string responseString = await PostMethodAsync(url, payload, CONTENTTYPE_FORM);
            if (String.IsNullOrEmpty(responseString)) return null;
            var response = JsonConvert.DeserializeObject<AccessTokenResponse>(responseString);
            
            return response;
        }
#endregion SMS helpers

#region Http method helpers
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
        
        private async Task<string> PostMethodAsync(string url, string payload, string contenttype)
        {
            string responseString = null;
            var content = new StringContent(payload, Encoding.UTF8, contenttype);
            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString; 
        }
        private async Task<bool> PutMethodAsync(string url, string payload, string contentType)
        {
            var content = new StringContent(payload, Encoding.UTF8, contentType);
            HttpResponseMessage response = await client.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }
    }
#endregion Http methods helpers
}
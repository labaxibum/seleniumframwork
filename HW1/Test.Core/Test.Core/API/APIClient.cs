using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.API
{
    public class APIClient
    {
        public readonly HttpClient httpClient;

        public readonly JsonSerializerSettings settings;

        public APIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            this.settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };
        }

        public APIClient()
        {
            this.httpClient = httpClient;

            this.settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };
        }

        public Uri BaseAddress
        {
            get { return httpClient.BaseAddress; }
            set { httpClient.BaseAddress = value; }
        }

        public TimeSpan Timeout
        {
            get { return httpClient.Timeout; }
            set { httpClient.Timeout = value; }
        }

        public void ClearHttpHeader(string headerName)
        {
            if (httpClient.DefaultRequestHeaders.Contains(headerName))
                httpClient.DefaultRequestHeaders.Remove(headerName);
        }

        public void SetHttpHeader(string headerName, string headerValue)
        {
            ClearHttpHeader(headerName);
            httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
        }
        public void SetAutherizationHeader(string authToken)
        {
            SetHttpHeader("Authorization", authToken);
        }
        public async Task<HttpResponseMessage> Get(string requestUri)
        {
            HttpResponseMessage response = await this.httpClient.GetAsync(requestUri);
            return response;
        }

        public async Task<T> Get<T>(string requestUri)
        {
            HttpResponseMessage respone = await this.httpClient.GetAsync(requestUri);
            string json = await respone.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public async Task<T> Post<T>(string requestUri, object request)
        {
            string json = await this.httpClient.PostAsync(requestUri, settings);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage respone = await httpClient.PostAsync(requestUri, content);
            var result = JsonConvert.DeserializeObject<T>(await respone.Content.ReadAsStringAsync(), settings);
            return result;
        }

        public async Task<T> Patch<T>(string requestUri, object request)
        {
            string json = JsonConvert.SerializeObject(request, this.settings);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            HttpResponseMessage respone = await httpClient.PatchAsync(requestUri, content);
            var result = JsonConvert.DeserializeObject<T>(await respone.Content.ReadAsStringAsync(), settings);
            return result;
        }
        private void RemoveHeader()
        {
            if (httpClient != null)
            {
                if (httpClient.DefaultRequestHeaders.Contains("auth"))
                    httpClient.DefaultRequestHeaders.Remove("auth");

                if (httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
        }

        public async Task<HttpResponseMessage> Post(string action, HttpContent content)
        {
            HttpResponseMessage response = await this.httpClient.PostAsync(action, content);
            return response;
        }

        public async Task<HttpResponseMessage> Patch(string action, HttpContent content)
        {
            HttpResponseMessage response = await this.httpClient.PatchAsync(action, content);
            return response;
        }

        public async Task<HttpResponseMessage> Delete(string requestUri)
        {
            HttpResponseMessage response = await this.httpClient.DeleteAsync(requestUri);
            return response;
        }

        public async Task<HttpResponseMessage> Send(string requestUri, HttpRequestMessage request)
        {
            request.RequestUri = new Uri($"{this.BaseAddress}{requestUri}");
            HttpResponseMessage response = await this.httpClient.SendAsync(request);
            return response;
        }
    }
}

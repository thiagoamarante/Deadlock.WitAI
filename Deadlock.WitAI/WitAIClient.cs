using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Deadlock.WitAI
{
    public class WitAIClient : IDisposable
    {
        #region Fields
        private string _AccessToken;
        private HttpClient _client;
        #endregion

        public WitAIClient(string accessToken)
        {
            this._AccessToken = accessToken;
            this._client = new HttpClient();
            this._client.BaseAddress = new Uri(UrlManager.Base);
            this._client.DefaultRequestHeaders.Accept.Clear();           
            this._client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
           
        }

        #region Methods Public
        public async Task<object> Message(string message)
        {
            try
            {
                HttpResponseMessage response = await this._client.GetAsync(UrlManager.Message(message));
                var isSuccessStatusCode = response.IsSuccessStatusCode;
                var statusCode = response.StatusCode;
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(content);
                //var data = JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings()
                //{
                //    NullValueHandling = NullValueHandling.Ignore
                //});
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        public async Task<Result<ConverseResponse>> Converse(string sessionId, string message, JObject context)
        {
            var content = context.ToString(Formatting.None);
            var jsonContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await this.Request<ConverseResponse>(UrlManager.Converse(sessionId, message), Method.Post, jsonContent);
        }

        public void Dispose()
        {
            this._client.Dispose();
        }
        #endregion

        #region Methods Private
        private async Task<Result<T>> Request<T>(string endpoint, Method method = Method.Get, HttpContent content = null)
        {           
            Result<T> result = Activator.CreateInstance<Result<T>>();
            try
            {
                HttpResponseMessage response = null;
                switch (method)
                {
                    case Method.Get:
                        response = await _client.GetAsync(endpoint);
                        break;
                    case Method.Post:
                        response = await _client.PostAsync(endpoint, content);
                        break;
                }

                result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                result.StatusCode = response.StatusCode;
                result.Content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result.Data = JsonConvert.DeserializeObject<T>(result.Content, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion
    }
}

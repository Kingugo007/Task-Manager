using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TaskManager.Core
{
    public class HttpCommandHandler : IHttpCommandHandler, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client = new HttpClient();

        public string baseUrl = "https://632b03111090510116ce8c28.mockapi.io/api/v1/Tasks";
        public HttpCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            //  _client.BaseAddress = new Uri(_configuration.GetSection(baseUrl).Value);
            _client.BaseAddress = new Uri(baseUrl);

        }

        #region Api Request handlers
        public async Task<TRes> PostRequest<TRes, TReq>(TReq requestModel, string requestUrl)
            where TRes : class
            where TReq : class
        {
            return await PostRequestAsync<TReq, TRes>(requestUrl, requestModel);
        }

        public async Task<TRes> GetRequest<TRes>(string requestUrl)
           where TRes : class
        {
            return await GetRequestAsync<TRes>(requestUrl);
        }
  
       public async Task<TRes> DeleteRequest<TRes>(string id)
          where TRes : class
        {
            return await DeleteRequestAsync<TRes>(id);
        }
        #endregion

        #region Implementatioin details
        private async Task<TRes> GetRequestAsync<TRes>(string requestUrl) where TRes : class
        {
            var client = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            return await GetResponseResultAsync<TRes>(client, request);
        }
   
        private async Task<TRes> DeleteRequestAsync<TRes>(string id) where TRes : class
        {
            var client = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, id.ToString());
            return await GetResponseResultAsync<TRes>(client, request);
        }

        private async Task<TRes> PostRequestAsync<TReq, TRes>(string requestUrl, TReq requestModel)
            where TReq : class
            where TRes : class
        {
            var caller = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")
            };
            return await GetResponseResultAsync<TRes>(caller, request);
        }

        private async Task<TRes> GetResponseResultAsync<TRes>(HttpClient clientCaller, HttpRequestMessage request) where TRes : class
        {
            var response = await clientCaller.SendAsync(request);
            if (!response.IsSuccessStatusCode) throw new ArgumentException(response.ReasonPhrase);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TRes>(responseString);
            return result;
        }

   
        private HttpClient CreateClient()
        {

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return _client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion
    }
}

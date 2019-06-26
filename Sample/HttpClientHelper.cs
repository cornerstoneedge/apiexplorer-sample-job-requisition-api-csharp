using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Collections.Specialized;
using System.Threading;

namespace Sample
{
    public class HttpClientHelper
    {
        private HttpClient _client;
        private HttpClientParameters _httpClientParameters;
        private string _result;
        private bool _success;

        public string Result { get { return _result; } }

        public bool Success { get { return _success; } }
        public HttpClientHelper(HttpClientParameters httpClientParameters)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
                                                   | SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
            _httpClientParameters = httpClientParameters;
        }

        public HttpClientParameters HttpClientParameters { get { return _httpClientParameters; } }

       
        public async Task<bool> CallService()
        {

            if (HttpClientParameters.EndPoint.IsNullOrBlank())
            {
                throw new Exception("ServiceURL Cannot be blank");
            }

            if (HttpClientParameters.Method.IsNullOrBlank())
            {
                throw new Exception("Method Cannot be blank");
            }

            _client = new HttpClient
            {
                BaseAddress = new Uri(HttpClientParameters.EndPoint),
                // Set timeout to infinite because Edge API service has its own timeout.
                Timeout = Timeout.InfiniteTimeSpan,
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(HttpClientParameters.EndPoint),
                Method = GetVerb(HttpClientParameters.Method),
                Content = HttpClientParameters.Method == "GET" ? null : new StringContent(HttpClientParameters.Body, HttpClientParameters.EncodingType, HttpClientParameters.ContentType)
            };

            AddHeadersToRequest(HttpClientParameters.Headers, request);

            var response = await _client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                _result = content;
                _success = true;
                return Success;
            }
            _result = $"Service Call failed with '{(int)response.StatusCode}' '{response.ReasonPhrase}'  '{response.Content.ReadAsStringAsync().Result}'.";
            _success = false;
            Console.WriteLine(Result);
            throw new Exception(Result);
        }


        private void AddHeadersToRequest(NameValueCollection headers, HttpRequestMessage request)
        {
            foreach (var key in headers.AllKeys)
            {
                request.Headers.Add(key, headers[key]);
            }
        }

        private HttpMethod GetVerb(string Method)
        {
            HttpMethod httpMethod = HttpMethod.Get;
            Method = Method.ToUpper();
            switch (Method)
            {
                case "GET":
                    httpMethod = HttpMethod.Get;
                    break;
                case "POST":
                    httpMethod = HttpMethod.Post;
                    break;
                case "PUT":
                    httpMethod = HttpMethod.Put;
                    break;
                case "PATCH":
                    httpMethod = new HttpMethod("PATCH");
                    break;
                case "DELETE":
                    httpMethod = HttpMethod.Delete;
                    break;

            }
            return httpMethod;
        }
    }
}

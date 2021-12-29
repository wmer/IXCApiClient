using ManyHelpers.Strings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IXCApiClient.Helpers {
    public class ApiConsumerHelper {
        private HttpClient _client;
        private string _baseAdress;
        private string _token;

        public ApiConsumerHelper(string baseAdress, string token) {
            _baseAdress = baseAdress;
            _token = StringHelper.Base64Encode(token);

            var handler = new HttpClientHandler {
                ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, policyErrors) => true
            };

            _client = new HttpClient(handler);
            _client.Timeout = TimeSpan.FromMinutes(30);
        }

        public (T result, string statusCode, string message) Get<T>(string endPoint, Dictionary<string, string> parameters) {
            return GetAsync<T>(endPoint, parameters).Result;
        }

        public (T result, string statusCode, string message) Post<T>(string endPoint, Dictionary<string, string> parameters) {
            return PostAsync<T>(endPoint, parameters).Result;
        }

        public async Task<(T result, string statusCode, string message)> GetAsync<T>(string endPoint, Dictionary<string, string> parameters) {
            using var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_baseAdress}{endPoint}");
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {_token}");
            request.Headers.TryAddWithoutValidation("ixcsoft", "listar");
            var call = JsonConvert.SerializeObject(parameters, Formatting.Indented);
            request.Content = new StringContent(call);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await _client.SendAsync(request);

            return DeserializeResponse<T>(response);
        }

        public async Task<(T result, string statusCode, string message)> PostAsync<T>(string endPoint, Dictionary<string, string> parameters) {
            using var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_baseAdress}{endPoint}");
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {_token}");
            request.Headers.TryAddWithoutValidation("ixcsoft", "inserir");

            request.Content = new StringContent(JsonConvert.SerializeObject(parameters, Formatting.Indented));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await _client.SendAsync(request);

            return DeserializeResponse<T>(response);
        }



        public (T result, string statusCode, string message) DeserializeResponse<T>(HttpResponseMessage response) {
            var responseContent = response.Content.ReadAsStringAsync().Result;
           // System.IO.File.WriteAllText("C:\\Users\\Administrator\\Desktop\\call.txt", responseContent);

            var statusCode = response.StatusCode.ToString();
            try {
                if (response.IsSuccessStatusCode) {
                    if (typeof(T).IsPrimitive || typeof(T) == typeof(string) || typeof(T) == typeof(decimal)) {
                        return ((T)Convert.ChangeType(responseContent, typeof(T)), statusCode, responseContent); ;
                    }
                    return (JsonConvert.DeserializeObject<T>(responseContent), statusCode, responseContent);
                } else {
                    return (default(T), statusCode, responseContent);
                }
            } catch (Exception e) {
                return (default(T), statusCode, responseContent);
            }

        }
    }
}

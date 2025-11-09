using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BrandInspector.Models;
using System.Configuration;

namespace BrandInspector.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public ApiService()
        {
            _client = new HttpClient();
            _baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        }

        public async Task<AuthToken> LoginAsync(string username, string password)
        {
            var body = JsonConvert.SerializeObject(new { username, password });
            var response = await _client.PostAsync($"{_baseUrl}/auth/login",
                new StringContent(body, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthToken>(json);
        }

        public async Task<T> GetAuthorizedAsync<T>(string endpoint, string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{_baseUrl}{endpoint}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

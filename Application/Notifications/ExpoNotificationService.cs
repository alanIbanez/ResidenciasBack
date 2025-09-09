using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Application.Notifications
{
    public class ExpoNotificationService : IExpoNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;

        // CAMBIO: IConfiguration viene de Microsoft.Extensions.Configuration
        public ExpoNotificationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiBaseUrl = _configuration["Expo:ApiBaseUrl"] ?? "https://exp.host/--/api/v2/push/";
        }

        public async Task<ExpoNotificationResponse> SendPushNotificationAsync(ExpoNotificationRequest request)
        {
            try
            {
                var expoRequest = new
                {
                    to = request.To,
                    title = request.Title,
                    body = request.Body,
                    data = request.Data,
                    sound = request.Sound,
                    ttl = request.Ttl,
                    expiration = request.Expiration,
                    priority = request.Priority,
                    channelId = request.ChannelId
                };

                var json = JsonConvert.SerializeObject(expoRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var accessToken = _configuration["Expo:AccessToken"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                }

                var response = await _httpClient.PostAsync(_apiBaseUrl + "send", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var expoResponse = JsonConvert.DeserializeObject<ExpoResponse>(responseContent);

                    if (expoResponse?.Data?.Status == "ok")
                    {
                        return new ExpoNotificationResponse
                        {
                            Success = true,
                            TicketId = expoResponse.Data.Id
                        };
                    }
                    else
                    {
                        return new ExpoNotificationResponse
                        {
                            Success = false,
                            Message = expoResponse?.Data?.Message ?? "Unknown Expo error"
                        };
                    }
                }
                else
                {
                    return new ExpoNotificationResponse
                    {
                        Success = false,
                        Message = $"HTTP error: {response.StatusCode} - {responseContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ExpoNotificationResponse
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<bool> ValidateExpoTokenAsync(string token)
        {
            try
            {
                return !string.IsNullOrEmpty(token) &&
                       token.StartsWith("ExponentPushToken[") &&
                       token.EndsWith("]");
            }
            catch
            {
                return false;
            }
        }
    }

    // Clases internas para deserialización
    internal class ExpoResponse
    {
        [JsonProperty("data")]
        public ExpoResponseData Data { get; set; }
    }

    internal class ExpoResponseData
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
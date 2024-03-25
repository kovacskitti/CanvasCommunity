using System.Text.Json;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CanvasCommunity.Services;

public class ArtsyTokenManager : IArtsyTokenManager
{
    private readonly HttpClient _httpClient;
    private string _currentToken;
    private DateTime _expiryTime;
    private readonly ILogger<ArtsyTokenManager> _logger;
    private readonly IConfiguration _configuration;

    public ArtsyTokenManager(HttpClient httpClient, ILogger<ArtsyTokenManager> logger,IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;

    }

    public async Task<string> GetTokenFromArtsyAsync()
    {
        if (DateTime.UtcNow >= _expiryTime)
        {
            var tokenData = await FetchNewTokenFromArtsyAsync();
            Console.WriteLine(tokenData);
            _currentToken = tokenData.xapp_token;
            _expiryTime = tokenData.expires_in;
        };
        return _currentToken;
    }

    public async Task<ArtsyTokenResponse> FetchNewTokenFromArtsyAsync()
    {   
       string clientId = _configuration["CLIENT_ID"];
       string clientSecret = _configuration["CLIENT_SECRET"];
      
       var url = $"https://api.artsy.net/api/v1/xapp_token?client_id={clientId}&client_secret={clientSecret}";
        try
        {

            var client = new HttpClient();

            _logger.LogInformation($"Calling OpenWeather API with url: {url}", url);
            Console.WriteLine("vmi");
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var tokenData = JsonSerializer.Deserialize<ArtsyTokenResponse>(json);
                Console.WriteLine($"ez itt a json:{json}");
                return (tokenData);
            }
            else
            {
                _logger.LogError($"Error response from Artsy API. Status code: {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in calling OpenWeather API with url: {url}");
        }

        return null;
    }
}
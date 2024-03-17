using CanvasCommunity.Services;
using Microsoft.AspNetCore.Mvc;

namespace CanvasCommunity;


[ApiController]
[Route("[controller]")]
public class ArtistController : ControllerBase
{
    private readonly ILogger<Painting> _logger;
    private readonly IArtsyTokenManager _artsyTokenManager;
    private readonly IHttpClientFactory _httpClientFactory;

    public ArtistController(ILogger<Painting> logger, IArtsyTokenManager artsyTokenManager, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _artsyTokenManager = artsyTokenManager;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("GetArtist")]
    public async Task<ActionResult<string>> GetArtist(string artistName)
    {
        var url = $"https://api.artsy.net/api/artists/{artistName}";
        try
        {
            var xappToken = await _artsyTokenManager.GetTokenFromArtsyAsync();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-XAPP-Token", xappToken);
            _logger.LogInformation($"Calling Artsy API with url: {url}", url);
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in Artsy OpenWeather API with url: {url}");
        }

        return null;
    }
}
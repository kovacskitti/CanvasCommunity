using CanvasCommunity.Services;
using Microsoft.AspNetCore.Mvc;

namespace CanvasCommunity;


[ApiController]
[Route("[controller]")]
public class PaintingController : ControllerBase
{
    private readonly ILogger<Painting> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IArtsyTokenManager _artsyTokenManager;

    public PaintingController(ILogger<Painting> logger, IHttpClientFactory clientFactory, IArtsyTokenManager artsyTokenManager)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _artsyTokenManager = artsyTokenManager;
    }

    [HttpGet("GetPainting")]
    public async Task<ActionResult<string>> GetPainting(string artistId)
    {
        var url = $"https://api.artsy.net/api/artists/{artistId}";
        try
        {

            var xappToken = await _artsyTokenManager.GetTokenFromArtsyAsync();
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-XAPP-Token", xappToken);
            
            _logger.LogInformation($"Calling Artsy API with url: {url}", url);
            
            var response = await client.GetAsync(url);
            
            
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in calling Artsy API with url: {url}");
        }

        return null;
    }
}
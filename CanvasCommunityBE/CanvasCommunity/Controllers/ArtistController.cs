using CanvasCommunity.Context;
using CanvasCommunity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CanvasCommunity;


[ApiController]
[Route("[controller]")]
public class ArtistController : ControllerBase
{
    private readonly ILogger<Painting> _logger;
    private readonly IArtsyTokenManager _artsyTokenManager;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AppDbContext _dbContext;

    public ArtistController(ILogger<Painting> logger, IArtsyTokenManager artsyTokenManager, IHttpClientFactory httpClientFactory, AppDbContext dbContext)
    {
        _logger = logger;
        _artsyTokenManager = artsyTokenManager;
        _httpClientFactory = httpClientFactory;
        _dbContext = dbContext;
    }

    [HttpGet("GetArtistByName")]
    public async Task<ActionResult<string>> GetArtistByName(string artistName)
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
    
    [HttpGet("GetArtistById")]
    public async Task<ActionResult<string>> GetArtistById(string artistId)
    {
        var url = $"https://api.artsy.net/api/artists/{artistId}";
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
            _logger.LogError(e, $"Error in calling Artsy API with url: {url}");
        }

        return null;
    }
    
    [HttpGet("GetPaintingsByArtistId")]
    public async Task<ActionResult<string>> GetPaintingsByArtistId(string artistId)
    {
        var url = $"https://api.artsy.net/api/artworks?artist_id={artistId}";
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
            _logger.LogError(e, $"Error in calling Artsy API with url: {url}");
        }

        return null;
    }
    
    [HttpGet("TestAddDB")]
    public async Task<ActionResult<Artist>> TestAddDB(string artistName)
    {
        
        try
        {
            Artist testArtist = new Artist()
            {
                Name = artistName
            };
            _dbContext.Artists.Add(testArtist);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine(testArtist.Id);
            return testArtist;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in test");
        }

        return null;
    }
    
}
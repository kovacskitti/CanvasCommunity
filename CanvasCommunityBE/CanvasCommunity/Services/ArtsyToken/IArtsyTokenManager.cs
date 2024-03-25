namespace CanvasCommunity.Services;

public interface IArtsyTokenManager
{
   Task<string> GetTokenFromArtsyAsync();

   Task<ArtsyTokenResponse> FetchNewTokenFromArtsyAsync();
}
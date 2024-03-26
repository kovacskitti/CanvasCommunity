using Microsoft.AspNetCore.Identity;

namespace CanvasCommunity.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user);
}
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace CanvasCommunity.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;
    

    public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResult> RegisterAsync(string email, string username, string password)
    {
        var result = await _userManager.CreateAsync(new IdentityUser { UserName = username, Email = email }, password);

        if (!result.Succeeded)
        {
            return FailedRegistration(result, email, username);
        }

        return new AuthResult(true, email, username, "");
    }

    private static AuthResult FailedRegistration(IdentityResult identityResult, string email, string username)
    {
        var authResult = new AuthResult(false, email, username, "");
        foreach (var error in identityResult.Errors)
        {
            authResult.ErrorMessages.Add(error.Code, error.Description);
        }

        return authResult;

    }
    
    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var managedUser = await _userManager.FindByEmailAsync(email);

        if (managedUser == null)
        {
            return InvalidInput(email);
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
        if (!isPasswordValid)
        {
            return InvalidInput(email);
        }

        var accessToken = _tokenService.CreateToken(managedUser);

        return new AuthResult(true, managedUser.Email, managedUser.UserName, accessToken);
    }

    private static AuthResult InvalidInput(string email)
    {
        var result = new AuthResult(false, email, "", "");
        result.ErrorMessages.Add("Bad credentials","Invalid data");
        return result;
    }
}
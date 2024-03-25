using CanvasCommunity.Contracts;
using CanvasCommunity.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CanvasCommunity;

[ApiController]
[Route ("[controller]")]
public class AuthController : ControllerBase
{
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
                _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
                if (!ModelState.IsValid)
                {
                        return BadRequest(ModelState);
                }

                var result = await _authService.RegisterAsync(request.Email, request.Username, request.Password);

                if (!result.Success)
                {
                        AddErrors(result);
                        return BadRequest(ModelState);
                }

                return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
        }
        
        private void AddErrors(Authresult result)
        {
                foreach (var error in result.ErrorMessages)
                {
                        ModelState.AddModelError(error.Key,error.Value);
                }
        }
}
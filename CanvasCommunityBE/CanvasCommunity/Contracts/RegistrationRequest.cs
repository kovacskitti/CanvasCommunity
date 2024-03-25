using System.ComponentModel.DataAnnotations;

namespace CanvasCommunity.Contracts;

public record RegistrationRequest(
    [Required]string Email, 
    [Required]string Username, 
    [Required]string Password);
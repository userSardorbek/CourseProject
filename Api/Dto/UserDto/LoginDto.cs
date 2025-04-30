using System.ComponentModel.DataAnnotations;

namespace Api.Dto.UserDto;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
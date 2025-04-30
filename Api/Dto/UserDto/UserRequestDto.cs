using System.ComponentModel.DataAnnotations;

namespace Api.Dto.UserDto;

public class UserRequestDto
{
    [Required]
    public string Username { get; set; }
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
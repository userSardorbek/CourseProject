using System.ComponentModel.DataAnnotations;

namespace Api.Dto.UserDto;

public class UpdateUserDataDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
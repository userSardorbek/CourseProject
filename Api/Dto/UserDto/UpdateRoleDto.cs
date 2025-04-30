using System.ComponentModel.DataAnnotations;

namespace Api.Dto.UserDto;

public class UpdateRoleDto
{
    [Required]
    public string username { get; set; }
    [Required]
    public string[] Roles { get; set; }
}
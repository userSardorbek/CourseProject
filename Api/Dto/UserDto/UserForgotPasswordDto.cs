namespace Api.Dto.UserDto;

public class UserForgotPasswordDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
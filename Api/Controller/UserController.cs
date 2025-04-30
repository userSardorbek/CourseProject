using Api.Dto;
using Api.Dto.UserDto;
using Api.Interfaces;
using Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controller;

[Route("api/account")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;

    public UserController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }
    
    [HttpPost("login")]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnUserDto>))]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnUserDto>("Your request model is invalid"));

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
        if (user == null) return Unauthorized("Invalid username!");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new ApiResponse<ReturnUserDto>()
        {
            Success = true, Data = new ReturnUserDto()
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user, roles)
            }
        });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequestDto userRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnUserDto>("Your request model is invalid"));
        try
        {
            var appUser = new User()
            {
               UserName = userRequestDto.Username,
               Email = userRequestDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, userRequestDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(new ApiResponse<ReturnUserDto>(new ReturnUserDto()
                    {
                        Username = appUser.UserName,
                        Email = appUser.Email
                    }));
                }

                return StatusCode(400, new ApiResponse<IEnumerable<IdentityError>>()
                        { Data = roleResult.Errors, Success = false, Error = "Check data object" });
            }

            return StatusCode(400,
                new ApiResponse<IEnumerable<IdentityError>>()
                    { Data = createdUser.Errors, Success = false, Error = "Check data object" });
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}
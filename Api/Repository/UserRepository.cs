using Api.Interfaces;
using Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IList<string>?> GetRolesByUsername(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return null; 
        }

        var roles = await _userManager.GetRolesAsync(user);
        return roles;
    }
}
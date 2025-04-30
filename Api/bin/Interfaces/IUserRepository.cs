using CourseProject.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseProject;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid userId);
    Task<IdentityResult> CreateUserAsync(string username, string email, string password);
    Task<IdentityResult> UpdateUserAsync(Guid userId, string username, string email, bool isBlocked);
    Task<IdentityResult> BlockUserAsync(Guid userId);
    Task<IdentityResult> UnblockUserAsync(Guid userId);
    Task<IdentityResult> DeleteUserAsync(Guid userId);
    Task<List<IdentityRole<Guid>>> GetAllRolesAsync();
    Task<IdentityResult> AddUserToAdminRoleAsync(Guid userId);
    Task<IdentityResult> RemoveUserFromAdminRoleAsync(Guid userId);
}
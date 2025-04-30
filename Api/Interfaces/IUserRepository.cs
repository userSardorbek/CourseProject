using Api.Model;

namespace Api.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsername(string username);
    Task<IList<string>?> GetRolesByUsername(string username);
}
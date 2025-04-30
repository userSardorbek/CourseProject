using Api.Model;

namespace Api.Interfaces;

public interface ITokenService
{
    string CreateToken(User user, IList<string> roles);
}
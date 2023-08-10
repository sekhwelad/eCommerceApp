using eShopper.Core.Entities.Identity;

namespace eShopper.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

using VenusOnCrisis.Entities;

namespace VenusOnCrisis.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(User user);
    }
}
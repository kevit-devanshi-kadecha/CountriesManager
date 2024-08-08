using CountriesManager.Core.DTO;
using CountriesManager.Core.Identity;
using System;

namespace CountriesManager.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}

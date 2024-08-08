using Microsoft.AspNetCore.Identity;

namespace CountriesManager.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;


namespace CarRent.DataAccessLayer.Model
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace Infraestructure.CustomEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}

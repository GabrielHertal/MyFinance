using Microsoft.AspNetCore.Identity;

namespace MyFinance.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
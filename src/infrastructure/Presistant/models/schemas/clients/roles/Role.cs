using Microsoft.AspNetCore.Identity;
namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.roles;
internal class Role : IdentityRole<long>
{
    public string PersianName { get; protected set; }
}

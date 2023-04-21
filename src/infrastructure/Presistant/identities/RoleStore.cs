using DomainDrivenDesignArchitucture.Domain.Contracts.repositories;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.sharedDatas;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.roles;
using Microsoft.AspNetCore.Identity;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.identities;

internal class RoleStore : IRoleStore
{
    private readonly RoleManager<Role> _roleManager;

    public RoleStore(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Create(string name, string persianName)
    {
        var role = await _roleManager.FindByNameAsync(name);

        if (!string.IsNullOrEmpty(role.Name))
        {
            return;
        }

        role = new Role()
        {
            ConcurrencyStamp = DateTime.UtcNow.Ticks.ToString(),
            Name = name,
            PersianName = persianName,
            NormalizedName = name.ToLower()
        };

        await _roleManager.CreateAsync(role);
    }

    public async Task<ListResult<Role>> GetAll()
    {
        var roles = await _roleManager.Roles.ToListAsync();

        return new ListResult<Role>(roles.Count(), roles);
    }

    public async Task<string?> Get(string id)
    {

        var role = await _roleManager.FindByIdAsync(id);
        return role?.Name;
    }

    public async Task<long?> GetByName(string name)
    {
        var role = await _roleManager.FindByNameAsync(name);
        return role?.Id;
    }

}

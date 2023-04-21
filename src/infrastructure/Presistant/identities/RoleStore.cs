using DomainDrivenDesignArchitucture.Domain.Contracts.repositories;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.sharedDatas;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.identities;

internal class RoleStore : IRoleStore
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RoleStore(RoleManager<Role> roleManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
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

    public async Task<bool> IsRoleWithSpecificNameExists(string name)
        => await _roleManager.Roles.AnyAsync(x => x.Name.ToLower() == name.ToLower());

    public async Task<IList<string>> GetCurrentPermissions()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers.Authorization;

        if(!string.IsNullOrEmpty(authorizationHeader))
        {
            var token = authorizationHeader.ToString().Split(' ')[0];

            var tokenValue = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var roleName = tokenValue.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            var role = await _roleManager.FindByNameAsync(roleName);
            var permissions = await _roleManager.GetClaimsAsync(role);

            return permissions.Select(x => x.Value).ToList();
        }

        return null;
    }

    public void Dispose() { }
}

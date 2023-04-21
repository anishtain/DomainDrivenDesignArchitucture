using DomainDrivenDesignArchitucture.Domain.Contracts.repositories;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.identities;

internal class UserStore : IUserStore
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IOptions<commons.sharedDatas.TokenConfig> _appSettings;

    public UserStore(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<commons.sharedDatas.TokenConfig> appSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _appSettings = appSettings;
    }

    public async Task<string> PasswordLogin(string username, string password)
    {
        var siginResult = await _signInManager.PasswordSignInAsync(username, password, false, false);

        if(siginResult.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(username);

            var role = await _userManager.GetRolesAsync(user);

            return GenerateToken(user.Id, role.First());
        }

        return null;
    }

    public async Task RegisterUser(string username, string password)
    {
        var user = new User()
        {
            UserName = username,
            ConcurrencyStamp = DateTime.Now.Ticks.ToString(),
            NormalizedUserName = username.ToLower(),
            SecurityStamp = DateTime.Now.Ticks.ToString()
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);

        await _userManager.CreateAsync(user);
    }

    private string GenerateToken<T,U>(T userId, U roleId)
    {
        commons.sharedDatas.TokenConfig securityModel = _appSettings.Value;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityModel.Secret));
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, roleId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }, "CookieAuth");

        var claimPrincipal = new ClaimsPrincipal(claims);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = securityModel.Issuer,
            Audience = securityModel.Audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

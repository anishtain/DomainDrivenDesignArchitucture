namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.sharedDatas;

internal class TokenConfig
{
    public string? Issuer { get; set; }

    public string? Audience { get; set; }

    public string? Secret { get; set; }
}

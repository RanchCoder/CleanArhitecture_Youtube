namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenSettings{
    public const string SectionName ="JwtSettings";
    public string Issuer {get;init;} = null!;
    public string ExpiryMinutes {get;init;} = null!;
    public string Audience {get;init;}= null!;
    public string Secret{get;init;}= null!;
        
}
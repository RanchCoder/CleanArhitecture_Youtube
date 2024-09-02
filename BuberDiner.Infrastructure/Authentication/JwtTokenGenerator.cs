using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDiner.Domain.Entities;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
 {

  private readonly IDateTimeProvider _dateTimeProvider;
  private readonly JwtTokenSettings _jwtTokenSettings;
  public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtTokenSettings> jwtTokenSettings){
    _dateTimeProvider = dateTimeProvider;
    _jwtTokenSettings = jwtTokenSettings.Value;
  }
   public string GenerateToken(User user){

   var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-of-webapi-HMAC-256")), SecurityAlgorithms.HmacSha256) ;
    var claims = new []{
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
         new Claim(JwtRegisteredClaimNames.GivenName,    user.FirstName),
         new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())        
         
    };


    var securityToken = new JwtSecurityToken(
      issuer: "BuberDinner",
      audience:"BuberDinner",
      expires: _dateTimeProvider.UTCNow.AddMinutes(60),
      claims : claims,
      signingCredentials: signingCredentials
    );

     return new JwtSecurityTokenHandler().WriteToken(securityToken);
   }

    
}
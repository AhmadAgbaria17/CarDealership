using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnetbackend.IServices;
using dotnetbackend.models;
using Microsoft.IdentityModel.Tokens;

namespace dotnetbackend.Services
{
  public class TokenService : ITokenService
  {
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration configuration)
    {
      _configuration = configuration;
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
    }
    public string CreateToken(Person person)
    {
      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.Email, person.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, person.UserName),
      };

      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = creds,
        Issuer = _configuration["JWT:Issuer"],
        Audience = _configuration["JWT:Audience"]
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token); 
    }
  }
}
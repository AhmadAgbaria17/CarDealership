using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnetbackend.Extensions
{
  public static class ClaimsExtensions
  {
    public static string GetUserName(this ClaimsPrincipal user)
{
    var claim = user?.Claims?.SingleOrDefault(c => 
        c.Type == ClaimTypes.GivenName || 
        c.Type == "https://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");

    return claim?.Value;
}

  }
}
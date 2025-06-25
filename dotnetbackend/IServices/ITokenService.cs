using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.models;

namespace dotnetbackend.IServices
{
  public interface ITokenService
  {
      string CreateToken(Person person);
        
    }
}
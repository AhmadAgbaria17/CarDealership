using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbackend.models
{
    public class Person
    {
      public int Id { get; set; }

      public String Email { get; set; } = string.Empty;
      public String Password { get; set; } = string.Empty;
      public String FirstName { get; set; } = string.Empty;
      public String LastName { get; set; } = string.Empty;
      public String Phone { get; set; } = string.Empty;
      public List<Car> LikedCars { get; set; } = new List<Car>();

    }
}

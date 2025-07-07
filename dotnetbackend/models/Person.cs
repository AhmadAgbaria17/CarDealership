using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace dotnetbackend.models
{
  [Table("Persons")]
  public class Person : IdentityUser
  {
   
    // Many to Many relationship with Car
    public List<LikedCar> LikedCar { get; set; } = new List<LikedCar>();


  }
}

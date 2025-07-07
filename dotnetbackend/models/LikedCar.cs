using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetbackend.models
{
    [Table("LikedCars")]
    public class LikedCar
    {
      public String PersonId { get; set; }
      public int CarId { get; set; }
      public Person Person { get; set; }      
      public Car Car { get; set; }
    }
}
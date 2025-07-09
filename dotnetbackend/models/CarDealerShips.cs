using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace dotnetbackend.models
{
  [Table("CarDealerShips")]
  public class CarDealerShips
  {
    public int Id { get; set; }
    public String Name { get; set; } = string.Empty;
    public String City { get; set; } = string.Empty;
    public String Address { get; set; } = string.Empty;
    public int[] Coordinates { get; set; } = new int[2];
    public String Phone { get; set; } = string.Empty;
    public List<Car> Cars { get; set; } 

    public String PersonId { get; set; } 
    public Person Person { get; set; }


  }
}
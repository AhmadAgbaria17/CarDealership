using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnetbackend.models
{
  public class CarDealerShips
  {
    public int Id { get; set; }
    public String Name { get; set; } = string.Empty;
    public String City { get; set; } = string.Empty;
    public String Address { get; set; } = string.Empty;
    public int[] Coordinates { get; set; } = new int[2];
    public String Phone { get; set; } = string.Empty;
    public List<Car> Cars { get; set; } = new List<Car>();

  }
}
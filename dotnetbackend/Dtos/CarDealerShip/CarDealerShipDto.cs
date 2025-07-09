using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Car;

namespace dotnetbackend.Dtos.CarDealerShip
{
  public class CarDealerShipDto
  {
    public int Id { get; set; }
    public String Name { get; set; } = string.Empty;
    public String City { get; set; } = string.Empty;
    public String Address { get; set; } = string.Empty;
    public int[] Coordinates { get; set; } = new int[2];
    public String Phone { get; set; } = string.Empty;

    public String CreatedBy { get; set; } = string.Empty;
    public List<CarDto> Cars { get; set; } = new List<CarDto>();



    }
}
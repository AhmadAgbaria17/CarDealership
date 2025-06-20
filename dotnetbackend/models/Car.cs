using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbackend.models
{
    public class Car
    {
        public int Id { get; set; }
        public string Company { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Fuel { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public string Mileage { get; set; } = string.Empty;
        public string Engine { get; set; } = string.Empty;
        public string HorsePower { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public int? CarDealerShipId { get; set; } //Navigation property
        public CarDealerShips? CarDealerShip { get; set; }

        public int? PersonId { get; set; } //Navigation propety 
        public Person? Person { get; set; }


    }
}

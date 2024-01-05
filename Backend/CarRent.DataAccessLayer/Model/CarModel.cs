using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.DataAccessLayer.Model
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        public string Maker { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public Decimal Cost { get; set; }

        [Required]
        public Decimal Mileage { get; set; }

        public string Availability { get; set; } = "Available";
    }
}

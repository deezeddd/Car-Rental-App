using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.DataAccessLayer.Model
{
    public class RentalAgreementModel
    {
        [Key]
        public int AgreementId { get; set; }

        public int CarId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Maker { get; set; }

        [Required]
        public Decimal RentalCost { get; set; }

        [Required]
        public Decimal TotalCost { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Requested { get; set; } = "Not Requested";

    }
}

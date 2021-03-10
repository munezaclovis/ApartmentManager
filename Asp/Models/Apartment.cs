using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Models
{
    public class Apartment
    {
        [Required]
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { set; get; } = Guid.NewGuid().ToString();

        [Required]
        public string Number { set; get; }

        [Required]
        public string Floor { set; get; }

        [Required]
        public string Bedrooms { set; get; }

        [Required]
        public string Bathrooms { set; get; }

        [Required]
        public string BuildingId { set; get; }

        [Required]
        public string Status { set; get; } = "A";

        public virtual Building Building { get; set; }
    }
}

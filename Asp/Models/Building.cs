using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Models
{
    public class Building
    {
        [Required]
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { set; get; } = Guid.NewGuid().ToString();

        [Required]
        public string Address { set; get; }

        [Required]
        public string City { set; get; }

        [Required]
        public string State { set; get; }

        [Required]
        public string Country { set; get; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string PostalCode { set; get; }

        [Required]
        [DefaultValue("A")]
        public string Status { set; get; } = "A";

        [Required]
        public string ManagerId { set; get; }


        public virtual User Manager { set; get; }
        public ICollection<Apartment> Apartments { set; get; }

        public string getFullAddress()
        {
            return this.Address + ", " + this.City + ", " + this.State + ", \n " + this.Country + ", " + this.PostalCode;
        }
    }
}

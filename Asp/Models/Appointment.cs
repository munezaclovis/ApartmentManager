using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Models
{
    public class Appointment
    {
        [Required]
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { set; get; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime Date { set; get; }

        [Required]
        public string TenantId { set; get; }

        [Required]
        public string ManagerId { set; get; }
    }


}

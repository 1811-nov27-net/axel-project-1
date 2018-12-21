using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PizzaStore.Models
{
    public class UserLocation
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(128)]
        public string Address { get; set; }
        [Required]
        [StringLength(128)]
        public string State { get; set; }
        public List<Orders> Orders { get; set; } = new List<Orders>();
    }
}

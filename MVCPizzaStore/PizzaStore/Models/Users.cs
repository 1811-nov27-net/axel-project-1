using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Users
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }

        public List<UserLocation> UserLocations { get; set; } = new List<UserLocation>();
    }
}

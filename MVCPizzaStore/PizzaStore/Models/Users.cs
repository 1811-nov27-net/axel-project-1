using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Users
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

      //  public IEnumerable<UserLocation> UserLocations { get; set; }
    }
}

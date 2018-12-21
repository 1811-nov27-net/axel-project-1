using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.DataAccess
{
    public partial class Users
    {
        public Users()
        {
            UserLocation = new HashSet<UserLocation>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        public virtual ICollection<UserLocation> UserLocation { get; set; }
    }
}

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
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<UserLocation> UserLocation { get; set; }
    }
}

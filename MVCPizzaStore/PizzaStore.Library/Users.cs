using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    public class Users
    {
        private int _id;
        private string _firstname;
        private string _lastname;

        public int id { get; set; }

        public string FirstName
        {
            get => _firstname;

            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("First name must not be empty.", nameof(value));
                }
                _firstname = value;
            }

        }
        public string LastName
        {
            get => _lastname;

            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("last name must not be empty.", nameof(value));
                }
                _lastname = value;
            }

        }
        public List<UserLocation> defaultLocation { get; set; } = new List<UserLocation>();

    }
}

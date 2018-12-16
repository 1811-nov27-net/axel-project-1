using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    public class UserLocation
    {
        public int id { get; set; }

        private string _userAddress;
        private string _state;

        public List<Orders> Orders { get; set; }

        public string UserAddress
        {
            get => _userAddress;

             set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("First name must not be empty.", nameof(value));
                }
                _userAddress = value;
            }
        }
        public string State

        {
            get => _state;

            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("First name must not be empty.", nameof(value));
                }
                _state = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace PizzaStore.DataAccess
{
    public interface IPizzaStoreRepository
    {
        IEnumerable<Users> GetUsers(string search);
        IEnumerable<Users> GetAllUsersDefaultLocation();
    }
}

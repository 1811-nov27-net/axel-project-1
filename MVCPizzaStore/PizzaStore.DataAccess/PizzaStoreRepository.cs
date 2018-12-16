using PizzaStore.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PizzaStore.DataAccess
{
    public class PizzaStoreRepository : IPizzaStoreRepository
    {
        private readonly PizzaStoreDBContext _db;

        /// <summary>
        /// Initializes a new restaurant repository given a suitable Entity Framework DbContext.
        /// </summary>
        /// <param name="db">The DbContext</param>
        public PizzaStoreRepository(PizzaStoreDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Users> GetAllUsersDefaultLocation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> GetUsers(string search)
        {
            return _db.Users.Where(u => u.LastName.Contains(search) || u.FirstName.Contains(search));
        }
    }
}

using PizzaStore.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PizzaStore.Models;
using PizzaStore.DataAccess;

namespace PizzaStore.Repos
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
        public List<Models.Pizza> GetAllPizzas()
        {
            return _db.Pizza.Include(p => p.Ingredients).Select(Map).ToList();
        }
        List<Models.Users> IPizzaStoreRepository.GetAllUsers()
        {
            return _db.Users.Include(p => p.UserLocation).Select(Map).ToList();
        }
        public List<Models.UserLocation> GetAllUserLocations()
        {
            return _db.UserLocation.Include(u => u.Orders).Select(Map).ToList();
        }
        public List<Models.PizzaOrder> GetAllPizzaOrders()
        {
            return _db.PizzaOrder.Include(u => u.Pizza).Select(Map).ToList();
        }
        public List<Models.Orders> GetAllOrders()
        {
            return _db.Orders.Include(u => u.PizzaOrder).Select(Map).ToList();
        }
        public List<Models.Store> GetAllStores()
        {
            return _db.Store.Include(s => s.Orders).Select(Map).ToList();
        }
        public List<Models.Ingredients> GetAllIngredients()
        {
            return _db.Ingredients.Select(Map).ToList();
        }
        public Models.Store GetStoreById(int id)
        {
            return Map(_db.Store
                    .Include(i => i.Ingredients)
                    .Include(o => o.Orders)
                    .AsNoTracking().First(r => r.Id == id));
        }

        public Models.Pizza GetByPizzaId(int id)
        {
            return Map(_db.Pizza.Include(r => r.Ingredients)
                 .AsNoTracking().First(r => r.Id == id));
        }
        public Models.Orders GetOrderById(int id)
        {
            return Map(_db.Orders.Include(r => r.PizzaOrder)
                .AsNoTracking().First(r => r.Id == id));
        }
         public Models.Ingredients GetIngredientsById(int id)
        {
            return Map(_db.Ingredients.AsNoTracking().First(r => r.Id == id));
        }

        public void AddUserLocation(Models.UserLocation userLocation)
        {
            _db.Add(Map(userLocation));
        }

        public void AddStores(Models.Store store)
        {
            _db.Add(Map(store));
        }

        public void AddIngredients(Models.Ingredients ingredients)
        {
            _db.Add(Map(ingredients));
        }

        public void AddOrders(Models.Orders orders)
        {
            _db.Add(Map(orders));
        }

        public void AddPizzaOrders(Models.PizzaOrder pizzaOrder)
        {
            _db.Add(Map(pizzaOrder));
        }
        public void AddUsers(Models.Users users)
        {
            _db.Add(Map(users));
        }
        public List<Models.Users> GetUsersBySearch(string search)
        {
            return _db.Users.Include(ul => ul.UserLocation).AsNoTracking()
                .Where(n => n.FirstName.Contains(search) || n.LastName.Contains(search)).Select(Map).ToList();
        }
       public Models.Users GetUsersById(int id)
        {
            return Map(_db.Users.Include(ul => ul.UserLocation).AsNoTracking().First(i => i.Id == id));
        }
        public Models.PizzaOrder GetPizzaOrderById(int id)
        {
            return Map(_db.PizzaOrder.Include(po => po.Pizza).AsNoTracking().First(i => i.Id == id));
        }
        public void AddPizza(Models.Pizza pizza, Models.PizzaOrder pizzaOrder)
        {
            if (pizzaOrder != null)
            {
                // get the db's version of that restaurant
                // (can't use Find with Include)
                DataAccess.PizzaOrder contextPizzaOrder = _db.PizzaOrder.Include(po => po.Pizza)
                    .First(po => po.Id == pizzaOrder.Id);
            //    pizzaOrder.Pizza.(pizza);
                contextPizzaOrder.Pizza.Add(Map(pizza));
            }
            else
            {
                _db.Add(Map(pizza));
            }
        }

        public void UpdateUsers(Models.Users users)
        {
            _db.Entry(_db.Users.Find(users.Id)).CurrentValues.SetValues(Map(users));
        }
        public void UpdateUserLocation(Models.UserLocation userLocation)
        {
            _db.Entry(_db.UserLocation.Find(userLocation.Id)).CurrentValues.SetValues(Map(userLocation));
        }

        public void UpdateStore(Models.Store store)
        {
            _db.Entry(_db.Store.Find(store.Id)).CurrentValues.SetValues(Map(store));
        }

        public void UpdateIngredients(Models.Ingredients ingredients)
        {
            _db.Entry(_db.Ingredients.Find(ingredients.Id)).CurrentValues.SetValues(Map(ingredients));
        }

        public void UpdatePizza(Models.Pizza pizza)
        {
            _db.Entry(_db.Pizza.Find(pizza.Id)).CurrentValues.SetValues(Map(pizza));
        }
        public void DeleteUsers(int id)
        {
            _db.Remove(_db.Users.Find(id));
        }

        public void DeleteUserLocation(int id)
        {
            _db.Remove(_db.UserLocation.Find(id));
        }

        public void DeleteStore(int id)
        {
            _db.Remove(_db.Store.Find(id));
        }

        public void DeleteIngredients(int id)
        {
            _db.Remove(_db.Ingredients.Find(id));
        }

        public void DeletePizza(int id)
        {
            _db.Remove(_db.Pizza.Find(id));
        }
        public static DataAccess.Users Map(Models.Users users) => new DataAccess.Users
        {
            Id = users.Id,
            FirstName = users.FirstName,
            LastName = users.LastName
        };
        public static DataAccess.UserLocation Map(Models.UserLocation userLocation) => new DataAccess.UserLocation
        {
            Id = userLocation.Id,
            Address = userLocation.Address,
            State = userLocation.State,
            Orders = Map(userLocation.Orders).ToList()
        };
        public static DataAccess.Store Map(Models.Store store) => new DataAccess.Store
        {
            Id = store.Id,
            Address = store.Address,
            State = store.State,
          //  Orders = Map(store.Orders).ToList(),
            Ingredients = Map(store.Ingredients).ToList()
        };
        public static DataAccess.Orders Map(Models.Orders orders) => new DataAccess.Orders
        {
            Id = orders.Id,
            TotalDue = orders.TotalDue,
            OrderTime = orders.OrderTime,
            PizzaOrder = Map(orders.PizzaOrder).ToList()
        };
        public static DataAccess.PizzaOrder Map(Models.PizzaOrder pizzaOrder) => new DataAccess.PizzaOrder
        {
            Id = pizzaOrder.Id,
            Quantity = pizzaOrder.Quantity,
            Pizza = Map(pizzaOrder.Pizza).ToList()
        };

        public static DataAccess.Pizza Map(Models.Pizza pizzas) => new DataAccess.Pizza
        {
            Id = pizzas.Id,
            Name = pizzas.Name,
            CrustType = pizzas.CrustType,
            LinePrice = pizzas.LinePrice,
            Ingredients = Map(pizzas.IngredientList).ToList()
        };
        public static DataAccess.Ingredients Map(Models.Ingredients ingredients) => new DataAccess.Ingredients
        {
            Id = ingredients.Id,
            Name = ingredients.Name,
            StockNumber = ingredients.StockNumber
        };

        public static Models.Users Map(DataAccess.Users users) => new Models.Users
        {
            Id = users.Id,
            FirstName = users.FirstName,
            LastName = users.LastName,
            UserLocations = Map(users.UserLocation).ToList()
        };
        public static Models.UserLocation Map(DataAccess.UserLocation userLocation) => new Models.UserLocation
        {
            Id = userLocation.Id,
            Address = userLocation.Address,
            State = userLocation.State,
            Orders = Map(userLocation.Orders).ToList()
        };
        public static Models.Store Map(DataAccess.Store store) => new Models.Store
        {
            Id = store.Id,
            Address = store.Address,
            State = store.State,
            Orders = Map(store.Orders).ToList(),
            Ingredients = Map(store.Ingredients).ToList()
        };
        public static Models.Orders Map(DataAccess.Orders orders) => new Models.Orders
        {
            Id = orders.Id,
            TotalDue = orders.TotalDue,
            OrderTime = orders.OrderTime,
            PizzaOrder = Map(orders.PizzaOrder).ToList()
        };
        public static Models.PizzaOrder Map(DataAccess.PizzaOrder pizzaOrder) => new Models.PizzaOrder
        {
            Id = pizzaOrder.Id,
            Quantity = pizzaOrder.Quantity,
            Pizza = Map(pizzaOrder.Pizza).ToList()
        };
        public static Models.Pizza Map(DataAccess.Pizza pizzas) => new Models.Pizza
        {
            Id = pizzas.Id,
            Name = pizzas.Name,
            CrustType = pizzas.CrustType,
            LinePrice = pizzas.LinePrice,
            IngredientList = Map(pizzas.Ingredients).ToList()
        };

        public static Models.Ingredients Map(DataAccess.Ingredients ingredients) => new Models.Ingredients
        {
            Id = ingredients.Id,
            Name = ingredients.Name,
            StockNumber = ingredients.StockNumber
        };

        public void AddPizzaToPizzaOrder(Models.Pizza pizza)
        {
            throw new NotImplementedException();
        }


        public void Save()
        {
            _db.SaveChanges();
        }

        public static IEnumerable<Models.Ingredients> Map(IEnumerable<DataAccess.Ingredients> ingredients) => ingredients.Select(Map);
        public static IEnumerable<Models.UserLocation> Map(IEnumerable<DataAccess.UserLocation> usersLocation) => usersLocation.Select(Map);
        public static IEnumerable<Models.Orders> Map(IEnumerable<DataAccess.Orders> orders) => orders.Select(Map);
        public static IEnumerable<Models.PizzaOrder> Map(IEnumerable<DataAccess.PizzaOrder> pizzaOrders) => pizzaOrders.Select(Map);
        public static IEnumerable<Models.Pizza> Map(IEnumerable<DataAccess.Pizza> pizzas) => pizzas.Select(Map);
        public static IEnumerable<DataAccess.PizzaOrder> Map(IEnumerable<Models.PizzaOrder> pizzaOrders) => pizzaOrders.Select(Map);
        public static IEnumerable<DataAccess.Pizza> Map(IEnumerable<Models.Pizza> pizzas) => pizzas.Select(Map);
        public static IEnumerable<DataAccess.Ingredients> Map(IEnumerable<Models.Ingredients> ingredients) => ingredients.Select(Map);
        public static IEnumerable<DataAccess.Orders> Map(IEnumerable<Models.Orders> orders) => orders.Select(Map);
        public static IEnumerable<DataAccess.UserLocation> Map(IEnumerable<Models.UserLocation> userLocations) => userLocations.Select(Map);       

       
    }

}


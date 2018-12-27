using System;
using System.Collections.Generic;
using PizzaStore.Models;

namespace PizzaStore.Repos
{
    public interface IPizzaStoreRepository
    {
        List<Users> GetUsersBySearch(string search);
        List<Users> GetAllUsers();
        List<Pizza> GetAllPizzas();
        List<Ingredients> GetAllIngredients();
        List<PizzaOrder> GetAllPizzaOrders();
        List<Store> GetAllStores();
        List<Orders> GetAllOrders();
        List<UserLocation> GetAllUserLocations();


        Users GetUsersById(int id);
        UserLocation GetUserLocationById(int id);
        Orders GetOrderById(int id);
        Pizza GetByPizzaId(int id);
        Ingredients GetIngredientsById(int id);
        PizzaOrder GetPizzaOrderById(int id);
        Store GetStoreById(int id);


        void AddUsers(Users users);
        void AddUserLocation(UserLocation userLocation);
        void AddOrders(Orders orders);       
        void AddPizzaOrders(PizzaOrder pizzaOrder);
        void AddPizza(Pizza pizza);
        void AddStores(Store store);
        void AddIngredients(Ingredients ingredients, Store store);
        void AddPizzaToPizzaOrder(Pizza pizza);


        void UpdateUsers(Users users);
        void UpdateUserLocation(UserLocation userLocation);
        void UpdateStore(Store store);
        void UpdateIngredients(Ingredients ingredients);
        void UpdatePizza(Pizza pizza);
        

        void DeleteUsers(int id);
        void DeleteUserLocation(int id);
        void DeleteStore(int id);
        void DeleteIngredients(int id);
        void DeletePizza(int id);


        void Save();
      
    }
}

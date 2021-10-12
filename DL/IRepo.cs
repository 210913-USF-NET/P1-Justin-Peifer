using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {

        //Storefronts:
        //StoreFront CreateStoreFront(StoreFront store);
        List<StoreFront> GetAllStoreFronts();

        Order PlaceOrder(Order order);

        void UpdateStock(List<LineItem> orderedProduct);

        int UpdateStock(Inventory inventoryToUpdate, int amountToAdd);//method overloading
        

        //Users:
        User EditUser(User userToEdit);
        List<User> GetAllUsers();
        User AddUser(User user);
        List<User> SearchUser(string search);
        User GetUserById(int? id);
        //Products:
        List<Product> GetAllProducts();
        Product ProductByID(int id);

        //List<Product> ProductByStoreFrontId();
        StoreFront StoreById(int Id);

        //inventory
        List<Inventory> GetAllInventory();
        List<Inventory> GetInventoryByStoreId(int id);
        Inventory InventoryById(int id);
        //Orders
        // void ClearBadOrder(int orderId);
        Order NewOrder(int userId);
        List <Order> OrderByUserId(int UserId);
        Order OrderInfoById(int id);
    }
}
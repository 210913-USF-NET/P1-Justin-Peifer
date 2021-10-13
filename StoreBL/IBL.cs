using Models;
using System.Collections.Generic;
using DL;

namespace SBL
{
    //this is an interface for all business logics i may or may not create
    //interface is a contract, all classes that implements an interface
    //must have the methods listed in the interface
    public interface IBL
    {
        List<StoreFront> GetAllStoreFronts();
        List<User> GetAllUsers();
        List<Product> GetAllProducts();
        Product ProductByID(int id);
        List<Inventory> GetAllInventory();
        List<Inventory> GetInventoryByStoreId(int id);
        Inventory InventoryById(int id);
        User AddUser(User user);

        User EditUser(User userToEdit);
        List<User> SearchUser(string search);
        User GetUserById(int? id);
        Order NewOrder(int userId);
        Order PlaceOrder(Order order);
        List<Order> OrderByStoreId(int storeId);
        Order OrderInfoById(int id);
        List <Order> OrderByUserId(int UserId);
        void UpdateStock(List<LineItem> orderedProduct);
        List<LineItem> ItemsInOrder(int orderId);


        int UpdateStock(Inventory inventoryToUpdate, int amountToAdd);

        StoreFront StoreById(int id);
        // StoreFront CreateStoreFront(StoreFront store);

        // StoreFront UpdateStoreFront(StoreFront StoreFrontToUpdate);
    }
}
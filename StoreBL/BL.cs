using System;
using Models;
using System.Collections.Generic;
using DL;

namespace SBL
{
    public class BL : IBL
    {
        private IRepo _repo;
        
        //IRepo repo is the dependency of Business logic, that is being passed in aka "injected"
        public BL(IRepo repo)
        {
            _repo = repo;
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            return _repo.GetAllStoreFronts();
        }

        public User EditUser(User userToEdit)
        {
            return _repo.EditUser(userToEdit);
        }

        public List<User> SearchUser(string search){
            return _repo.SearchUser(search);
        }
        public User GetUserById(int? id)
        {
            return _repo.GetUserById(id);
        }

        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        public User AddUser(User user)
        {
            return _repo.AddUser(user);
        }

        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
            
        }

        // public void ClearBadOrder(int orderId){
        //     return ClearBadOrder (orderId);
        // }
        public Order NewOrder(int userId)
        {
            return _repo.NewOrder(userId);
        }
        public List<LineItem> ItemsInOrder(int orderId)
        {
            return _repo.ItemsInOrder(orderId);
        }
        public Order OrderInfoById(int id){
            return _repo.OrderInfoById(id);
        }
        public List <Order> OrderByUserId(int UserId){
            return _repo.OrderByUserId(UserId);
        }
        public List<Order> OrderByStoreId(int storeId)
        {
            return _repo.OrderByStoreId(storeId);
        }
        public Order PlaceOrder(Order order){
            return _repo.PlaceOrder(order);
        }
        
        public void UpdateStock(List<LineItem> orderedProduct){
            _repo.UpdateStock(orderedProduct);
        }

        public int UpdateStock(Inventory inventoryToUpdate, int amountToAdd){
                    return _repo.UpdateStock(inventoryToUpdate, amountToAdd);
                }
        

        public Product ProductByID(int id)
        {
            return _repo.ProductByID(id);
            
        }
        //products

        public List<Inventory> GetAllInventory()
        {
            return _repo.GetAllInventory();
        }

        public List<Inventory> GetInventoryByStoreId(int id)
        {
            return _repo.GetInventoryByStoreId(id);
        }
        public Inventory InventoryById(int id)
        {
            return _repo.InventoryById(id);
        }
        public StoreFront StoreById(int id)
        {
            return _repo.StoreById(id);
            }
    }
}

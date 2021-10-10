using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;


namespace DL
{
    public class DBRepo : IRepo
    {
        private P1DBContext _context;

        public DBRepo(P1DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Starts a new order for the customer by grabbing a new Order number from the database
        /// </summary>
        /// <param name="UserId"> userID to be linked to the newly created order</param>
        /// <returns>An empty order that has the new OrderId with the UserId contained as well</returns>
        public Order NewOrder(int UserId)
        {
            Order orderAdded = new Order()
            {
                UserId = UserId,
                DateOrdered = DateTime.Now,
            };

            orderAdded = _context.Add(orderAdded).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return new Order()
            {
                Id = orderAdded.Id,
                DateOrdered = orderAdded.DateOrdered,
                UserId = orderAdded.UserId
            };
        }

        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        /// <param name="user">The collected info from the NewUserMenu</param>
        /// <returns>returns an updated version of the submitted User, complete with UserId given by the database.</returns>
        public User AddUser(User user)
        {
            User userToAdd = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Access = user.Access,
                Age = user.Age
            };

            userToAdd = _context.Add(userToAdd).Entity;

            //the "changes" don't get executed until you call the SaveChanges method
            _context.SaveChanges();

            //this clears the changetracker so it returns to a clean slate
            _context.ChangeTracker.Clear();

            return new User()
            {
                Id = userToAdd.Id,
                Name = userToAdd.Name,
                Password = userToAdd.Password,
                Age = userToAdd.Age,
                Access = userToAdd.Access,
                Email = userToAdd.Email
            };
        }


        /// <summary>
        /// Gets a list of all users stored in the database
        /// </summary>
        /// <returns>List of all Users.</returns>
        public List<User> GetAllUsers()
        {

            return _context.Users.AsNoTracking().Select(
                User => new User()
                {
                    Id = User.Id,
                    Email = User.Email,
                    Password = User.Password,
                    Name = User.Name,
                    Age = User.Age,
                    Access = User.Access
                }
            ).ToList();
        }

        /// <summary>
        /// Searchest for a user based on their name
        /// </summary>
        /// <param name="search"></param>
        /// <returns>A list of all users that fit under the search</returns>
        public List<User> SearchUser(string search)
        {
            return _context.Users.Where(
                user => user.Name.ToLower().Contains(search.ToLower())
            ).AsNoTracking().Select(
                u => new User()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Password = u.Password,
                    Name = u.Name,
                    Age = u.Age,
                    Access = u.Access
                }
            ).ToList();
        }

        public User GetUserById(int? id)
        {
            User userById =
                _context.Users
                .AsNoTracking()
                //.include(i => i.inventories)
                .FirstOrDefault(u => u.Id == id);

            return new User()
            {
                Id = userById.Id,
                Email = userById.Email,
                Password = userById.Password,
                Name = userById.Name,
                Age = userById.Age,
                Access = userById.Access
                //Inventory = storeById.Inventories.Select(i => new Inventory()
                //{
                //    Id = i.Id,
                //    ProductId = i.ProductId,
                //    Quantity = i.Quantity,

                //}).ToList()
            };

        }

        /// <summary>
        /// Changes a customer account into a manager account
        ///  by changing the ".Access" boolean to "True"
        /// </summary>
        /// <param name="newManager">the user to change from customer to manager</param>
        /// <returns>The updated user object</returns>
        public User EditUser(User userToEdit)
        {

            User editedUser = new User()
            {
                Id = userToEdit.Id,
                Name = userToEdit.Name,
                Password = userToEdit.Password,
                Email = userToEdit.Email,
                Age = userToEdit.Age,
                Access = userToEdit.Access
            };
            editedUser = _context.Users.Update(editedUser).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return editedUser;
        }

        /// <summary>
        /// Gets more detailed order information, including LineItems, based on OrderId
        /// </summary>
        /// <param name="id">The OrderId for the order we are searching</param>
        /// <returns>The Order's information</returns>
        public Order OrderInfoById(int id)
        {
            Order orderById =
                _context.Orders
                .Include(l => l.LineItems)
                .FirstOrDefault(l => l.Id == id);

            return new Order()
            {
                Id = orderById.Id,
                DateOrdered = orderById.DateOrdered,
                UserId = orderById.UserId,
                LineItems = orderById.LineItems.Select(l => new LineItem()
                {
                    OrderId = l.OrderId,
                    StoreFrontId = l.StoreFrontId,
                    Quantity = l.Quantity

                }).ToList()
            };
        }

        /// <summary>
        /// Gets the order history of a user based on his/her UserId
        /// </summary>
        /// <param name="UserId">The User's ID</param>
        /// <returns>A list of every order that the User has made</returns>
        public List<Order> OrderByUserId(int UserId)
        {
            return _context.Orders.Where(
                order => order.UserId.Equals(UserId)
            ).AsNoTracking().Select(
                Order => new Order()
                {
                    Id = Order.Id,
                    DateOrdered = Order.DateOrdered,
                    UserId = Order.UserId
                }
            ).ToList();
        }


        /// <summary>
        /// Gets a list of every order in the database
        /// </summary>
        /// <returns>The list of every order</returns>
        public List<Order> GetAllOrders()
        {
            return _context.Orders.AsNoTracking().Select(
                Order => new Order()
                {
                    Id = Order.Id,
                    DateOrdered = Order.DateOrdered,
                    UserId = Order.UserId
                }
            ).ToList();
        }

        /// <summary>
        /// finds the order history for a given store
        /// </summary>
        /// <param name="store">the store that we're looking for orders from</param>
        /// <returns>Returns a list of all orders made at a specific store</returns>

        // public void ClearBadOrder(int orderId){
        // var order = _context.Orders.Where(order => order.UserId.Equals(orderId)).FirstOrDefault();
        //     _context.Orders.DeleteObject(order);
        // }


        /// <summary>
        /// Updates the order that was opened at the start of the OrderMenu to add bought LineItems
        /// </summary>
        /// <param name="storeOrderedFrom"> The Store that the order was placed at</param>
        /// <param name="order">The Order, should include LineItems</param>
        /// <returns>Returns the placed order</returns>
        public Order PlaceOrder(StoreFront storeOrderedFrom, Order order)
        {

            foreach (LineItem item in order.LineItems)
            {
                LineItem addedItem = new LineItem()
                {
                    OrderId = order.Id,
                    StoreFrontId = storeOrderedFrom.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                addedItem = _context.Add(addedItem).Entity;
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
            }
            return order;
        }

        /// <summary>
        /// Updates a store's inventory quantity based off of attributes used for placing an order
        /// </summary>
        /// <param name="storeToUpdate">The store that needs to be updated</param>
        /// <param name="orderedProduct"> A list of LineItems that were bought. This needs to be subtracted from the store's inventory</param>
        public void UpdateStock(StoreFront storeToUpdate, List<LineItem> orderedProduct)
        {
            foreach (LineItem item in orderedProduct)
            {
                Inventory updatedInventory = (from i in _context.Inventories
                                                       where i.ProductId == item.ProductId && i.StoreFrontId == storeToUpdate.Id
                                                       select i).SingleOrDefault();

                updatedInventory.Quantity = updatedInventory.Quantity - item.Quantity;

            }
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        /// <summary>
        /// Updates from a Manager's perspective, using an int with
        /// </summary>
        /// <param name="inventoryToUpdate"></param>
        /// <param name="amountToAdd"></param>
        /// <param name="StoreFrontId"></param>
        /// <returns></returns>
        public int UpdateStock(Inventory inventoryToUpdate, int amountToAdd, int StoreFrontId)
        {
            Inventory inventoryToEdit = new Inventory()
            {
                Id = inventoryToUpdate.Id,
                StoreFrontId = StoreFrontId,
                ProductId = inventoryToUpdate.ProductId,
                Quantity = inventoryToUpdate.Quantity + amountToAdd

            };



            inventoryToEdit = _context.Inventories.Update(inventoryToEdit).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return Convert.ToInt32(inventoryToEdit.Quantity);

        }

        /// <summary>
        /// Gets a list of all existing storefronts
        /// </summary>
        /// <returns>A list of all existing storefronts</returns>
        public List<StoreFront> GetAllStoreFronts()
        {

            //same as select * from StoreFront in sql query
            return _context.StoreFronts.AsNoTracking().Select(
                Storefront => new StoreFront()
                {
                    Id = Storefront.Id,
                    State = Storefront.State,
                    Zipcode = Storefront.Zipcode
                }
            ).ToList();

        }

        /// <summary>
        /// Looks up a detailed view of a store, including its inventory, from the store's unique Id
        /// </summary>
        /// <param name="id">The storefront's Id</param>
        /// <returns>A detailed object of the store, which includes a list of the store's inventory.</returns>
        public StoreFront StoreById(int id)
        {
            StoreFront storeById =
                _context.StoreFronts
                .AsNoTracking()
                /*.include(i => i.Inventories)*/
                .FirstOrDefault(i => i.Id == id);

            return new StoreFront()
            {
                Id = storeById.Id,
                Zipcode = storeById.Zipcode,
                State = storeById.State,
                //Inventory = storeById.Inventories.Select(i => new Inventory()
                //{
                //    Id = i.Id,
                //    ProductId = i.ProductId,
                //    Quantity = i.Quantity,

                //}).ToList()
            };
        }

        public List<Inventory> GetInventoryByStoreId(int id)
        {
            return _context.Inventories.Where(
                stock => stock.StoreFrontId.Equals(id))
                .Include(l => l.Product)
                .AsNoTracking().Select(
                i => new Inventory()
                {
                    Id = i.Id,
                    StoreFrontId = i.StoreFrontId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    }
            ).ToList();
        }

        /// <summary>
        /// Gets a list of every product that Beelicious sells
        /// </summary>
        /// <returns>List of all products</returns>
        public List<Product> GetAllProducts()
        {

            return _context.Products.AsNoTracking().Select(
                Product => new Product()
                {
                    Id = Product.Id,
                    Name = Product.Name,
                    Description = Product.Description,
                    Price = Product.Price,
                    Alcohol = Product.Alcohol
                }
            ).ToList();
        }

        /// <summary>
        /// Looks up a detailed view of a product, including the inventory, based on its Id
        /// </summary>
        /// <param name="id"> the product's unique Id</param>
        /// <returns>a Product and the inventory that each store has of it</returns>
        public Product ProductByID(int id)
        {
            Product productByID =
                _context.Products
                .AsNoTracking()
                .FirstOrDefault(i => i.Id == id);

            return new Product()
            {
                Id = productByID.Id,
                Name = productByID.Name,
                Description = productByID.Description,
                Price = productByID.Price,
                Alcohol = productByID.Alcohol,
            };
        }

        public Inventory InventoryById(int id)
        {
            Inventory InventoryById =
                _context.Inventories
                .AsNoTracking()
                .FirstOrDefault(i => i.Id == id);

            return new Inventory()
            {
                Id = InventoryById.Id,
                StoreFrontId = InventoryById.StoreFrontId,
                ProductId = InventoryById.ProductId,
                Quantity = InventoryById.Quantity
            };
        }

        //inventory
        /// <summary>
        /// gets a list of all inventory Beelicious has
        /// </summary>
        /// <returns>A list of every inventory item</returns>
        public List<Inventory> GetAllInventory()
        {
            //same as select * from Inventory in sql query
            return _context.Inventories.AsNoTracking().Select(
                Inventory => new Inventory()
                {
                    Id = Inventory.Id,
                    StoreFrontId = Inventory.StoreFrontId,
                    ProductId = Inventory.ProductId,
                    Quantity = Inventory.Quantity
                }
            ).ToList();
        }


    }
}
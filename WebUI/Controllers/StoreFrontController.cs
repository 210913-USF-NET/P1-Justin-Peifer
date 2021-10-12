using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Models;
using SBL;

namespace WebUI.Controllers
{


    public class StoreFrontController : Controller
    {

        private IBL _bl;
        public StoreFrontController(IBL bl)
        {
            _bl = bl;
        }


        static List<LineItem> shoppingCart = new List<LineItem>();
        // GET: HomeController1
        public ActionResult Index()
        {
            List<StoreFront> allStoreFronts = _bl.GetAllStoreFronts();
            return View(allStoreFronts);
        }

        public ActionResult ViewInventory(int id)
        {
            ViewBag.Stock = _bl.GetInventoryByStoreId(id);
            ViewBag.ProductInfo = _bl.GetAllProducts();
            return View();
        }

        public ActionResult EditInventory(int id)
        {
            ViewBag.Item = _bl.InventoryById(id);
            ViewBag.ProductInfo = _bl.ProductByID(ViewBag.Item.ProductId);
            return View();
        }
        [HttpPost]
        public ActionResult EditInventory(int quantityToChange, int invId)
        {
            Inventory item = _bl.InventoryById(invId);
            _bl.UpdateStock(item, quantityToChange);
            return RedirectToAction("Index");
        }


        public ActionResult ChangeStock(Inventory item)
        {
            return View(item);
        }
        
      /*  public ActionResult ChangeStock(int quantity)
        {
            _bl.UpdateStock(quantity);
        }*/
        public ActionResult DeleteLineItem(int index)
        {
            shoppingCart.RemoveAt(index);
            return RedirectToAction("ViewCurrentOrder");
        }
        public ActionResult ViewOrders(int id)
            //uses a customer's Id if the user is a customer, but uses the storeId if a manager uses it
        {
            if (Request.Cookies["CurrentUserAccess"] == "True")
            {
                List<Order> orders = _bl.OrderByUserId(id);
                return View(orders);
            }
            else if (Request.Cookies["CurrentUserAccess"] == "True")
            {
                List<Order> orders = _bl.OrderByStoreId(id);
                return View(orders);
            }
            return View();
        }

        public ActionResult ViewCurrentOrder()
        {
            if (shoppingCart.Count() !=0)
            {
                ViewBag.Price = 0;
                shoppingCart.Sort((x, y) => x.StoreFrontId.CompareTo(y.StoreFrontId));//sorts by store Id for easier reading if user bought from multiple stores.
                ViewBag.Cart = shoppingCart;
                ViewBag.ProductInfo = _bl.GetAllProducts();
                ViewBag.Stores = _bl.GetAllStoreFronts();
                return View();
            }
            else return RedirectToAction("Index");
        }

        public ActionResult ViewAllOrders(List<Order> orders)
        {
            return View(orders);
        }

        public ActionResult SubmitOrder(int price)
        {
            Order orderToSend = _bl.NewOrder(Convert.ToInt32(Request.Cookies["CurrentUserId"]));
            orderToSend.LineItems = shoppingCart;
            _bl.PlaceOrder(orderToSend);
            _bl.UpdateStock(shoppingCart);
            shoppingCart.Clear();
            Response.Cookies.Delete("Checkout");
            return View(price);
        }

        // GET: HomeController1/Details/5
        public ActionResult Products(int id)
        {
            List<Product> productInfo = new List<Product>();
            List<Inventory> storeInventory = _bl.GetInventoryByStoreId(id);
            for(int x =0; x<storeInventory.Count(); x++)
            {
                productInfo.Add(_bl.ProductByID(storeInventory[x].ProductId));
            }

            ViewBag.Inventory = storeInventory;
            ViewBag.Product = productInfo;
            return View();
        }
        [HttpGet]
        public ActionResult ProductDetails(int id)
        {
            Inventory stock = _bl.InventoryById(id);
            Product productDetail = _bl.ProductByID(stock.ProductId);
            ViewBag.ChosenProductInv = stock;
            ViewBag.ChosenProduct = productDetail;
            return View();
        }

        [HttpPost]
        public ActionResult ProductDetails(int orderQuantity, int productId, int storeId)
        {

            LineItem toBuy = new LineItem(storeId, productId, orderQuantity);
            if (shoppingCart.Count()==0)
            {
                shoppingCart.Add(toBuy);
            }
            else
            {
                List<LineItem> originalCart = shoppingCart;
                bool change = false;
                foreach (LineItem item in shoppingCart){
                    if(item.ProductId==productId && item.StoreFrontId == storeId)
                    {
                        change = true;
                        item.Quantity = orderQuantity;
                    }
                }
                if (change==false)
                {
                    shoppingCart.Add(toBuy);
                }
            }
            return RedirectToAction("ViewCurrentOrder");
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}

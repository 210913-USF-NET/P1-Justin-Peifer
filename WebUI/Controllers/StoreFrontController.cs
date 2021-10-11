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
        // GET: HomeController1
        public ActionResult Index()
        {
            List<StoreFront> allStoreFronts = _bl.GetAllStoreFronts();
            return View(allStoreFronts);
        }

        public ActionResult ViewCurrentOrder()
        {
            if (ViewBag.Shoppingcart != null)
            {
                List<LineItem> cart = ViewBag.Shoppingcart;
                return View(cart);
            }
            else return RedirectToAction("Index");
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
            if (ViewBag.ShoppingCart==null)
            {
                ViewBag.ShoppingCart = new List<LineItem>();
                LineItem item = new LineItem(storeId, productId, orderQuantity);
                ViewBag.ShoppinCart.Add(item);
            }
            else
            {
                foreach (LineItem item in ViewBag.ShoppingCart){
                    if(item.ProductId==productId && item.StoreFrontId == storeId)
                    {
                        item.Quantity = orderQuantity;
                    }
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

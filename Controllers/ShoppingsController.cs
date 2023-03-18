using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftNET_Project.Data;
using SoftNET_Project.Models;

using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

using ClosedXML.Excel;
using System.IO;
using Newtonsoft.Json;
using System.Security.Claims;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Xml;

namespace SoftNET_Project.Controllers
{
    public class ShoppingsController : Controller
    {
        private readonly SystemContext _context;
        public ShoppingsController(SystemContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = _context.Users.FirstOrDefault(i => i.NickName == User.Identity.Name);
            ViewBag.Wishlist = _context.Products.Include(p => p.Wishlist).ThenInclude(i => i.User).Where(i => i.Wishlist.Any(a => a.UserId == user.ID));
            ViewBag.Carts = _context.Products.Include(p => p.Carts).ThenInclude(i => i.User).Where(i => i.Carts.Any(a => a.UserId == user.ID));

            return View(await _context.Products.ToListAsync());
        }


        

        public IActionResult Wishlist()
        {
            var user = _context.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            ViewBag.Wishlist = _context.Products.Include(p => p.Wishlist).ThenInclude(i => i.User).Where(i => i.Wishlist.Any(a => a.UserId == user.ID));

            return View();
        }

        public IActionResult Carts()
        {
            var user = _context.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            ViewBag.Carts = _context.Products.Include(p => p.Carts).ThenInclude(i => i.User).Where(i => i.Carts.Any(a => a.UserId == user.ID));

            string tcmb = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlFile = new XmlDocument();
            xmlFile.Load(tcmb);
            ViewBag.dolar = Convert.ToDouble(xmlFile.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml);
            ViewBag.euro = Convert.ToDouble(xmlFile.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml);
            ViewBag.pound = Convert.ToDouble(xmlFile.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteSelling").InnerXml);


            return View();
        }

        public IActionResult Purchased() {
            var user = _context.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            ViewBag.Purchaseds = _context.Products.Include(p => p.Purchaseds).ThenInclude(i => i.User).Where(i => i.Purchaseds.Any(a => a.UserId == user.ID));

            return View(); 
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }




        // Data Controls

        [HttpPost, ActionName("AddWish")]
        public IActionResult AddWish(int id)
        {
            int userId = _context.Users.FirstOrDefault(i => i.NickName == User.Identity.Name).ID;
            User user = _context.Users.Include(i => i.Wishlists).First(i => i.NickName == User.Identity.Name);

            ICollection<UserWishlist> wishlists = user.Wishlists;
            wishlists.Add(new UserWishlist
            {
                UserId = userId,
                ProductId = id
            });

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("AddCart")]
        public IActionResult AddCart(int id)
        {
            int userId = _context.Users.FirstOrDefault(i => i.NickName == User.Identity.Name).ID;
            User user = _context.Users.Include(i => i.Carts).First(i => i.NickName == User.Identity.Name);

            ICollection<UserCarts> carts = user.Carts;
            carts.Add(new UserCarts
            {
                UserId = userId,
                ProductId = id
            });

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("RemoveWish")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveWish(int id)
        {
            int userId = _context.Users.FirstOrDefault(i => i.NickName == User.Identity.Name).ID;
            User user = _context.Users.Include(i => i.Wishlists).First(i => i.NickName == User.Identity.Name);

            ICollection<UserWishlist> wishlists = user.Wishlists;
            wishlists.Remove(wishlists.FirstOrDefault(i => i.UserId == userId && i.ProductId == id));

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("RemoveCart")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveCart(int id)
        {
            int userId = _context.Users.FirstOrDefault(i => i.NickName == User.Identity.Name).ID;
            User user = _context.Users.Include(i => i.Carts).First(i => i.NickName == User.Identity.Name);

            ICollection<UserCarts> carts = user.Carts;
            carts.Remove(carts.FirstOrDefault(i => i.UserId == userId && i.ProductId == id));

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("RemoveCartFromYourCarts")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveCartFromYourCarts(int id)
        {
            int userId = _context.Users.FirstOrDefault(i => i.NickName == User.Identity.Name).ID;
            User user = _context.Users.Include(i => i.Carts).First(i => i.NickName == User.Identity.Name);

            ICollection<UserCarts> carts = user.Carts;
            carts.Remove(carts.FirstOrDefault(i => i.UserId == userId && i.ProductId == id));

            _context.SaveChanges();
            return RedirectToAction("Carts");
        }


        [HttpPost, ActionName("RemoveAllWishes")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveAllWishes()
        {
            User user = _context.Users.Include(i => i.Wishlists).First(i => i.NickName == User.Identity.Name);

            ICollection<UserWishlist> wishlists = user.Wishlists;
            foreach (var item in wishlists)
            {
                wishlists.Remove(item);
            }

            _context.SaveChanges();
            return RedirectToAction("Wishlist");
        }


        [HttpPost, ActionName("RemoveAllCarts")]
        public IActionResult RemoveAllCarts()
        {
            User user = _context.Users.Include(i => i.Carts).First(i => i.NickName == User.Identity.Name);

            ICollection<UserCarts> carts = user.Carts;
            foreach (var item in carts)
            {
                carts.Remove(item);
            }

            _context.SaveChanges();
            return RedirectToAction("Carts");
        }


        [HttpPost, ActionName("Buy")]
        public IActionResult Buy()
        {


            int userId = _context.Users.FirstOrDefault(i => i.NickName == User.Identity.Name).ID;
            User userForCarts = _context.Users.Include(i => i.Carts).First(i => i.NickName == User.Identity.Name);
            User userForPurchased = _context.Users.Include(i => i.Purchaseds).First(i => i.NickName == User.Identity.Name);

            ICollection<UserCarts> carts = userForCarts.Carts;
            ICollection<UserPurchased> purchaseds = userForCarts.Purchaseds;

            foreach (var item in carts)
            {
                purchaseds.Add(new UserPurchased
                {
                    UserId = item.UserId,
                    ProductId = item.ProductId,
                });
                _context.Products.FirstOrDefault(i => i.Id == item.ProductId).Count--;
                carts.Remove(item);
            }

            _context.SaveChanges();
            return RedirectToAction("Carts");
        }
    }
}

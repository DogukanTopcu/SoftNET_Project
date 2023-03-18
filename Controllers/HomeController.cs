using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SoftNET_Project.Data;
using SoftNET_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SoftNET_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SystemContext _context;

        public HomeController(ILogger<HomeController> logger, SystemContext context)
        {
            _logger = logger;
            _context = context;
        }


        [Authorize]
        public IActionResult Index()
        {
            return View(GetProducts());
        }

        public IActionResult VisualizeProductSoldeds()
        {
            return Json(GetProducts());
        }

        public List<ProductSolded> GetProducts()
        {
            List<ProductSolded> pl = new List<ProductSolded>();
            foreach (var item in _context.Products)
            {
                int totalSales = 0;
                int totalIncome = 0;
                Console.WriteLine(item.ProductName);
                List<User> users = _context.Users.Include(i => i.Purchaseds).ThenInclude(i => i.Product).Where(w => w.Purchaseds.Any(a => a.ProductId == item.Id)).ToList();
                foreach (var user in users)
                {
                    totalSales++;
                }
                totalIncome += item.Price * totalSales;

                pl.Add(new ProductSolded { barcode = item.Barcode, category = item.Category, count = item.Count, price = item.Price, productId = item.Id, productName = item.ProductName, sales = totalSales, income = totalIncome }); ;
            }
            return pl;
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

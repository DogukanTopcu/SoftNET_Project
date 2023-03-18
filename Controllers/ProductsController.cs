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
using Microsoft.AspNetCore.Cors;
using System.Xml;
using BarkodOkuApi;
using ClosedXML;

namespace SoftNET_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly SystemContext _context;


        public ProductsController(SystemContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.ProductsCarts = _context.Users.Include(i => i.Carts).ThenInclude(i => i.Product).Where(i => i.Carts.Any(a => a.ProductId == product.Id)).ToList().Count();
            ViewBag.ProductsWishlists = _context.Users.Include(i => i.Wishlists).ThenInclude(i => i.Product).Where(i => i.Wishlists.Any(a => a.ProductId == product.Id)).ToList().Count();
            ViewBag.ProductsPurchaseds = _context.Users.Include(i => i.Purchaseds).ThenInclude(i => i.Product).Where(i => i.Purchaseds.Any(a => a.ProductId == product.Id)).ToList().Count();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> BarcodeCreate(string barcode)
        {
            string apiKey = "765625d4916685d097ceb7ff3a0717e4020ea114fce35d73587a859730523c00";
            BarkodServisSoapClient service = new BarkodServisSoapClient(BarkodServisSoapClient.EndpointConfiguration.BarkodServisSoap);
            Product productTemplate = new Product { ProductName="", Barcode="", Category="", Price=0, Count=0 };
            try
            {
                BarkodGetirResponse item = await service.BarkodGetirAsync(apiKey, barcode);

                productTemplate.ProductName = item.Body.BarkodGetirResult.UrunBarkod.UrunAd.ToString();
                productTemplate.Barcode = barcode;

            }
            catch (Exception)
            {
                return RedirectToAction("Barcode");
                throw;
            }
            return View(productTemplate);
        }

        [HttpGet]
        public IActionResult Barcode()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Barcode,Count,Price,Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Barcode,Count,Price,Category")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }


        [HttpPost, ActionName("BarkodOku")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BarkodOku(string barcode)
        {
            string apiKey = "765625d4916685d097ceb7ff3a0717e4020ea114fce35d73587a859730523c00";
            BarkodServisSoapClient service = new BarkodServisSoapClient(BarkodServisSoapClient.EndpointConfiguration.BarkodServisSoap);
            Product productTemplate = new Product();

            try
            {
                BarkodGetirResponse item = await service.BarkodGetirAsync(apiKey, barcode);

                if (item.Body.BarkodGetirResult.UrunBarkod.UrunAd != null)
                {
                    productTemplate.ProductName = item.Body.BarkodGetirResult.UrunBarkod.UrunAd.ToString();
                }
                else
                {
                    productTemplate.ProductName = "";
                }

                if (item.Body.BarkodGetirResult.UrunBarkod.UrunFiyat != null)
                {
                    productTemplate.Price = Convert.ToInt32(item.Body.BarkodGetirResult.UrunBarkod.UrunFiyat.ToString());
                }
                else
                {
                    productTemplate.Price = 0;
                }
                productTemplate.Barcode = barcode;

                if (item.Body.BarkodGetirResult.UrunBarkod.BarkodTip != null)
                {
                    Console.WriteLine(item.Body.BarkodGetirResult.UrunBarkod.BarkodTip.ToString());
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Barcode");
                throw;
            }
            return RedirectToAction("Barcode", productTemplate);
        }



        // --------------------------------- Excel Export ------------------------------------
        public IActionResult ExportExcelProductList()
        {
            using (var workbook = new XLWorkbook())
            {
                var products = workbook.Worksheets.Add("Product List");
                products.Cell(1, 1).Value = "ID";
                products.Cell(1, 2).Value = "Product Name";
                products.Cell(1, 3).Value = "Barcode";
                products.Cell(1, 4).Value = "Count";
                products.Cell(1, 5).Value = "Price";
                products.Cell(1, 6).Value = "Category";
                products.Cell(1, 7).Value = "Carts Number";
                products.Cell(1, 8).Value = "Wishlists";
                products.Cell(1, 9).Value = "Purchaseds";

                int productRowCount = 2;
                foreach (var item in GetProducts())
                {
                    products.Cell(productRowCount, 1).Value = item.Id;
                    products.Cell(productRowCount, 2).Value = item.ProductName;
                    products.Cell(productRowCount, 3).Value = item.Barcode;
                    products.Cell(productRowCount, 4).Value = item.Count;
                    products.Cell(productRowCount, 5).Value = item.Price;
                    products.Cell(productRowCount, 6).Value = item.Category;
                    products.Cell(productRowCount, 7).Value = _context.Users.Include(i => i.Carts).ThenInclude(i => i.Product).Where(i => i.Carts.Any(a => a.ProductId == item.Id)).ToList().Count();
                    products.Cell(productRowCount, 8).Value = _context.Users.Include(i => i.Wishlists).ThenInclude(i => i.Product).Where(i => i.Wishlists.Any(a => a.ProductId == item.Id)).ToList().Count();
                    products.Cell(productRowCount, 9).Value = _context.Users.Include(i => i.Purchaseds).ThenInclude(i => i.Product).Where(i => i.Purchaseds.Any(a => a.ProductId == item.Id)).ToList().Count();

                    productRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
                }
            }
        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            var pro = _context.Products;
            foreach (var item in pro)
            {
                products.Add(item);
            }
            return products;
        }

        // --------------------------------------------------------------------------------
    }
}

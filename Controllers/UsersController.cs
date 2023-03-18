using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftNET_Project.Data;
using SoftNET_Project.Models;
using ClosedXML.Excel;

namespace SoftNET_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly SystemContext _context;

        public static User user;

        public UsersController(SystemContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,NickName,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,NickName,Password,Role")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User user)
        {
            var identity = _context.Users.FirstOrDefault(x => x.NickName == user.NickName && x.Password == user.Password);
            if (identity != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, identity.NickName),
                    new Claim(ClaimTypes.Email, identity.NickName),
                    new Claim(ClaimTypes.Role, identity.Role),
                };
                ClaimsIdentity userIdentity;
                if (identity.Role == "Admin")
                {
                    userIdentity = new ClaimsIdentity(claims, "LoginAdmin");
                }
                else
                {
                    userIdentity = new ClaimsIdentity(claims, "LoginUser");
                }
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                //HttpCookie role = new HttpCookie("Role", identity.Role);
                //Response.Cookies.Append("Role", identity.Role.ToString());
                Response.Cookies.Append("User", identity.ToString());
                Response.Cookies.Append("NickName", identity.NickName.ToString());
                Response.Cookies.Append("Name", identity.Name.ToString());
                Response.Cookies.Append("Role", identity.Role.ToString());

                user = identity;

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }



        public IActionResult ExportExcelUserList()
        {
            using (var workbook = new XLWorkbook())
            {
                var users = workbook.Worksheets.Add("Product List");
                users.Cell(1, 1).Value = "ID";
                users.Cell(1, 2).Value = "Name";
                users.Cell(1, 3).Value = "Username";
                users.Cell(1, 4).Value = "Password";
                users.Cell(1, 5).Value = "Role";

                users.Cell(1, 6).Value = "Purchaseds";
                users.Cell(1, 7).Value = "Carts";
                users.Cell(1, 8).Value = "Wishlist";

                int productRowCount = 2;
                foreach (var item in GetUsers())
                {
                    users.Cell(productRowCount, 1).Value = item.ID;
                    users.Cell(productRowCount, 2).Value = item.Name;
                    users.Cell(productRowCount, 3).Value = item.NickName;
                    users.Cell(productRowCount, 4).Value = item.Password;
                    users.Cell(productRowCount, 5).Value = item.Role;

                    users.Cell(productRowCount, 6).Value = string.Join(", ", _context.Products.Include(i => i.Purchaseds).ThenInclude(i => i.User).Where(w => w.Purchaseds.Any(a => a.UserId == item.ID)).ToList().Select(i => i.ProductName));
                    users.Cell(productRowCount, 7).Value = string.Join(", ", _context.Products.Include(i => i.Carts).ThenInclude(i => i.User).Where(w => w.Carts.Any(a => a.UserId == item.ID)).ToList().Select(i => i.ProductName));
                    users.Cell(productRowCount, 8).Value = string.Join(", ", _context.Products.Include(i => i.Wishlist).ThenInclude(i => i.User).Where(w => w.Wishlist.Any(a => a.UserId == item.ID)).ToList().Select(i => i.ProductName));

                    productRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");
                }
            }
        }

        private List<User> GetUsers()
        {
            List<User> users = new List<User>();
            var dataUsers = _context.Users;
            foreach (var item in dataUsers)
            {
                users.Add(item);
            }
            return users;
        }
    }
}

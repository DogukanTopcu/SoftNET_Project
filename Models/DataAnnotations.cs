using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace SoftNET_Project.Models
{
    public class UserCarts
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }

    public class UserWishlist
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }

    public class UserPurchased
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}

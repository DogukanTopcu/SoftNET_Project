using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftNET_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }

        public ICollection<UserCarts> Carts { get; set; }
        public ICollection<UserWishlist> Wishlist { get; set; }
        public ICollection<UserPurchased> Purchaseds { get; set; }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftNET_Project.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NickName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        //public string RoleName { get; set; }
        //public Role Role { get; set; }


        //public ICollection<Product> Products { get; set; }
        public ICollection<UserCarts> Carts { get; set; }
        public ICollection<UserWishlist> Wishlists { get; set; }
        public ICollection<UserPurchased> Purchaseds { get; set; }
    }
}

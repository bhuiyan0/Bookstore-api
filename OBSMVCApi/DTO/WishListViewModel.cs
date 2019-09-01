using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBSMVCApi.DTO
{
    public class WishListViewModel
    {
        public int WishListId{ get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
using OBSMVCApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBSMVCApi.DTO
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string ImageUrl { get; set; }

    }
    public class CartEditViewModel
    {
        public string UserId { get; set; }
        public List<CartList> CartList { get; set; }
    }

    public class CartList
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string ImageUrl { get; set; }

    }
}
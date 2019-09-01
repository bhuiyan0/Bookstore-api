using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OBSMVCApi.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string Descriptions { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string Edition { get; set; }
        public string ISBN { get; set; }
        public int TranslatorId { get; set; }  // author id 
        public int NumberOfPage { get; set; }
        public string Language { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        //for stock 
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }

        // primary entit's property for view data
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public string CategoryName { get; set; }
    }

    public class BookEditViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string Descriptions { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string Edition { get; set; }
        public string ISBN { get; set; }
        public int TranslatorId { get; set; }  // author id 
        public int NumberOfPage { get; set; }
        public string Language { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        //for stock 
        public int ReorderLevel { get; set; }

        // primary entit's property for view data
    }

}
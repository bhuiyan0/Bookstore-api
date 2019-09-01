using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OBSMVCApi.Models
{
    #region Author

    public class Author
    {

        [Key]
        public int AuthorId { get; set; }

        [StringLength(50), Required]
        public string AuthorName { get; set; }

        public DateTime DoB { get; set; }

        [StringLength(50), Required]
        public string ContactNo { get; set; }

        [StringLength(50), EmailAddress, Required]
        public string Email { get; set; }

        [ StringLength(200)]
        public string Address { get; set; }

        public string AuthorInfo { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Book> Books { get; set; }

    }

    #endregion

    #region Category

    public class Category
    {

        [Key]
        public int CategoryId { get; set; }

        [StringLength(50), Required]

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Book> Books { get; set; }
    }

    #endregion

    #region Publisher

    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [StringLength(50), Required]
        public string PublisherName { get; set; }

        [StringLength(20), Required]
        public string ContactNo { get; set; }

        [StringLength(50), EmailAddress, Required]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Book> Books { get; set; }
       // public ICollection<Purchase> Purchases { get; set; }
    }

    #endregion

    #region Book
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [StringLength(50), Required]
        public string BookName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public string Descriptions { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        [StringLength(50)]
        public string Edition { get; set; }

        [StringLength(30)]
        public string ISBN { get; set; }

      //  public int TranslatorId { get; set; }  // author id 
        public int NumberOfPage { get; set; }

        [StringLength(30)]
        public string Language { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
        public ICollection<BookReview> BookReviews { get; set; }
        public ICollection<WishList> Wishlists { get; set; }
        public ICollection<Cart> Carts { get; set; }

    }
    #endregion

    #region Stock

    public class Stock
    {
        [Key]
        public int StockId { get; set; }

        public int BookId { get; set; }
       // public Book Book { get; set; }

        public int Quantity { get; set; }

        public int ReorderLevel { get; set; }
    }

    #endregion

    #region Purchase

    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        public string ReferenceNo { get; set; }
        public string PurchaseStatus { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal VatAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal NetAmount
        {
            get { return (TotalAmount + VatAmount) - DiscountAmount; }
        }

        public ICollection<PurchaseLine> PurchaseLine { get; set; }

        public int PaymentId { get; set; }
        public Payment Payment { get; set; }


    }

    #endregion

    #region PurchaseLine

    public class PurchaseLine
    {
        [Key]
        public int PurchaseLineId { get; set; }

        public int BookId { get; set; }
        //public  Book Book { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount
        {
            get { return Quantity * Rate; }
        }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }


    }

    #endregion

    #region Order

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string OrderStatus { get; set; }
        public string UserID { get; set; }
          
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal VatAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal NetAmount
        {
            get { return (TotalAmount + VatAmount) - DiscountAmount; }
        }

        public int ShippingAddressId { get; set; } // Shipping Address Id
        public ShippingAddress ShippingAddress { get; set; }

        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public ICollection<OrderLine> OrderLine { get; set; }

    }

    #endregion

    #region OrderLine

    public class OrderLine
    {
        [Key]
        public int OrderLineId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount
        {
            get { return Quantity * Rate; }
        }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

    #endregion

    #region BookReview

    public class BookReview
    {
        [Key]
        public int BookReviewId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime ReviewDate { get; set; }

        [StringLength(300)]
        public string Comments { get; set; }
    }

    #endregion

    #region Feedback

    public class Feedback
    {
        public int FeedbackId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime FeedbackDate { get; set; }

        [StringLength(300)]
        public string Comments { get; set; }
    }

    #endregion

    #region WishList

    public class WishList
    {
        [Key]
        public int WishListId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }

    #endregion

    #region ShippingAddress

    public class ShippingAddress
    {
        [Key]
        public int AddressId { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50),EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string District { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

    
    }


    #endregion

    #region Payment 
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }  // order or purchase 
        public string PaymentMethod { get; set; }  // cash, bkash, rocket or bank 
        public decimal Amount { get; set; }

        public string BankName { get; set; }  // for cheque
        public string AccountNo{ get; set; }   //bkash, rocket or bank account no 
        public string TransactionId { get; set; }  //bkash, rocket tran. id or cheque no 
        public string PaymentNote { get; set; }

    }
    #endregion

    #region Cart

    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }

    #endregion

}
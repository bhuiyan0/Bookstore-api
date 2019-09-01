using OBSMVCApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBSMVCApi.DTO
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int ShippingAddressId { get; set; } // Shipping Address Id
        public int PaymentId { get; set; }
        public int Quantity { get; set; }


        // payment entity
        public string PaymentMethod { get; set; }  // cash, bkash, rocket or bank 
        public decimal Amount { get; set; }    // total amount of order
        public string BankName { get; set; }  // for cheque
        public string AccountNo { get; set; }   //bkash, rocket or bank account no 
        public string TransactionId { get; set; }  //bkash, rocket tran. id or cheque no 
        public string PaymentNote { get; set; }



        // shipping address entity
        public int AddressId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string District { get; set; }
        public string Address { get; set; }


        // orderline entity
        public List<OrderLine> OrderLines { get; set; }

        ////orderline entity
        //public int OrderLineId { get; set; }

        //public int BookId { get; set; }
        //public Book Book { get; set; }

        //public int Quantity { get; set; }

        //public decimal Rate { get; set; }



        //user data
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }

    }


    public class OrdersViewModel
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int ShippingAddressId { get; set; } // Shipping Address Id
        public int PaymentId { get; set; }
        public int Quantity { get; set; }


        // payment entity
        public string PaymentMethod { get; set; }  // cash, bkash, rocket or bank 
        public decimal Amount { get; set; }    // total amount of order
        public string BankName { get; set; }  // for cheque
        public string AccountNo { get; set; }   //bkash, rocket or bank account no 
        public string TransactionId { get; set; }  //bkash, rocket tran. id or cheque no 
        public string PaymentNote { get; set; }



        // shipping address entity
        public int AddressId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string District { get; set; }
        public string Address { get; set; }


        // orderline entity
        public List<OrderLineViewModel> OrderLines { get; set; }

        //user data
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }

        public string BookName { get; set; }


        public decimal Rate { get; set; }
        public decimal ItemTotal { get; set; }

    }

    public class OrderLineViewModel
    {

        public string BookName{ get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }
        public decimal ItemTotal { get; set; }


    }

}
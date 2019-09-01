using OBSMVCApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OBSMVCApi.DTO
{
    public class PurchaseViewModel
    {
        [Required,StringLength(30)]
        public string ReferenceNo { get; set; }

        [Required, StringLength(30)]
        public string PurchaseStatus { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        //payment entity
        [Required]
        public string PaymentType { get; set; }  // order or purchase 

        [Required]
        public string PaymentMethod { get; set; }  // cash, bkash, rocket or bank 
        public decimal Amount { get; set; }
        public string BankName { get; set; }  // for cheque
        public string AccountNo { get; set; }   //bkash, rocket or bank account no 
        public string TransactionId { get; set; }  //bkash, rocket tran. id or cheque no 
        public string PaymentNote { get; set; }

        public List<PurchaseLine> PurchaseLines { get; set; }
    }
}
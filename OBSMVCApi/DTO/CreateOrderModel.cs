using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OBSMVCApi.Models;

namespace OBSMVCApi.DTO
{
    public class CreateOrderModel
    {
        [Key]
        public int OrderId { get; set; }

        public string UserName { get; set; }
        //public ApplicationUser User { get; set; }

        [Column(TypeName = "date"), DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal VatAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal NetAmount
        {
            get { return (TotalAmount + VatAmount) - DiscountAmount; }
        }

        public ICollection<OrderLine> OrderLine { get; set; }

        public int BAddId { get; set; } // BAddId => Billing Address Id
        public int SAddId { get; set; } // SAddId => Shipping Address Id
    }
}
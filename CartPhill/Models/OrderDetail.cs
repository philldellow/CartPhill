using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CartPhill.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quanity { get; set; }
        public decimal Price { get; set; }
        public virtual OPP OPP { get; set; }
        public virtual Order Order { get; set; }
    }
}
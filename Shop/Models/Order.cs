using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }


        [Required(ErrorMessage = "Wprowadź imię")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwisko")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Wprowadź ulicę")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Wprowadź miasto")]
        [StringLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage = "Wprowadź kod pocztowy")]
        [StringLength(6)]
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime DateAdded { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public decimal ValueOrder { get; set; }

        public List<PositionOrder> PositionOrder { get; set; }
    }

    public enum StatusOrder
    {
        New,
        Realized
    }
}
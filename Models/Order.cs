using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuplementsStore1.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public bool Shipped { get; set; }


        [Required(ErrorMessage = "Please enter a first name")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the address line")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        [MaxLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(32)]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        [MaxLength(50)]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
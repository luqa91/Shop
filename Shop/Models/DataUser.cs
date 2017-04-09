using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class DataUser
    {

            public string Name { get; set; }

            public string Forname { get; set; }

            public string Adress { get; set; }

            public string City { get; set; }
            public string PostCode { get; set; }


            [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
            public string Phone { get; set; }

            [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
            public string Email { get; set; }
        

        }
    }
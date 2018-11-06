using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReDataViz.Models
{
    public class User
    {
        //public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Key]
        public string EmailID { get; set; }

        public string Passsword { get; set; }
    }
}

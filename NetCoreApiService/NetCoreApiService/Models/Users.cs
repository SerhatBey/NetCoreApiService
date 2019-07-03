using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApiService.Models
{
    public class Users
    {
       [Key]
        public int User_ID { get; set; }
        public string Username { get; set; }
        public virtual List<Products> Products { get; set; }
    
    }

}

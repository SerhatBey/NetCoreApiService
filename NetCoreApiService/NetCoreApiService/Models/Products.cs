using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApiService.Models
{
    public class Products
    {
        [Key]
        public int Product_ID { get; set; }
        public int User_ID { get; set; }
        public string Product_Name { get; set; }
    }

}


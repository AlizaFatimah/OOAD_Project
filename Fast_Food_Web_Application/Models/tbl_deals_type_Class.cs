using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace Fast_Food_Web_Application.Models
{
    public class tbl_deals_type_Class
    {
     
    
        public int dealsType_id { get; set; }

        [Display(Name = "Deals")]
        [Required(ErrorMessage = "*dealsName required")]
        
        public string dealsName { get; set; }
      
    
           }
}
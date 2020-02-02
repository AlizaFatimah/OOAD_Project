using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace Fast_Food_Web_Application.Models
{
    public class tbl_product_Class
    {
        
        public int pro_Food_id { get; set; }



        [Required(ErrorMessage = "*Food Name required")]
        [Display(Name = "Food Name")]

        public string pro_FoodName { get; set; }

        [Required(ErrorMessage = "*Food Description required")]
        [Display(Name = "Food Description")]

        public string pro_FoodDescription { get; set; }

        [Required(ErrorMessage = "*Food Price required")]
        [Display(Name = "Food Price")]

        public int pro_FoodPrice { get; set; }

        [Display(Name = "Food Image")]
        [Required(ErrorMessage = "*Food Image required")]

        public string pro_FoodImage { get; set; }


        [Display(Name = "Deals")]
        [Required(ErrorMessage = "*dealsName required")]

        public string pro_dealsName { get; set; }
      
    
       
        
    }   

    

}
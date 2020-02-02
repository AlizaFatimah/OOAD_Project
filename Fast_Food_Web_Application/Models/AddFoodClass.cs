using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Fast_Food_Web_Application.Models
{
    public class AddFoodClass
    {
        public int Food_id { get; set; }



        [Required(ErrorMessage = "*Food Name required")]
        [Display(Name = "Food Name")]

        public string FoodName { get; set; }

        [Required(ErrorMessage = "*Food Description required")]
        [Display(Name = "Food Description")]
        
        public string FoodDescription { get; set; }

        [Required(ErrorMessage = "*Food Price required")]
        [Display(Name = "Food Price")]

        public int FoodPrice { get; set; }
        [Display(Name = "Deal Name")]

        public string DealsType { get; set; }

        [Display(Name = "Food Image")]
        [Required(ErrorMessage = "*Food Image required")]
       
        public string FoodImage { get; set; }



    }
}
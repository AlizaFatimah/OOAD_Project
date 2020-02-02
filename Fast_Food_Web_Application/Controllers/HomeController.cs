using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Fast_Food_Web_Application.Models;
namespace Fast_Food_Web_Application.Controllers
{

    public class HomeController : Controller
    {
        string MyConnection = "Data Source=DESKTOP-4JGOIVS\\SQLEXPRESS;Initial Catalog=Restaurent_Management_System;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        //
        // GET: /Home/
        DBModels db = new DBModels();
        public ActionResult EveryDayValue()
        {

            kbcc();
            EveryDayValue_Class EveryDayValue = new EveryDayValue_Class();
           var p = EveryDayValue.getImages();
           return View(p);
        }
        public ActionResult MakeItAMeal()
        {
            kbcc();
            MakeItAMeal_Class MakeItAMeal = new MakeItAMeal_Class();
            return View(MakeItAMeal.getImages());
        }
        public ActionResult MealBox()
        {
            kbcc();
            MealBox_Class MealBox = new MealBox_Class();
            return View(MealBox.getImages());
        }
        public ActionResult Sharing()
        {

            kbcc();
            Sharing_Class Sharing = new Sharing_Class();
            return View(Sharing.getImages());
        }
        public ActionResult Promotions()
        {
            kbcc();
            Promotions_Class promotions = new Promotions_Class();
            return View(promotions.getImages());
        }
        public ActionResult Snacks()
        {
            Snacks_Class snacks = new Snacks_Class();
             return View(snacks.getImages());
        }
        public ActionResult MidNightDeals()
        {
            kbcc();
            MidNightDeals_Class MidNightDeals = new MidNightDeals_Class();
            return View(MidNightDeals.getImages());
        }

        public void  kbcc()
        {
            tbl_invoice iv = new tbl_invoice();
          
           
            if (TempData["card"] != null)
            {
                float x = 0;
                List<card> li2 = TempData["card"] as List<card>;
                foreach (var item in li2)
                {
                    x += item.pro_product_Bill;

                }

                TempData["total"] = x;
            }
            TempData.Keep();

       }

        public ActionResult addtocart(int? id)
        {
            tbl_product p = db.tbl_product.Where(x => x.pro_Food_id == id).SingleOrDefault();
            return View(p);
        }

        List<card> li = new List<card>();

        [HttpPost]
        public ActionResult addtocart(tbl_product pi, string qty, int id)
        {
            tbl_product p = db.tbl_product.Where(x => x.pro_Food_id == id).SingleOrDefault();
            card c = new card();
            c.pro_product_id = p.pro_Food_id;
            c.pro_product_Price = (int)p.pro_FoodPrice;
            c.qty = Convert.ToInt32(qty);
            c.pro_product_Bill = c.pro_product_Price * c.qty;
            c.pro_product_Name = p.pro_FoodName;
            if (TempData["card"] == null)
            {
                li.Add(c);
                TempData["card"] = li;

            }
            else
            {
                /*List<card> li2 = TempData["card"] as List<card>;
                li2.Add(c);
                TempData["card"] = li2;
            
                 */
                List<card> li2 = TempData["card"] as List<card>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.pro_product_id == c.pro_product_id)
                    {
                        item.qty += c.qty;
                        item.pro_product_Bill += c.pro_product_Bill;
                        flag = 1;

                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }
                TempData["card"] = li2;

            }

            TempData.Keep();



            return RedirectToAction("EveryDayValue");
        }

        public ActionResult checkout()
        {
            TempData.Keep();


            return View();
        }
        [HttpPost]
        public ActionResult checkout(tbl_order o)
        {
           /* List<card> li = TempData["card"] as List<card>;
            tbl_invoice iv = new tbl_invoice();
            iv.in_fk_Login_id = Convert.ToInt32(Session["Login_id"].ToString());
            iv.in_Date_Time = System.DateTime.Now;
            iv.in_total_bill = (float)TempData["total"];
            db.tbl_invoice.Add(iv);
            db.SaveChanges();
            foreach (var item in li)
            {
                tbl_order od = new tbl_order();
                od.o_fk_pro_Food_id = item.pro_product_id;
                od.o_fk_in_id = iv.in_id;
                od.o_Date_Time = System.DateTime.Now;
                od.o_Quantity = item.qty;
                od.o_unitprice = (int)item.pro_product_Price;
                od.o_bill = item.pro_product_Bill;
                db.tbl_order.Add(od);
                db.SaveChanges();

            }
            TempData.Remove("total");

            TempData.Remove("card");
            TempData["msg"] = "Transaction Completed....";
            TempData.Keep();*/
            return View();
        }

        public ActionResult Remove(int? id)
        {

            List<card> li2 = TempData["card"] as List<card>;
            card c = li2.Where(x => x.pro_product_id == id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            foreach (var item in li2)
            {
                h += item.pro_product_Bill;

            }
            TempData["total"] = h;


            return RedirectToAction("checkout");
        }


	}//end home controller brakets...
}
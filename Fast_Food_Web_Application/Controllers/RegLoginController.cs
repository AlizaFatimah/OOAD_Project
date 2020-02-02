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
    public class RegLoginController : Controller
    {

        string MyConnection = "Data Source=DESKTOP-4JGOIVS\\SQLEXPRESS;Initial Catalog=Restaurent_Management_System;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
       
        //
        // GET: /RegLogin/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            User_Registration_Class reg = new User_Registration_Class();
            return View(reg);
        }

        [HttpPost]
        public ActionResult Registration(User_Registration_Class uc)
        {
            SqlConnection sqlcon = new SqlConnection(MyConnection);
            sqlcon.Open();
            SqlCommand sqlcmd = new SqlCommand("spInsertRegLogin", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserName", uc.UserName);
            sqlcmd.Parameters.AddWithValue("@Email", uc.Email);
            sqlcmd.Parameters.AddWithValue("@Password", uc.Password);

            sqlcmd.Parameters.AddWithValue("@Gender", uc.Gender);
       
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();
            ViewData["Message"] = "User Record" + uc.UserName + "Is Saved Successfully!";

          return RedirectToAction("Login");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User_LoginClass userModel, tbl_order o)
        {


            using (DBModels db = new DBModels())
            {


                var userDetails = db.User_Login.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                var admin = db.User_Login.Where(x => x.Email == "admin@gmail.com" && x.Password == "12345").FirstOrDefault();


                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "*Wrong User Name Or Password.";
                    return View("Login", userModel);
                }
                else if (userDetails == admin)
                {
                    Session["Login_id"] = userDetails.Login_id;
                    ViewData["Message"] = "User Record" + userModel.Email + "Is Login Successfully!";

                    return RedirectToAction("ViewRegisterFood");

                }
                else 
                {
                    Session["Login_id"] = userDetails.Login_id;
                    List<card> li = TempData["card"] as List<card>;
                    tbl_invoice iv = new tbl_invoice();
                    iv.in_fk_Login_id = Convert.ToInt32(Session["Login_id"].ToString());
                    if (TempData["card"] != null)
                    {
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

                        TempData.Keep();





                        return RedirectToAction("EveryDayValue", "Home", userModel);
                    }
                    else
                    {
                       userModel.LoginErrorMessage = "*Cart is Empty";

                       return View("Login", userModel);
                    }
                }
                
            }
        }
         public ActionResult  RegisterFood()
        {
            tbl_product_Class addfood = new tbl_product_Class();
            
            return View(addfood);
        }
        [HttpPost]
         public ActionResult RegisterFood(tbl_product_Class addfood, HttpPostedFileBase file)
         {
           
             SqlConnection con = new SqlConnection(MyConnection);
             con.Open();
             SqlCommand sqlcmd = new SqlCommand("spInsertIntoDeals", con);
             sqlcmd.CommandType = CommandType.StoredProcedure;
             sqlcmd.Parameters.AddWithValue("@pro_FoodName", addfood.pro_FoodName);
             sqlcmd.Parameters.AddWithValue("@pro_FoodDescription", addfood.pro_FoodDescription);
             sqlcmd.Parameters.AddWithValue("@pro_FoodPrice", addfood.pro_FoodPrice);
             if (file != null && file.ContentLength > 0)
             {
                 string fileName = Path.GetFileName(file.FileName);
                 string imgPath = Path.Combine(Server.MapPath("~/User-Images/"), fileName);
                 file.SaveAs(imgPath);

             }
             sqlcmd.Parameters.AddWithValue("@pro_FoodImage", "~/User-Images/" + file.FileName);
             sqlcmd.Parameters.AddWithValue("@pro_dealsName", addfood.pro_dealsName);

            sqlcmd.ExecuteNonQuery();
             con.Close();
            
            
            ViewData["Message"] = "Food Record " + addfood.pro_FoodName + "Is Saved Successfully!";



             return RedirectToAction("ViewRegisterFood");
         }



        [HttpGet]
        public ActionResult ViewRegisterFood()
        {

          //  AddFoodClass addfood = new AddFoodClass();

            DataTable dtbProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(MyConnection))
            {
                sqlcon.Open();
               string query = "select * from tbl_product";
                //string query = "SELECT pro_FoodName, pro_FoodDescription, dealsName FROM tble_product  INNER JOIN tbl_Deals_Type  ON tble_product.pro_Food_id = tbl_Deals_Type.dealsType_id";
                
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.Fill(dtbProduct);

            }
            return View(dtbProduct);
        }



        //For Edir
        public ActionResult Edit(int id)
        {

            tbl_product_Class productModel = new tbl_product_Class();
            DataTable dtbProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(MyConnection))
            {
                sqlcon.Open();
                string query = "select * from tbl_product where pro_Food_id = @pro_Food_id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@pro_Food_id", id);
                sqlDa.Fill(dtbProduct);

            }
            if (dtbProduct.Rows.Count == 1)
            {
                productModel.pro_Food_id = Convert.ToInt32(dtbProduct.Rows[0][0].ToString());
                productModel.pro_FoodName = dtbProduct.Rows[0][1].ToString();
                productModel.pro_FoodDescription = dtbProduct.Rows[0][2].ToString();
                productModel.pro_FoodPrice = Convert.ToInt32(dtbProduct.Rows[0][3].ToString());
                productModel.pro_dealsName = dtbProduct.Rows[0][4].ToString();
                
                return View(productModel);

            }
            else
            {
                return RedirectToAction("ViewRegisterFood");
            }
        }


        [HttpPost]
        public ActionResult Edit(tbl_product_Class productModel)
        {

            using (SqlConnection sqlcon = new SqlConnection(MyConnection))
            {
                sqlcon.Open();
                string query = "Update tbl_product set  pro_FoodName =  @pro_FoodName , pro_FoodDescription  = @pro_FoodDescription, pro_FoodPrice =  @pro_FoodPrice ,pro_dealsName = @pro_dealsName  where pro_Food_id = @pro_Food_id ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@pro_Food_id", productModel.pro_Food_id);

                sqlcmd.Parameters.AddWithValue("@pro_FoodName", productModel.pro_FoodName);
                sqlcmd.Parameters.AddWithValue("@pro_FoodDescription", productModel.pro_FoodDescription);
                sqlcmd.Parameters.AddWithValue("@pro_FoodPrice", productModel.pro_FoodPrice);
                sqlcmd.Parameters.AddWithValue("@pro_dealsName", productModel.pro_dealsName);
             
               
                sqlcmd.ExecuteNonQuery();

            }

            return RedirectToAction("ViewRegisterFood");
        }
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(MyConnection))
            {
                sqlcon.Open();
                string query = "Delete from tbl_product where pro_Food_id = @pro_Food_id  ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@pro_Food_id", id);

                sqlcmd.ExecuteNonQuery();

            }

            return RedirectToAction("ViewRegisterFood");

        }





	}
}
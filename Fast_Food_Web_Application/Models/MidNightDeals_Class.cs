using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;

namespace Fast_Food_Web_Application.Models
{
    public class MidNightDeals_Class
    {
        string MyConnection = "Data Source=DESKTOP-4JGOIVS\\SQLEXPRESS;Initial Catalog=Restaurent_Management_System;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        public IList<tbl_product_Class> getImages()
        {
            List<tbl_product_Class> photo = new List<tbl_product_Class>();


            SqlConnection sqlcon = new SqlConnection(MyConnection);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = sqlcon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select pro_Food_id,pro_FoodName,pro_FoodDescription,pro_FoodPrice,pro_FoodImage from tbl_product where pro_dealsName LIKE '%MIDNIGHT DEALS%'";
                sqlcon.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tbl_product_Class addfood = new tbl_product_Class();
                    addfood.pro_Food_id = Convert.ToInt32(dr["pro_Food_id"]);
                    
                    addfood.pro_FoodName = dr["pro_FoodName"].ToString();
                    addfood.pro_FoodDescription = dr["pro_FoodDescription"].ToString();
                    addfood.pro_FoodPrice = Convert.ToInt32(dr["pro_FoodPrice"]);
                    addfood.pro_FoodImage = dr["pro_FoodImage"].ToString();
                    photo.Add(addfood);
                }
                if (dr != null)
                {
                    dr.Dispose();
                    dr.Close();

                }
                sqlcon.Close();
                return photo.ToList();
            }

        }

    }
}
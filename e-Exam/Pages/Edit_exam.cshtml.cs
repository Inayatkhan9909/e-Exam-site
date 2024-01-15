using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace e_Exam.Pages
{
    public class Edit_examModel : PageModel
    {

        private readonly string connectionstring;
        public ExamInfo examinfo = new ExamInfo();

        public Edit_examModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("My_database");
        }

        public void OnGet()
        {
            string id = Request.Query["Exam_id"];
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    string sql = "Select * from Exam_details where Exam_id = @id";
                    using (SqlCommand cmd = new SqlCommand(sql,con))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader =  cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                examinfo.Exam_id = ""+reader.GetInt32(0);
                                examinfo.Exam_name = reader.GetString(1);
                                examinfo.Exam_date = reader.GetString(2);
                                examinfo.Exam_description = reader.GetString(3);
                                Console.WriteLine(reader.GetString(3));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.ToString());
            }
        }

        public void Onpost(int Exam_id) 
        {
           Console.WriteLine("parameter id :"+Exam_id);
            examinfo.Exam_id = Request.Form["Exam_id"];
            Console.WriteLine("examinfo id :" + examinfo.Exam_id);
            examinfo.Exam_name= Request.Form["Exam_name"];
            Console.WriteLine("examinfo name :" + examinfo.Exam_name);
            examinfo.Exam_date = Request.Form["Exam_date"];
            examinfo.Exam_description = Request.Form["Exam_description"];


            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    string sql = "Update Exam_details set Exam_name=@Exam_name,Exam_date=@Exam_date," +
                        "Exam_description=@Exam_description where Exam_id = @Exam_id";
                  

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("Exam_id", examinfo.Exam_id);
                        cmd.Parameters.AddWithValue("Exam_name", examinfo.Exam_name);
                        cmd.Parameters.AddWithValue("Exam_date", examinfo.Exam_date);
                        cmd.Parameters.AddWithValue("Exam_description",examinfo.Exam_description);
                        cmd.ExecuteNonQuery();
                        
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine( ex.ToString());
            }
        }

    }
}

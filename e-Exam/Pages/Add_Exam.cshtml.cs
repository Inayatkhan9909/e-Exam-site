using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace e_Exam.Pages
{
    public class Add_ExamModel : PageModel
    {

        private readonly string connectionstring;
        ExamInfo examInfo = new ExamInfo();
        
        public Add_ExamModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("My_database");
        }

        public void OnPost()
        {
          examInfo.Exam_name = Request.Form["Exam_name"];
            examInfo.Exam_date = Request.Form["Exam_date"];
            examInfo.Exam_description= Request.Form["Exam_description"];

            try
            {
                 using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();

                    string sql = "insert into Exam_details(Exam_name,Exam_date,Exam_description) values" +
                        "( @Exam_name,@Exam_date,@Exam_description)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Exam_name", examInfo.Exam_name);
                        cmd.Parameters.AddWithValue("@Exam_date", examInfo.Exam_date);
                        cmd.Parameters.AddWithValue("@Exam_description", examInfo.Exam_description);
                        cmd.ExecuteNonQuery();



                    }
                }


            }
            catch (Exception ex) 
            {
                Console.WriteLine( ex.ToString());
            }

            examInfo.Exam_name = "";
            examInfo.Exam_date = "";
            examInfo.Exam_description = "";

            Response.Redirect("Admin");


        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace e_Exam.Pages
{
    public class Delete_examModel : PageModel
    {
        private readonly string connectionstring;
        public ExamInfo examinfo = new ExamInfo();
        public Delete_examModel(IConfiguration configuration)
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
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                examinfo.Exam_id = "" + reader.GetInt32(0);
                                examinfo.Exam_name = reader.GetString(1);
                                examinfo.Exam_date = reader.GetString(2);
                                examinfo.Exam_description = reader.GetString(3);
                                Console.WriteLine(reader.GetString(3));
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace e_Exam.Pages
{
    public class Edit_exam_listModel : PageModel
    {
        private readonly string connectionstring;

        public Edit_exam_listModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("My_database");

        }


        public List<ExamInfo> examlist = new List<ExamInfo>();
        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    
                    //string sql = "Select * from Exam_details";
                    string sql = "select * from Exam_details";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("cmd dandi");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                          while (reader.Read())
                            {
                                
                                ExamInfo exam = new ExamInfo();
                               

                                exam.Exam_id = "" + reader.GetInt32(0);

                                exam.Exam_name = reader.GetString(1);
                                exam.Exam_date = reader.GetString(2);
                                exam.Exam_description = reader.GetString(3);
                               
                                examlist.Add(exam);
                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace e_Exam.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string connectionstring;
        

        public IndexModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("My_database");
        }

        public List<ExamInfo> examinfolist = new List<ExamInfo>();

        public void OnGet()
        {
            try
            {
                using (SqlConnection con  = new SqlConnection(connectionstring))
                {
                    con.Open();
                    string sql = "select * from Exam_details";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ExamInfo examInfo = new ExamInfo();
                                examInfo.Exam_id = "" + reader.GetInt32(0);
                            
                                examInfo.Exam_name= reader.GetString(1);
                                examInfo.Exam_date= reader.GetString(2);
                                examInfo.Exam_description= reader.GetString(3);
                                examinfolist.Add(examInfo);

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
    }
}

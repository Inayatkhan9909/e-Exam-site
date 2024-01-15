using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace e_Exam.Pages
{
    public class Delete_confirmedModel : PageModel
    {
        private readonly string connectionstring;
        public ExamInfo examinfo = new ExamInfo();
        public Delete_confirmedModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("My_database");

        }
        public void OnGet(int Exam_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    string sql = "Delete from Exam_details where Exam_id = @Exam_id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Exam_id", Exam_id);
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("Edit_exam_list");
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.ToString());
            }
        }
    }
}

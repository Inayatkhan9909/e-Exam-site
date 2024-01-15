using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace e_Exam.Pages
{
    public class EnrollModel : PageModel
    {

        public readonly string connectionstring;
        Enroll_students enroll = new Enroll_students();

        public EnrollModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("My_database");
        }

        public void OnPost()
        {
            enroll.Name = Request.Form["Name"];
            enroll.Email = Request.Form["Email"];
            enroll.Phone = Request.Form["Phone"];
            enroll.Address = Request.Form["Address"];
            enroll.Exam = Request.Form["Exam"];
            enroll.Qualification = Request.Form["Qualification"];

          try
          {

                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    Console.WriteLine("above sql");
                    string sql = "Insert into Enroll_students (Name,Email,Phone,Address,Exam,Qualification)" +
                        " values(@Name,@Email,@Phone,@Address,@Exam,@Qualification)";
                    Console.WriteLine("Below sql");
                    Console.WriteLine( sql);

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine(  "at top of parameters");
                        cmd.Parameters.AddWithValue("Name", enroll.Name);
                        cmd.Parameters.AddWithValue("Email"  , enroll.Email);
                        cmd.Parameters.AddWithValue("Phone"  , enroll.Phone);
                        cmd.Parameters.AddWithValue("Address", enroll.Address);
                        cmd.Parameters.AddWithValue("Exam"   , enroll.Exam);
                        cmd.Parameters.AddWithValue("Qualification", enroll.Qualification);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
          catch (Exception ex)
          {
                Console.WriteLine( ex.ToString());
          }

            enroll.Name = "";
            enroll.Email = "";
            enroll.Phone = "";
            enroll.Address = "";
            enroll.Exam = "";
            enroll.Qualification = "";

            Response.Redirect("/Index");

        }
    }
}

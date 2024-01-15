using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace e_Exam.Pages
{
    public class Enroll_listModel : PageModel
    {
        private string connectionstring;
        public Enroll_listModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("My_database");

        }
        public List<Enroll_students> enroll_list = new List<Enroll_students>();

        public void OnGet()
        {
            try
            {
                using(SqlConnection con = new SqlConnection(connectionstring))
                {
                      con.Open();
                    string sql = "Select * from Enroll_students ";

                  using(  SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Enroll_students enroll = new Enroll_students();
                                enroll.Enroll_id = ""+ reader.GetInt32(0);
                                enroll.Name = reader.GetString(1);
                                enroll.Email = reader.GetString(2);
                                enroll.Phone = reader.GetString(3);
                                enroll.Address = reader.GetString(4);
                                enroll.Exam= reader.GetString(5);
                                enroll.Qualification = reader.GetString(6);

                                enroll_list.Add(enroll);
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

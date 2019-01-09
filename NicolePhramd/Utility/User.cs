using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace NicolePhramd.Utility
{
    public class User : PageModel
    {
        public int userId { get; set; }
        public bool isAddUser { get; set; }

        public void checkUser(string email, string password)
        {
            userId = 0;
            using (SqlConnection myConn = new SqlConnection(Program.Fetch.cs))
            {
                SqlCommand login = new SqlCommand();
                login.Connection = myConn;
                myConn.Open();

                login.Parameters.AddWithValue("@email", email);
                login.Parameters.AddWithValue("@password", password);

                login.CommandText = ("[spLogin]");
                login.CommandType = System.Data.CommandType.StoredProcedure;

                var result = login.ExecuteScalar();

                if (result != null)
                {
                    userId = Convert.ToInt32(result);
                }
            }
           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace WebApplication1
{
    public partial class staffLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //sql connection stuff
            SqlConnection connection;
            SqlCommand command;

            //testConnectionString from webconfig
            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;

            connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            connection.Open();
            try
            {
                command = new SqlCommand("SELECT * FROM Staff WHERE UserName = '" + TextBox1.Text + "' AND Password = '" + TextBox2.Text + "'", connection);
                SqlDataReader nameReader = command.ExecuteReader();

                //command = new SqlCommand(null, connection);
                //command.CommandText = "SELECT * FROM Staff WHERE UserName = CAST((@username) as varchar(8000))  AND Password = CAST((@password) as varchar(8000))";
                //SqlParameter userParam2 = new SqlParameter("@username", System.Data.SqlDbType.Text, 100);
                //SqlParameter userParam3 = new SqlParameter("@password", System.Data.SqlDbType.Text, 100);
                //userParam2.Value = TextBox1.Text;
                //userParam3.Value = TextBox2.Text;
                //command.Parameters.Add(userParam2);
                //command.Parameters.Add(userParam3);
                //command.Prepare();
                //command.ExecuteNonQuery();
                //SqlDataReader nameReader = command.ExecuteReader();

                while (nameReader.Read())
                {
                    if (nameReader.HasRows)
                    {
                        Response.Redirect("Search.aspx");
                    }
                }

            }
            catch (Exception) { }
            Label1.Text = "Wrong Credentials";
            connection.Close();
        }
    }
}
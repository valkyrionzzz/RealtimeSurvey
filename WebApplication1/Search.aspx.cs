using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //do sql stuff

            SqlConnection connection = new SqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;
            connection.ConnectionString = connectionString;

            connection.Open();

            SqlCommand command = null;
            SqlDataReader reader = null;

            //setup basic sql command
            command = new SqlCommand("SELECT * FROM Respondant", connection);

            //execute command
            reader = command.ExecuteReader();

            DataTable dt = new DataTable();

            //setup the columns
            dt.Columns.Add("RID", typeof(Int32));
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Date Of Creation", typeof(DateTime));
            dt.Columns.Add("D.O.B", typeof(DateTime));
            dt.Columns.Add("Phone number", typeof(String));
            dt.Columns.Add("Last Name", typeof(String));
            dt.Columns.Add("User Name", typeof(String));

            //reads 1 row at a time from our sql set of results
            while (reader.Read())
            {
                //generate an empty row for our table
                DataRow row = dt.NewRow();
                //fill in row from this row of results
                row["RID"] = reader["RID"];
                row["Name"] = reader["Name"];
                row["Date Of Creation"] = reader["Date"];
                row["D.O.B"] = reader["Dob"];
                row["Phone number"] = reader["Phone"];
                row["Last Name"] = reader["LastName"];
                row["User Name"] = reader["UserName"];
                //add this row to our data table
                dt.Rows.Add(row);
            }
            //show results in gridview
            GridView1.DataSource = dt;
            GridView1.DataBind();

            connection.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string paramater1;
            string paramater2;
            string paramater3;
            string paramater4;

            //check drop down states and assigning variables depending on the state
            if (DropDownList1.Text != "PickOne")
            {
                paramater1 = DropDownList1.Text;
            }
            else
                paramater1 = "pass";

            if (DropDownList2.Text != "PickOne")
            {
                paramater2 = DropDownList2.Text;
            }
            else
                paramater2 = "pass";

            if (DropDownList3.Text != "PickOne")
            {
                paramater3 = DropDownList3.Text;
            }
            else
                paramater3 = "pass";

            if (DropDownList4.Text != "PickOne")
            {
                paramater4 = DropDownList4.Text;
            }
            else
                paramater4 = "pass";


            //SQL stuff
            SqlConnection connection = new SqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;
            connection.ConnectionString = connectionString;

            connection.Open();

            SqlCommand command2 = null;
            SqlDataReader queryReader = null;

            command2 = new SqlCommand(null, connection);

            //build queries if selected a field
            string extraQuery1 = "";
            string extraQuery2 = "";
            string extraQuery3 = "";
            string extraQuery4 = "";

            try
            {
                //query buildling with user input
                string baseQuery = "SELECT * FROM Respondant,Answer WHERE Respondant.RID = Answer.RID";

                if (paramater1 != "pass")
                    extraQuery1 = " AND Answer.Text = '" + paramater1 + "'";
                if (paramater2 != "pass")
                    extraQuery2 = " AND Answer.Text = '" + paramater2 + "'";
                if (paramater3 != "pass")
                    extraQuery3 = " AND Answer.Text = '" + paramater3 + "'";
                if (paramater4 != "pass")
                    extraQuery4 = " AND Answer.Text = '" + paramater4 + "'";

                //string query1 = 
                command2 = new SqlCommand(baseQuery + extraQuery1 + extraQuery2 + extraQuery3 + extraQuery4, connection);
                //command2 = new SqlCommand("SELECT * FROM Respondant,Answer WHERE Respondant.RID = Answer.RID AND Answer.Text = 'Male'", connection);
                queryReader = command2.ExecuteReader();

                DataTable dt = new DataTable();

                //setup the columns
                dt.Columns.Add("RID", typeof(Int32));
                dt.Columns.Add("Name", typeof(String));
                dt.Columns.Add("Date Of Creation", typeof(DateTime));
                dt.Columns.Add("D.O.B", typeof(DateTime));
                dt.Columns.Add("Phone number", typeof(String));
                dt.Columns.Add("Last Name", typeof(String));
                dt.Columns.Add("User Name", typeof(String));

                while (queryReader.Read())
                {
                    //check anonymous
                    string anon = "";
                    anon = queryReader["Anon"].ToString();
                    //generate an empty row for our table
                    DataRow row = dt.NewRow();
                    //fill in row from this row of results
                    row["RID"] = queryReader["RID"];
                    if (anon == "false")
                    {
                        row["Name"] = queryReader["Name"];
                        row["Last Name"] = queryReader["LastName"];
                    }
                    else
                    {
                        row["Name"] = "ANON";
                        row["Last Name"] = "ANON";
                    }
                    row["Date Of Creation"] = queryReader["Date"];
                    row["D.O.B"] = queryReader["Dob"];
                    row["Phone number"] = queryReader["Phone"];
                    row["User Name"] = queryReader["UserName"];
                    //add this row to our data table
                    dt.Rows.Add(row);
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception) { }

        }


    }
}
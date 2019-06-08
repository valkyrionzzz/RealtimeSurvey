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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //do sql stuff

            SqlConnection connection = new SqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;
            connection.ConnectionString = connectionString;

            connection.Open();

            SqlCommand command = null;
            SqlCommand command2 = null;
            SqlCommand command3;
            
            //init/declaration

            string Namee;
            string IP;
            string currentDate;
            string DOB;
            float phone;
            string LName;
            string UName;
            int anon;
            bool pass;
            bool isNumeric;

            Namee = TextBox2.Text;

            IP = Request.UserHostAddress;

            currentDate = DateTime.Now.Date.ToString("yyyy-MM-dd");

            DOB = Calendar1.SelectedDate.ToString();

            try
            {
                phone = float.Parse(TextBox4.Text);
            }
            catch (Exception) { phone = 0; }

            LName = TextBox3.Text;

            UName = TextBox1.Text;
            
            isNumeric = false;
            try
            {
                //check if phone is all numbers
                isNumeric = float.TryParse(TextBox4.Text, out float n);
            }
            catch (Exception) { }

            if (CheckBox1.Checked)
            {
                anon = 1;
            }
            else
                anon = 0;

            //---------------------------------- SAVE TO SESSION ----------------------------------

            List<string> userInfo = new List<string>();
            //get prev values
            if (Session["Info"] != null)
                userInfo = (List<string>)Session["Info"];

            userInfo.Add(Namee);
            userInfo.Add(IP);
            userInfo.Add(currentDate);
            userInfo.Add(DOB);
            userInfo.Add(phone.ToString());
            userInfo.Add(LName);
            userInfo.Add(UName);

            Session["info"] = userInfo;



            command = new SqlCommand(null, connection);

            //---------------------------------- CHECKS ----------------------------------

            pass = true;

            if (TextBox2.Text == "" ||
                Calendar1.SelectedDate.ToString() == "" ||
                TextBox4.Text == "" || isNumeric == false||
                TextBox3.Text == "" ||
                TextBox1.Text == "")
            {
                pass = false;
            }

            //Check if username is unique ----couldnt get working(sometime later?) <couldnt match varchar with text, couldnt prepare()>
            //command2.CommandText = "SELECT * FROM Respondant WHERE UserName = @user";
            //SqlParameter userParam = new SqlParameter("@user", System.Data.SqlDbType.Text, 100);
            //userParam.Value = UName;
            //command2.Parameters.Add(userParam);
            //command2.Prepare();
            //command2.ExecuteNonQuery();

            command2 = new SqlCommand("SELECT * FROM Respondant WHERE UserName = '"+UName+"'", connection);
            SqlDataReader nameReader = command2.ExecuteReader();
            while (nameReader.Read())
            {
                //check if there are rows matching username
                if (nameReader.HasRows)
                {
                    pass = false;
                    Label1.Text = "USERNAME TAKEN";
                }
                else
                    Label1.Text = "USERNAME IS OK";
            }
                //---------------------------------- SQL COMMAND/PREP ----------------------------------
                //if all checks are ok
            if (pass == true)
            {
                try
                {
                    //get new username
                    userInfo = (List<string>)Session["Info"];
                    userInfo.RemoveAt(6);
                    userInfo.Add(UName);

                    //insert text
                    command.CommandText =
                    "INSERT INTO Respondant ([Name], Ip, Date, Dob, Phone, LastName, UserName, Anon) " +
                    "VALUES (@FirstName, @IP, @TheDate, @DOB, @phone, @LName, @UName, @anon)";

                    //set parameters
                    SqlParameter FirstNameParam = new SqlParameter("@FirstName", System.Data.SqlDbType.Text, 100);
                    FirstNameParam.Value = Namee;

                    SqlParameter IpParam = new SqlParameter("@Ip", System.Data.SqlDbType.Text, 100);
                    IpParam.Value = IP;

                    SqlParameter DateParam = new SqlParameter("@TheDate", System.Data.SqlDbType.DateTime, 100);
                    DateParam.Value = currentDate;

                    SqlParameter DobParam = new SqlParameter("@DOB", System.Data.SqlDbType.Date, 100);
                    DobParam.Value = DOB;

                    SqlParameter PhoneParam = new SqlParameter("@phone", System.Data.SqlDbType.Float, 100);
                    PhoneParam.Value = phone;

                    SqlParameter LNameParam = new SqlParameter("@LName", System.Data.SqlDbType.Text, 100);
                    LNameParam.Value = LName;

                    SqlParameter UNameParam = new SqlParameter("@UName", System.Data.SqlDbType.Text, 100);
                    UNameParam.Value = UName;

                    SqlParameter anonParam = new SqlParameter("@anon", System.Data.SqlDbType.Int, 10);
                    anonParam.Value = anon;

                    //add parameters
                    command.Parameters.Add(FirstNameParam);
                    command.Parameters.Add(IpParam);
                    command.Parameters.Add(DateParam);
                    command.Parameters.Add(DobParam);
                    command.Parameters.Add(PhoneParam);
                    command.Parameters.Add(LNameParam);
                    command.Parameters.Add(UNameParam);
                    command.Parameters.Add(anonParam);

                    // Call Prepare after setting the Commandtext and Parameters.
                    command.Prepare();
                    command.ExecuteNonQuery();

                    //get RID and save in session for survey
                    int RID = 0;
                    command3 = new SqlCommand("SELECT * FROM Respondant Where UserName = '" + UName + "'", connection);
                    SqlDataReader RIDReader = command3.ExecuteReader();
                    while (RIDReader.Read())
                    {
                        RID = Int32.Parse(RIDReader["RID"].ToString());

                    }
                        ////get RID and save in session for survey
                        //

                        //command3.CommandText = command2.CommandText;
                        //SqlParameter userParam2 = new SqlParameter("@user", System.Data.SqlDbType.Text, 100);
                        //userParam2.Value = UName;
                        //command3.Parameters.Add(userParam2);
                        //command3.Prepare();
                        //command3.ExecuteNonQuery();

                        //reader = command3.ExecuteReader();
                        //while (reader.Read())
                        //{
                        //    RID = Int32.Parse(reader["RID"].ToString());
                        //}
                        userInfo = (List<string>)Session["Info"];
                        userInfo.Add(RID.ToString());

                        foreach (string c in userInfo)
                                    System.Diagnostics.Debug.WriteLine(c);

                            Session["info"] = userInfo;

                        Response.Redirect("WebForm1.aspx");
                    }
                catch (Exception) { }
            }
            connection.Close();
        }

    }
}
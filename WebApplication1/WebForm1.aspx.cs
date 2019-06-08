using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//ADD THESE
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace WebApplication1
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get current question number
            int currentQuestion = 1;
            string ipAdress = Request.UserHostAddress;                          //get user ip adress
            string date = DateTime.Now.ToString("dd-MM-YYYY");                  //get year
            if (HttpContext.Current.Session["questionNumber"] == null)
            {
                HttpContext.Current.Session["questionNumber"] = 1; //then set it
            }
            else
                currentQuestion = (int)HttpContext.Current.Session["questionNumber"];

            //get user infor from register session
            if (Session["info"] == null)
            {
                System.Diagnostics.Debug.WriteLine("something is wrong :/");
            }
            else
            {
                List<string> userInfo = new List<string>();
                userInfo = (List<string>)Session["Info"];
                foreach (string c in userInfo)
                    System.Diagnostics.Debug.WriteLine(c);
            }


            //get current question from DB
            SqlConnection connection;
            SqlCommand command;

            //testConnectionString from webconfig
            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;

            connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            connection.Open();//open the sql connection using the connection string info

            //just setup a basic sql command (referencing the connection)
            try
            {
                command = new SqlCommand("SELECT * FROM Question, Type WHERE Question.TID = Type.TID AND Question.QID = " + currentQuestion, connection);

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    string questionText = reader["Text"].ToString();
                    string questionType = reader["Name"].ToString().ToLower();
                    currentQuestion = Int32.Parse(reader["QID"].ToString());
                    if (questionType.Equals("textbox"))
                    {
                        //load up textbox controls
                        try
                        {
                            TextQuestionControl textControl = (TextQuestionControl)LoadControl("~/TextQuestionControl.ascx");

                            textControl.ID = "question" + currentQuestion + "Control";
                            textControl.QuestionLabel.Text = questionText;

                            HttpContext.Current.Session["questionNumber123"] = textControl.ID;
                            //add it to the ui
                            PlaceHolder1.Controls.Add(textControl);
                        }
                        catch (Exception) { }

                    }
                    else if (questionType.Equals("radiobox"))
                    {
                        try
                        {
                            RadioButtonQuestionControl radioButtonControl = (RadioButtonQuestionControl)LoadControl("~/RadioButtonQuestionControl.ascx");
                            radioButtonControl.ID = "question" + currentQuestion + "Control";
                            radioButtonControl.QuestionLabel.Text = questionText;

                            HttpContext.Current.Session["questionNumber123"] = radioButtonControl.ID;

                            SqlCommand optionCommand = new SqlCommand("SELECT * FROM [Option] WHERE QID = " + currentQuestion, connection);

                            SqlDataReader optionReader = optionCommand.ExecuteReader();

                            while (optionReader.Read())
                            {
                                //                           text you see,                      value its worth
                                ListItem item = new ListItem(optionReader["Value"].ToString(), optionReader["OID"].ToString());
                                radioButtonControl.QuestionRadioButtonList.Items.Add(item); //add item to option list
                            }

                            //done, add it to placeholder
                            PlaceHolder1.Controls.Add(radioButtonControl);
                        }
                        catch (Exception) { }
                    }
                    else if (questionType.Equals("checkbox"))
                    {
                        try
                        {
                            //load up checkbox controls
                            CheckBoxQuestionControl checkBoxControl = (CheckBoxQuestionControl)LoadControl("~/CheckBoxQuestionControl.ascx");
                            checkBoxControl.ID = "question" + currentQuestion + "Control";
                            checkBoxControl.QuestionLabel.Text = questionText;

                            HttpContext.Current.Session["questionNumber123"] = checkBoxControl.ID;

                            //load up checkbox options/choices to add to checkbox control
                            SqlCommand optionCommand = new SqlCommand("SELECT * FROM [Option] WHERE QID = " + currentQuestion, connection);

                            SqlDataReader optionReader = optionCommand.ExecuteReader();
                            //cycle through all options
                            while (optionReader.Read())
                            {
                                //                           text you see,                      value its worth
                                ListItem item = new ListItem(optionReader["Value"].ToString(), optionReader["OID"].ToString());
                                checkBoxControl.QuestionCheckBoxList.Items.Add(item); //add item to option list
                            }

                            //done, add it to placeholder
                            PlaceHolder1.Controls.Add(checkBoxControl);
                        }
                        catch (Exception) { }
                    }

                }

                connection.Close();
            }
            catch (Exception) { }
        }


        //--------------------------------BUTTON---------------------------

        protected void Button1_Click(object sender, EventArgs e)
        {
            //---------------------------ANSWER LIST---------------------
            List<string> answers = new List<string>();
            //get prev values
            if(Session["Answers"] != null)
                answers = (List<string>)Session["Answers"];



            //string asd = (HttpContext.Current.Session["questionNumber"]).ToString();
            //if (asd == "7")
            //{
            //    foreach (string c in answers)
            //        System.Diagnostics.Debug.WriteLine(c);
            //}

            {
                string BoxID = (HttpContext.Current.Session["questionNumber123"]).ToString();

                try
                {
                //------------------CHECK IF TEXTBOX-----------
                    TextQuestionControl textBoxQuestion = (TextQuestionControl)PlaceHolder1.FindControl(BoxID);
                    if (textBoxQuestion != null)
                    {
                        answers.Add(HttpContext.Current.Session["questionNumber"].ToString());
                        answers.Add(textBoxQuestion.QuestionTextBox.Text);
                        Session["Answers"] = answers;
                    }
                }
                catch (Exception) { }

                //------------------CHECK IF CHECKBOX-----------
                try
                {
                    CheckBoxQuestionControl checkBoxControl = (CheckBoxQuestionControl)PlaceHolder1.FindControl(BoxID);
                    if (checkBoxControl != null)
                    {
                        foreach (ListItem item in checkBoxControl.QuestionCheckBoxList.Items)
                        {
                            if (item.Selected)
                            {
                                answers.Add(HttpContext.Current.Session["questionNumber"].ToString());
                                answers.Add(item.ToString());
                            }
                        }
                    }
                }
                catch (Exception) { }

                //------------------CHECK IF RADIOBOX-----------
                try
                {
                    RadioButtonQuestionControl radioButtonControl = (RadioButtonQuestionControl)PlaceHolder1.FindControl(BoxID);
                    if (radioButtonControl != null)
                    {
                        foreach (ListItem item in radioButtonControl.QuestionRadioButtonList.Items)
                        {
                            if (item.Selected)
                            {
                                answers.Add(HttpContext.Current.Session["questionNumber"].ToString());
                                answers.Add(item.ToString());
                                Session["Answers"] = answers;
                            }
                        }
                    }
                }
                catch (Exception) { }

            }

            //reference Custom User Control.zip to get answer from custom controls on screen

        //Get to next question:
        //Note: extract out to method
        int currentQuestion = 1;//TESTING, CHANGE BACK TO 1!!!!
            if (HttpContext.Current.Session["questionNumber"] == null)
                HttpContext.Current.Session["questionNumber"] = 1; //then set it
            else
                currentQuestion = (int)HttpContext.Current.Session["questionNumber"];

            
            ////ALSO check for bonus questions, if they are, add them into the follow up list
            //List<Int32> followUpQuestions = new List<int>();
            //if (HttpContext.Current.Session["followUpQuestions"] != null)
            //    followUpQuestions = (List<Int32>)HttpContext.Current.Session["followUpQuestions"];

            //if (currentQuestion == 1) //dont do this if statement, check if question actually has follow up questions
            //{
            //    //followUpQuestions.Add(3);
            //    //followUpQuestions.Add(4);
            //}


            
            SqlConnection connection = new SqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;
            connection.ConnectionString = connectionString;

            connection.Open();

            SqlCommand command = null;
            SqlDataReader reader = null;

            //---------------------ADDITIONAL QUESTION STUFF HERE -------------------------
            int questionToGoTO = 0;
            bool overule = false;
            bool anotherOverule = false;

            List<string> tempo = answers;
            tempo.Reverse();

            try
            {
                string userAnswer = "";

                foreach (string c in tempo)
                {
                    if (c == "Fiji" || c == "Hawaii")
                    {
                        userAnswer = c;
                        anotherOverule = true;
                        break;
                    }
                    else if (c == "Travel")
                    {
                        userAnswer = c;                        
                        foreach (string d in tempo)
                        {
                            if (d == "Sport")
                            {
                                overule = true;
                                break;
                            }
                        }                            
                        break;
                    }
                    else if (c == "Fiji" || c == "Hawaii")
                    {
                        userAnswer = c;
                        break;
                    }
                    else if (c == "Comic")
                    {
                        userAnswer = c;
                        break;
                    }
                    else if(c == "NONE")
                    {
                        userAnswer = c;
                        anotherOverule = true;
                        break;
                    }
                    else if(c == "ING")
                    {
                        userAnswer = c;
                        break;
                    }
                }
                tempo.Reverse();
                string userSQLAnswer = "Value = '" + userAnswer + "' AND ";

                try
                {
                    command = new SqlCommand("SELECT TOP 1 * FROM [Option] WHERE " + userSQLAnswer + "QID = " + currentQuestion, connection);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        questionToGoTO = Int32.Parse(reader["NQ"].ToString());

                        //fudging some more
                        if (overule == true)
                        {
                            questionToGoTO = 0;
                        }
                        else if (anotherOverule == true)
                        {
                            questionToGoTO = -1;
                        }
                    }
                }
                catch (Exception) { }
            }
            catch (Exception){}

            try
            {
                command = new SqlCommand("SELECT * FROM Question WHERE QID = " + currentQuestion, connection);
                reader = command.ExecuteReader(); //execute above query


                string dsad = (HttpContext.Current.Session["questionNumber"]).ToString();
                if (dsad != "0")
                {
                    foreach (string c in answers)
                        System.Diagnostics.Debug.WriteLine(c); //-------------testing
                }

                while (reader.Read())
                {
                    //if next question is not null
                    int nextQuestionColumnIndex = reader.GetOrdinal("QID");
                    if (!reader.IsDBNull(nextQuestionColumnIndex))
                    {
                        int nextQuestion = Int32.Parse(reader["QID"].ToString()) + 1;
                        HttpContext.Current.Session["questionNumber"] = nextQuestion; //update session with new question number
                        currentQuestion = nextQuestion;

                        if (questionToGoTO > 0 && questionToGoTO < 12)
                        {
                            nextQuestion = questionToGoTO;
                            HttpContext.Current.Session["questionNumber"] = nextQuestion; //update session with new question number
                            questionToGoTO = 0;
                        }
                        else if (questionToGoTO < 0)
                        {
                            //INSERT INTO SQL
                            List<string> anotherTemp = answers;

                            List<string> userInfo = new List<string>();
                            userInfo = (List<string>)Session["Info"];

                            bool print = false;
                            bool empty = false;
                            int realQuestionNumber = 0;

                            foreach (string c in userInfo)
                                System.Diagnostics.Debug.WriteLine(c);

                            string RID = Int32.Parse(userInfo[7]).ToString();

                            if (empty == false)
                            {
                                foreach (string c in anotherTemp)
                                {
                                    var isNumeric = int.TryParse(c, out int n);
                                    if (isNumeric)
                                    {
                                        realQuestionNumber = Int32.Parse(c);
                                        print = true;
                                    }
                                    else if (c == "")
                                    {
                                        empty = true;
                                    }
                                    else if (c != "" && print == true && empty == false)
                                    {
                                        command = new SqlCommand("INSERT INTO Answer VALUES ("+RID+"," + realQuestionNumber + ",'" + c + "')", connection);
                                        reader = command.ExecuteReader();
                                        print = false;
                                    }
                                }
                            }
                            connection.Close();
                            Response.Redirect("WebForm2.aspx");
                        }
                        //go to same page to reload it properly
                        connection.Close();
                        Response.Redirect("WebForm1.aspx");
                    }
                    else
                    {
                        connection.Close();
                        Response.Redirect("WebForm2.aspx");
                    }
                }
            }
            catch (Exception) { }

            //make sure we close connection at the END
            connection.Close();

        }
    }
}
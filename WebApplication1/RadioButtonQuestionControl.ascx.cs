using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class RadioButtonQuestionControl : System.Web.UI.UserControl
    {
        public Label QuestionLabel
        {
            get
            {
                return questionLabel;
            }
            set
            {
                questionLabel = value;
            }
        }
        public RadioButtonList QuestionRadioButtonList
        {
            get
            {
                return RadioButtonList1;
            }
            set
            {
                RadioButtonList1 = value;
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
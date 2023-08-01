using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Aris.DivisionControls
{
    public partial class DivisionHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                name.Text= Session["Name"].ToString();
              
            }

        }
    }
}
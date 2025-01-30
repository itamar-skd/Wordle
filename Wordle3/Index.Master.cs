using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wordle
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string login = "<p class=\"navbar-items\"><span><a href=\"Register.aspx\">Register</a></span><span><a href=\"Login.aspx\">Login</a></span></p>", logout = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["username"] is null))
            {
                login = "";
                logout = "<p class=\"navbar-items\"><span><a href=\"Logout.aspx\">Logout</a></span></p>";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wordle
{
    public partial class Login : System.Web.UI.Page
    {
        public string message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] is null)
            {
                if (Request.Form["submit"] != null)
                {
                    string username = Request.Form["username"];
                    string password = Request.Form["password"];
                    if (username.Length != 0 && password.Length != 0)
                    {
                        string query = $"SELECT password FROM Users WHERE username='{username}'";
                        string pass = SQLHelper.SelectScalarToString(query);
                        if (pass.Length != 0 && password == pass)
                        {
                            Session["username"] = username;
                            Response.Redirect("Wordle.aspx");
                        }
                        else message = "ERROR: One of the fields (or more) were incorrect.";
                    }
                    else message = "ERROR: One of the fields was empty.";
                }
            }
            else Response.Redirect("Wordle.aspx");
        }
    }
}
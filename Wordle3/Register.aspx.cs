using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wordle3
{
    public partial class Register : System.Web.UI.Page
    {
        public string message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] is null)
            {
                if (!(Request.Form["submit"] is null))
                {
                    string username = Request.Form["username"];
                    string password = Request.Form["password"];

                    if (username.Length != 0 && password.Length != 0)
                    {
                        if (password.Length >= 8)
                        {
                            string query = $"SELECT username FROM [Users] WHERE username='{username}'";
                            if (SQLHelper.SelectScalarToString(query).Length > 0)
                                message = "ERROR: The username used is already taken.";
                            else
                            {
                                string emptyRow = string.Concat(Enumerable.Repeat("<span class=\"char\"></span>", 5));
                                query = $"insert into [Users] values ('{username}', '{password}', {DateTime.Now.Day}, {DateTime.Now.Month}, {DateTime.Now.Year}, '{emptyRow}', '{emptyRow}', '{emptyRow}', '{emptyRow}', '{emptyRow}', 0, 0)";
                                int affected = SQLHelper.DoQuery(query);
                                Session["username"] = username;
                                Response.Redirect("Wordle.aspx");
                            }
                        }
                        else message = "ERROR: Remember, a good password is at least 8 characters in length!";
                    }
                    else message = "ERROR: One of the fields was empty.";
                }
            }
            else Response.Redirect("Wordle.aspx");
        }
    }
}
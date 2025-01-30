using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wordle
{
    public partial class Stats : System.Web.UI.Page
    {
        public string word = "", first = "", second = "", third = "", fourth = "", fifth = "", failed = "";
        private string getStats(string position)
        {
            string query = $"SELECT {position} FROM [Words] WHERE word='{Session["word"].ToString()}'";
            int amount = SQLHelper.SelectScalarToInt32(query);

            query = $"SELECT total FROM [Words] WHERE word='{Session["word"].ToString()}'";
            int total = SQLHelper.SelectScalarToInt32(query);

            string[] attemptWords = { "first", "second", "third", "fourth", "fifth", "failed" };
            int userAttempt = SQLHelper.SelectScalarToInt32($"SELECT row FROM [Users] WHERE username='{Session["username"].ToString()}'");
            string you = "";
            if (attemptWords[userAttempt] == position) you = "<span style=\"color: #60d394;\"> (YOU!)</span>";

            string attempt = " try";
            if (position == "failed") attempt = "";

            return $"<span>{position}{attempt}{you}</span><span style=\"float: right\"><progress value=\"{amount}\" max=\"{total}\"></progress><span style=\"margin-left: 5px\">{amount}</span></span>";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["username"] is null))
            {
                string completedQuery = $"SELECT username FROM [Users] WHERE username='{Session["username"].ToString()}' AND completed=1 AND day={DateTime.Now.Day} AND month={DateTime.Now.Month} AND year={DateTime.Now.Year}";
                string completed = SQLHelper.SelectScalarToString(completedQuery);
                if (completed.Length > 0)
                {
                    if (Session["word"] is null) Session["word"] = SQLHelper.SelectScalarToString($"SELECT word FROM [Words] WHERE day={DateTime.Now.Day} AND month={DateTime.Now.Month} AND year={DateTime.Now.Year}");
                    string query = $"SELECT total FROM [Words] WHERE word='{Session["word"].ToString()}";
                    int total = SQLHelper.SelectScalarToInt32(query);

                    word = Session["word"].ToString().ToUpper();
                    first = getStats("first");
                    second = getStats("second");
                    third = getStats("third");
                    fourth = getStats("fourth");
                    fifth = getStats("fifth");
                    failed = getStats("failed");
                }
                else Response.Redirect("Wordle.aspx");
            }
            else Response.Redirect("Login.aspx");
        }
    }
}
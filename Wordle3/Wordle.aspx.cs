using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wordle3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string[] words = {
            "aside","album","above","awful","axile","among","anvil","avoid","alive","angry","agony","amber",
            "banjo","blaze","boink","block","boxed","blown","birch","brick","bimbo","bench","baked","bambo",
            "chump","climb","chunk","crazy","cubic","crumps","combs","cheap","chief","chips","cloak","clove",
            "dizzy","daily","darks","dozen","dummy","decoy","drink","drank","demon","dying","diver","drums",
            "enjoy","emoji","eject","equip","exams","elvan","exist","elbow","evoke","extra","ember","evils",
            "flows","flame","fancy","fetch","freez","flowz","folks","fully","fishy","flows","faked","flake",
            "grovy","giver","gazed","geeky","ditch","gravy","glove","given","goofy","graph","gamed","ghoul",
            "hacky","hacky","hijab","hoppy","hippo","hokey","happy","hatch","hyped","haven","heigh","holly",
            "itchy","input","image","igloo","irony","irony","ideal","items","issue","irons","icons","inked",
            "jeeze","junky","jewel","javas","jewel","jocks","jumpy","joker","juicy","judge","jukes","jolts",
            "kicks","knock","kebab","kneel","knife","carma","kinky","kiddy","kruby","kinds","kissy","zoner",
            "lazed","lumpy","latex","licks","level","lived","lobby","loved","lough","lying","latex","lucks",
            "mixer","mumbo","major","match","march","money","mamba","milky","mercy","muddy","movie","mowed",
            "ninja","navvy","nancy","novel","naked","named","night","nerve","nuked","nerdy","never","nanny",
            "offer","organ","omega","olive","ought","outer","otter","outro","oiled","oreos","older","opens",
            "quick","quack","quote","quits","quash","query","quill","rough","wheat","young","yucks","zinky",
            "radio","relax","remix","rocks","razor","rebuy","rival","river","racon","runes","realm","raven",
            "sized","smoke","squat","spunk","sizes","stuck","skunk","spawn","speak","stock","stick","stuff",
            "toxic","truck","tummy","trick","treat","trump","topic","tocuh","twerk","talks","teach","throw",
            "unzip","upper","umper","uncle","unarm","unsub","unfit","under","unfit","urban","unity","urges",
            "venom","vital","voice","valid","vault","value","vegan","virgo","valor","virus","video","viral",
            "wacky","wheep","winks","waver","woman","wagon","whale","whole","width","while","wrong","waldo",
            "yards","youth","youre","yacht","zebra",

        };
        public string message = "", rowOne = "", rowTwo = "", rowThree = "", rowFour = "", rowFive = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["username"] is null))
            {
                if (Session["word"] is null)
                {
                    string wordQuery = $"SELECT word FROM [Words] WHERE day={DateTime.Now.Day} AND month={DateTime.Now.Month} AND year={DateTime.Now.Year}";
                    string word = SQLHelper.SelectScalarToString(wordQuery);
                    if (!(word.Length > 0))
                    {
                        Random rand = new Random();
                        word = words[rand.Next(words.Length)];
                        SQLHelper.DoQuery($"INSERT INTO [Words] values ('{word}', {DateTime.Now.Day}, {DateTime.Now.Month}, {DateTime.Now.Year}, 0, 0, 0, 0, 0, 0, 0)");
                    }

                    Session["word"] = word;
                }

                string completedQuery = $"SELECT username FROM [Users] WHERE username='{Session["username"].ToString()}' AND completed=1 AND day={DateTime.Now.Day} AND month={DateTime.Now.Month} AND year={DateTime.Now.Year}";
                string completed = SQLHelper.SelectScalarToString(completedQuery);
                if (completed.Length == 0)
                {
                    string userQuery = $"SELECT username FROM [Users] WHERE username='{Session["username"].ToString()}' AND day={DateTime.Now.Day} AND month={DateTime.Now.Month} AND year={DateTime.Now.Year}";
                    string user = SQLHelper.SelectScalarToString(userQuery);
                    if (!(user.Length > 0))
                    {
                        string emptyRow = string.Concat(Enumerable.Repeat("<span class=\"char\"></span>", 5));
                        string updateQuery = $"UPDATE [Users] SET day={DateTime.Now.Day}, month={DateTime.Now.Month}, year={DateTime.Now.Year}, rowOne='{emptyRow}', rowTwo='{emptyRow}', rowThree='{emptyRow}', rowFour='{emptyRow}', rowFive='{emptyRow}', row=0, completed=0 WHERE username='{Session["username"].ToString()}'";
                        SQLHelper.DoQuery(updateQuery);
                    }

                    rowOne = SQLHelper.SelectScalarToString($"SELECT rowOne FROM [Users] WHERE username='{Session["username"].ToString()}'");
                    rowTwo = SQLHelper.SelectScalarToString($"SELECT rowTwo FROM [Users] WHERE username='{Session["username"].ToString()}'");
                    rowThree = SQLHelper.SelectScalarToString($"SELECT rowThree FROM [Users] WHERE username='{Session["username"].ToString()}'");
                    rowFour = SQLHelper.SelectScalarToString($"SELECT rowFour FROM [Users] WHERE username='{Session["username"].ToString()}'");
                    rowFive = SQLHelper.SelectScalarToString($"SELECT rowFive FROM [Users] WHERE username='{Session["username"].ToString()}'");

                    if (Session["row"] is null) Session["row"] = SQLHelper.SelectScalarToInt32($"SELECT row FROM [Users] WHERE username='{Session["username"].ToString()}'");

                    if (!(Session["row"] is null) && Convert.ToInt32(Session["row"]) < 5)
                    {
                        if (!(Request.Form["submit"] is null))
                        {
                            string[] numberWords = { "One", "Two", "Three", "Four", "Five" };
                            string input = Request.Form["input"];
                            string word = Session["word"].ToString();
                            string row = numberWords[Convert.ToInt32(Session["row"])];
                            string final = "";
                            int correct = 0;

                            if (input.Length == 5)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    if (word.Contains(input[i]))
                                    {
                                        if (word[i] == input[i])
                                        {
                                            final += $"<span class=\"char green\">{input[i]}</span>";
                                            correct++;
                                        }
                                        else final += $"<span class=\"char yellow\">{input[i]}</span>";
                                    }
                                    else final += $"<span class=\"char gray\">{input[i]}</span>";
                                }

                                if (row == "One") rowOne = final;
                                else if (row == "Two") rowTwo = final;
                                else if (row == "Three") rowThree = final;
                                else if (row == "Four") rowFour = final;
                                else rowFive = final;

                                if (correct == 5)
                                {
                                    string updateUserQuery = $"UPDATE [Users] SET row{row}='{final}', completed=1 WHERE username='{Session["username"].ToString()}'";
                                    SQLHelper.DoQuery(updateUserQuery);

                                    string attempt;
                                    if (row == "One") attempt = "first";
                                    else if (row == "Two") attempt = "second";
                                    else if (row == "Three") attempt = "third";
                                    else if (row == "Four") attempt = "fourth";
                                    else attempt = "Fifth";
                                    string updateWordQuery = $"UPDATE [Words] SET {attempt} = {attempt} + 1, total = total + 1 WHERE word='{Session["word"].ToString()}'";
                                    SQLHelper.DoQuery(updateWordQuery);

                                    System.Threading.Thread.Sleep(2000);
                                    Response.Redirect("Stats.aspx");
                                }
                                else
                                {
                                    Session["row"] = Convert.ToInt32(Session["row"]) + 1;
                                    string complete = "";
                                    if (Convert.ToInt32(Session["row"]) == 5) complete = ", completed = 1";
                                    string updateQuery = $"UPDATE [Users] SET row{row}='{final}', row = row + 1{complete} WHERE username='{Session["username"].ToString()}'";
                                    SQLHelper.DoQuery(updateQuery);

                                    if (Convert.ToInt32(Session["row"]) == 5)
                                    {
                                        string updateUserQuery = $"UPDATE [Users] SET completed=1 WHERE username='{Session["username"].ToString()}'";
                                        SQLHelper.DoQuery(updateUserQuery);
                                        string updateWordQuery = $"UPDATE [Words] SET failed = failed + 1, total = total + 1 WHERE word='{Session["word"].ToString()}'";
                                        SQLHelper.DoQuery(updateWordQuery);
                                        System.Threading.Thread.Sleep(2000);
                                        Response.Redirect("Stats.aspx");
                                    }
                                }
                            }
                        }
                    }
                } else
                {
                    Response.Redirect("Stats.aspx");
                }   
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
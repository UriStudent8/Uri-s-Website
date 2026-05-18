using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    [Serializable]
    public class PhishingCase
    {
        public string ImageUrl { get; set; }
        public bool IsFishy { get; set; }
        public string Explanation { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        admin.Visible = false;

        if ((bool)Session["isLoggedIn"])
        {
            LoginLogout.HRef = "Logout.aspx";
            LoginLogout.InnerText = "Hello: " + Session["userName"] + "  (Logout)";
            members.Visible = true;

            if ((bool)Session["isAdmin"])
            {

                admin.Visible = true;
            }
            
        }
        else
        {
            LoginLogout.HRef = "Login.aspx";
            LoginLogout.InnerText = "Login";
            members.Visible= false;
        }

        if (!IsPostBack)
        {
            // Display the current server date in the header
            DateLabel.Text = DateTime.Now.ToString("d");

            // Set the image based on the day of the week
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            string imagePath = GetImagePathForDay(dayOfWeek);

            DayImage.ImageUrl = imagePath;
            DayImage.AlternateText = "Image for dayOfWeek" + dayOfWeek;

            if (!IsPostBack)
            {
                LoadRandomCase();
            }
        }


    }



    private string GetImagePathForDay(string dayOfWeek)
    {
        string path = "images/week/";
        // Use different image paths for each day
        switch (dayOfWeek)
        {
            case "Sunday":
                return path + "sunday.png";
            case "Monday":
                return path + "monday.png";
            case "Tuesday":
                return path + "tuesday.png";
            case "Wednesday":
                return path + "wednesday.png";
            case "Thursday":
                return path + "thursday.png";
            case "Friday":
                return path + "friday.png";
            case "Saturday":
                return path + "saturday.png";
            default:
                return path + "default.png"; // Fallback image
        }
    }

    private void LoadRandomCase()
    {
        List<PhishingCase> cases = new List<PhishingCase>
        {
            new PhishingCase { ImageUrl = "images/p1.png", IsFishy = true, Explanation = "הכתובת לא תואמת לאתר הרשמי." },
            new PhishingCase { ImageUrl = "images/s1.png", IsFishy = false, Explanation = "זהו מייל אבטחה תקין לחלוטין." }
        };

        Random rnd = new Random();
        var selectedCase = cases[rnd.Next(cases.Count)];

        imgEmail.ImageUrl = selectedCase.ImageUrl;
        ViewState["CurrentCase"] = selectedCase;
    }

    // --- 3. פונקציית הכפתור (מחוץ ל-Page_Load) ---
    protected void CheckAnswer_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentCase"] != null)
        {
            Button btn = (Button)sender;
            string userChoice = btn.CommandArgument;
            PhishingCase currentCase = (PhishingCase)ViewState["CurrentCase"];

            bool isCorrect = (userChoice == "fishy" && currentCase.IsFishy) ||
                             (userChoice == "safe" && !currentCase.IsFishy);

            pnlResult.Visible = true;
            lblExplanation.Text = currentCase.Explanation;
            lblResult.Text = isCorrect ? "🏆 כל הכבוד! צדקת." : "❌ טעות, זה היה מסוכן.";
            lblResult.ForeColor = isCorrect ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }
    }
}





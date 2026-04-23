using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((bool)Session["isLoggedIn"])
        {
            LoginLogout.HRef = "Logout.aspx";
            LoginLogout.InnerText = "Hello: " + Session["userName"] + "  (Logout)";
        }
        else
        {
            LoginLogout.HRef = "Login.aspx";
            LoginLogout.InnerText = "Login";
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

}

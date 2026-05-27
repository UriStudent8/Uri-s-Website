using System;
using System.Collections.Generic;
using System.Web;
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

   
    private List<PhishingCase> GetAntiPhishingCases()
    {
        return new List<PhishingCase>
        {
            new PhishingCase {
                ImageUrl = "Images/Bonus/Apple.Legit.jpeg",
                IsFishy = false,
                Explanation = "Legitimate! This is a standard Apple 2FA SMS. It doesn't contain any links and explicitly warns you not to share the code."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/Poalim.phishing.jpeg",
                IsFishy = true,
                Explanation = "Phishing! Look at the URL: 'bank-poalim-security.ru'. Banks will never use a Russian (.ru) domain. It also uses urgent language to panic you."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/EmailWeekly.legit.jpeg",
                IsFishy = true,
                Explanation = "Phishing! The link points to 'telegram-verification.com', which is a fake domain. Official Telegram support will never message you directly threatening a ban within 24 hours."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/Paypal.phishing.jpeg",
                IsFishy = true,
                Explanation = "Phishing! The sender email uses a misspelled domain. Large companies like PayPal don't make typos in their official email domains."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/TelegramNews.legit.jpeg",
                IsFishy = false,
                Explanation = "Legitimate! This is a regular tech channel/group post sharing news with a standard link. It does not ask for personal details."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/MicrosoftOutlook.phishing#5.jpeg",
                IsFishy = true,
                Explanation = "Phishing! Notice the typos in the sender's name 'Mircosoft' and the fake alert domain 'mircosoft-alerts.com'."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/GmailCode.legit#4.jpeg",
                IsFishy = false,
                Explanation = "Legitimate! A standard 2FA login code from Google. No external links, just the code for you to enter safely."
            },

            new PhishingCase {
            ImageUrl = "Images/Bonus/GoodlePay.phishing.jpeg", 
            IsFishy = true,
            Explanation = "Phishing! A classic credit card scam disguised as a Google Pay update, redirecting you to a fraudulent 'gpay-update.co.il' URL."
          },

            new PhishingCase {
                ImageUrl = "Images/Bonus/IsraelPost.phishing#4.jpeg",
                IsFishy = true,
                Explanation = "Phishing! The link goes to 'israelpost-delivery.com'. Israel Post is a government body and its official domain must end with '.gov.il'."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/Cellcoom.legit.jpeg",
                IsFishy = false,
                Explanation = "Legitimate! A routine service text from Cellcom informing the client about their data plan ending."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/Microsoft.legit#6.jpeg",
                IsFishy = false,
                Explanation = "Legitimate! An official automated system notification inside Telegram or Microsoft informing you of a successful new login."
            },
            new PhishingCase {
                ImageUrl = "Images/Bonus/Microsoft.legit#6.jpeg",
                IsFishy = false,
                Explanation = "Legitimate! An official automated system notification inside Telegram or Microsoft informing you of a successful new login."
            },
             new PhishingCase {
                ImageUrl = "EmailWeekly.legitReal.jpeg",
                IsFishy = false,
                Explanation = "Legitimate! This is a routine Google Security account summary. It just informs you about your status and doesn't demand immediate action."
            }



        };
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        admin.Visible = false;

        if (Session["isLoggedIn"] != null && (bool)Session["isLoggedIn"])
        {
            LoginLogout.HRef = "Logout.aspx";
            LoginLogout.InnerText = "Hello: " + Session["userName"] + "  (Logout)";
            members.Visible = true;

            if (Session["isAdmin"] != null && (bool)Session["isAdmin"])
            {
                admin.Visible = true;
            }
        }
        else
        {
            LoginLogout.HRef = "Login.aspx";
            LoginLogout.InnerText = "Login";
            members.Visible = false;
        }

        if (!IsPostBack)
        {
            DateLabel.Text = DateTime.Now.ToString("d");

            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            string imagePath = GetImagePathForDay(dayOfWeek);
            DayImage.ImageUrl = imagePath;
            DayImage.AlternateText = "Image for dayOfWeek " + dayOfWeek;

           
            LoadRandomCase();
        }
    }

    private string GetImagePathForDay(string dayOfWeek)
    {
        string path = "images/week/";
        switch (dayOfWeek)
        {
            case "Sunday": return path + "sunday.png";
            case "Monday": return path + "monday.png";
            case "Tuesday": return path + "tuesday.png";
            case "Wednesday": return path + "wednesday.png";
            case "Thursday": return path + "thursday.png";
            case "Friday": return path + "friday.png";
            case "Saturday": return path + "saturday.png";
            default: return path + "default.png";
        }
    }

    private void LoadRandomCase()
    {
        var cases = GetAntiPhishingCases();
        Random rnd = new Random();
        int randomIndex = rnd.Next(cases.Count);

        
        ViewState["CurrentCaseIndex"] = randomIndex;

        DisplayCase(cases[randomIndex]);

        
        pnlResult.Visible = false;

       
        btnSafe.Enabled = true;
        btnFishy.Enabled = true;
    }

    private void DisplayCase(PhishingCase pc)
    {
        
        string directoryPath = "~/Images/Bonus/";

        
        string fileName = pc.ImageUrl.Replace("Images/Bonus/", "");

      
        string encodedFileName = Server.UrlEncode(fileName);

       
        imgEmail.ImageUrl = directoryPath + encodedFileName;
    }

    protected void CheckAnswer_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentCaseIndex"] != null)
        {
            int currentIndex = (int)ViewState["CurrentCaseIndex"];
            var cases = GetAntiPhishingCases();
            PhishingCase currentCase = cases[currentIndex];

            Button btn = (Button)sender;
            string userChoice = btn.CommandArgument;

            
            bool isCorrect = (userChoice == "fishy" && currentCase.IsFishy) ||
                             (userChoice == "safe" && !currentCase.IsFishy);

            pnlResult.Visible = true;
            lblExplanation.Text = currentCase.Explanation;

            if (isCorrect)
            {
                lblResult.Text = "🏆 Correct! Well done.";
                lblResult.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblResult.Text = "❌ Wrong!";
                lblResult.ForeColor = System.Drawing.Color.Red;
            }

           
            btnSafe.Enabled = false;
            btnFishy.Enabled = false;
        }
    }
}
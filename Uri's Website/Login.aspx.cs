using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        


        if (IsPostBack)
        {

            int result = GetUserTypeFromDB(Request.Form["userName"], Request.Form["password"]);

            if (result > 0)
            { 
                Session["userName"] = Request.Form["userName"];
                Session["isLoggedIn"] = true;

                Response.Redirect("Default.aspx");
            }
            else
            {
                LoginResult.InnerText = "שם משתמש או סיסמה אינם נכונים";
            }
        }


    }

    //returns:
    //0 - if user is not valid
    //1 - is user is valid
    private int GetUserTypeFromDB(string userName, string password)
    {
        string dbPath = this.MapPath("App_Data/Database.mdf");
        DAL dal = new DAL(dbPath);

        string sqlQuery = "SELECT * FROM Users " +
                            "WHERE user_name = '" + userName +
                            "' AND pswd = '" + password + "'";

        DataTable dt = dal.GetDataTable(sqlQuery);

        if (dt.Rows.Count == 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

}
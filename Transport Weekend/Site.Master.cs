using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Transport_Weekend
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            menuUserCurrent.Visible = false;
            menuLogin.Visible = true;
            menuSubordinates.Visible = false;
            menuAddModifALL.Visible = false;
            menuAddModidSOME.Visible = false;   
            menuTemporaryTransport.Visible = false;
            menuReportPage.Visible = false;
            menuLogout.Visible = false;
            if (Session["login"] != null)
            {
                menuLogin.Visible = false;
                string temprole = "";
                string tempUserName = "";
                string tempId = "";
                tempId = Request.Cookies["userdata"].Value;

                GetPrivilage getPrivilage = new GetPrivilage();
                GetUserName getUserName = new GetUserName();

                temprole = getPrivilage.GetRights(tempId);
                tempUserName = getUserName.GetName(tempId);

                if (temprole == "ADMIN")
                {
                    menuUserCurrent.Visible = true;
                    textPlace.InnerText= "Bun venit, " + tempUserName;
                    menuLogin.Visible = false;
                    menuSubordinates.Visible = true;
                    menuAddModifALL.Visible = true;
                    menuAddModidSOME.Visible = true;
                    menuTemporaryTransport.Visible = true;
                    menuReportPage.Visible = true;
                    menuLogout.Visible = true;
                }
                else if (temprole == "SEF SCHIMB")
                {
                    menuUserCurrent.Visible = true;
                    textPlace.InnerText = "Bun venit, " + tempUserName;
                    menuLogin.Visible = false;
                    menuSubordinates.Visible = true;
                    menuAddModifALL.Visible = false;
                    menuAddModidSOME.Visible = true;
                    menuTemporaryTransport.Visible = true;
                    menuReportPage.Visible = false;
                    menuLogout.Visible = true;
                }
                else
                {
                    menuUserCurrent.Visible = false;
                    menuLogin.Visible = true;
                    menuSubordinates.Visible = false;
                    menuAddModifALL.Visible = false;
                    menuAddModidSOME.Visible = false;
                    menuTemporaryTransport.Visible = false;
                    menuReportPage.Visible = false;
                    menuLogout.Visible = false;
                }


            }


        }
    }
}
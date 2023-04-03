using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Transport_Weekend
{
    public partial class Logout : System.Web.UI.Page
    {

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("Login.aspx");
                Response.Cookies["userdata"].Expires = DateTime.Now.AddDays(-1D);
                Session.Abandon();
                Session.Clear();
                Response.Cookies.Clear();
                FormsAuthentication.SignOut();
                HttpCookie cooku = new HttpCookie("userdata");
                // IsPersistent = false;
                cooku.Expires = DateTime.Now.AddDays(1);
                cooku.Value = "";
                Response.Cookies.Add(cooku);

                Response.Redirect("~/Default.aspx");

                //SiteMaster s = new SiteMaster();

                //FormsAuthentication.RedirectToLoginPage();
            }
            catch (Exception)
            {
                MsgBox("Logout unsuccesfull", this.Page, this);
            }
        }
    }
}
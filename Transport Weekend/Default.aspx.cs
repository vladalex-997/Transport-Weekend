using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Transport_Weekend
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Cookies["userdata"].Expires = DateTime.Now.AddDays(-1D);

                string UniqueId = "";
                string UserPassword = "";

                string VerifyId = "";
                string VerifyPass = "";
                string VerifyActive = "";

                UniqueId = inputId.Value;
                UserPassword = inputPassword.Value;

                Database databaseObject = new Database();

                string queryLogin = "SELECT UniqueId,UserPassword,UserStatus from Users where UniqueId=@UniqueId AND UserPassword=@UserPassword";
                SqlCommand cmdLogin = new SqlCommand(queryLogin, databaseObject.myConnection);

                cmdLogin.Parameters.AddWithValue("@UniqueId", UniqueId);
                cmdLogin.Parameters.AddWithValue("@UserPassword", UserPassword);

                databaseObject.OpenConnection();
                SqlDataReader reader = cmdLogin.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        VerifyId = reader[0].ToString();
                        VerifyPass = reader[1].ToString();
                        VerifyActive = reader[2].ToString();
                    }
                }
                reader.Close();

                databaseObject.CloseConnection();

                
                    if (UniqueId == VerifyId && UserPassword == VerifyPass)
                    {
                        if (VerifyActive == "ACTIVE")
                        {
                        HttpCookie cooku = new HttpCookie("userdata");
                        cooku.Expires = DateTime.Now.AddDays(1);
                        cooku.Value = UniqueId;
                        Response.Cookies.Add(cooku);
                        Session["login"] = UniqueId;

                        string temprole = "";

                        GetPrivilage getPrivilage = new GetPrivilage();
                       

                        temprole = getPrivilage.GetRights(UniqueId);

                        if (temprole == "ADMIN")
                        {
                            Response.Redirect("~/ReportPage.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/Subordinates.aspx");
                        }

                       
                        }
                        else
                        {
                        MsgBox("User is no longer active", this.Page, this);
                        }
                       
                    }
                    else
                    {
                        MsgBox("Incorrect ID or Password", this.Page, this);
                    }
               

                

            }
            catch (Exception ex) 
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
           



        }
    }
}
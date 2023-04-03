using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Transport_Weekend
{
    public partial class AddModifALL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject= new Database();
                databaseObject.OpenConnection();
                string query = "SELECT * from Users";
                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);

                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataSet ds = new DataSet();
                daquery.Fill(ds);
                GridViewUsers.DataSource = ds;
                GridViewUsers.DataBind();
                databaseObject.CloseConnection();
            } 
            catch(Exception ex)
            {
                MsgBox(ex.ToString(),this.Page,this);
            }
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnModif_Click(object sender, EventArgs e)
        {

        }

        protected void btnAutoCom_Click(object sender, EventArgs e)
        {

        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {

        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Transport_Weekend
{
    public partial class Subordonates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        public void RefreshGrid()
        {
            string loggedin = Request.Cookies["userdata"].Value;
            try
            {
                string active = "ACTIVE";
                Database databaseObject = new Database();
                databaseObject.OpenConnection();
                string query = "SELECT * FROM Employees WHERE Superior = @Superior AND UserStatus = @Active";
                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);
                myquerytab.Parameters.AddWithValue("@Superior", loggedin);
                myquerytab.Parameters.AddWithValue("@Active", active);

                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataSet ds = new DataSet();
                daquery.Fill(ds);
                GridViewEmployees.DataSource = ds;
                GridViewEmployees.DataBind();
                databaseObject.CloseConnection();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e) 
        {
            RefreshGrid();
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }
}
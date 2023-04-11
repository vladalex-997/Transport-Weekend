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
                if (!Page.IsPostBack)
                {
                    Database databaseObject=new Database();
                    databaseObject.OpenConnection();
                    string Routestatus = "ACTIVE";
                    string query = "SELECT RouteName from EmployeeRoutes WHERE RouteStatus=@RouteStatus";
                    SqlCommand cmd = new SqlCommand(query, databaseObject.myConnection);
                    cmd.Parameters.AddWithValue("@RouteStatus", Routestatus);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    SelectRouteAdd.DataSource = ds.Tables[0];
                    SelectRouteAdd.DataTextField = ds.Tables[0].Columns["RouteName"].ToString();
                    SelectRouteAdd.DataValueField = ds.Tables[0].Columns["RouteName"].ToString();

                    SelectRouteAdd.DataBind();

                    SelectRouteMod.DataSource = ds.Tables[0];
                    SelectRouteMod.DataTextField = ds.Tables[0].Columns["RouteName"].ToString();
                    SelectRouteMod.DataValueField = ds.Tables[0].Columns["RouteName"].ToString();

                    SelectRouteMod.DataBind();

                    databaseObject.CloseConnection();
                }

            }
            catch(Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
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
            try
            {
                Database databaseObject = new Database();
                string Id = inputIdAdd.Value;
                string Password=inputPassAdd.Value; 
                string FullName=inputNameAdd.Value;
                string UserRole=SelectRoleAdd.Value;
                string UserStatus=SelectStatusAdd.Value;    
                string EmployeeRoute=SelectRouteAdd.Value;
                string Company=SelectCompanyAdd.Value;
                bool verif = (string.IsNullOrEmpty(Company) || string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(UserRole) || string.IsNullOrEmpty(UserStatus) || string.IsNullOrEmpty(EmployeeRoute));

                if (verif==false)
                {
                  
                    string queryverif = "SELECT * from Users WHERE UniqueId=@UniqueId";
                    SqlCommand cmdverif = new SqlCommand(queryverif,databaseObject.myConnection);
                    cmdverif.Parameters.AddWithValue("@Uniqueid", Id);
                    databaseObject.OpenConnection();
                    var resverif=cmdverif.ExecuteScalar();
                    databaseObject.CloseConnection(); 
                    
                    if(resverif is null)
                    {
                        string queryadd = "INSERT INTO Users(UniqueId,UserPassword,UserNameandSurname,UserRole,UserStatus,EmployeeRoute,Company) " +
                        "VALUES (@UniqueId,@UserPassword,@UserNameandSurname,@UserRole,@UserStatus,@EmployeeRoute,@Company)";
                        SqlCommand cmd = new SqlCommand(queryadd, databaseObject.myConnection);
                        cmd.Parameters.AddWithValue("@Uniqueid", Id);
                        cmd.Parameters.AddWithValue("@UserPassword", Password);
                        cmd.Parameters.AddWithValue("@UserNameandSurname", FullName);
                        cmd.Parameters.AddWithValue("@UserRole", UserRole);
                        cmd.Parameters.AddWithValue("@UserStatus", UserStatus);
                        cmd.Parameters.AddWithValue("@EmployeeRoute", EmployeeRoute);
                        cmd.Parameters.AddWithValue("@Company", Company);

                        databaseObject.OpenConnection();
                        var result = cmd.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                        MsgBox("User added succesfully !", this.Page, this);
                        inputIdAdd.Value = "";
                        inputPassAdd.Value = "";
                        inputNameAdd.Value = "";
                        SelectRoleAdd.SelectedIndex = 0;
                        SelectRouteAdd.SelectedIndex = 0;
                        SelectStatusAdd.SelectedIndex = 0;
                        SelectCompanyAdd.SelectedIndex = 0;
                    }
                    else
                    {
                        MsgBox("The User already exists !!!", this.Page, this);
                    }

                }
                else
                {
                    MsgBox("Complete all fields !!!", this.Page, this);
                }

            }
            catch(Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

        protected void btnModif_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = inputIdMod.Value;

                string loggedin = Request.Cookies["userdata"].Value;

                if (loggedin != Id)
                {
                    Database databaseObject=new Database();
                    string Password=inputPassMod.Value;
                    string FullName=inputNameMod.Value;
                    string UserRole = SelectRoleMod.Value;
                    string UserStatus = SelectStatusMod.Value;
                    string EmployeeRoute = SelectRouteMod.Value;
                    string Company = SelectCompanyMod.Value;
                    bool verif = (string.IsNullOrEmpty(Company) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(UserRole) || string.IsNullOrEmpty(UserStatus) || string.IsNullOrEmpty(EmployeeRoute));
                    if (verif == false)
                    {
                        string queryup = "UPDATE Users SET UserPassword=@UserPassword, UserNameandSurname=@UserNameandSurname, " +
                            "UserRole=@UserRole, UserStatus=@UserStatus, EmployeeRoute=@EmployeeRoute, Company=@Company WHERE UniqueId=@UniqueId";
                        SqlCommand cmd= new SqlCommand(queryup,databaseObject.myConnection);
                        cmd.Parameters.AddWithValue("@Uniqueid", Id);
                        cmd.Parameters.AddWithValue("@UserPassword", Password);
                        cmd.Parameters.AddWithValue("@UserNameandSurname", FullName);
                        cmd.Parameters.AddWithValue("@UserRole", UserRole);
                        cmd.Parameters.AddWithValue("@UserStatus", UserStatus);
                        cmd.Parameters.AddWithValue("@EmployeeRoute", EmployeeRoute);
                        cmd.Parameters.AddWithValue("@Company", Company);

                        databaseObject.OpenConnection();
                        var result= cmd.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                        if(result != 0 )
                        {
                            MsgBox("Update succesfull !!!", this.Page, this);
                            inputIdMod.Value = "";
                            inputPassMod.Value = "";
                            inputNameMod.Value = "";
                            SelectRoleMod.SelectedIndex = 0;
                            SelectRouteMod.SelectedIndex = 0;
                            SelectStatusMod.SelectedIndex = 0;
                            SelectCompanyMod.SelectedIndex = 0;
                        }
                        else
                        {
                            MsgBox("The user with this ID does not exist !!!", this.Page, this);
                        }
                    }
                    else
                    {
                        MsgBox("Complete all fields !!!", this.Page, this);
                    }

                }
                else
                {
                    MsgBox("You cannot modify the logged in user", this.Page, this);
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }

           
        }

        protected void btnAutoCom_Click(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject = new Database();
                string Id=inputIdMod.Value;
                string Password = "";
                string FullName = "";
                string Role = "";
                string UserStatus = "";
                string EmployeeRoute = "";
                string Company = "";

                string queryauto = "SELECT UserPassword,UserNameandSurname,UserRole,UserStatus,EmployeeRoute,Company from Users WHERE UniqueId=@UniqueId";
                SqlCommand cmd = new SqlCommand(queryauto, databaseObject.myConnection);
                cmd.Parameters.AddWithValue("@Uniqueid", Id);
                databaseObject.OpenConnection();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Password = reader[0].ToString();
                        FullName = reader[1].ToString();
                        Role = reader[2].ToString();
                        UserStatus = reader[3].ToString();
                        EmployeeRoute = reader[4].ToString();
                        Company = reader[5].ToString();
                    }
                }
                reader.Close();
                databaseObject.CloseConnection();

                inputPassMod.Value = Password;
                inputNameMod.Value = FullName;
                SelectRoleMod.Value = Role;
                SelectStatusMod.Value = UserStatus;
                SelectRouteMod.Value = EmployeeRoute;   
                SelectCompanyMod.Value = Company;
            }
            catch(Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
          
                try
                {
                    
                    Database databaseObject = new Database();
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
                catch (Exception ex)
                {
                    MsgBox(ex.ToString(), this.Page, this);
                }
            
        }
    }
}
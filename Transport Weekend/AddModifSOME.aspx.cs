using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Transport_Weekend
{
    public partial class AddModifSOME : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Database databaseObject = new Database();
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
            catch (Exception ex)
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
            string loggedinID = Request.Cookies["userdata"].Value;
            GetUserName getUserName = new GetUserName();
            string loggedin = getUserName.GetName(loggedinID);

            try
            {
                Database databaseObject = new Database();
                string Id = inputIdAdd.Value;
                string Company = SelectCompanyAdd.Value;
                string CNP = inputCNPAdd.Value;
                string CostCenter = inputCostCenterAdd.Value;
                string CostCenterName = inputCostCenterNameAdd.Value;
                string FullName = inputNameSurnameAdd.Value;
                string Department = inputDepartmentAdd.Value;
                string Phone = inputPhoneAdd.Value;
                string HomeAddress = inputHomeAddressAdd.Value;
                string UserStatus = SelectStatusAdd.Value;
                string Superior = loggedin;
                string EmployeeRoute = SelectRouteAdd.Value;
                bool verif = (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Company) || string.IsNullOrEmpty(CNP) || string.IsNullOrEmpty(CostCenter) || string.IsNullOrEmpty(CostCenterName) || string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Department) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(HomeAddress) || string.IsNullOrEmpty(UserStatus) ||  string.IsNullOrEmpty(Superior) || string.IsNullOrEmpty(EmployeeRoute));

                if (verif == false)
                {

                    string queryverif = "SELECT * from Employees WHERE SAPid=@Id";
                    SqlCommand cmdverif = new SqlCommand(queryverif, databaseObject.myConnection);
                    cmdverif.Parameters.AddWithValue("@Id", Id);
                    databaseObject.OpenConnection();
                    var resverif = cmdverif.ExecuteScalar();
                    databaseObject.CloseConnection();

                    if (resverif is null)
                    {
                        string queryadd = "INSERT INTO Employees(Company,CNP,SAPid,CostCentre,CostCentreName,NameandSurname,Deparment,Phone,HomeAddress,UserStatus,Superior,EmployeeRoute) " +
                        "VALUES (@Company,@CNP,@SAPid,@CostCentre,@CostCentreName,@NameandSurname,@Deparment,@Phone,@HomeAddress,@UserStatus,@Superior,@EmployeeRoute)";
                        SqlCommand cmd = new SqlCommand(queryadd, databaseObject.myConnection);
                        cmd.Parameters.AddWithValue("@Company", Company);
                        cmd.Parameters.AddWithValue("@CNP", CNP);
                        cmd.Parameters.AddWithValue("@SAPid", Id);
                        cmd.Parameters.AddWithValue("@CostCentre", CostCenter);
                        cmd.Parameters.AddWithValue("@CostCentreName", CostCenterName);
                        cmd.Parameters.AddWithValue("@NameandSurname", FullName);
                        cmd.Parameters.AddWithValue("@Deparment", Department);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@HomeAddress", HomeAddress);
                        cmd.Parameters.AddWithValue("@UserStatus", UserStatus);
                        cmd.Parameters.AddWithValue("@Superior", Superior);
                        cmd.Parameters.AddWithValue("@EmployeeRoute", EmployeeRoute);

                        databaseObject.OpenConnection();
                        var result = cmd.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                        MsgBox("User added succesfully !", this.Page, this);
                        SelectCompanyAdd.SelectedIndex = 0;
                        inputCNPAdd.Value = "";
                        inputIdAdd.Value = "";
                        inputCostCenterAdd.Value = "";
                        inputCostCenterNameAdd.Value = "";
                        inputNameSurnameAdd.Value = "";
                        inputDepartmentAdd.Value = "";
                        inputPhoneAdd.Value = "";
                        inputHomeAddressAdd.Value = "";
                        SelectStatusAdd.SelectedIndex = 0;
                        SelectRouteAdd.SelectedIndex = 0;
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
            catch (Exception ex)
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
                    Database databaseObject = new Database();
                    string Company = SelectCompanyMod.Value;
                    string CNP = inputCNPMod.Value;
                    string CostCenter = inputCostCenterMod.Value;
                    string CostCenterName = inputCostCenterNameMod.Value;
                    string FullName = inputNameSurnameMod.Value;
                    string Department = inputDepartmentMod.Value;
                    string Phone = inputPhoneMod.Value;
                    string HomeAddress = inputHomeAddressMod.Value;
                    string UserStatus = SelectStatusMod.Value;
                    string EmployeeRoute = SelectRouteMod.Value;
                    bool verif = (string.IsNullOrEmpty(Company) || string.IsNullOrEmpty(CNP) || string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(CostCenter) || string.IsNullOrEmpty(CostCenterName) || string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Department) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(HomeAddress) || string.IsNullOrEmpty(UserStatus) || string.IsNullOrEmpty(EmployeeRoute));
                    if (verif == false)
                    {
                        string queryup = "UPDATE Employees SET Company = @Company, CNP = @CNP, CostCentre = @CostCentre," +
                            " CostCentreName = @CostCentreName, NameandSurname = @NameandSurname, Deparment = @Deparment, Phone = @Phone," +
                            " HomeAddress = @HomeAddress, UserStatus = @UserStatus," +
                            " EmployeeRoute = @EmployeeRoute WHERE SAPid = @SAPid";
                        SqlCommand cmd = new SqlCommand(queryup, databaseObject.myConnection);
                        cmd.Parameters.AddWithValue("@Company", Company);
                        cmd.Parameters.AddWithValue("@CNP", CNP);
                        cmd.Parameters.AddWithValue("@SAPid", Id);
                        cmd.Parameters.AddWithValue("@CostCentre", CostCenter);
                        cmd.Parameters.AddWithValue("@CostCentreName", CostCenterName);
                        cmd.Parameters.AddWithValue("@NameandSurname", FullName);
                        cmd.Parameters.AddWithValue("@Deparment", Department);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@HomeAddress", HomeAddress);
                        cmd.Parameters.AddWithValue("@UserStatus", UserStatus);
                        cmd.Parameters.AddWithValue("@EmployeeRoute", EmployeeRoute);

                        databaseObject.OpenConnection();
                        var result = cmd.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                        if (result != 0)
                        {
                            MsgBox("Update succesfull !!!", this.Page, this);
                            SelectCompanyMod.SelectedIndex = 0;
                            inputCNPMod.Value = "";
                            inputIdMod.Value = "";
                            inputCostCenterMod.Value = "";
                            inputCostCenterNameMod.Value = "";
                            inputNameSurnameMod.Value = "";
                            inputDepartmentMod.Value = "";
                            inputPhoneMod.Value = "";
                            inputHomeAddressMod.Value = "";
                            SelectStatusMod.SelectedIndex = 0;
                            SelectRouteMod.SelectedIndex = 0;
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
                string Id = inputIdMod.Value;
                string Company = "";
                string CNP = "";
                string CostCenter = "";
                string CostCenterName = "";
                string FullName = "";
                string Department = "";
                string Phone = "";
                string HomeAddress = "";
                string UserStatus = "";
                string EmployeeRoute = "";

                string queryauto = "SELECT Company,CNP,CostCentre,CostCentreName,NameandSurname,Deparment,Phone,HomeAddress,UserStatus,Superior,EmployeeRoute from Employees WHERE SAPid=@UniqueId";
                SqlCommand cmd = new SqlCommand(queryauto, databaseObject.myConnection);
                cmd.Parameters.AddWithValue("@Uniqueid", Id);
                databaseObject.OpenConnection();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Company = reader[0].ToString();
                        CNP = reader[1].ToString();
                        CostCenter = reader[2].ToString();
                        CostCenterName = reader[3].ToString();
                        FullName = reader[4].ToString();
                        Department = reader[5].ToString();
                        Phone = reader[6].ToString();
                        HomeAddress = reader[7].ToString();
                        UserStatus = reader[8].ToString();
                        EmployeeRoute = reader[9].ToString();
                    }
                }
                reader.Close();
                databaseObject.CloseConnection();

                SelectCompanyMod.Value = Company;
                inputCNPMod.Value = CNP;
                inputCostCenterMod.Value = CostCenter;
                inputCostCenterNameMod.Value = CostCenterName;
                inputNameSurnameMod.Value = FullName;
                inputDepartmentMod.Value = Department;
                inputPhoneMod.Value = Phone;
                inputHomeAddressMod.Value = HomeAddress;
                SelectStatusMod.Value = UserStatus;
                SelectRouteMod.Value = EmployeeRoute;
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            string loggedinID = Request.Cookies["userdata"].Value;
            GetUserName getUserName = new GetUserName();
            string loggedin = getUserName.GetName(loggedinID);
            try
            {
                    Database databaseObject = new Database();
                    databaseObject.OpenConnection();
                    string query = "SELECT * FROM Employees WHERE Superior = @Superior";
                    SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);
                    myquerytab.Parameters.AddWithValue("@Superior", loggedin);

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
    }
}
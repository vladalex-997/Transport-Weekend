using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime.Misc;

namespace Transport_Weekend
{
    public partial class Subordonates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RefreshGridAll();
                RefreshGridProgrammed();
                ReloadDelete();
                ReloadNames();
                
            }
          
        }

        public void ReloadNames()
        {
            string loggedinID = Request.Cookies["userdata"].Value;
            GetUserName getUserName = new GetUserName();
            string loggedin = getUserName.GetName(loggedinID);

            Database databaseObject = new Database();
            databaseObject.OpenConnection();
            string Userstatus = "ACTIVE";
            string available = "AVAILABLE";
            string query = "SELECT NameandSurname from Employees WHERE UserStatus=@UserStatus AND Superior=@Superior AND (AvailableSaturday=@AvailableSaturday OR AvailableSunday=@AvailableSunday)";
            SqlCommand cmd = new SqlCommand(query, databaseObject.myConnection);
            cmd.Parameters.AddWithValue("@Superior", loggedin);
            cmd.Parameters.AddWithValue("@UserStatus", Userstatus);
            cmd.Parameters.AddWithValue("@AvailableSaturday", available);
            cmd.Parameters.AddWithValue("@AvailableSunday", available);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            SelectName.DataSource = ds.Tables[0];
            SelectName.DataTextField = ds.Tables[0].Columns["NameandSurname"].ToString();
            SelectName.DataValueField = ds.Tables[0].Columns["NameandSurname"].ToString();

            SelectName.DataBind();

            databaseObject.CloseConnection();
        }

        public void ReloadDelete()
        {

            string loggedinID = Request.Cookies["userdata"].Value;
            GetUserName getUserName = new GetUserName();
            string loggedin = getUserName.GetName(loggedinID);

            Database databaseObject = new Database();
            databaseObject.OpenConnection();
            string Userstatus = "ACTIVE";
            string available = "PROGRAMMED";
            string query = "SELECT NameandSurname from Employees WHERE UserStatus=@UserStatus AND Superior=@Superior AND (AvailableSaturday=@AvailableSaturday OR AvailableSunday=@AvailableSunday)";
            SqlCommand cmd = new SqlCommand(query, databaseObject.myConnection);
            cmd.Parameters.AddWithValue("@Superior", loggedin);
            cmd.Parameters.AddWithValue("@UserStatus", Userstatus);
            cmd.Parameters.AddWithValue("@AvailableSaturday", available);
            cmd.Parameters.AddWithValue("@AvailableSunday", available);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            SelectDel.DataSource = ds.Tables[0];
            SelectDel.DataTextField = ds.Tables[0].Columns["NameandSurname"].ToString();
            SelectDel.DataValueField = ds.Tables[0].Columns["NameandSurname"].ToString();

            SelectDel.DataBind();

            databaseObject.CloseConnection();
        }
        public void RefreshGridAll()
        {
            string loggedinID = Request.Cookies["userdata"].Value;
            GetUserName getUserName = new GetUserName();
            string loggedin = getUserName.GetName(loggedinID);
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

        public void RefreshGridProgrammed()
        {
            string loggedinID = Request.Cookies["userdata"].Value;
            GetUserName getUserName = new GetUserName();
            string loggedin = getUserName.GetName(loggedinID);
            try
            {
                string active = "ACTIVE";
                Database databaseObject = new Database();
                databaseObject.OpenConnection();
                string query = "SELECT * FROM ScheduleTemporary WHERE Superior = @Superior AND UserStatus = @Active";
                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);
                myquerytab.Parameters.AddWithValue("@Superior", loggedin);
                myquerytab.Parameters.AddWithValue("@Active", active);

                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataSet ds = new DataSet();
                daquery.Fill(ds);
                GridViewProgrammed.DataSource = ds;
                GridViewProgrammed.DataBind();
                databaseObject.CloseConnection();


            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e) 
        {
            RefreshGridAll();
        }

        protected void btnProgrammed_Click(object sender, EventArgs e)
        {
            RefreshGridProgrammed();
            ReloadDelete();
        }

        protected void btndeleteProgrammed_Click(object sender, EventArgs e)
        {
            try
            {
                string loggedinID = Request.Cookies["userdata"].Value;
                GetUserName getUserName = new GetUserName();
                string loggedin = getUserName.GetName(loggedinID);

                Database databaseObject = new Database();
                string query = "SELECT NameandSurname from ScheduleTemporary WHERE Superior=@Superior";
                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);
                myquerytab.Parameters.AddWithValue("@Superior",loggedin);
                databaseObject.OpenConnection();
                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataTable dt = new DataTable();
                daquery.Fill(dt);
                var listtemporary = dt.AsEnumerable().Select(r => r.Field<string>("NameandSurname")).ToList();
                databaseObject.CloseConnection();

                
                string AvailableSaturday = "";
                string AvailableSunday = "";
                string ShiftSaturday = "";
                string ShiftSunday = "";

                foreach (string name in listtemporary)
                {
                    //updatam alea la NONE si available si dupa stergem tabel

                    string verif = "SELECT AvailableSaturday,AvailableSunday,ShiftSaturday,ShiftSunday from Employees WHERE NameandSurname=@NameandSurname";
                    SqlCommand cmdverif= new SqlCommand(verif, databaseObject.myConnection);
                    cmdverif.Parameters.AddWithValue("@NameandSurname",name);

                    databaseObject.OpenConnection();
                    SqlDataReader reader = cmdverif.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AvailableSaturday = reader[0].ToString();
                            AvailableSunday = reader[1].ToString();
                            ShiftSaturday= reader[2].ToString();
                            ShiftSunday= reader[3].ToString();
                        }

                    }
                        databaseObject.CloseConnection();

                    if (AvailableSaturday != "BUSY") 
                    {
                        AvailableSaturday = "AVAILABLE";
                        ShiftSaturday = "NONE";

                    }

                    if (AvailableSunday != "BUSY")
                    {
                        AvailableSunday = "AVAILABLE";
                        ShiftSunday = "NONE";

                    }

                    string queryup = "UPDATE Employees SET AvailableSaturday=@AvailableSaturday, AvailableSunday=@AvailableSunday, " +
                        "ShiftSaturday=@ShiftSaturday, ShiftSunday=@ShiftSunday WHERE NameandSurname=@NameandSurname ";
                    SqlCommand comup = new SqlCommand(queryup,databaseObject.myConnection);
                    comup.Parameters.AddWithValue("@NameandSurname", name);
                    comup.Parameters.AddWithValue("@AvailableSaturday", AvailableSaturday);
                    comup.Parameters.AddWithValue("@AvailableSunday", AvailableSunday);
                    comup.Parameters.AddWithValue("@ShiftSaturday", ShiftSaturday);
                    comup.Parameters.AddWithValue("@ShiftSunday", ShiftSunday);

                    databaseObject.OpenConnection();
                    var res= comup.ExecuteNonQuery();
                    databaseObject.CloseConnection();

                }



                DeleteTemp();
                RefreshGridAll();
                RefreshGridProgrammed();
                ReloadNames();
                ReloadDelete();
                MsgBox("Deleted Schedules Succesfully", this.Page,this);


            }
            catch(Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

        public void DeleteTemp()
        {
            string loggedinID = Request.Cookies["userdata"].Value;
            GetUserName getUserName = new GetUserName();
            string loggedin = getUserName.GetName(loggedinID);
            Database databaseObject= new Database();    
            string querydel = "DELETE from ScheduleTemporary WHERE Superior=@Superior";
            SqlCommand cmddel = new SqlCommand(querydel, databaseObject.myConnection);
            cmddel.Parameters.AddWithValue("@Superior",loggedin);
            databaseObject.OpenConnection();
            var resu = cmddel.ExecuteNonQuery();
            databaseObject.CloseConnection();
        }

        public void DeleteOne()
        {
            string loggedinID = Request.Cookies["userdata"].Value;
            GetUserName getUserName = new GetUserName();
            string loggedin = getUserName.GetName(loggedinID);
            Database databaseObject = new Database();
            string name = SelectDel.Value;
            string querydel = "DELETE from ScheduleTemporary WHERE Superior=@Superior AND NameandSurname=@NameandSurname";
            SqlCommand cmddel = new SqlCommand(querydel, databaseObject.myConnection);
            cmddel.Parameters.AddWithValue("@Superior", loggedin);
            cmddel.Parameters.AddWithValue("@NameandSurname",name);
            databaseObject.OpenConnection();
            var resu = cmddel.ExecuteNonQuery();
            databaseObject.CloseConnection();
        }

        public static string getHtml(DataTable table1)
        {
            try
            {
                string messageBody = "<center> <h2> Martur Fompak International </h2>  "+ "- Weekend Transport Application - </br></br></br> </center> " + "<center><big> Hello, thank you for booking your transportation.This email is to confirm your reservation details </big> </br>.</center>" + "<center>";
               

                if (table1.Rows.Count == 0)
                    return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style =\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Name and Surname " + htmlTdEnd;
                messageBody += htmlTdStart + "Department " + htmlTdEnd;
                messageBody += htmlTdStart + "Superior " + htmlTdEnd;
                messageBody += htmlTdStart + "Route " + htmlTdEnd;
               
                messageBody += htmlTdStart + "Shift Saturday " + htmlTdEnd;
               
                messageBody += htmlTdStart + "Shift Sunday " + htmlTdEnd;
                messageBody += htmlTdStart + "Phone " + htmlTdEnd;

                messageBody += htmlHeaderRowEnd;

                //foreach (DataRow Row in dataSet.Tables[0].Rows)
                //{
                //    messageBody = messageBody + htmlTrStart;
                //    messageBody = messageBody + htmlTdStart + Row["fieldName"] + htmlTdEnd;
                //    messageBody = messageBody + htmlTrEnd;
                //}
                //messageBody = messageBody + htmlTableEnd;

                for (int i = 0; i <= table1.Rows.Count - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][0] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][1] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][2] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][3] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][4] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][5] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][6] + htmlTdEnd;
                   
                    messageBody = messageBody + htmlTrEnd;
                }

                messageBody = messageBody + htmlTableEnd;
                messageBody += "</center></br></br>"+ "<center><big>This email is automatically generated, please do not reply.</big></center>";
                return messageBody;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject = new Database();
                string Id = "";
                string Company = "";
                string CNP = "";
                string CostCenter = "";
                string CostCenterName = "";
                string FullName = "";
                string Department = "";
                string Phone = "";
                string HomeAddress = "";
                string UserStatus = "";
                string Superior = "";
                string EmployeeRoute = "";
                string SaturdayStatus = "";
                string SundayStatus = "";
                string SaturdayShift = "";
                string SundayShift = "";



                string loggedinID = Request.Cookies["userdata"].Value;
                GetUserName getUserName = new GetUserName();
                string loggedin = getUserName.GetName(loggedinID);


                string querynames = "SELECT NameandSurname from ScheduleTemporary WHERE Superior=@Superior";
                SqlCommand myquerytab = new SqlCommand(querynames, databaseObject.myConnection);
                myquerytab.Parameters.AddWithValue("@Superior",loggedin);
                databaseObject.OpenConnection();
                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataTable dt = new DataTable();
                daquery.Fill(dt);
                var listtemporary = dt.AsEnumerable().Select(r => r.Field<string>("NameandSurname")).ToList();
                databaseObject.CloseConnection();

                foreach (string name in listtemporary)
                {
                    string queryfill = "SELECT * from ScheduleTemporary WHERE NameandSurname=@NameandSurname";
                    SqlCommand cmd = new SqlCommand(queryfill, databaseObject.myConnection);
                    cmd.Parameters.AddWithValue("@NameandSurname", name);
                    databaseObject.OpenConnection();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Company = reader[1].ToString();
                            CNP = reader[2].ToString();
                            Id = reader[3].ToString();
                            CostCenter = reader[4].ToString();
                            CostCenterName = reader[5].ToString();
                            FullName = reader[6].ToString();
                            Department = reader[7].ToString();
                            Phone = reader[8].ToString();
                            HomeAddress = reader[9].ToString();
                            UserStatus = reader[10].ToString();
                            Superior = reader[11].ToString();
                            EmployeeRoute = reader[12].ToString();
                            SaturdayStatus = reader[13].ToString();
                            SundayStatus = reader[15].ToString();
                            SaturdayShift = reader[14].ToString();
                            SundayShift = reader[16].ToString();
                        }
                    }

                   

                    if (SaturdayStatus == "PROGRAMMED")
                    {
                        SaturdayStatus = "BUSY";
                    }

                    if (SundayStatus == "PROGRAMMED")
                    {
                        SundayStatus = "BUSY";
                    }

                    reader.Close();
                    databaseObject.CloseConnection();


                    string verify = "SELECT * from DefinitiveSchedule WHERE NameandSurname=@NameandSurname";
                    SqlCommand cmdverif = new SqlCommand(verify, databaseObject.myConnection);
                    cmdverif.Parameters.AddWithValue("@NameandSurname", FullName);

                    databaseObject.OpenConnection();
                    var temporary = cmdverif.ExecuteScalar();

                    databaseObject.CloseConnection();

                    if (temporary is null)
                    {
                        string queryadd = "INSERT INTO DefinitiveSchedule(Company,CNP,SAPid,CostCentre,CostCentreName,NameandSurname,Department,Phone,HomeAddress,UserStatus,Superior,EmployeeRoute,AvailableSaturday,AvailableSunday,ShiftSaturday,ShiftSunday) " +
                        "VALUES(@Company,@CNP,@SAPid,@CostCentre,@CostCentreName,@NameandSurname,@Deparment,@Phone,@HomeAddress,@UserStatus,@Superior,@EmployeeRoute,@AvailableSaturday,@AvailableSunday,@ShiftSaturday,@ShiftSunday)";
                        SqlCommand cmdadd = new SqlCommand(queryadd, databaseObject.myConnection);
                        cmdadd.Parameters.AddWithValue("@Company", Company);
                        cmdadd.Parameters.AddWithValue("@CNP", CNP);
                        cmdadd.Parameters.AddWithValue("@SAPid", Id);
                        cmdadd.Parameters.AddWithValue("@CostCentre", CostCenter);
                        cmdadd.Parameters.AddWithValue("@CostCentreName", CostCenterName);
                        cmdadd.Parameters.AddWithValue("@NameandSurname", FullName);
                        cmdadd.Parameters.AddWithValue("@Deparment", Department);
                        cmdadd.Parameters.AddWithValue("@Phone", Phone);
                        cmdadd.Parameters.AddWithValue("@HomeAddress", HomeAddress);
                        cmdadd.Parameters.AddWithValue("@UserStatus", UserStatus);
                        cmdadd.Parameters.AddWithValue("@Superior", Superior);
                        cmdadd.Parameters.AddWithValue("@EmployeeRoute", EmployeeRoute);
                        cmdadd.Parameters.AddWithValue("@AvailableSaturday", SaturdayStatus);
                        cmdadd.Parameters.AddWithValue("@ShiftSaturday", SaturdayShift);
                        cmdadd.Parameters.AddWithValue("@AvailableSunday", SundayStatus);
                        cmdadd.Parameters.AddWithValue("@ShiftSunday", SundayShift);

                        databaseObject.OpenConnection();
                        var result = cmdadd.ExecuteNonQuery();
                        databaseObject.CloseConnection();


                        string update = "UPDATE Employees SET AvailableSaturday=@SaturdayStatus, ShiftSaturday=@SaturdayShift, AvailableSunday=@SundayStatus, ShiftSunday=@SundayShift WHERE NameandSurname=@NameandSurname ";
                        SqlCommand cmdup = new SqlCommand(update, databaseObject.myConnection);
                        cmdup.Parameters.AddWithValue("@SaturdayStatus", SaturdayStatus);
                        cmdup.Parameters.AddWithValue("@SaturdayShift", SaturdayShift);
                        cmdup.Parameters.AddWithValue("@SundayStatus", SundayStatus);
                        cmdup.Parameters.AddWithValue("@SundayShift", SundayShift);
                        cmdup.Parameters.AddWithValue("@NameandSurname", FullName);

                        databaseObject.OpenConnection();
                        var res = cmdup.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                    }

                    else
                    {
                        string updatetem = "UPDATE DefinitiveSchedule SET AvailableSaturday=@SaturdayStatus, ShiftSaturday=@SaturdayShift, AvailableSunday=@SundayStatus, ShiftSunday=@SundayShift WHERE NameandSurname=@NameandSurname ";
                        SqlCommand cmduptem = new SqlCommand(updatetem, databaseObject.myConnection);
                        cmduptem.Parameters.AddWithValue("@SaturdayStatus", SaturdayStatus);
                        cmduptem.Parameters.AddWithValue("@SaturdayShift", SaturdayShift);
                        cmduptem.Parameters.AddWithValue("@SundayStatus", SundayStatus);
                        cmduptem.Parameters.AddWithValue("@SundayShift", SundayShift);
                        cmduptem.Parameters.AddWithValue("@NameandSurname", FullName);

                        databaseObject.OpenConnection();
                        var restem = cmduptem.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                        string update = "UPDATE Employees SET AvailableSaturday=@SaturdayStatus, ShiftSaturday=@SaturdayShift, AvailableSunday=@SundayStatus, ShiftSunday=@SundayShift WHERE NameandSurname=@NameandSurname ";
                        SqlCommand cmdup = new SqlCommand(update, databaseObject.myConnection);
                        cmdup.Parameters.AddWithValue("@SaturdayStatus", SaturdayStatus);
                        cmdup.Parameters.AddWithValue("@SaturdayShift", SaturdayShift);
                        cmdup.Parameters.AddWithValue("@SundayStatus", SundayStatus);
                        cmdup.Parameters.AddWithValue("@SundayShift", SundayShift);
                        cmdup.Parameters.AddWithValue("@NameandSurname", FullName);

                        databaseObject.OpenConnection();
                        var res = cmdup.ExecuteNonQuery();
                        databaseObject.CloseConnection();
                    }



                }

                string queryfilldata = "SELECT NameandSurname,Department,Superior,EmployeeRoute,ShiftSaturday,ShiftSunday,Phone from ScheduleTemporary WHERE Superior=@Superior";
                SqlCommand filldata = new SqlCommand(queryfilldata, databaseObject.myConnection);
                filldata.Parameters.AddWithValue("@Superior", loggedin);

                databaseObject.OpenConnection();

                SqlDataAdapter datable = new SqlDataAdapter(filldata);
                DataTable dttable = new DataTable();
                datable.Fill(dttable);

                databaseObject.CloseConnection();


                string querymail = "SELECT Email from Users WHERE UserNameandSurname=@UserNameandSurname";
                SqlCommand cmdmail= new SqlCommand(querymail, databaseObject.myConnection);
                cmdmail.Parameters.AddWithValue("@UserNameandSurname",loggedin);

                databaseObject.OpenConnection();
                var resultbla=cmdmail.ExecuteScalar();
                databaseObject.CloseConnection();



                string Subiect;
                string Text;
                string Emailget;
                string Emailsend;
               
                string templogged = "";
                Subiect = "Weekend Transport Confirmation";
                Text = getHtml(dttable);
               
              
                templogged=resultbla.ToString();
               
               
                Emailsend = templogged+ ";ovidiu.gionea@marturfompak.com";
                Emailget = "cristian.nedelea@marturfompak.com;"+"vlad.arsene@marturfompak.com";

                Email ema = new Email();

               var resultmail= ema.Send(Text, Subiect, Emailsend, Emailget);

                DeleteTemp();
                RefreshGridAll();
                RefreshGridProgrammed();
                ReloadNames();
                ReloadDelete();

                if (resultmail)
                {
                    MsgBox("List send succesfully", this.Page, this);
                }
                else
                {
                    MsgBox("List send succesfully. Failed to send Email", this.Page, this);
                }
                
            }
            catch(Exception ex )
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedName = SelectName.Value;
                string selectedDay = SelectDay.Value; 
                string selectedShift = SelectShift.Value;
                var expr = string.IsNullOrEmpty(selectedName) || string.IsNullOrEmpty(selectedDay) || string.IsNullOrEmpty(selectedShift);
                if (expr==false)
                {
                    Database databaseObject = new Database();

                    string Id = "";
                    string Company = "";
                    string CNP = "";
                    string CostCenter = "";
                    string CostCenterName = "";
                    string FullName = selectedName;
                    string Department = "";
                    string Phone = "";
                    string HomeAddress = "";
                    string UserStatus = "";
                    string Superior = "";
                    string EmployeeRoute = "";
                    string SaturdayStatus = "";
                    string SundayStatus = "";
                    string SaturdayShift = "";
                    string SundayShift = "";


                    string queryauto = "SELECT Company,CNP,SAPid,CostCentre,CostCentreName,Deparment,Phone,HomeAddress,UserStatus,Superior,EmployeeRoute,AvailableSaturday,AvailableSunday,ShiftSaturday,ShiftSunday from Employees WHERE NameandSurname=@NameandSurname";
                    SqlCommand cmd = new SqlCommand(queryauto, databaseObject.myConnection);
                    cmd.Parameters.AddWithValue("@NameandSurname", selectedName);
                    databaseObject.OpenConnection();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Company = reader[0].ToString();
                            CNP = reader[1].ToString();
                            Id = reader[2].ToString();
                            CostCenter = reader[3].ToString();
                            CostCenterName = reader[4].ToString();
                            Department = reader[5].ToString();
                            Phone = reader[6].ToString();
                            HomeAddress = reader[7].ToString();
                            UserStatus = reader[8].ToString();
                            Superior = reader[9].ToString();
                            EmployeeRoute = reader[10].ToString();
                            SaturdayStatus = reader[11].ToString();
                            SundayStatus = reader[12].ToString();
                            SaturdayShift = reader[13].ToString();
                            SundayShift = reader[14].ToString();
                        }
                    }
                    reader.Close();
                    databaseObject.CloseConnection();



                    if (selectedDay == "Saturday")
                    {
                        
                        SaturdayStatus = "PROGRAMMED";
                        SaturdayShift = selectedShift;

                    }
                    else if(selectedDay == "Sunday")
                    {
                        SundayStatus = "PROGRAMMED";
                        SundayShift = selectedShift;
                    }

                    string update = "UPDATE Employees SET AvailableSaturday=@SaturdayStatus, ShiftSaturday=@SaturdayShift, AvailableSunday=@SundayStatus, ShiftSunday=@SundayShift WHERE NameandSurname=@NameandSurname ";
                    SqlCommand cmdup= new SqlCommand(update,databaseObject.myConnection);
                    cmdup.Parameters.AddWithValue("@SaturdayStatus",SaturdayStatus);
                    cmdup.Parameters.AddWithValue("@SaturdayShift", SaturdayShift);
                    cmdup.Parameters.AddWithValue("@SundayStatus", SundayStatus);
                    cmdup.Parameters.AddWithValue("@SundayShift", SundayShift);
                    cmdup.Parameters.AddWithValue("@NameandSurname", selectedName);

                    databaseObject.OpenConnection();
                    var res = cmdup.ExecuteNonQuery();
                    databaseObject.CloseConnection();

                    string verify = "SELECT * from ScheduleTemporary WHERE NameandSurname=@NameandSurname";
                    SqlCommand cmdverif=new SqlCommand(verify,databaseObject.myConnection);
                    cmdverif.Parameters.AddWithValue("@NameandSurname", selectedName);

                    databaseObject.OpenConnection();
                    var temporary = cmdverif.ExecuteScalar();

                    databaseObject.CloseConnection();

                    if( temporary is null)
                    {
                        string queryadd = "INSERT INTO ScheduleTemporary(Company,CNP,SAPid,CostCentre,CostCentreName,NameandSurname,Department,Phone,HomeAddress,UserStatus,Superior,EmployeeRoute,AvailableSaturday,AvailableSunday,ShiftSaturday,ShiftSunday) " +
                        "VALUES (@Company,@CNP,@SAPid,@CostCentre,@CostCentreName,@NameandSurname,@Deparment,@Phone,@HomeAddress,@UserStatus,@Superior,@EmployeeRoute,@AvailableSaturday,@AvailableSunday,@ShiftSaturday,@ShiftSunday)";
                        SqlCommand cmdadd = new SqlCommand(queryadd, databaseObject.myConnection);
                        cmdadd.Parameters.AddWithValue("@Company", Company);
                        cmdadd.Parameters.AddWithValue("@CNP", CNP);
                        cmdadd.Parameters.AddWithValue("@SAPid", Id);
                        cmdadd.Parameters.AddWithValue("@CostCentre", CostCenter);
                        cmdadd.Parameters.AddWithValue("@CostCentreName", CostCenterName);
                        cmdadd.Parameters.AddWithValue("@NameandSurname", FullName);
                        cmdadd.Parameters.AddWithValue("@Deparment", Department);
                        cmdadd.Parameters.AddWithValue("@Phone", Phone);
                        cmdadd.Parameters.AddWithValue("@HomeAddress", HomeAddress);
                        cmdadd.Parameters.AddWithValue("@UserStatus", UserStatus);
                        cmdadd.Parameters.AddWithValue("@Superior", Superior);
                        cmdadd.Parameters.AddWithValue("@EmployeeRoute", EmployeeRoute);
                        cmdadd.Parameters.AddWithValue("@AvailableSaturday", SaturdayStatus);
                        cmdadd.Parameters.AddWithValue("@ShiftSaturday", SaturdayShift);
                        cmdadd.Parameters.AddWithValue("@AvailableSunday", SundayStatus);
                        cmdadd.Parameters.AddWithValue("@ShiftSunday", SundayShift);

                        databaseObject.OpenConnection();
                        var result = cmdadd.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                        MsgBox("Employee Programmed Succesfully !", this.Page, this);
                        RefreshGridAll();
                        RefreshGridProgrammed();
                        ReloadNames();
                        ReloadDelete();
                        SelectDay.SelectedIndex = 0;
                        SelectName.SelectedIndex = 0;
                        SelectShift.SelectedIndex = 0;
                    }
                    else
                    {
                        string updatetem = "UPDATE ScheduleTemporary SET AvailableSaturday=@SaturdayStatus, ShiftSaturday=@SaturdayShift, AvailableSunday=@SundayStatus, ShiftSunday=@SundayShift WHERE NameandSurname=@NameandSurname ";
                        SqlCommand cmduptem = new SqlCommand(updatetem, databaseObject.myConnection);
                        cmduptem.Parameters.AddWithValue("@SaturdayStatus", SaturdayStatus);
                        cmduptem.Parameters.AddWithValue("@SaturdayShift", SaturdayShift);
                        cmduptem.Parameters.AddWithValue("@SundayStatus", SundayStatus);
                        cmduptem.Parameters.AddWithValue("@SundayShift", SundayShift);
                        cmduptem.Parameters.AddWithValue("@NameandSurname", selectedName);

                        databaseObject.OpenConnection();
                        var restem = cmduptem.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                        MsgBox("Employee Programmed Succesfully !", this.Page, this);
                        RefreshGridAll();
                        RefreshGridProgrammed();
                        ReloadNames();
                        ReloadDelete();
                        SelectDay.SelectedIndex = 0;
                        SelectName.SelectedIndex = 0;
                        SelectShift.SelectedIndex = 0;
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

        protected void btnScheduleALL_Click(object sender, EventArgs e)
        {
            try
            {
                string ShiftAll = SelectShiftALL.Value;
                string DayAll=SelectDayALL.Value;

                var expr = (string.IsNullOrEmpty(ShiftAll) || string.IsNullOrEmpty(DayAll));

                if (expr == false)
                {
                    string loggedinID = Request.Cookies["userdata"].Value;
                    GetUserName getUserName = new GetUserName();
                    string loggedin = getUserName.GetName(loggedinID);

                    Database databaseObject = new Database();





                    string query = "SELECT NameandSurname from Employees WHERE Superior=@Superior";
                    SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);
                    myquerytab.Parameters.AddWithValue("@Superior", loggedin);
                    databaseObject.OpenConnection();
                    SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                    DataTable dt = new DataTable();
                    daquery.Fill(dt);
                    var listtemporary = dt.AsEnumerable().Select(r => r.Field<string>("NameandSurname")).ToList();
                    databaseObject.CloseConnection();
                   
                    foreach (string name in listtemporary)
                    {

                        string Id = "";
                        string Company = "";
                        string CNP = "";
                        string CostCenter = "";
                        string CostCenterName = "";
                        string FullName = name;
                        string Department = "";
                        string Phone = "";
                        string HomeAddress = "";
                        string UserStatus = "";
                        string Superior = "";
                        string EmployeeRoute = "";
                        string SaturdayStatus = "";
                        string SundayStatus = "";
                        string SaturdayShift = "";
                        string SundayShift = "";


                        string queryauto = "SELECT Company,CNP,SAPid,CostCentre,CostCentreName,Deparment,Phone,HomeAddress,UserStatus,Superior,EmployeeRoute,AvailableSaturday,AvailableSunday,ShiftSaturday,ShiftSunday from Employees WHERE NameandSurname=@NameandSurname";
                        SqlCommand cmd = new SqlCommand(queryauto, databaseObject.myConnection);
                        cmd.Parameters.AddWithValue("@NameandSurname", name);
                        databaseObject.OpenConnection();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Company = reader[0].ToString();
                                CNP = reader[1].ToString();
                                Id = reader[2].ToString();
                                CostCenter = reader[3].ToString();
                                CostCenterName = reader[4].ToString();
                                Department = reader[5].ToString();
                                Phone = reader[6].ToString();
                                HomeAddress = reader[7].ToString();
                                UserStatus = reader[8].ToString();
                                Superior = reader[9].ToString();
                                EmployeeRoute = reader[10].ToString();
                                SaturdayStatus = reader[11].ToString();
                                SundayStatus = reader[12].ToString();
                                SaturdayShift = reader[13].ToString();
                                SundayShift = reader[14].ToString();
                            }
                        }
                        reader.Close();
                        databaseObject.CloseConnection();



                        if (DayAll == "Saturday")
                        {

                            SaturdayStatus = "PROGRAMMED";
                            SaturdayShift = ShiftAll;

                        }
                        else if (DayAll == "Sunday")
                        {
                            SundayStatus = "PROGRAMMED";
                            SundayShift = ShiftAll;
                        }

                        string update = "UPDATE Employees SET AvailableSaturday=@SaturdayStatus, ShiftSaturday=@SaturdayShift, AvailableSunday=@SundayStatus, ShiftSunday=@SundayShift WHERE NameandSurname=@NameandSurname ";
                        SqlCommand cmdup = new SqlCommand(update, databaseObject.myConnection);
                        cmdup.Parameters.AddWithValue("@SaturdayStatus", SaturdayStatus);
                        cmdup.Parameters.AddWithValue("@SaturdayShift", SaturdayShift);
                        cmdup.Parameters.AddWithValue("@SundayStatus", SundayStatus);
                        cmdup.Parameters.AddWithValue("@SundayShift", SundayShift);
                        cmdup.Parameters.AddWithValue("@NameandSurname", name);

                        databaseObject.OpenConnection();
                        var res = cmdup.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                        string verify = "SELECT * from ScheduleTemporary WHERE NameandSurname=@NameandSurname";
                        SqlCommand cmdverif = new SqlCommand(verify, databaseObject.myConnection);
                        cmdverif.Parameters.AddWithValue("@NameandSurname", name);

                        databaseObject.OpenConnection();
                        var temporary = cmdverif.ExecuteScalar();

                        databaseObject.CloseConnection();

                        if (temporary is null)
                        {
                            string queryadd = "INSERT INTO ScheduleTemporary(Company,CNP,SAPid,CostCentre,CostCentreName,NameandSurname,Department,Phone,HomeAddress,UserStatus,Superior,EmployeeRoute,AvailableSaturday,AvailableSunday,ShiftSaturday,ShiftSunday) " +
                            "VALUES (@Company,@CNP,@SAPid,@CostCentre,@CostCentreName,@NameandSurname,@Deparment,@Phone,@HomeAddress,@UserStatus,@Superior,@EmployeeRoute,@AvailableSaturday,@AvailableSunday,@ShiftSaturday,@ShiftSunday)";
                            SqlCommand cmdadd = new SqlCommand(queryadd, databaseObject.myConnection);
                            cmdadd.Parameters.AddWithValue("@Company", Company);
                            cmdadd.Parameters.AddWithValue("@CNP", CNP);
                            cmdadd.Parameters.AddWithValue("@SAPid", Id);
                            cmdadd.Parameters.AddWithValue("@CostCentre", CostCenter);
                            cmdadd.Parameters.AddWithValue("@CostCentreName", CostCenterName);
                            cmdadd.Parameters.AddWithValue("@NameandSurname", FullName);
                            cmdadd.Parameters.AddWithValue("@Deparment", Department);
                            cmdadd.Parameters.AddWithValue("@Phone", Phone);
                            cmdadd.Parameters.AddWithValue("@HomeAddress", HomeAddress);
                            cmdadd.Parameters.AddWithValue("@UserStatus", UserStatus);
                            cmdadd.Parameters.AddWithValue("@Superior", Superior);
                            cmdadd.Parameters.AddWithValue("@EmployeeRoute", EmployeeRoute);
                            cmdadd.Parameters.AddWithValue("@AvailableSaturday", SaturdayStatus);
                            cmdadd.Parameters.AddWithValue("@ShiftSaturday", SaturdayShift);
                            cmdadd.Parameters.AddWithValue("@AvailableSunday", SundayStatus);
                            cmdadd.Parameters.AddWithValue("@ShiftSunday", SundayShift);

                            databaseObject.OpenConnection();
                            var result = cmdadd.ExecuteNonQuery();
                            databaseObject.CloseConnection();

                            //MsgBox("Employee Programmed Succesfully !", this.Page, this);
                            //RefreshGridAll();
                            //RefreshGridProgrammed();
                            //ReloadNames();
                            //ReloadDelete();
                            //SelectDay.SelectedIndex = 0;
                            //SelectName.SelectedIndex = 0;
                            //SelectShift.SelectedIndex = 0;
                        }
                        else
                        {
                            string updatetem = "UPDATE ScheduleTemporary SET AvailableSaturday=@SaturdayStatus, ShiftSaturday=@SaturdayShift, AvailableSunday=@SundayStatus, ShiftSunday=@SundayShift WHERE NameandSurname=@NameandSurname ";
                            SqlCommand cmduptem = new SqlCommand(updatetem, databaseObject.myConnection);
                            cmduptem.Parameters.AddWithValue("@SaturdayStatus", SaturdayStatus);
                            cmduptem.Parameters.AddWithValue("@SaturdayShift", SaturdayShift);
                            cmduptem.Parameters.AddWithValue("@SundayStatus", SundayStatus);
                            cmduptem.Parameters.AddWithValue("@SundayShift", SundayShift);
                            cmduptem.Parameters.AddWithValue("@NameandSurname", name);

                            databaseObject.OpenConnection();
                            var restem = cmduptem.ExecuteNonQuery();
                            databaseObject.CloseConnection();

                            //MsgBox("Employee Programmed Succesfully !", this.Page, this);
                            //RefreshGridAll();
                            //RefreshGridProgrammed();
                            //ReloadNames();
                            //ReloadDelete();
                            //SelectDay.SelectedIndex = 0;
                            //SelectName.SelectedIndex = 0;
                            //SelectShift.SelectedIndex = 0;
                        }

                    }


                    MsgBox("Employees Programmed Succesfully !", this.Page, this);
                    RefreshGridAll();
                    RefreshGridProgrammed();
                    ReloadNames();
                    ReloadDelete();
                    SelectDay.SelectedIndex = 0;
                    SelectName.SelectedIndex = 0;
                    SelectShift.SelectedIndex = 0;



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

        protected void btnDeleteOne_Click(object sender, EventArgs e)
        {

            try
            {
                string loggedinID = Request.Cookies["userdata"].Value;
                GetUserName getUserName = new GetUserName();
                string loggedin = getUserName.GetName(loggedinID);

                Database databaseObject = new Database();


                string name = SelectDel.Value;
                string AvailableSaturday = "";
                string AvailableSunday = "";
                string ShiftSaturday = "";
                string ShiftSunday = "";

                

                    string verif = "SELECT AvailableSaturday,AvailableSunday,ShiftSaturday,ShiftSunday from Employees WHERE NameandSurname=@NameandSurname";
                    SqlCommand cmdverif = new SqlCommand(verif, databaseObject.myConnection);
                    cmdverif.Parameters.AddWithValue("@NameandSurname", name);

                    databaseObject.OpenConnection();
                    SqlDataReader reader = cmdverif.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AvailableSaturday = reader[0].ToString();
                            AvailableSunday = reader[1].ToString();
                            ShiftSaturday = reader[2].ToString();
                            ShiftSunday = reader[3].ToString();
                        }

                    }
                    databaseObject.CloseConnection();

                    if (AvailableSaturday != "BUSY")
                    {
                        AvailableSaturday = "AVAILABLE";
                        ShiftSaturday = "NONE";

                    }

                    if (AvailableSunday != "BUSY")
                    {
                        AvailableSunday = "AVAILABLE";
                        ShiftSunday = "NONE";

                    }

                    string queryup = "UPDATE Employees SET AvailableSaturday=@AvailableSaturday, AvailableSunday=@AvailableSunday, " +
                        "ShiftSaturday=@ShiftSaturday, ShiftSunday=@ShiftSunday WHERE NameandSurname=@NameandSurname ";
                    SqlCommand comup = new SqlCommand(queryup, databaseObject.myConnection);
                    comup.Parameters.AddWithValue("@NameandSurname", name);
                    comup.Parameters.AddWithValue("@AvailableSaturday", AvailableSaturday);
                    comup.Parameters.AddWithValue("@AvailableSunday", AvailableSunday);
                    comup.Parameters.AddWithValue("@ShiftSaturday", ShiftSaturday);
                    comup.Parameters.AddWithValue("@ShiftSunday", ShiftSunday);

                    databaseObject.OpenConnection();
                    var res = comup.ExecuteNonQuery();
                    databaseObject.CloseConnection();

                



                DeleteOne();
                RefreshGridAll();
                RefreshGridProgrammed();
                ReloadNames();
                ReloadDelete();
                MsgBox("Deleted Schedule Succesfully", this.Page, this);


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

        protected void GridViewEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.Cells[6].Text).ToUpper() == "AVAILABLE")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                }
                else if ((e.Row.Cells[6].Text).ToUpper() == "BUSY")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                }
                else if ((e.Row.Cells[6].Text).ToUpper() == "PROGRAMMED")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[8].Text).ToUpper() == "AVAILABLE")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                }
                else if ((e.Row.Cells[8].Text).ToUpper() == "BUSY")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                }
                else if ((e.Row.Cells[8].Text).ToUpper() == "PROGRAMMED")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[7].Text).ToUpper() == "NONE")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                }
                else 
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[9].Text).ToUpper() == "NONE")
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }


        protected void GridViewProgrammed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.Cells[6].Text).ToUpper() == "AVAILABLE")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                }
                else if ((e.Row.Cells[6].Text).ToUpper() == "BUSY")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                }
                else if ((e.Row.Cells[6].Text).ToUpper() == "PROGRAMMED")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[8].Text).ToUpper() == "AVAILABLE")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                }
                else if ((e.Row.Cells[8].Text).ToUpper() == "BUSY")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                }
                else if ((e.Row.Cells[8].Text).ToUpper() == "PROGRAMMED")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[7].Text).ToUpper() == "NONE")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[9].Text).ToUpper() == "NONE")
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
}
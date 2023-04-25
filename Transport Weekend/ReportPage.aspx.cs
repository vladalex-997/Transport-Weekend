using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Transport_Weekend
{
    public partial class ReportPage : System.Web.UI.Page
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
                    
                    SelectRouteSearch.DataSource = ds.Tables[0];
                    SelectRouteSearch.DataTextField = ds.Tables[0].Columns["RouteName"].ToString();
                    SelectRouteSearch.DataValueField = ds.Tables[0].Columns["RouteName"].ToString();

                    SelectRouteSearch.DataBind();
                    SelectRouteSearch.Items.Insert(0, "Open Select");

                    databaseObject.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            inputShiftLeaderSearch.Value = "";
            inputSubordinateSearch.Value = "";
            SelectDaySearch.Value = "";
            SelectShiftSearch.Value = "";
            SelectRouteSearch.Value = "Open Select";
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the run time error "  
            //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Raport_" + DateTime.Now.ToString("dd.MM.yyyy") + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridViewRaport.GridLines = GridLines.Both;
            GridViewRaport.HeaderStyle.Font.Bold = true;
            GridViewRaport.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        protected void btnShowFilter_Click(object sender, EventArgs e)
        {
            string ShiftLeader = inputShiftLeaderSearch.Value;
            string Subordinate = inputSubordinateSearch.Value;
            string Day = SelectDaySearch.Value;
            string Shift = SelectShiftSearch.Value;
            string Route = SelectRouteSearch.Value;
            string queryfin = "";

            var v1 = (ShiftLeader == "" && Subordinate == "" && Day == "" && Shift == "" && Route == "Open Select"); // ALL EMPTY

            var v2 = (ShiftLeader != "" && Subordinate == "" && Day == "" && Shift == "" && Route == "Open Select"); // ShiftLeader
            var v3 = (ShiftLeader == "" && Subordinate != "" && Day == "" && Shift == "" && Route == "Open Select"); // Subordinate
            var v4 = (ShiftLeader == "" && Subordinate == "" && Day != "" && Shift == "" && Route == "Open Select"); // Day
            var v5 = (ShiftLeader == "" && Subordinate == "" && Day == "" && Shift != "" && Route == "Open Select"); // Shift
            var v6 = (ShiftLeader == "" && Subordinate == "" && Day == "" && Shift == "" && Route != "Open Select"); // Route

            var v7 = (ShiftLeader != "" && Subordinate != "" && Day == "" && Shift == "" && Route == "Open Select"); // ShiftLeader + Subordinate
            var v8 = (ShiftLeader != "" && Subordinate == "" && Day != "" && Shift == "" && Route == "Open Select"); // ShiftLeader + Day
            var v9 = (ShiftLeader != "" && Subordinate == "" && Day == "" && Shift != "" && Route == "Open Select"); // ShiftLeader + Shift
            var v10 = (ShiftLeader != "" && Subordinate == "" && Day == "" && Shift == "" && Route != "Open Select"); // ShiftLeader + Route

            var v11 = (ShiftLeader == "" && Subordinate != "" && Day != "" && Shift == "" && Route == "Open Select"); // Subordinate + Day
            var v12 = (ShiftLeader == "" && Subordinate != "" && Day == "" && Shift != "" && Route == "Open Select"); // Subordinate + Shift
            var v13 = (ShiftLeader == "" && Subordinate != "" && Day == "" && Shift == "" && Route != "Open Select"); // Subordinate + Route

            var v14 = (ShiftLeader == "" && Subordinate == "" && Day != "" && Shift != "" && Route == "Open Select"); // Day + Shift
            var v15 = (ShiftLeader == "" && Subordinate == "" && Day != "" && Shift == "" && Route != "Open Select"); // Day + Route

            var v16 = (ShiftLeader == "" && Subordinate == "" && Day == "" && Shift != "" && Route != "Open Select"); // Shift + Route

            var v17 = (ShiftLeader != "" && Subordinate != "" && Day != "" && Shift == "" && Route == "Open Select"); // ShiftLeader + Subordinate + Day
            var v18 = (ShiftLeader != "" && Subordinate != "" && Day == "" && Shift != "" && Route == "Open Select"); // ShiftLeader + Subordinate + Shift
            var v19 = (ShiftLeader != "" && Subordinate != "" && Day == "" && Shift == "" && Route != "Open Select"); // ShiftLeader + Subordinate + Route
            var v20 = (ShiftLeader != "" && Subordinate == "" && Day != "" && Shift != "" && Route == "Open Select"); // ShiftLeader + Day + Shift
            var v21 = (ShiftLeader != "" && Subordinate == "" && Day != "" && Shift == "" && Route == "Open Select"); // ShiftLeader + Day + Route
            var v22 = (ShiftLeader != "" && Subordinate == "" && Day == "" && Shift != "" && Route == "Open Select"); // ShiftLeader + Shift + Route

            var v23 = (ShiftLeader == "" && Subordinate != "" && Day != "" && Shift != "" && Route == "Open Select"); // Subordinate + Day + Shift
            var v24 = (ShiftLeader == "" && Subordinate != "" && Day != "" && Shift == "" && Route == "Open Select"); // Subordinate + Day + Route
            var v25 = (ShiftLeader == "" && Subordinate != "" && Day == "" && Shift != "" && Route == "Open Select"); // Subordinate + Shift + Route

            var v26 = (ShiftLeader == "" && Subordinate == "" && Day != "" && Shift != "" && Route == "Open Select"); // Day + Shift + Route

            var v27 = (ShiftLeader != "" && Subordinate != "" && Day != "" && Shift != "" && Route == "Open Select"); // ShiftLeader + Subordinate + Day + Shift
            var v28 = (ShiftLeader != "" && Subordinate != "" && Day != "" && Shift == "" && Route == "Open Select"); // ShiftLeader + Subordinate + Day + Route
            var v29 = (ShiftLeader != "" && Subordinate != "" && Day == "" && Shift != "" && Route == "Open Select"); // ShiftLeader + Subordinate + Shift + Route
            var v30 = (ShiftLeader != "" && Subordinate == "" && Day != "" && Shift != "" && Route == "Open Select"); // ShiftLeader + Day + Shift + Route
            var v31 = (ShiftLeader == "" && Subordinate != "" && Day != "" && Shift != "" && Route == "Open Select"); // Subordinate + Day + Shift + Route

            var v32 = (ShiftLeader != "" && Subordinate != "" && Day != "" && Shift != "" && Route == "Open Select"); // ALL FILLED

            if (v1)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE (AvailableSaturday = @Busy OR AvailableSunday = @Busy)"; // ALL EMPTY
            }
            else if (v2)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND (AvailableSaturday = @Busy OR AvailableSunday = @Busy)"; // ShiftLeader
            }
            else if (v3)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND (AvailableSaturday = @Busy OR AvailableSunday = @Busy)"; // Subordinate
            }
            else if (v4)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE AvailableSaturday = @Busy"; // Day
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE AvailableSunday = @Busy"; // Day
                }
            }
            else if (v5)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE ShiftSaturday = @ShiftSaturday OR ShiftSunday = @ShiftSunday AND (AvailableSaturday = @Busy OR AvailableSunday = @Busy)"; // Shift
            }
            else if (v6)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE EmployeeRoute LIKE @Route AND (AvailableSaturday = @Busy OR AvailableSunday = @Busy)"; // Route
            }
            else if (v7)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND (AvailableSaturday = @Busy OR AvailableSunday = @Busy) "; // ShiftLeader + Subordinate
            }
            else if (v8)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND AvailableSaturday = @Busy"; // ShiftLeader + Day
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND AvailableSunday = @Busy"; // ShiftLeader + Day
                }
            }
            else if (v9)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND ShiftSaturday = @ShiftSaturday  OR ShiftSunday = @ShiftSunday AND AvailableSaturday = @Busy AND AvailableSunday = @Busy"; // ShiftLeader + Shift
            }
            else if (v10)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND EmployeeRoute LIKE @Route AND (AvailableSaturday= @Busy OR AvailableSunday = @Busy)"; // ShiftLeader + Route
            }
            else if (v11)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND AvailableSaturday = @Busy"; // Subordinate + Day
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND AvailableSunday = @Busy"; // Subordinate + Day
                }
            }
            else if (v12)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND ShiftSaturday = @ShiftSaturday  OR ShiftSunday = @ShiftSunday AND AvailableSaturday = @Busy AND AvailableSunday = @Busy";// Subordinate + Shift
            }
            else if (v13)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND EmployeeRoute LIKE @Route AND (AvailableSaturday= @Busy OR AvailableSunday = @Busy)"; // Subordinate + Route
            }
            else if (v14)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE ShiftSaturday = @ShiftSaturday AND AvailableSaturday = @Busy"; //  Day + Shift
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE ShiftSunday = @ShiftSunday AND AvailableSunday = @Busy"; // Day + Shift
                }
            }
            else if (v15)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE EmployeeRoute LIKE @Route AND AvailableSaturday = @Busy"; //  Day + Route
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE EmployeeRoute LIKE @Route AND AvailableSunday = @Busy"; // Day + Route
                }
            }
            else if (v16)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE ShiftSaturday = @ShiftSaturday OR ShiftSunday = @ShiftSunday AND EmployeeRoute LIKE @Route"; // Shift + Route
            }
            else if (v17)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND AvailableSaturday = @Busy"; //  ShiftLeader + Subordinate + Day
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND AvailableSunday = @Busy"; // ShiftLeader + Subordinate + Day
                }
            }
            else if (v18)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND ShiftSaturday = @ShiftSaturday  OR ShiftSunday = @ShiftSunday AND AvailableSaturday = @Busy AND AvailableSunday = @Busy"; // ShiftLeader + Subordinate + Shift
            }
            else if (v19)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND ShiftSaturday = @ShiftSaturday  OR ShiftSunday = @ShiftSunday AND EmployeeRoute LIKE @Route"; // ShiftLeader + Subordinate + Route
            }
            else if (v20)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND ShiftSaturday = @ShiftSaturday AND AvailableSaturday = @Busy"; // ShiftLeader + Day + Shift
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND ShiftSunday = @ShiftSunday AND AvailableSunday = @Busy"; // ShiftLeader + Day + Shift
                }
            }
            else if (v21)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND EmployeeRoute LIKE @Route AND AvailableSaturday = @Busy"; // ShiftLeader + Day + Route
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND EmployeeRoute LIKE @Route AND AvailableSunday = @Busy"; // ShiftLeader + Day + Route
                }
            }
            else if (v22)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND EmployeeRoute LIKE @Route AND ShiftSaturday = @ShiftSaturday  OR ShiftSunday = @ShiftSunday"; // ShiftLeader + Shift + Route
            }
            else if (v23)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND ShiftSaturday = @ShiftSaturday AND AvailableSaturday = @Busy"; // Subordinate + Day + Shift
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND ShiftSunday = @ShiftSunday AND AvailableSunday = @Busy"; // Subordinate + Day + Shift
                }
            }
            else if (v24)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND EmployeeRoute LIKE @Route AND AvailableSaturday = @Busy"; // Subordinate + Day + Route
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND EmployeeRoute LIKE @Route AND AvailableSunday = @Busy"; // Subordinate + Day + Route
                }
            }
            else if (v25)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND EmployeeRoute LIKE @Route AND ShiftSaturday = @ShiftSaturday  OR ShiftSunday = @ShiftSunday"; // Subordinate + Shift + Route
            }
            else if (v26)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE ShiftSaturday = @ShiftSaturday AND EmployeeRoute LIKE @Route AND AvailableSaturday = @Busy"; // Day + Shift + Route
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE ShiftSunday = @ShiftSunday AND EmployeeRoute LIKE @Route AND AvailableSunday = @Busy"; // Day + Shift + Route
                } 
            }
            else if (v27)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND ShiftSaturday = @ShiftSaturday AND AvailableSaturday = @Busy"; // ShiftLeader + Subordinate + Day + Shift
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND ShiftSunday = @ShiftSunday AND AvailableSunday = @Busy"; // ShiftLeader + Subordinate + Day + Shift
                }
            }
            else if (v28)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND EmployeeRoute LIKE @Route AND AvailableSaturday = @Busy"; // ShiftLeader + Subordinate + Day + Route
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND EmployeeRoute LIKE @Route AND AvailableSunday = @Busy"; // ShiftLeader + Subordinate + Day + Route
                }
            }
            else if (v29)
            {
                queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND NameandSurname LIKE @Subordinate AND EmployeeRoute LIKE @Route AND ShiftSaturday = @ShiftSaturday  OR ShiftSunday = @ShiftSunday"; // ShiftLeader + Subordinate + Shift + Route
            }
            else if (v30)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND ShiftSaturday = @ShiftSaturday AND EmployeeRoute LIKE @Route AND AvailableSaturday = @Busy"; // ShiftLeader + Day + Shift + Route
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE Superior LIKE @ShiftLeader AND ShiftSunday = @ShiftSunday AND EmployeeRoute LIKE @Route AND AvailableSunday = @Busy"; // ShiftLeader + Day + Shift + Route
                } 
            }
            else if (v31)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND ShiftSaturday = @ShiftSaturday AND EmployeeRoute LIKE @Route AND AvailableSaturday = @Busy"; // Subordinate + Day + Shift + Route
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND ShiftSunday = @ShiftSunday AND EmployeeRoute LIKE @Route AND AvailableSunday = @Busy"; // Subordinate + Day + Shift + Route
                }
            }
            else if (v32)
            {
                if (Day == "SATURDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND Superior LIKE @ShiftLeader AND ShiftSunday = @ShiftSunday AND EmployeeRoute LIKE @Route AND AvailableSaturday = @Busy"; // ShiftLeader + Subordinate + Day + Shift + Route
                }
                else if (Day == "SUNDAY")
                {
                    queryfin = "SELECT * FROM DefinitiveSchedule WHERE NameandSurname LIKE @Subordinate AND Superior LIKE @ShiftLeader AND ShiftSaturday = @ShiftSaturday AND EmployeeRoute LIKE @Route AND AvailableSunday = @Busy"; // ShiftLeader + Subordinate + Day + Shift + Route
                }
            }
            else
            {
                queryfin = "SELECT * FROM DefinitiveSchedule"; // ALL EMPTY
            }

            Database databaseObject = new Database();
            databaseObject.OpenConnection();

            SqlCommand myquerytab = new SqlCommand(queryfin, databaseObject.myConnection);
            myquerytab.Parameters.AddWithValue("@Busy", "BUSY");
            myquerytab.Parameters.AddWithValue("@ShiftLeader","%" + ShiftLeader + "%");
            myquerytab.Parameters.AddWithValue("@Subordinate","%" + Subordinate + "%");
            myquerytab.Parameters.AddWithValue("@Day", Day);
            myquerytab.Parameters.AddWithValue("@ShiftSaturday", Shift);
            myquerytab.Parameters.AddWithValue("@ShiftSunday", Shift);
            myquerytab.Parameters.AddWithValue("@Route", Route);

            SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
            DataTable dttab = new DataTable();
            DataSet ds = new DataSet();
            daquery.Fill(dttab);
            daquery.Fill(ds);
            GridViewRaport.DataSource = ds;
            GridViewRaport.DataBind();

            databaseObject.CloseConnection();
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        protected void GridViewRaport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.Cells[11].Text).ToUpper() == "AVAILABLE")
                {
                    e.Row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                }
                else if ((e.Row.Cells[11].Text).ToUpper() == "BUSY")
                {
                    e.Row.Cells[11].BackColor = System.Drawing.Color.Red;
                }
                else if ((e.Row.Cells[11].Text).ToUpper() == "PROGRAMMED")
                {
                    e.Row.Cells[11].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[13].Text).ToUpper() == "AVAILABLE")
                {
                    e.Row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                }
                else if ((e.Row.Cells[13].Text).ToUpper() == "BUSY")
                {
                    e.Row.Cells[13].BackColor = System.Drawing.Color.Red;
                }
                else if ((e.Row.Cells[13].Text).ToUpper() == "PROGRAMMED")
                {
                    e.Row.Cells[13].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[12].Text).ToUpper() == "NONE")
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Yellow;
                }


                if ((e.Row.Cells[14].Text).ToUpper() == "NONE")
                {
                    e.Row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    e.Row.Cells[14].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
}
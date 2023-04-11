﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Subordinates.aspx.cs" Inherits="Transport_Weekend.Subordonates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />

    <div style="height: 30px"></div>
    <div class="col-12 d-flex justify-content-between my-5">
        <h1>All Subordinates </h1>
        <asp:Button Text="Refresh" ID="btnRefresh" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnRefresh_Click" />

    </div>

     <%-- Table --%>
    <div class="row mt-5 mb-4 align-items-center justify-content-center">
        <div class="col-12 offset-0 text-center">
            <asp:GridView ID="GridViewEmployees" class="table table-responsive table-hover wrapword"  runat="server" AutoGenerateColumns="false" OnRowDataBound="GridViewEmployees_RowDataBound">
                <HeaderStyle  BackColor="#0d6efd" ForeColor="White" />
                <Columns>
                <asp:BoundField DataField="SAPid" HeaderText="SAP ID" />
                <asp:BoundField DataField="NameandSurname" HeaderText="Full Name" />
                <asp:BoundField DataField="Deparment" HeaderText="Department" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:BoundField DataField="HomeAddress" HeaderText="Address" />
                <asp:BoundField DataField="EmployeeRoute" HeaderText="Route" />
                <asp:BoundField DataField="AvailableSaturday" HeaderText="Saturday" />
                <asp:BoundField DataField="ShiftSaturday" HeaderText="Shift Saturday" />
                <asp:BoundField DataField="AvailableSunday" HeaderText="Sunday" />
               
                <asp:BoundField DataField="ShiftSunday" HeaderText="Shift Sunday" />

                </Columns>
            </asp:GridView>
        </div>

    </div>

     <div class="col-12 d-flex justify-content-between my-5">
         <div>
            <select class="form-select fs-5"  aria-describedby="Name" id="SelectName" runat="server" style="width:300px;" >
                <option selected value="">Open select</option>
              <%--  <option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>--%>

            </select>

              <span id="Name" class="form-text fs-5 mt-5 ms-2">Select Employee Name.</span>

        </div>
         <div>
            <select class="form-select fs-5" aria-describedby="Day" id="SelectDay" runat="server">
                <option selected value="">Open select</option>
                <option value="Saturday">Saturday</option>
                <option value="Sunday">Sunday</option>

            </select>
              <span id="Day" class="form-text fs-5 mt-5 ms-2">Select Day.</span>
        </div>
         <div>
            <select class="form-select fs-5" aria-describedby="Shift" id="SelectShift" runat="server">
                <option selected value="">Open select</option>
                <option value="SHIFT 1">Shift 1</option>
                <option value="SHIFT 2">Shift 2</option>
                <option value="SHIFT 3">Shift 3</option>

            </select>

              <span id="Shift" class="form-text fs-5 mt-5 ms-2">Select Shift.</span>
        </div>

          <asp:Button Text="Schedule Employee" ID="buttonSchedule" CssClass="btn btn-primary fs-4" Width="300px" runat="server" OnClick="btnSchedule_Click" />


     </div>


       <div style="height: 30px"></div>
    <div class="col-12 d-flex justify-content-between my-5">
        <h1>Programmed Subordinates </h1>
        <asp:Button Text="Delete Programmed/Reset" ID="buttondeleteProgrammed" CssClass="btn btn-danger fs-5" Width="400px" runat="server" OnClick="btndeleteProgrammed_Click" />

        <asp:Button Text="Refresh" ID="buttonProgrammed" CssClass="btn btn-primary fs-4" Width="220px" runat="server" OnClick="btnProgrammed_Click" />

    </div>

     <%-- Table --%>
    <div class="row mt-5 mb-4 align-items-center justify-content-center">
        <div class="col-12 offset-0 text-center">
            <asp:GridView ID="GridViewProgrammed" class="table table-responsive table-hover wrapword"  runat="server" AutoGenerateColumns="false" OnRowDataBound="GridViewProgrammed_RowDataBound">
                <HeaderStyle  BackColor="#0d6efd" ForeColor="White" />
                <Columns>
                <asp:BoundField DataField="SAPid" HeaderText="SAP ID" />
                <asp:BoundField DataField="NameandSurname" HeaderText="Full Name" />
                <asp:BoundField DataField="Department" HeaderText="Department" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:BoundField DataField="HomeAddress" HeaderText="Address" />
                <asp:BoundField DataField="EmployeeRoute" HeaderText="Route" />
                <asp:BoundField DataField="AvailableSaturday" HeaderText="Saturday" />
                <asp:BoundField DataField="ShiftSaturday" HeaderText="Shift Saturday" />
                <asp:BoundField DataField="AvailableSunday" HeaderText="Sunday" />
                
                <asp:BoundField DataField="ShiftSunday" HeaderText="Shift Sunday" />

                </Columns>
            </asp:GridView>
        </div>

    </div>

</asp:Content>

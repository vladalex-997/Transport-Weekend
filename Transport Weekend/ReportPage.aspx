<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportPage.aspx.cs" Inherits="Transport_Weekend.ReportPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- nume superior, nume angajat, zi, schimb, ruta --%>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <div class="row mt-5 mb-4 align-items-center">
        <div class="justify-content-between d-inline-block">
            <%-- Shift Leader --%>
            <div class="col-1 d-inline-block me-4">
                <label for="inputShiftLeaderSearch" class="col-form-label fs-5">Shift Leader:</label>
            </div>
            <div class="col-2 d-inline-block">
                <input type="text" id="inputShiftLeaderSearch" class="form-control fs-5" aria-describedby="LeaderHelpInlineSearch" runat="server" placeholder="Enter Shift Leader">
            </div>
            <%-- Subordinate --%>
            <div class="col-1 d-inline-block offset-1 me-4">
                <label for="inputSubordinateSearch" class="col-form-label fs-5">Subordinate:</label>
            </div>
            <div class="col-2 d-inline-block">
                <input type="text" id="inputSubordinateSearch" class="form-control fs-5" aria-describedby="SubordinateHelpInlineSearch" runat="server" placeholder="Enter Subordinate">
            </div>
        </div>
    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <%-- Day --%>
        <div class="col-1 me-4">
            <label for="inputDaySearch" class="col-form-label fs-5">Day:</label>
        </div>
        <div class="col-2">
            <select class="form-select fs-5" aria-label="Select Day" aria-describedby="DayHelpInlineSearch" id="SelectDaySearch" runat="server">
                <option selected value="">Open select</option>
                <option value="SATURDAY">Saturday</option>
                <option value="SUNDAY">Sunday</option>
            </select>
        </div>

        <%-- Shift --%>
        <div class="col-1 offset-1 me-4">
            <label for="inputShiftSearch" class="col-form-label fs-5">Shift:</label>
        </div>
        <div class="col-2">
            <select class="form-select fs-5" aria-label="Select Day" aria-describedby="ShiftHelpInlineSearch" id="SelectShiftSearch" runat="server">
                <option selected value="">Open select</option>
                <option value="SHIFT 1">Shift 1</option>
                <option value="SHIFT 2">Shift 2</option>
                <option value="SHIFT 3">Shift 3</option>
            </select>
        </div>

        <%-- Route --%>
        <div class="col-1 offset-1 me-4">
            <label for="inputRouteSearch" class="col-form-label fs-5">Route:</label>
        </div>
        <div class="col-2">
            <select class="form-select fs-5" aria-label="Select Route" aria-describedby="RouteHelpInlineSearch" id="SelectRouteSearch" runat="server">
                <option selected value="">Open select</option>
            </select>
        </div>
    </div>
    
    <%-- Buttons --%>
    <div class="row mt-5 mb-4 align-items-center">
        <%-- Show Filter --%>
        <div class="col-1 offset-1">
            <asp:Button Text="Show Filter" ID="btnShowFilter" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnShowFilter_Click" />
        </div>
        
        <%-- Export Excel --%>
        <div class="col-1 offset-1">
            <asp:Button Text="Export" ID="btnExport" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnExport_Click" />
        </div>

        <%-- Reset Fields --%>
        <div class="col-1 offset-1">
            <asp:Button Text="Reset Fields" ID="btnReset" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnReset_Click" />
        </div>
    </div>

    
        
    <div class="row mt-5 mb-4 align-items-center">
        <%-- Table --%>
        <asp:GridView ID="GridViewRaport" class="table table-responsive table-hover wrapword"  runat="server" AutoGenerateColumns="false">
                <HeaderStyle  BackColor="#0d6efd" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="Company" HeaderText="Company" />
                    <asp:BoundField DataField="CNP" HeaderText="CNP" />
                    <asp:BoundField DataField="SAPid" HeaderText="SAP ID" />
                    <asp:BoundField DataField="CostCentre" HeaderText="Cost Center" />
                    <asp:BoundField DataField="CostCentreName" HeaderText="Cost Center Name" />
                    <asp:BoundField DataField="NameandSurname" HeaderText="Full Name" />
                    <asp:BoundField DataField="Superior" HeaderText="Leader" />
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
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Subordinates.aspx.cs" Inherits="Transport_Weekend.Subordonates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />

    <div style="height: 30px"></div>
    <div class="col-12 d-flex justify-content-between my-5">
        <h1>Available Subordinates </h1>
        <asp:Button Text="Refresh Available" ID="btnRefresh" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnRefresh_Click" />

    </div>

     <%-- Table --%>
    <div class="row mt-5 mb-4 align-items-center justify-content-center">
        <div class="col-12 offset-0 text-center">
            <asp:GridView ID="GridViewEmployees" class="table table-responsive table-hover wrapword"  runat="server" AutoGenerateColumns="false">
                <HeaderStyle  BackColor="#0d6efd" ForeColor="White" />
                <Columns>
                <asp:BoundField DataField="Company" HeaderText="Company" />
                <asp:BoundField DataField="CNP" HeaderText="CNP" />
                <asp:BoundField DataField="SAPid" HeaderText="SAP ID" />
                <asp:BoundField DataField="CostCentre" HeaderText="Cost Center" />
                <asp:BoundField DataField="CostCentreName" HeaderText="Cost Center Name" />
                <asp:BoundField DataField="NameandSurname" HeaderText="Full Name" />
                <asp:BoundField DataField="Deparment" HeaderText="Department" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:BoundField DataField="HomeAddress" HeaderText="Address" />
                <asp:BoundField DataField="UserStatus" HeaderText="Status" />
                <asp:BoundField DataField="EmployeeRoute" HeaderText="Route" />
                </Columns>
            </asp:GridView>
        </div>

    </div>

</asp:Content>

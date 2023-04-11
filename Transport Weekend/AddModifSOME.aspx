<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddModifSOME.aspx.cs" Inherits="Transport_Weekend.AddModifSOME" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />

    <div class="row mt-5 ">

        <div class="col-3 offset-0 me-4">
            <h2>Add Employee</h2>
        </div>

        <div class="col-4 offset-3">
            <h2>Modify Employee</h2>
        </div>

    </div>


     <%-- SAP ID --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4">
            <label for="inputIdAdd" class="col-form-label fs-5">SAP ID:</label>
        </div>
        <div class="col-2 offset-0">
            <input type="text" id="inputIdAdd" class="form-control fs-5" aria-describedby="idHelpInlineAdd" runat="server" placeholder="Enter SAP ID">
        </div>
        

        <div class="col-1 offset-3 me-4">
            <label for="inputIdMod" class="col-form-label fs-5">SAP ID:</label>
        </div>
        <div class="col-2 offset-0">
            <input type="text" id="inputIdMod" class="form-control fs-5" aria-describedby="idHelpInlineMod" runat="server" placeholder="Enter SAP ID">
        </div>

        <div class="col-2">
            <asp:Button Text="Autocom by ID" ID="Button1" CssClass="btn btn-primary fs-4" runat="server" OnClick="btnAutoCom_Click" />
        </div>
    </div>


    <%-- CNP --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4">
            <label for="inputCPNAdd" class="col-form-label fs-5">CNP:</label>
        </div>

        <div class="col-2 offset-0">
            <input type="text" id="inputCNPAdd" class="form-control fs-5" aria-describedby="cnpHelpInlineAdd" runat="server" placeholder="Enter CNP">
        </div>

        

        <div class="col-1 offset-3 me-4">
            <label for="inputCNPMod" class="col-form-label fs-5">CNP:</label>
        </div>

        <div class="col-2 offset-0">
            <input type="text" id="inputCNPMod" class="form-control fs-5" aria-describedby="cnpHelpInlineMod" runat="server" placeholder="Enter CNP">
        </div>

       

    </div>


    
    <%-- Company --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label class="col-form-label fs-5">Company:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Company" aria-describedby="CompanyHelpInlineAdd" id="SelectCompanyAdd" runat="server">
                <option selected value="">Open select</option>
                <option value="MARTUR">Martur</option>
                <option value="FOMPAK">Fompak</option>

            </select>
        </div>


        <div class="col-1 offset-3 me-4">
            <label class="col-form-label fs-5">Company:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Company" aria-describedby="CompanyHelpInlineMod" id="SelectCompanyMod" runat="server">
                <option selected value="">Open select</option>
                <option value="MARTUR">Martur</option>
                <option value="FOMPAK">Fompak</option>

            </select>
        </div>
        

    </div>
   

    <%-- COST CENTER --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label for="inputCostCenterAdd" class="col-form-label fs-5">CostCenter:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputCostCenterAdd" class="form-control fs-5" aria-describedby="costCenterHelpInlineAdd" runat="server" placeholder="Enter Cost Center">
        </div>

        <div class="col-1 offset-3 me-4">
            <label for="inputCostCenterMod" class="col-form-label fs-5">CostCenter:</label>
        </div>
        <div class="col-2 offset-0">
            <input type="text" id="inputCostCenterMod" class="form-control fs-5" aria-describedby="inputCostCenterMod" runat="server" placeholder="Enter Cost Center">
        </div>
    </div>

    <%-- Cost Center Name --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label for="inputCostCenterNameAdd" class="col-form-label fs-5">CenterName:</label>
        </div>
        <div class="col-2 offset-0">
            <input type="text" id="inputCostCenterNameAdd" class="form-control fs-5" aria-describedby="costCenterNameHelpInlineAdd" runat="server" placeholder="Enter Center Name">
        </div>

        <div class="col-1 offset-3 me-4">
            <label for="inputCostCenterNameMod" class="col-form-label fs-5">CenterName:</label>
        </div>
        <div class="col-2 offset-0">
            <input type="text" id="inputCostCenterNameMod" class="form-control fs-5" aria-describedby="inputCostCenterNameMod" runat="server" placeholder="Enter Center Name">
        </div>
    </div>

    <%-- Name and Surname --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label for="inputNameSurnameAdd" class="col-form-label fs-5">FullName:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputNameSurnameAdd" class="form-control fs-5" aria-describedby="nameSurnameHelpInlineAdd" runat="server" placeholder="Enter Full Name">
        </div>

        <div class="col-1 offset-3 me-4">
            <label for="inputNameSurnameMod" class="col-form-label fs-5">FullName:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputNameSurnameMod" class="form-control fs-5" aria-describedby="inputNameSurnameMod" runat="server" placeholder="Enter Full Name">
        </div>
    </div>

    <%-- Department --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label for="inputDepartmentAdd" class="col-form-label fs-5">Department:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputDepartmentAdd" class="form-control fs-5" aria-describedby="departmentHelpInlineAdd" runat="server" placeholder="Enter Department">
        </div>

        <div class="col-1 offset-3 me-4 ">
            <label for="inputDepartmentMod" class="col-form-label fs-5">Department:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputDepartmentMod" class="form-control fs-5" aria-describedby="inputDepartmentMod" runat="server" placeholder="Enter Department">
        </div>
    </div>

    <%-- Phone --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label for="inputPhoneAdd" class="col-form-label fs-5">Phone:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputPhoneAdd" class="form-control fs-5" aria-describedby="phoneHelpInlineAdd" runat="server" placeholder="Enter Phone">
        </div>

        <div class="col-1 offset-3 me-4 ">
            <label for="inputPhoneMod" class="col-form-label fs-5">Phone:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputPhoneMod" class="form-control fs-5" aria-describedby="inputPhoneMod" runat="server" placeholder="Enter Phone">
        </div>
    </div>

    <%-- HomeAddress --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label for="inputHomeAddressAdd" class="col-form-label fs-5">Address:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputHomeAddressAdd" class="form-control fs-5" aria-describedby="homeAddressHelpInlineAdd" runat="server" placeholder="Enter Address">
        </div>

        <div class="col-1 offset-3 me-4 ">
            <label for="inputHomeAddressMod" class="col-form-label fs-5">Address:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputHomeAddressMod" class="form-control fs-5" aria-describedby="inputHomeAddressMod" runat="server" placeholder="Enter Address">
        </div>
    </div>

    <%-- UserStatus --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label class="col-form-label fs-5">User Stat:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Status" aria-describedby="StatusHelpInlineAdd" id="SelectStatusAdd" runat="server">
                <option selected value="">Open select</option>
                <option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>

            </select>
        </div>

        <div class="col-1 offset-3 me-4 ">
            <label class="col-form-label fs-5">User Stat:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Status" aria-describedby="StatusHelpInlineMod" id="SelectStatusMod" runat="server">
                <option selected value="">Open select</option>
                <option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>

            </select>
        </div>

    </div>

    <%-- EmployeeRoute --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
            <label class="col-form-label fs-5">Route:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Route" aria-describedby="RouteHelpInlineAdd" id="SelectRouteAdd" runat="server">
                <option selected value="">Open select</option>
              <%--  <option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>--%>

            </select>
        </div>

        <div class="col-1 offset-3 me-4 ">
            <label class="col-form-label fs-5">Route:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Route" aria-describedby="RouteHelpInlineMod" id="SelectRouteMod" runat="server">
                <option selected value="">Open select</option>
                <%--<option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>--%>

            </select>
        </div>

    </div>

    <%-- AvailableSaturday --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
        </div>
        <div class="col-1 offset-5 me-4 ">
            <label class="col-form-label fs-5">Saturday:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select Saturday Status" aria-describedby="StatusHelpInlineMod" id="SelectSaturdayMod" runat="server">
                <option selected value="">Open select</option>
                <option value="AVAILABLE">Active</option>
                <option value="PROGRAMED">Programed</option>
                <option value="BUSY">Busy</option>
            </select>
        </div>

    </div>

    <%-- AvailableSunday --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
        </div>
        <div class="col-1 offset-5 me-4 ">
            <label class="col-form-label fs-5">Sunday:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select Saturday Status" aria-describedby="StatusHelpInlineMod" id="SelectSundayMod" runat="server">
                <option selected value="">Open select</option>
                <option value="AVAILABLE">Active</option>
                <option value="PROGRAMED">Programed</option>
                <option value="BUSY">Busy</option>
            </select>
        </div>

    </div>

    <%-- ShiftSunday --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0 me-4 ">
        </div>
        <div class="col-1 offset-5 me-4 ">
            <label class="col-form-label fs-5">Sunday:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select Saturday Status" aria-describedby="StatusHelpInlineMod" id="Select1" runat="server">
                <option selected value="">Open select</option>
                <option value="NONE">None</option>
                <option value="PROGRAMED">Programed</option>
                <option value="BUSY">Busy</option>
            </select>
        </div>

    </div>

    <%-- Buttons --%>
    <div class="row my-5 align-items-center">

        <%-- ADD USER --%>
        <div class="col-3 offset-0 me-4">
            <asp:Button Text="Add Employee" ID="btnAdd" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnAdd_Click" />
        </div>

        <%-- MOD USER --%>
        <div class="col-3 offset-3">
            <asp:Button Text="Modify Employee" ID="btnModif" CssClass="btn btn-primary fs-4" Width="220px" runat="server" OnClick="btnModif_Click" />
        </div>

    </div>

    <%-- SHOW ALL --%>
    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-2 offset-0 text-center">
            <asp:Button Text="Show All" ID="btnShowAll" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnShowAll_Click" />
        </div>
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

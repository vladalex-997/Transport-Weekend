<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddModifALL.aspx.cs" Inherits="Transport_Weekend.AddModifALL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
   
    <div class="row mt-5 ">

        <div class="col-4 offset-0">
            <h2>Add Application User</h2>
        </div>


        <div class="col-4 offset-3">
            <h2>Modify Application User</h2>
        </div>

    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0">
            <label for="inputIdAdd" class="col-form-label fs-5">SAP ID:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputIdAdd" class="form-control fs-5" aria-describedby="idHelpInlineAdd" runat="server" placeholder="Enter SAP ID">
        </div>
        <div class="col-3">
            <span id="idHelpInlineAdd" class="form-text fs-5">Enter Unique ID (Cannot be modified later).
            </span>
        </div>

        <div class="col-1 offset-1">
            <label for="inputIdMod" class="col-form-label fs-5">SAP ID:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputIdMod" class="form-control fs-5" aria-describedby="idHelpInlineMod" runat="server" placeholder="Enter SAP ID">
        </div>
        <div class="col-2">
            <asp:Button Text="Autocom by ID" ID="btnAutoCom" CssClass="btn btn-primary fs-4" runat="server" OnClick="btnAutoCom_Click" />

        </div>
    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0">
            <label for="inputPassAdd" class="col-form-label fs-5">Password:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputPassAdd" class="form-control fs-5" aria-describedby="PassHelpInlineAdd" runat="server" placeholder="Enter Password">
        </div>
        <div class="col-2">
            <span id="PassHelpInlineAdd" class="form-text fs-5">Enter Password.
            </span>
        </div>

        <div class="col-1 offset-2">
            <label for="inputPassMod" class="col-form-label fs-5">Password:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputPassMod" class="form-control fs-5" aria-describedby="PassHelpInlineMod" runat="server" placeholder="Enter Password">
        </div>
        <div class="col-2">
            <span id="PassHelpInlineMod" class="form-text fs-5">Enter Password.
            </span>
        </div>
    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0">
            <label for="inputNameAdd" class="col-form-label fs-5">Name:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputNameAdd" class="form-control fs-5" aria-describedby="NameHelpInlineAdd" runat="server" placeholder="Enter Full Name">
        </div>
        <div class="col-2">
            <span id="NameHelpInlineAdd" class="form-text fs-5">Enter Name like in domain (name.surname).
            </span>
        </div>

        <div class="col-1 offset-2">
            <label for="inputNameMod" class="col-form-label fs-5">Name:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputNameMod" class="form-control fs-5" aria-describedby="NameHelpInlineMod" runat="server" placeholder="Enter Full Name">
        </div>
        <div class="col-2">
            <span id="NameHelpInlineMod" class="form-text fs-5">Enter Name like in domain (name.surname).
            </span>
        </div>
    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0">
            <label class="col-form-label fs-5">User Role:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Role" aria-describedby="RoleHelpInlineAdd" id="SelectRoleAdd" runat="server">
                <option selected value="">Open select</option>
                <option value="ADMIN">Admin</option>
                <option value="SEF SCHIMB">Sef Schimb</option>

            </select>
        </div>
        <div class="col-2">
            <span id="RoleHelpInlineAdd" class="form-text fs-5">Select a value.
            </span>
        </div>

        <div class="col-1 offset-2">
            <label class="col-form-label fs-5">User Role:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Role" aria-describedby="RoleHelpInlineMod" id="SelectRoleMod" runat="server">
                <option selected value="">Open select</option>
                <option value="ADMIN">Admin</option>
                <option value="SEF SCHIMB">Sef schimb</option>

            </select>
        </div>
        <div class="col-2">
            <span id="RoleHelpInlineMod" class="form-text fs-5">Select a value.
            </span>
        </div>

    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0">
            <label class="col-form-label fs-5">User Stat:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Status" aria-describedby="StatusHelpInlineAdd" id="SelectStatusAdd" runat="server">
                <option selected value="">Open select</option>
                <option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>

            </select>
        </div>
        <div class="col-2">
            <span id="StatusHelpInlineAdd" class="form-text fs-5">Select a value.
            </span>
        </div>

        <div class="col-1 offset-2">
            <label class="col-form-label fs-5">User Stat:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Status" aria-describedby="StatusHelpInlineMod" id="SelectStatusMod" runat="server">
                <option selected value="">Open select</option>
                <option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>

            </select>
        </div>
        <div class="col-2">
            <span id="StatusHelpInlineMod" class="form-text fs-5">Select a value.
            </span>
        </div>

    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0">
            <label class="col-form-label fs-5">Route:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Route" aria-describedby="RouteHelpInlineAdd" id="SelectRouteAdd" runat="server">
                <option selected value="">Open select</option>
              <%--  <option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>--%>

            </select>
        </div>
        <div class="col-2">
            <span id="RouteHelpInlineAdd" class="form-text fs-5">Select a value.
            </span>
        </div>

        <div class="col-1 offset-2">
            <label class="col-form-label fs-5">Route:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Route" aria-describedby="RouteHelpInlineMod" id="SelectRouteMod" runat="server">
                <option selected value="">Open select</option>
                <%--<option value="ACTIVE">Active</option>
                <option value="INACTIVE">Inactive</option>--%>

            </select>
        </div>
        <div class="col-2">
            <span id="RouteHelpInlineMod" class="form-text fs-5">Select a value.
            </span>
        </div>

    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-0">
            <label class="col-form-label fs-5">Company:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Company" aria-describedby="CompanyHelpInlineAdd" id="SelectCompanyAdd" runat="server">
                <option selected value="">Open select</option>
                <option value="MARTUR">Martur</option>
                <option value="FOMPAK">Fompak</option>

            </select>
        </div>
        <div class="col-2">
            <span id="CompanyHelpInlineAdd" class="form-text fs-5">Select a value.
            </span>
        </div>

        <div class="col-1 offset-2">
            <label class="col-form-label fs-5">Company:</label>
        </div>
        <div class="col-2 offset-0">
            <select class="form-select fs-5" aria-label="Select User Company" aria-describedby="CompanyHelpInlineMod" id="SelectCompanyMod" runat="server">
                <option selected value="">Open select</option>
                <option value="MARTUR">Martur</option>
                <option value="FOMPAK">Fompak</option>

            </select>
        </div>
        <div class="col-2">
            <span id="CompanyHelpInlineMod" class="form-text fs-5">Select a value.
            </span>
        </div>

    </div>


    <div class="row my-5 align-items-center">
        <div class="col-3 offset-0">
            <asp:Button Text="Add New User" ID="btnAdd" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnAdd_Click" />
        </div>



        <div class="col-3 offset-4">
            <asp:Button Text="Modify User" ID="btnModif" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnModif_Click" />
        </div>

    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-2 offset-0 text-center">
            <asp:Button Text="Show All Users" ID="btnShowAll" CssClass="btn btn-primary fs-4" Width="200px" runat="server" OnClick="btnShowAll_Click" />
        </div>
    </div>

    <div class="row mt-5 mb-4 align-items-center justify-content-center">
        <div class="col-12 offset-0 text-center">
            <asp:GridView ID="GridViewUsers" class="table table-responsive table-hover wrapword"  runat="server" AutoGenerateColumns="false">
                <HeaderStyle  BackColor="#0d6efd" ForeColor="White" />
                <Columns>
                <asp:BoundField DataField="UniqueId" HeaderText="SAP ID" />
                <asp:BoundField DataField="UserPassword" HeaderText="Password" />
                <asp:BoundField DataField="UserNameandSurname" HeaderText="Full Name" />
                <asp:BoundField DataField="UserRole" HeaderText="User Role" />
                <asp:BoundField DataField="UserStatus" HeaderText="User Status" />
                <asp:BoundField DataField="EmployeeRoute" HeaderText="User Route" />
                <asp:BoundField DataField="Company" HeaderText="Company" />
                </Columns>
            </asp:GridView>
        </div>

    </div>

</asp:Content>

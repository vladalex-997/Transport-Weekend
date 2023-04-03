<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Transport_Weekend._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div style="height: 50px"></div>
    <div class="col-12 d-flex justify-content-center my-5">
        <h1>Application Login </h1>
    </div>

    <div class="row mt-5 mb-4 align-items-center">
        <div class="col-1 offset-4">
            <label for="inputId" class="col-form-label fs-5">Login ID:</label>
        </div>
        <div class="col-2">
            <input type="text" id="inputId" class="form-control fs-5" aria-describedby="idHelpInline" runat="server" required>
        </div>
        <div class="col-3">
            <span id="idHelpInline" class="form-text fs-5">Enter Unique ID.
            </span>
        </div>
    </div>

    <div class="row align-items-center">
        <div class="col-1 offset-4">
            <label for="inputPassword" class="col-form-label fs-5">Password:</label>
        </div>
        <div class="col-2">
            <input type="password" id="inputPassword" class="form-control fs-5" aria-describedby="passwordHelpInline" runat="server" required>
        </div>
        <div class="col-3">
            <span id="passwordHelpInline" class="form-text fs-5">Enter Account Password.
            </span>
        </div>
    </div>

    <div class="row my-5 align-items-center">
        <div class="col-3 offset-5">
            <asp:Button Text="Log-In" ID="btnLogin" CssClass="btn btn-primary fs-3" Width="200px" runat="server" OnClick="btnLogin_Click" />
        </div>

    </div>





</asp:Content>

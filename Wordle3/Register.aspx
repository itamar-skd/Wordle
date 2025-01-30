<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Wordle.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Login.css" rel="stylesheet" />
    <p style="
        text-align: center;
        font-size: 24px;
        font-family: 'Lato', sans-serif;
        color: #e63946;
    "><% = message %></p>
    <div class="wrapper register-wrapper">
        <form id="register" method="post">
            <label for="username">Username: </label>
            <input type="text" name="username" id="username" /><br />
            <label for="password">Password: </label>
            <input type="password" name="password" id="password" /><br />
            <input type="submit" name="submit" id="submit" />
        </form>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Wordle.aspx.cs" Inherits="Wordle.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Wordle.css" rel="stylesheet" />
    <div id="wordle">
        <p id="rowOne">
            <% = rowOne %>
        </p>
        <p id="rowTwo">
            <% = rowTwo %>
        </p>
        <p id="rowThree">
            <% = rowThree %>
        </p>
        <p id="rowFour">
            <% = rowFour %>
        </p>
        <p id="rowFive">
            <% = rowFive %>
        </p>
        <form id="inputForm" method="post">
            <input type="text" id="input" name="input" placeholder="Word: " />
            <input type="submit" id="submit" name="submit" />
        </form>
    </div>
</asp:Content>
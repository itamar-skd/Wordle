<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="Wordle.Stats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Stats.css" rel="stylesheet" />
    <div id="heading">
        <p style="font-size: 24px; text-align: center;">The word was: <span style="color: #60d394;"><% = word %>!</span></p>
        <p style="font-size: 20px; text-align: center;">View how well others did with this word!</p>
    </div>
    <div id="stats">
        <p class="row">
            <% = first %>
        </p>
        <p class="row">
            <% = second %>
        </p>
        <p class="row">
            <% = third %>
        </p>
        <p class="row">
            <% = fourth %>
        </p>
        <p class="row">
            <% = fifth %>
        </p>
        <p class="row">
            <% = failed %>
        </p>
    </div>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">

    
       <label for="userName">שם משתמש:</label><br>
       <input type="text" id="userName" name="userName"><br>
       <label for="password">סיסמה:</label><br>
       <input type="password" id="password" name="password">
       <input type="submit" value="Submit">
  

    <br><br>
	
    <input type="button" value="להרשמה" onclick="window.location.href='Registration.aspx';" />

    <div runat="server" id="LoginResult"></div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScript" Runat="Server">
</asp:Content>


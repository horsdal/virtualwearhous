<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Boolean>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>

<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% if (Model)
    {
    %>
User deleted succesfully
<%
    }
    else
    {
    %>
User deletion failed
<%
    }%>
</asp:Content>

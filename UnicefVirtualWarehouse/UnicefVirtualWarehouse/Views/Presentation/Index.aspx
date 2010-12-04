<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<UnicefVirtualWarehouse.Models.Presentation>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% if(Model.Any()) { %>
    <% foreach(var p in Model){%>
    <a href=/Presentation/Details/<%=p.Id %>><%=p.Name %></a><br />
    <%}%>
<%}else{%>
    <p>No presentations available</p>
<%}%>
</asp:Content>

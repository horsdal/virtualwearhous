<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.Product>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="UnicefVirtualWarehouse.Models" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">Unicef Virtual Warehouse Products</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h3>Products</h3>
<% if(Model.Any()) { %>	
	<table width="100%">
  <% if (Request.IsAuthenticated && (User.IsInRole(UnicefRole.Unicef.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString()))) {%>
    <th></th>
    <% } %>	
    <th>Name</th>
	<% int counter = 0; %>
    <% foreach(var p in Model){%>
	<tr onMouseOver="this.bgColor='#FCEB8B'" onMouseOut="this.bgColor='#FFFFFF'">
    <% if (Request.IsAuthenticated && (User.IsInRole(UnicefRole.Unicef.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString()))) {%>
     		<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>

                <%: Html.ActionLink("Delete", "Delete", new { p.Id })%>
            </td>
    <% } %>
	<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
	<% counter++; %>
		<a href=/Presentation/Product/<%=p.Id %>><%=p.Name %></a><br />
		</td>
	</tr>
    <%}%>
	</table>
<%}else{%>
    <p>No products available.</p>
<%}%>
</asp:Content>
<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<UnicefVirtualWarehouse.Models.ProductCategory>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="UnicefVirtualWarehouse.Models" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">ChildMed -  Product Categories</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h3>Product Categories</h3>
        <% if (Request.IsAuthenticated && (User.IsInRole(UnicefRole.Unicef.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString()))) {%>
       <p>
            <%: Html.ActionLink("Create New", "Create") %>
        </p>
    <% } %>
<% if(Model.Any()) { %>	
	<table width="100%">
	<th>Name</th>
    <% if (Request.IsAuthenticated && (User.IsInRole(UnicefRole.Unicef.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString()))) {%>
    <th></th>
    <% } %>
	<% int counter = 0; %>
	<% foreach(var p in Model){%>
	<tr onMouseOver="this.bgColor='#FCEB8B'" onMouseOut="this.bgColor='#FFFFFF'">
		<% counter++; %>
		<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
			<a href=/Product/ProductCategory/<%=p.Id %>><%=p.Name %></a><br />
		</td>
    <% if (Request.IsAuthenticated && (User.IsInRole(UnicefRole.Unicef.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString()))) {%>
		<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
                <%: Html.ActionLink("Delete", "Delete", new { p.Id })%>
            </td>
    <% } %>
	</tr>
	<%}%>

	</table>
<%}else{%>
	<p>No Product Categories available.</p>
<%}%>
</asp:Content>
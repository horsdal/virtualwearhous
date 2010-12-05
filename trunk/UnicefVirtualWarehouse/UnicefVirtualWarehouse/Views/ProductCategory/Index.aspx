<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.ProductCategory>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% if(Model.Any()) { %>
	<h3>Product Categories</h3>
	<table width="100%">
	<th>Name</th>
	<% int counter = 0; %>
	<% foreach(var p in Model){%>
	<tr>
		<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
		<% counter++; %>
			<a href=/Product/ProductCategory/<%=p.Id %>><%=p.Name %></a><br />
		</td>
	</tr>
	<%}%>

	</table>
<%}else{%>
	<p>No productcategories available</p>
<%}%>
</asp:Content>
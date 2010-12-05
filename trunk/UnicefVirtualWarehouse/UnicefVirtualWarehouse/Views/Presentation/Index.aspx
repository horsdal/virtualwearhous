<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.Presentation>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% if(Model.Any()) { %>
	<h3>Presentation</h3>
	<table width="100%">
		<th>Name</th>
		<% int counter = 0; %>
			<% foreach(var p in Model){%>
			<tr>
				<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
				<% counter++; %>
					<a href="../../ManufacturerPresentation/Details/<%=p.Id %>"><%=p.Name%></a>
				</td>
			</tr>
			<%}%>
	</table> 
<%}else{%>
    <p>No presentations available</p>
<%}%>
</asp:Content>
<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.Presentation>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">Unicef Virtual Warehouse Product Presentation</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h3>Product Presentation</h3>
<% if(Model.Any()) { %>	
	<table width="100%">
		<th>Name</th>
		<% int counter = 0; %>
			<% foreach(var p in Model){%>
			<tr onMouseOver="this.bgColor='#FCEB8B'" onMouseOut="this.bgColor='#FFFFFF'">
				<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
				<% counter++; %>
					<a href="../../ManufacturerPresentation/Details/<%=p.Id %>"><%=p.Name%></a>
				</td>
			</tr>
			<%}%>
	</table> 
<%}else{%>
    <p>No Presentations available.</p>
<%}%>
</asp:Content>
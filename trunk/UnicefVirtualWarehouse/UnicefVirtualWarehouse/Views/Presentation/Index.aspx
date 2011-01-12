<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Controllers.PresentationViewModel>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">Unicef Virtual Warehouse Product Presentation</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h3>Product Presentation</h3>
<% if(Model.Any()) { %>	
	<table width="100%">
		<th>Name</th><th>Max price (USD)</th><th>Average Price (USD)</th><th>Max price (USD)</th>
		<% int counter = 0; %>
			<% foreach(var p in Model){%>
			<tr onMouseOver="this.bgColor='#FCEB8B'" onMouseOut="this.bgColor='#FFFFFF'">
				<% counter++; %>
				<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
					<a href="../../ManufacturerPresentation/Details/<%=p.Presentation.Id %>"><%=p.Presentation.Name%></a>
				</td>
				<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
					<%= ((decimal) p.MaxPrice) / 100 %>
				</td>
				<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
					<%= ((decimal) p.AveragePrice) / 100 %>
				</td>
				<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
					<%= ((decimal) p.MinPrice) / 100 %>
				</td>
			</tr>
			<%}%>
	</table> 
<%}else{%>
    <p>No Presentations available.</p>
<%}%>
</asp:Content>
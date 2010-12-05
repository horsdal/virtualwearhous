<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.ManufacturerPresentation>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h3>Product Presentations from Manufacturers</h3>
<% int counter = 0; %>
<% if(Model.Any()) { %>
	<table width="100%">	
			<th>Manufacturer</th>
				<th>Presentation</th>
				<th>Size</th>
				<th>Price</th>
				<th>Minimal Unit</th>
				<th>Licensed</th>
				<th>CPP</th>

			<% foreach(var p in Model){%>
				<%=counter % 2 != 0 ? "<tr style=\"background-color:#e8eef4\">" : "<tr style=\"background-color:transparent\">"%>
				<% counter++; %>
				<td>
					<a  href="../../Contact/Details/<%=p.Manufacturer.Id %>"><%=p.Manufacturer.Name %></a>
				</td>
				<td>
					<%=p.Presentation.Name %>
				</td>
				<td>
					<%=p.Size %>
				</td>
				<td>
					<%=p.Price %>
				</td>
				<td>
					<%=p.MinUnit %>
				</td>
				<td>
					<%=p.Licensed %>
				</td>
				<td>
					<%=p.CPP %>
				</td>
			</tr>
			<%}%>
	</table> 
<%}else{%>
    <p>No Presentations from Manufacturers available.</p>
<%}%>
</asp:Content>

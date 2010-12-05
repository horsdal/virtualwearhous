<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.ManufacturerPresentation>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% if(Model.Any()) { %>
	<table>
		<th> Manufacturer's Presentation </th>
			<% foreach(var p in Model){%>
			<tr>
				<td>
					<a  href="../ManfucaturerPresentation/"<%=p.Manufacturer.Id %>><%=p.Manufacturer.Name %></a>
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
    <p>No Manufacturer's presentations available</p>
<%}%>
</asp:Content>

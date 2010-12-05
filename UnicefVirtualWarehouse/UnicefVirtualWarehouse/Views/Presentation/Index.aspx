<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.Presentation>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% if(Model.Any()) { %>
	<table>
		<th> Presentation </th>
			<% foreach(var p in Model){%>
			<tr>
				<td>
					<a  href="../../ManufacturerPresentation/Details/<%=p.Id %>"><%=p.Name%></a>
				</td>
			</tr>
			<%}%>
	</table> 
<%}else{%>
    <p>No presentations available</p>
<%}%>
</asp:Content>
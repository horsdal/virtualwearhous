<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.ManufacturerPresentation>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="UnicefVirtualWarehouse.Models" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">ChildMed -  Product Presentations</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h3>Product Presentations from Manufacturers</h3>
<% int counter = 0; %>
<% if(Model.Any()) { %>
	<table width="100%">	
    			<th>Manufacturer</th>
				<th>Presentation</th>
				<th>Size</th>
				<% if (Request.IsAuthenticated) {%> <th>Price in USD</th> <% } %>
				<th>Minimal Unit</th>
				<th>Licensed</th>
				<th>CPP</th>
                <% if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Administrator.ToString())) {%>
                    <th></th>
                <% } %>	

			<% foreach(var p in Model){%>
				<% counter++; %>
				<%=counter % 2 != 0 ? "<tr onMouseOver=\"this.bgColor='#FCEB8B'\" onMouseOut=\"this.bgColor='#FFFFFF'\" style=\"background-color:#e8eef4\">" : "<tr onMouseOver=\"this.bgColor='#FCEB8B'\" onMouseOut=\"this.bgColor='#FFFFFF'\" style=\"background-color:transparent\">"%>
				<td>
					<a  href="../../Contact/Details/<%=p.Manufacturer.Id %>"><%=p.Manufacturer.Name %></a>
				</td>
				<td>
					<%=p.Presentation.Name %>
				</td>
				<td>
					<%=p.Size %>
				</td>
                <% if (Request.IsAuthenticated) {%>
				<td>
					<%=((decimal) p.Price)/100%>
				</td>
                <% }%>
				<td>
					<%=p.MinUnit %>
				</td>
				<td>
                <img src="../../Content/<%=p.Licensed ? "check_16.png": "delete_16.png" %>" />
				</td>
				<td>
                <img src="../../Content/<%=p.CPP ? "check_16.png": "delete_16.png" %>" />
				</td>
                <% if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Administrator.ToString())) {%>
     		        <%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
                        <%: Html.ActionLink("Delete", "Delete", new { p.ID })%>
                    </td>
                <% } %>
			</tr>
			<%}%>
	</table> 
    <% if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Administrator.ToString())) {%>
        <p>
           <%: Html.ActionLink("Create New", "Create") %>
        </p>
    <% } %>
<%}else{%>
    <p>No Presentations from Manufacturers available.</p>
<%}%>
</asp:Content>

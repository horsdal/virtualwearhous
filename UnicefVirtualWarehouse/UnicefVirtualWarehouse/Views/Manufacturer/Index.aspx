<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<UnicefVirtualWarehouse.Models.Manufacturer>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="UnicefVirtualWarehouse.Models" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h3>Manufaturer</h3>
    <% if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Administrator.ToString())) {%>
        <p>
            <%: Html.ActionLink("Create New", "Create") %>
        </p>
    <% } %>
<% if(Model.Any()) { %>	
	<table width="100%">
        <% if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Administrator.ToString())) {%>
             <th></th>
        <% } %>	
		<th>Name</th>
		<% int counter = 0; %>
			<% foreach(var m in Model){%>
			<tr onMouseOver="this.bgColor='#FCEB8B'" onMouseOut="this.bgColor='#FFFFFF'">
				<% counter++; %>               
				<%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
					<%: Html.ActionLink(m.Name, "Details", new { m.Id} ) %>
				</td>
                 <% if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Administrator.ToString())) {%>
     		        <%=counter % 2 != 0 ? "<td style=\"background-color:#e8eef4\">" : "<td style=\"background-color:transparent\">"%>
                        <%: Html.ActionLink("Delete", "Delete", new { m.Id })%>
                    </td>
                <% } %>
			</tr>
			<%}%>
	</table> 
<%}else{%>
    <p>No Presentations available.</p>
<%}%></asp:Content>

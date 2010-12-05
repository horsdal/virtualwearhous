<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.Contact>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% if(Model.Any()) { %>
	<%{%>
		<%var p = Model.FirstOrDefault();%>
		<h3>Contact Details</h3>
		<table>
			<tr><td>Address:</td><td> <%=p.Address%></td></tr>
			<tr><td>Zip:</td><td> <%=p.Zip%></td></tr>
			<tr><td>City:</td><td> <%=p.City%></td></tr>
			<tr><td>Phone:</td><td> <%=p.Phone%></td></tr>
			<tr><td>Fax:</td><td> <%=p.Fax%></td></tr>
			<tr><td>Email:</td><td> <%=p.Email%></td></tr>
			<tr><td>Website:</td><td> <%=p.Website%></td></tr>
		</table>
	<%}%>
<%}else{%>
    <p>No contacts available</p>
<%}%>
</asp:Content>

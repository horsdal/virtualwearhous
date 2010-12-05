<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.Contact>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% if(Model.Any()) { %>
	<%{%>
	<ul>
		<%var p = Model.FirstOrDefault();%>
		<li>Address: <%=p.Address%></li>
		<li>City: <%=p.City%></li>
		<li>Email: <%=p.Email%></li>
		<li>Fax: <%=p.Fax%></li>
		<li>Phone: <%=p.Phone%></li>
		<li>Website: <%=p.Website%></li>
		<li>Zip: <%=p.Zip%></li>
	<%}%>
	<ul>
<%}else{%>
    <p>No contacts available</p>
<%}%>
</asp:Content>

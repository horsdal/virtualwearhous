<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<UnicefVirtualWarehouse.Models.Contact>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>


<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">Unicef Virtual Warehouse: Contacts</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">

<head>
<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=ABQIAAAA7BuiUF-VpDr0X8SrDakqMRT2yXp_ZAY8_ufC3CFXhHIE1NvwkxSSm8_3EBdOlKDP3H7FWnv47b_o2A" type="text/javascript"></script>


<script type="text/javascript">

	function initialize() {
		var map = new GMap2(document.getElementById("map_canvas"));
		map.setCenter(new GLatLng(37.4419, -122.1419), 13);
		map.setUIToDefault();
	}
 
    </script>

</head>
<body onload="initialize()">
<% if(Model.Any()) { %>
	<%{%>
		<%var p = Model.FirstOrDefault();%>
		<h3>Contact Details</h3>
		<table style="border-style:none">
			<tr><td style="border-color:transparent">Address:</td><td style="border-color:transparent; font-weight:bold"> <%=p.Address%></td></tr>
			<tr><td style="border-color:transparent">Zip:</td><td style="border-color:transparent; font-weight:bold"> <%=p.Zip%></td></tr>
			<tr><td style="border-color:transparent">City:</td><td style="border-color:transparent; font-weight:bold"> <%=p.City%></td></tr>
			<tr><td style="border-color:transparent">Phone:</td><td style="border-color:transparent; font-weight:bold"> <%=p.Phone%></td></tr>
			<tr><td style="border-color:transparent">Fax:</td><td style="border-color:transparent; font-weight:bold"> <%=p.Fax%></td></tr>
			<tr><td style="border-color:transparent">Email:</td><td style="border-color:transparent; font-weight:bold"><%=p.Email%></td></tr>
			<tr><td style="border-color:transparent">Web Site:</td><td style="border-color:transparent; font-weight:bold"> <%=p.Website%></td></tr>
		</table>
	<%}%>
<%}else{%>
    <p>No Contacts available.</p>
<%}%>



<div id="map_canvas" style="width: 500px; height: 300px"></div>	

</asp:Content>



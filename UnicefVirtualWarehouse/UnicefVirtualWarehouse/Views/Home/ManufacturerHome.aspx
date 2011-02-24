<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UnicefVirtualWarehouse.Models.Manufacturer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.Name %> Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.Name %> Home</h2>
    <%: Html.ActionLink("Manage product presentations", "Manage", "Presentation") %> <br />
    <%: Html.ActionLink("Manage manufaturer presentations", "Manage", "ManufacturerPresentation") %> <br />
    <%: Html.ActionLink("Edit contact information", "Edit", "Contact", new { id = Model.Contact.Id }, null )%>
</asp:Content>

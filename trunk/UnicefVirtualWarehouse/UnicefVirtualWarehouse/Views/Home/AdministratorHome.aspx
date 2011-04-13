<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	UnicefHome
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Unicef Home</h2>
   <%: Html.ActionLink("Manage product categories", "Index", "Productcategory") %> <br />
   <%: Html.ActionLink("Manage products", "Index", "Product") %> <br />
   <%: Html.ActionLink("Product presentations", "Index", "Presentation") %> <br />
   <%: Html.ActionLink("Manufacturer presentation", "Index", "ManufacturerPresentation") %> <br />
   <%: Html.ActionLink("Manage manufacturers", "Index", "Manufacturer") %> <br />
   <%: Html.ActionLink("Manage users", "Manage", "Account") %> <br />
   <a href="/App_Data/log.txt">View latest application error log</a>
</asp:Content>
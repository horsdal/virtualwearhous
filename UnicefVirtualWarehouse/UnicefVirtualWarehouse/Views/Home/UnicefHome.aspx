<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	UnicefHome
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Unicef Home</h2>
   <%: Html.ActionLink("Manage product categories", "Index", "Productcategory") %> <br />
    <%: Html.ActionLink("Manage products", "Index", "Product") %> <br />
</asp:Content>

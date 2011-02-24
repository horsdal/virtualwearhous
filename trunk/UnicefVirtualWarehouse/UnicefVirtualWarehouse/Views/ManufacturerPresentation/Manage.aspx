<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<UnicefVirtualWarehouse.Models.ManufacturerPresentation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Manage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Manage</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Licensed
            </th>
            <th>
                CPP
            </th>
            <th>
                MinUnit
            </th>
            <th>
                Size
            </th>
            <th>
                Price
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Delete", "Delete", new { item.ID })%>
            </td>
            <td>
                <%: item.Licensed %>
            </td>
            <td>
                <%: item.CPP %>
            </td>
            <td>
                <%: item.MinUnit %>
            </td>
            <td>
                <%: item.Size %>
            </td>
            <td>
                <%: item.Price %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>


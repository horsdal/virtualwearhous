<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<UnicefVirtualWarehouse.Models.ManufacturerPresentation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Manage manufacturer presentations
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Manage</h2>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

    <table>
        <tr>
            <th>
                Prensentation
            </th>
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
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.Presentation.Name %>
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
				<td>
                <img src="../../Content/<%=item.Licensed ? "check_16.png": "delete_16.png" %>" />
				</td>
				<td>
                <img src="../../Content/<%=item.CPP ? "check_16.png": "delete_16.png" %>" />
				</td>
            <td>
                <%: Html.ActionLink("Delete", "Delete", new { item.ID })%>
            </td>
        </tr>
    
    <% } %>

    </table>


</asp:Content>


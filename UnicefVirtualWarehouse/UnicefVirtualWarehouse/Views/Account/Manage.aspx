<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<UnicefVirtualWarehouse.Models.User>>" %>
<%@ Import Namespace="UnicefVirtualWarehouse.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Manage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Manage</h2>

    <table>
        <tr>
            <th>
                Id
            </th>
            <th>
                UserName
            </th>
            <th>
                Role
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.Id %>
            </td>
            <td>
                <%: item.UserName %>
            </td>
            <td>
                <%: ((UnicefRole) item.Role).ToString()  %>
            </td>
            <td>
                <%: Html.ActionLink("Delete", "Delete", new { item.Id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Register New", "Register") %>
    </p>

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UnicefVirtualWarehouse.Models.ManufacturerPresentation>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>
    <% if (Model != null)
       {%>
    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">ID</div>
        <div class="display-field"><%: Model.ID%></div>
        
        <div class="display-label">Licensed</div>
        <div class="display-field"><%: Model.Licensed%></div>
        
        <div class="display-label">CPP</div>
        <div class="display-field"><%: Model.CPP%></div>
        
        <div class="display-label">MinUnit</div>
        <div class="display-field"><%: Model.MinUnit%></div>
        
        <div class="display-label">Size</div>
        <div class="display-field"><%: Model.Size%></div>
        
        <div class="display-label">Price</div>
        <div class="display-field"><%: Model.Price%></div>
        
    </fieldset>
    <% using (Html.BeginForm())
       { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Index")%>
        </p>
    <% } %>
    <% } else { %>
    You do not have permisions to delete this manufacturer presentation
    <% } %>

</asp:Content>


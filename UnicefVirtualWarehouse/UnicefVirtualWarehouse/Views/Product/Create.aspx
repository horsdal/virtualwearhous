﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<KeyValuePair<UnicefVirtualWarehouse.Models.Product, IEnumerable<UnicefVirtualWarehouse.Models.ProductCategory>>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ChildMed - : Create Product
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Create</h3>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Key.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Key.Name) %>
                <%: Html.ValidationMessageFor(model => model.Key.Name) %>
            </div>
            <div class="display-label">
                Category
            </div>
            <div class="editor-field">
                <%:
    Html.DropDownListFor(model => model.Value,
                    Model.Value.Select(pc => new SelectListItem {Text = pc.Name, Value = pc.Id.ToString()}).ToList()) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <%
} %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>


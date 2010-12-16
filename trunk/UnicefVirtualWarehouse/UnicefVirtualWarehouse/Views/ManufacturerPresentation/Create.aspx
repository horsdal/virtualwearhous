<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<KeyValuePair<UnicefVirtualWarehouse.Models.ManufacturerPresentation, IEnumerable<UnicefVirtualWarehouse.Models.Presentation>>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Key.Licensed) %>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(model => model.Key.Licensed) %>
                <%: Html.ValidationMessageFor(model => model.Key.Licensed) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Key.CPP) %>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(model => model.Key.CPP) %>
                <%: Html.ValidationMessageFor(model => model.Key.CPP) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Key.MinUnit) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Key.MinUnit) %>
                <%: Html.ValidationMessageFor(model => model.Key.MinUnit) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Key.Size) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Key.Size) %>
                <%: Html.ValidationMessageFor(model => model.Key.Size) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Key.Price) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Key.Price) %>
                <%: Html.ValidationMessageFor(model => model.Key.Price) %>
            </div>
            
        <div class="display-label">
            Presentation
        </div>
        <div class="editor-field">
            <%: Html.DropDownListFor(model => model.Value,
                    Model.Value.Select(p => new SelectListItem {Text = p.Name, Value = p.Id.ToString()}).ToList()) %>
        </div>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>


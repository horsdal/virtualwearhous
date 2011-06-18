<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UnicefVirtualWarehouse.Controllers.CreateManufacturerPresentationViewModel>" %>
<%@ Import Namespace="UnicefVirtualWarehouse.Models" %>

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
                <%: Html.LabelFor(model => model.ManufacturerPresentation.Licensed) %>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(model => model.ManufacturerPresentation.Licensed) %>
                <%: Html.ValidationMessageFor(model => model.ManufacturerPresentation.Licensed) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ManufacturerPresentation.CPP) %>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(model => model.ManufacturerPresentation.CPP) %>
                <%: Html.ValidationMessageFor(model => model.ManufacturerPresentation.CPP) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ManufacturerPresentation.MinUnit) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ManufacturerPresentation.MinUnit) %>
                <%: Html.ValidationMessageFor(model => model.ManufacturerPresentation.MinUnit) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ManufacturerPresentation.Size) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ManufacturerPresentation.Size) %>
                <%: Html.ValidationMessageFor(model => model.ManufacturerPresentation.Size) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ManufacturerPresentation.Price) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ManufacturerPresentation.Price) %>
                <%: Html.ValidationMessageFor(model => model.ManufacturerPresentation.Price) %>
            </div>
     
        <div class="display-label">
            Presentation
        </div>
        <div class="editor-field">
            <%: Html.DropDownListFor(model => model.Presentations,
                    Model.Presentations.Select(p => new SelectListItem {Text = p.Name, Value = p.Id.ToString()}).ToList()) %>
        </div>
        <% if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Administrator.ToString())) {%>
            <div class="display-label">
                Manufacturer
            </div>
            <div class="editor-field"> 
                <%: Html.DropDownListFor(model => model.Manufacturers,
                    Model.Manufacturers.Select(p => new SelectListItem {Text = p.Name, Value = p.Id.ToString()}).ToList()) %>
            </div>
        <% } %>  
                     <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>


﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UnicefVirtualWarehouse.Models.Presentation>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>
    <% if (Model == null)
       {%>
    Delete failed. Cannot delete presentation if there are manufacturer presentations under the presentation.<br />
     <%:Html.ActionLink("Back to List", "Index")%>
     <%
        }
       else
       {%>
    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">Id</div>
        <div class="display-field"><%:Model.Id%></div>
        
        <div class="display-label">Name</div>
        <div class="display-field"><%:Model.Name%></div>
        
    </fieldset>
    <%
           using (Html.BeginForm())
           {%>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%:Html.ActionLink("Back to List", "Index")%>
        </p>
    <%
           }%>
    <%
       }%>

</asp:Content>


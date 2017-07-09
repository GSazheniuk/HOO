<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="MainJQuery.aspx.cs" Inherits="HOO.WebClient.MainJQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div id="mainContainer"></div>
    <div class="popup" data-popup="popupWithClose">
        <div class="popup-inner">
            <span id="popupContainer1"></span>
            <a class="popup-close" data-popup-close="popupWithClose" href="#">x</a>
        </div>
    </div>
    <div class="popup" data-popup="popupNoClose">
        <div class="popup-inner">
            <span id="popupContainer2"></span>
            <a class="popup-close" data-popup-close="popupWithClose" href="#">x</a>
        </div>
    </div>
</asp:Content>

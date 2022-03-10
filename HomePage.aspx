<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="MKForum.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="js/bootstrap.js"></script>
    <style>
        div {
            border: 1px solid #000;
        }

        .col-md-0 {
            flex: 0 0 auto;
            width: 0%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="col-lg-6 col-md-12">
            <div class="hot-cboards">

            </div>

            <div class="hot-posts">
                <div class="title">
                    <img src="./css/HOT.png">
                    <h1>熱門討論區</h1>
                </div>

            </div>
        </div>
</asp:Content>

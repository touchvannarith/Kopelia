<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAdministrator.Master" AutoEventWireup="true" CodeBehind="Administrator_login.aspx.cs" Inherits="NotaliaOnline.Administrator_login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        @media screen and (min-width:900px){
            footer {
                clear: both;
                position: absolute;
                bottom: 0;
                width: 100%;
            }
        }
    </style>
    <div class="m-3">
        <div class="row justify-content-center">
            <div class="col-12 col-lg-6">
                <div class="card">
                    <div class="card-header">
                        Connexion administrateur
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right font-weight-bold">Login : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtLogin"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right font-weight-bold">Mot de passe : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox TextMode="Password" CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtPassword"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:LinkButton type="button" class="btn btn-primary" runat="server" ClientIDMode="Static" ID="btnLogin" OnClick="btnLogin_Click">
                        <i class="fa fa-chevron-circle-right" aria-hidden="true"></i>
                        Je me connecte
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

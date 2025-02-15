<%@ Page Title="Gestion des utilisateurs" Language="C#" MasterPageFile="~/SiteAdministrator.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="NotaliaOnline.UserManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="m-3">
        <div class="card">
            <div class="card-header">
                Gestion des utilisateurs
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Adresse email :</label>
                    <div class="col-9 col-sm-4">
                        <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtEmail"></asp:TextBox>
                    </div>
                    <div class="col-3 col-sm-1 text-center">
                        <asp:LinkButton runat="server" ClientIDMode="Static" ID="btnSearch" CssClass="btn btn-primary" OnClick="btnSearch_Click">
                            <i class="fa fa-search"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Comptes utilisateur
                    </div>
                    <div class="card-body">
                        <asp:Repeater runat="server" ID="repeatClient" OnItemDataBound="repeatClient_ItemDataBound">
                            <ItemTemplate>
                                <div class="row">
                                    <label class="col-md-2 col-sm-6 col-12">
                                        <a class="btn btn-success btnCollapse" data-toggle="collapse" href="#collapseGroup<%#Eval("Id")%>" role="button" aria-expanded="false" aria-controls="collapseExample" style="padding: 0 6px; font-size: 14px">+</a> <%# Eval("EmailAddress") %>
                                    </label>
                                    <%--<label class="col-md-2 col-sm-6 col-12"><%# Eval("EmailAddress") %></label>--%>
                                    <label class="col-md-2 col-sm-2 col-12"><%# Eval("PhoneNumber") %></label>
                                    <label class="col-md-1 col-sm-4 col-12"><%# Eval("LimitDevice") %> <span>license(s)</span></label>
                                    <label class="col-md-1 col-sm-4 col-12"><%# Eval("Package") %></label>
                                    <label class="col-md-1 col-sm-4 col-12">
                                        <a target="_blank" href="images/logo/<%# Eval("ImageLogo") %>">
                                            <img class="logo-thumbnail" src="images/logo/<%# Eval("ImageLogo") %>" alt="None" style="height: 20px">
                                        </a>
                                    </label>
                                    <label class="col-md-2 col-sm-4 col-12"><%# Eval("CreatedDate", "{0:dd/MM/yyyy HH:mm:ss}") %></label>
                                    <div class="col-md-3 col-sm-12 col-12 text-right">
                                        <button type="button" runat="server" class="btn btn-success btn-sm" style="font-size: 18px" onclick='<%# "sentToForm(" + Eval("Id") + ");" %>'>
                                            <asp:HiddenField runat="server" ID="hdClientId" Value='<%#Eval("Id") %>' />
                                            Télécharger le logo
                                        </button>
                                        <button type="button" runat="server" class="btn btn-success btn-sm" style="font-size: 18px" onclick='<%# "modalWarningMessageRemoval(" + Eval("Id") + ");" %>'>
                                            Supprimer
                                        </button>
                                    </div>
                                </div>
                                <div class="collapse" id="collapseGroup<%#Eval("Id")%>">
                                    <asp:Repeater runat="server" ID="repeatUser">
                                        <ItemTemplate>
                                            <div class="row form-group">
                                                <label class="col-md-2 col-sm-6 col-12"><%# Eval("EmailAddress") %></label>
                                                <label class="col-md-2 col-sm-2 col-12"><%# Eval("PhoneNumber") %></label>
                                                <label class="col-md-1 col-sm-4 col-12"></label>
                                                <label class="col-md-1 col-sm-4 col-12"></label>
                                                <label class="col-md-1 col-sm-4 col-12"></label>
                                                <label class="col-md-2 col-sm-4 col-12"><%# Eval("CreatedDate", "{0:dd/MM/yyyy HH:mm:ss}") %></label>
                                                <div class="col-md-3 col-sm-12 col-12 text-right">
                                                    <asp:LinkButton runat="server" class="btn btn-success" style="font-size: 18px" id="btnRemoveSubClient" OnClick="btnRemoveSubClient_Click">
                                                        <asp:HiddenField runat="server" ID="hdRemoveId" Value='<%#Eval("Id") %>' />
                                                        Supprimer
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="text-center mb-3 mt-2">
                            <asp:Repeater ID="rptPager1" runat="server" OnItemCommand="rptPager1_ItemCommand">
                                <%--<ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server"
                                        Text='<%#Eval("Text") %>'
                                        CommandArgument='<%#Eval("Value") %>'
                                        Enabled='<%#Eval("Enabled") %>'
                                        OnClick="lnkPage_Click"
                                        ForeColor="#267CB2"
                                        Font-Bold="true" />
                                </ItemTemplate>--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage1"
                                        Style="padding: 4px 8px; background: #304f73; color: #fff; border: solid 1px #304f73; border-radius: 6px"
                                        CommandName="Page" CommandArgument="<%# Container.DataItem %>" runat="server" Font-Bold="True"><%# Container.DataItem %>  
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Calculez maintenant
            </div>
            <div class="card-body">
                <asp:Repeater runat="server" ID="repeatCalculezMaintenant">
                    <ItemTemplate>
                        <div class="row">
                            <label class="col-sm-6 col-12"><%# Eval("EmailAddress") %></label>
                            <label class="col-sm-4 col-12"><%# Eval("DateCreated", "{0:dd/MM/yyyy HH:mm:ss}") %></label>
                            <label class="col-sm-2 col-12"><%# Eval("SimulationUsed") %></label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="text-center mb-3 mt-2">
                    <asp:Repeater ID="rptPager2" runat="server" OnItemCommand="rptPager2_ItemCommand">
                        <%--<ItemTemplate>
                            <asp:LinkButton ID="lnkPage2" runat="server"
                                Text='<%#Eval("Text") %>'
                                CommandArgument='<%#Eval("Value") %>'
                                Enabled='<%#Eval("Enabled") %>'
                                OnClick="lnkPage2_Click"
                                ForeColor="#267CB2"
                                Font-Bold="true" />
                        </ItemTemplate>--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage2"
                                Style="padding: 4px 8px; background: #304f73; color: #fff; border: solid 1px #304f73; border-radius: 6px"
                                CommandName="Page" CommandArgument="<%# Container.DataItem %>" runat="server" Font-Bold="True"><%# Container.DataItem %>  
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="modal fade" id="myUploadImage" style="text-align: center" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Télécharger le logo</h4>
                    </div>
                    <div class="modal-body">
                        Nom de fichier :
                                    <asp:FileUpload runat="server" CssClass="file-upload form-control" ID="fileUpload" Style="height: auto" />
                        <img id="img-upload" />
                        <asp:HiddenField runat="server" ID="hdClientId" Value='<%#Eval("Id") %>' />
                        <%--<div class="text-left" style="color: red">Note: Width of image must be less then or equal to 535px.</div>--%>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnUploadLogo" OnClick="btnUploadLogo_Click" Text="Télécharger" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modalWarningMessageRemoval" style="text-align: center" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Warning!</h4>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField runat="server" ID="hdOwnerId" Value='<%#Eval("Id") %>' />
                        <p class="mb-0">All the licences for this owner will be removed.</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnYes" OnClick="btnYes_Click" Text="Oui" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Non</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function sentToForm(clientId) {
            document.getElementById('<%= hdClientId.ClientID %>').value = clientId;
            $("#myUploadImage").modal();
        }
        function modalWarningMessageRemoval(clientId) {
            document.getElementById('<%= hdOwnerId.ClientID %>').value = clientId;
            $("#modalWarningMessageRemoval").modal();
        }
        $('.btnCollapse').on('click', function () {
            var self = $(this);
            if (self.text() === "+") {
                self.text("-");
            } else {
                self.text("+");
            }
        });
    </script>
</asp:Content>

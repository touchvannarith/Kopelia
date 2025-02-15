<%@ Page Title="Tarifs du logiciel - kopelia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarifs.aspx.cs" Inherits="NotaliaOnline.Tarifs"
    MetaDescription="Consultez le prix et l'ensemble de la grille tarifaire de notre logiciel pour les notaires Kopelia. Le logiciel notarial en SaaS." %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .mobile .offer {
            font-size: 22px;
            font-family: 'DIN Condensed-Bold';
            text-align: center;
            border-radius: 1em;
        }

            .mobile .offer button {
                width: 100%;
                height: auto;
                padding: 6px 0;
                border-radius: 20px;
                font-size: 28px;
                border: 6px solid #FFFFFF;
                background: #304f73;
            }

            .mobile .offer .offer-title {
                font-size: 28px;
                font-family: 'DIN Condensed-Bold';
                padding: 10px 0;
                background-color: #304f73;
                color: #FFFFFF;
                border-top-left-radius: 15px;
                border-top-right-radius: 15px;
            }

        .offer.Standard .offer-title {
            font-size: 28px;
            font-family: 'DIN Condensed-Bold';
            padding: 10px 0;
            background-color: #FF7D26;
            color: #FFFFFF;
            border-top-left-radius: 15px;
            border-top-right-radius: 15px;
        }

        .offer .offer-price {
            background-color: #01ABE4;
            color: #FFFFFF;
            padding: 10px 0;
        }

            .offer .offer-price span:first-child {
                font-size: 2em;
                font-family: 'DIN';
            }

        .mobile .offer .offer-content .price {
            font-family: ''Trebuchet MS',Arial,Helvetica,sans-serif';
            font-size: 2em;
            font-weight: bold;
        }

        .offer .offer-content {
            background-color: #01ABE4;
            color: #FFFFFF;
            margin-top: 5px;
        }

        .offer.Standard .offer-content {
            background-color: #FAB37B;
        }

        .mobile .offer .offer-content span {
            font-family: 'DIN Condensed-Bold' !important;
            font-size: 24px !important;
            /*padding: 0 2px;*/
        }

        .offer .offer-footer button, .offer .offer-footer a {
            padding: 10px 0;
            border-bottom-left-radius: 15px;
            border-bottom-right-radius: 15px;
            width: 100%;
            height: auto;
        }

        .offer.Standard .offer-footer button, .offer .offer-footer a {
            background-color: #FF7D26;
        }

        .mobile .offer button:active {
            box-shadow: none !important;
            border: 6px solid #304f73;
            background-color: #FFFFFF;
            color: #304f73;
        }

        .mobile .offer button:hover {
            box-shadow: none !important;
        }

        .feature-extra, .feature-extra span {
            font-size: 18px !important;
            line-height: 1.2em;
        }


        table.table-pricing {
            font-family: 'DIN';
            font-size: 22px;
            font-weight: bold;
            line-height: 1.4em;
            font-style: normal;
            border-collapse: separate;
            width: 100%;
            border-spacing: 0px;
        }

        .table-pricing thead th {
            color: white;
            border-left: 6px solid white;
            border-right: 6px solid white;
            border-top: 6px solid white;
            background-color: #304f73;
            text-align: center;
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius: 5px 5px 0px 0px;
            border-top-left-radius: 1em;
            border-top-right-radius: 1em;
            width: 15%;
        }

            .table-pricing thead th:first-child {
                width: 40%;
                background-color: #FFFFFF;
                vertical-align: top;
                cursor: default !important;
            }

            .table-pricing thead th > label {
                font-family: 'DIN Condensed-Bold' !important;
                font-size: 28px;
                padding: 10px 0;
            }

            .table-pricing thead th > div, .table-pricing thead th button > div {
                background-color: #01ABE4;
                padding: 10px 5px;
                margin-top: 10px;
                font-family: 'Trebuchet MS',Arial,Helvetica,sans-serif;
                line-height: 0.8;
            }

                .table-pricing thead th > div > div:first-child, .table-pricing thead th button > div > div:first-child {
                    font-size: 2.5rem;
                    margin: 10px 0;
                    font-family: ''Trebuchet MS',Arial,Helvetica,sans-serif';
                }

                .table-pricing thead th > div > span:last-child {
                    font-weight: normal;
                    font-size: 18px;
                }

            .table-pricing thead th.standard {
                background-color: #FF7D26;
            }

                .table-pricing thead th.standard > div {
                    background-color: #FAB37B;
                }

            .table-pricing thead th:first-child button, #btnAbonnement_Mensuel, #btnAbonnement_Annuel {
                font-size: 28px;
                background-color: #304f73;
                border-radius: 1em;
                width: 100%;
                height: auto;
                padding: 12px;
                box-shadow: none !important;
                border: 2px solid #304f73;
                margin-bottom: 15px !important;
            }

                .table-pricing thead th:first-child button:last-child, #btnAbonnement_Annuel {
                    background-color: #FF7D26;
                    border: 2px solid #FF7D26;
                    margin-bottom: 15px !important
                }

                .table-pricing thead th:first-child button:hover {
                    background-color: #FFFFFF !important;
                    color: #304f73;
                    border: 2px solid #304f73 !important;
                }

                .table-pricing thead th:first-child button:last-child:hover {
                    background-color: #FFFFFF;
                    color: #FF7D26;
                    border: 2px solid #FF7D26 !important;
                }

        .table-pricing tbody th {
            font-family: 'DIN Cond-Light' !important;
            font-size: 28px;
            padding: 12px;
            font-weight: normal;
            color: #1D9BD7;
        }

        .table-pricing thead th {
            font-family: 'DIN Condensed-Bold' !important;
            font-size: 28px;
            text-align: center;
            color: white;
            border-left: 6px solid white;
            border-right: 6px solid white;
            background-color: #304f73;
            cursor: pointer;
            border-top-left-radius: 1em;
            border-top-right-radius: 1em;
            padding: 0;
        }

        .table-pricing tfoot td {
            font-family: 'DIN Condensed-Bold' !important;
            font-size: 28px;
            text-align: center;
            color: white;
            border-left: 6px solid white;
            border-right: 6px solid white;
            border-bottom: 6px solid white;
            background-color: #304f73;
            text-align: center;
            border-bottom-left-radius: 1em;
            border-bottom-right-radius: 1em;
            cursor: pointer;
        }

        .table-pricing thead th button {
            width: 100%;
            height: auto;
            padding: 10px 0 0 0;
            border-top-left-radius: 1em;
            border-top-right-radius: 1em;
            border: none;
            background: #304f73;
        }

        .table-pricing tfoot td button {
            width: 100%;
            height: auto;
            padding: 10px 0;
            border-bottom-left-radius: 0.6em;
            border-bottom-right-radius: 0.6em;
            border: none;
            background-color: #304f73;
        }

            .table-pricing tfoot td button:hover, .table-pricing thead th button:hover {
                box-shadow: none !important;
            }

        .table-pricing tfoot td.standard {
            background-color: #FF7D26
        }

        .table-pricing tfoot th {
            color: #666;
        }

        .table-pricing tbody td {
            color: #304f73;
            font-size: 34px;
            text-align: center;
        }

            .table-pricing tbody td button {
                width: 100%;
                height: 100%;
                padding: 12px 0;
                border-style: none;
                color: #304f73;
            }

                .table-pricing tbody td button:hover {
                    box-shadow: none !important;
                }

            .table-pricing tbody td.standard {
                color: #FF7D26;
            }

            .table-pricing tbody td:first-child {
                border-left: 6px solid #FFFFFF;
            }

            .table-pricing tbody td:last-child {
                border-right: 6px solid #FFFFFF;
                border-left: 6px solid transparent;
            }

        .table-pricing tbody tr:nth-child(odd) td.standard {
            background-color: #fddec3;
            border-left: 6px solid #C6EAFB;
            border-right: 6px solid #C6EAFB;
        }

        .table-pricing tbody tr:nth-child(odd), .table-pricing tbody tr:nth-child(odd) button {
            background-color: #FFFFFF;
        }

        .table-pricing tbody tr:nth-child(even), .table-pricing tbody tr:nth-child(even) button {
            background-color: #C6EAFB;
        }

        button:visited, button:focus {
            outline: none !important;
        }

        .btn:focus {
            color: #ffffff;
        }

        .mobile .table-pricing tbody tr {
            background-color: #C6EAFB;
        }

        .mobile .table-pricing tbody td {
            border-left: 6px solid white;
            border-right: 6px solid white;
        }

        .mobile .table-pricing thead th button {
            border: 3px solid white !important;
            background-color: #01ABE4;
            color: white;
        }

        .table-pricing .fa-check-circle::before {
            /*display: none;*/
            content: "\f058";
            color: darkgreen;
        }
        .table-pricing .fa-check-circle.Offre_dessai::before {
            /*display: none;*/
            content: "\f057";
            color: darkred;
        }

        .mobile ul {
            margin-left: 0 !important;
            margin-right: 30px;
        }
        .mobile ul li {
            padding: 0 !important;
            /*border-bottom: 1px solid #304f73;*/
        }
        .mobile .offer .offer-content ul li span {
            font-family: DIN Cond-Light !important;
        }
        .mobile li::after {
            color: darkgreen;
            content: "\f058";
            font-family: FontAwesome;
            display: inline-block;
            margin-left: -1.3em;
            width: 1.3em;
            float: right;
            margin-right: -38px;
        }
        .mobile ul li.Offre_dessai::after{
            content: "\f057";
            color: darkred;
        }
    </style>
    <div class="container-md mt-2 mb-2">
        <div runat="server" id="divIframe">
            <iframe id="content_iframe" src="" style="width: 100%; height: 810px;" frameborder="0"></iframe>
        </div>
        <div runat="server" id="divOffer">
            <table class="table-pricing d-none d-sm-block">
                <thead>
                    <tr>
                        <th scope="col"></th>
                        <asp:Repeater runat="server" ClientIDMode="Static" ID="repeatOfferHeader" OnItemDataBound="repeatOffer_OnItemDataBound">
                            <ItemTemplate>
                                <th scope="col" class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>" id="offer-<%# Eval("Id") %>">
                                    <button runat="server" id="btnTableHead" type="button" class="btn-primary" onserverclick="btnSouscrivez_OnServerClick">
                                        <%# Eval("Name") %>
                                        <div class="offer-content">
                                            <div class="price"><%# Eval("PricingLocalized") %></div>
                                            <div class="cell-feature">
                                                <asp:Repeater runat="server" ID="repeatFeature">
                                                    <ItemTemplate>
                                                        <span class="feature-extra">
                                                            <%# Eval("PricingLocalized") %>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <div><%# Eval("DurationRecurrence") %> <%# Eval("UnitRecurrence") %></div>
                                        </div>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th style="font-size: 35px; padding-left: 12px; background-color: #304f73; color: #fff">Les modules de calculs :</th>
                        <asp:Repeater runat="server" ID="RepeaterTitle1">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Actes portant sur la transmission du patrimoine à titre gratuit (7 modules)</th>
                        <asp:Repeater runat="server" ID="Repeater1">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Actes de partage et de licitation - indivision familiale (7 modules)</th>
                        <asp:Repeater runat="server" ID="Repeater2">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Actes relatifs régissant les liens familiale (8 modules)</th>
                        <asp:Repeater runat="server" ID="Repeater3">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Actes relatifs aux biens immobiliers (5 modules)</th>
                        <asp:Repeater runat="server" ID="Repeater4">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Actes relatifs à l'activité économique (9 modules)</th>
                        <asp:Repeater runat="server" ID="Repeater5">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th style="font-size: 35px; padding-left: 12px; background-color: #304f73; color: #fff">Les fonctionnalités :</th>
                        <asp:Repeater runat="server" ID="RepeaterTitle2">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Archivage des simulations</th>
                        <asp:Repeater runat="server" ID="Repeater6">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle <%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Partage des simulations</th>
                        <asp:Repeater runat="server" ID="Repeater7">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle <%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Impression des simulations avec entête personnalisé</th>
                        <asp:Repeater runat="server" ID="Repeater8">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle <%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <th scope="row">Transmission des simulations par email</th>
                        <asp:Repeater runat="server" ID="Repeater9">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>">
                                    <button type="button" runat="server" id="btnTableBody" onserverclick="btnSouscrivez_OnServerClick">
                                        <i class="fa fa-check-circle <%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"></i>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <th scope="row"></th>
                        <asp:Repeater runat="server" ID="repeatOfferFooter">
                            <ItemTemplate>
                                <td class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>" id="offer-<%# Eval("Id") %>">
                                    <button type="button" class="btn-primary" runat="server" id="btnStandardSouscrivez" onserverclick="btnSouscrivez_OnServerClick">
                                        <%# Eval("TextButton") %>
                                        <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                    </button>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </tfoot>
            </table>
        </div>
        <div class="mobile d-block d-sm-none" runat="server" clientidmode="Static" id="divOfferMobile">
            <asp:Repeater runat="server" ClientIDMode="Static" ID="repeatOffer" OnItemDataBound="repeatOffer_OnItemDataBound">
                <ItemTemplate>
                    <div class="col-xs-12">
                        <div class="offer <%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>" id="offer-<%# Eval("Id") %>">
                            <button runat="server" id="btnTableHead" type="button" class="btn-primary" onserverclick="btnSouscrivez_OnServerClick">
                                <%# Eval("Name") %>
                                <div class="offer-content pl-2 pr-2">
                                    <div class="price"><%# Eval("PricingLocalized") %></div>
                                    <div class="cell-feature">
                                        <asp:Repeater runat="server" ID="repeatFeature">
                                            <ItemTemplate>
                                                <span class="feature-extra">
                                                    <%# Eval("PricingLocalized") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="text-left">
                                            <div><span>Les modules de calculs :</span></div>
                                            <div>
                                                <ul>
                                                    <li><span>Actes portant sur la transmission du patrimoine à titre gratuit (7 modules)</span></li>
                                                    <li><span>Actes de partage et de licitation - indivision familiale (7 modules)</span></li>
                                                    <li><span>Actes relatifs régissant les liens familiale (8 modules)</span></li>
                                                    <li><span>Actes relatifs aux biens immobiliers (5 modules)</span></li>
                                                    <li><span>Actes relatifs à l'activité économique (9 modules)</span></li>
                                                </ul>
                                            </div>
                                            <div><span>Les fonctionnalités :</span></div>
                                            <div>
                                                <ul>
                                                    <li class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"><span>Archivage des simulations</span></li>
                                                    <li class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"><span>Partage des simulations</span></li>
                                                    <li class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"><span>Impression des simulations avec entête personnalisé</span></li>
                                                    <li class="<%# Eval("Name").ToString().Replace("'", "").Replace(" ","_") %>"><span>Transmission des simulations par email</span></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div><%# Eval("DurationRecurrence") %> <%# Eval("UnitRecurrence") %></div>
                                </div>
                                <asp:HiddenField runat="server" ID="hdReferenceOffer" Value='<%#Eval("ReferenceOffer")%>' />
                                <div class="offer-footer">
                                    <%# Eval("TextButton") %>
                                </div>
                            </button>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <script type="text/javascript">
        function iframeContent(src) {
            document.getElementById('content_iframe').src = src;
        }
        $('table.table-pricing td, table.table-pricing thead th').hover(function () {
            var cl = "." + $(this).attr("class");
            var id = "#" + $(this).attr("id");
            if ($(this).hasClass('Standard')) {
                $('td.Standard, th.Standard').css({ "border-left": "6px solid #FF7D26", "border-right": "6px solid #FF7D26" });
                $('thead th.Standard').css({ "border-top": "6px solid #FF7D26", "background-color": "#FFFFFF", "color": "#FF7D26" });
                $('tfoot td.Standard').css({ "border-bottom": "6px solid #FF7D26", "background-color": "#FFFFFF", "color": "#FF7D26" });
                $('tfoot td' + cl + ' button').css({ "background-color": "#FFFFFF", "color": "#FF7D26", "border-bottom-left-radius": "1em", "border-bottom-right-radius": "1em" });
            } else {
                $('td' + id + ', th' + id + ', td' + cl + ', th' + cl).css({ "border-left": "6px solid #304f73", "border-right": "6px solid #304f73" });
                $('thead th' + cl).css({ "border-top": "6px solid #304f73", "background-color": "#FFFFFF", "color": "#304f73" });
                $('thead th' + cl + ' button').css({ "background-color": "#FFFFFF", "color": "#304f73" });
                $('tfoot td' + cl).css({ "border-bottom": "6px solid #304f73", "background-color": "#FFFFFF", "color": "#304f73" });
                $('tfoot td' + cl + ' button').css({ "background-color": "#FFFFFF", "color": "#304f73", "border-bottom-left-radius": "1em", "border-bottom-right-radius": "1em" });
            }
        });
        $('table.table-pricing td, table.table-pricing thead th').mouseleave(function () {
            var cl = "." + $(this).attr("class");
            if ($(this).hasClass('Standard')) {
                $('td.Standard, th.Standard, td button').removeAttr("style");
            } else {
                $('td' + cl + ', th' + cl + ', td button').removeAttr("style");
                $('th' + cl + ' button').removeAttr("style");
            }
        });
        var maxHeight = Math.max.apply(null, $("div.cell-feature").map(function () {
            return $(this).height();
        }).get());
        $('div.cell-feature').css('height', (maxHeight + 20) + 'px');
    </script>
</asp:Content>

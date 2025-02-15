<%@ Page Title="Fonctionnalités du logiciel notaire - Kopelia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fonctionnalités.aspx.cs" Inherits="NotaliaOnline.Fonctionnalités" 
    MetaDescription="Logiciel Kopelia: Calcul les frais d'actes notariés, donations, actes de partage et de licitation et autres actes." %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .body-content, #MobileMaster {
            font-family: DIN Cond-Light;
            color: #1D9BD7;
            font-size: 1.8rem;
        }

            /*.body-content li::marker, #MobileMaster li::marker {
                content: "\f061";
                font-family: FontAwesome;
                display: inline-block;
                color: #FF7D26;
            }*/
            .body-content li::before, #MobileMaster li::before {
                color: #FF7D26;
                content: "\f061";
                font-family: FontAwesome;
                display: inline-block;
                margin-left: -1.3em;
                width: 1.3em;
            }
    </style>

    <div class="container pt-3">
        <div class="row">
            <div class="col-12 col-lg-9">
                <div style="font-size: 2.2rem">
                    <b>Kopelia</b> permet de calculer de façon précise et rapide les frais d'actes notariés.<br />
                    Un ensemble de simulateurs à votre disposition permet de couvrir l'ensemble de la pratique notariale.<br />
                    Avec Kopelia, vous allez pouvoir :
                </div>
                <ul>
                    <li><b>Archiver</b> vos simulations afin de pouvoir éventuellement les modifier par la suite,</li>
                    <li><b>Partager</b> vos simulations avec les collaborateurs de votre Office,</li>
                    <li><b>Imprimer</b> avec un entête personnalisé vos simulations afin de fournir à vos clients un rapport clair et précis,</li>
                    <li><b>Transmettre</b> vos simulations par email,</li>
                </ul>
            </div>
            <div class="col-12 col-lg-3 text-center">
                <img src="images/Kopelia-a-logo.png" alt="plan-circle" />
            </div>
        </div>
    </div>

    <div style="background-color: #C6EAFB">
        <div class="container pt-3">
            <div class="row text-center">
                <h2 class="col-12" style="font-family: Manus">Nos modules de calculs</h2>
            </div>
            <div class="row">
                <div class="col-12 col-lg-9">
                    <h2>Actes portant sur la transmission du patrimoine à titre gratuit</h2>
                    <ul>
                        <li>Attestation notariée</li>
                        <li>Donation et donation partage (1 donateur)</li>
                        <li>Donation et donation partage (2 donateurs)</li>
                        <li>Donation - Calcul des droits de mutation à titre gratuit</li>
                        <li>Notoriété après décès</li>
                        <li>Règlement successoral ab intestat</li>
                        <li>Succession - Calcul des droits de mutation à titre gratuit</li>
                    </ul>
                </div>
                <div class="col-12 col-lg-3 text-center">
                    <img src="images/divorce-partage.png" alt="plan-circle" />
                </div>
            </div>
        </div>
    </div>

    <div class="container pt-3">
        <div class="row">
            <div class="col-12 col-lg-9">
                <h2>Actes de partage et de licitation - indivision familiale</h2>
                <ul>
                    <li>Cession de droit indivis – Indivision familiale </li>
                    <li>Licitation faisant cesser une indivision familiale</li>
                    <li>Licitation faisant cesser une indivision suite à divorce ou rupture de PACS</li>
                    <li>Partage de biens de communauté consécutif à divorce (art 746 CGI)</li>
                    <li>Partage de biens de communauté ou de succession (art 748 CGI)</li>
                    <li>Partage de biens indivis - Indivision familiale (art 748 CGI)</li>
                    <li>Partage de biens indivis consécutif à divorce ou rupture de PACS (art 746 CGI)</li>
                </ul>
            </div>
            <div class="col-12 col-lg-3 text-center">
                <img src="images/demembrement.png" alt="plan-circle" />
            </div>
        </div>
    </div>

    <div style="background-color: #CED1D1">
        <div class="container pt-3">
            <div class="row">
                <div class="col-12 col-lg-9">
                    <h2>Actes relatifs régissant les liens familiaux</h2>
                    <ul>
                        <li>Changement de régime matrimonial</li>
                        <li>Contrat de mariage</li>
                        <li>Donation entre époux</li>
                        <li>Liquidation d'un régime communautaire dans le cas d'un divorce</li>
                        <li>Mandat posthume & Mandat de protection future</li>
                        <li>Pacte civil de solidarité</li>
                        <li>Procuration</li>
                        <li>Testaments (authentique et olographe)</li>
                    </ul>
                </div>
                <div class="col-12 col-lg-3 text-center">
                    <img src="images/plus-values.png" alt="plan-circle" /> 
                </div>
            </div>
        </div>
    </div>

    <div class="container pt-3">
        <div class="row">
            <div class="col-12 col-lg-9">
                <h2>Actes relatifs aux biens immobiliers</h2>
                <ul>
                    <li>Baux de droit commun (- de 12 ans)</li>
                    <li>Baux spécifiques (+ de 12 ans)</li>
                    <li>Notoriété prescription acquisitive</li>
                    <li>Ventes en l'état futur d'achèvement</li>
                    <li>Ventes immobilières</li>
                </ul>
            </div>
            <div class="col-12 col-lg-3 text-center">
                <img src="images/ventes-cessions.png" alt="plan-circle" />
            </div>
        </div>
    </div>

    <div style="background-color: #C6EAFB">
        <div class="container pt-3">
            <div class="row">
                <div class="col-12 col-lg-9">
                    <h2>Actes relatifs à l'activité économique</h2>
                    <ul>
                        <li>Cession de droit indivis - Droit commun</li>
                        <li>Cession de droits sociaux d'une SCI d'attribution</li>
                        <li>Cession de droits sociaux ordinaire</li>
                        <li>Crédit bail</li>
                        <li>Echange</li>
                        <li>Licitation faisant cesser une indivision de droit commun</li>
                        <li>Mainlevées et quittances</li>
                        <li>Partage de droit commun</li>
                        <li>Prêts hypothécaires</li>
                    </ul>
                </div>
                <div class="col-12 col-lg-3 text-center">
                    <img src="images/donations-successions.png" alt="plan-circle" />
                </div>
            </div>
        </div>
    </div>

    <div style="background: #01ABE4">
        <div class="container text-center pb-4 pt-4">
            <a href="/Tarifs" class="btn btn-success" style="font-size:2.5rem;background:#8fc73e">
                <i class="fa fa-eye" aria-hidden="true"></i>
                Essayez KOPELIA gratuitement !
            </a>
        </div>
    </div>
</asp:Content>

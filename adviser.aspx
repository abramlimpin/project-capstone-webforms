<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="adviser.aspx.cs" Inherits="adviser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-9">
                <div class="card">
                    <div class="body clearfix">
                        <asp:Image ID="imgUser" runat="server" CssClass="img-responsive col-lg-2" />
                        <div class="col-lg-10">
                            <h2>
                                <asp:Literal ID="ltName" runat="server" /></h2>
                            <h5>Email:
                                <asp:Literal ID="ltEmail" runat="server" /></h5>
                            <h5>Studio:
                                <asp:Literal ID="ltStudio" runat="server" /></h5>
                            <blockquote class="m-b-25">
                                <p>
                                    <asp:Literal ID="ltManifesto" runat="server" />
                                </p>
                            </blockquote>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="body clearfix">

                        <ul class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active">
                                <a href="#profile" data-toggle="tab" aria-expanded="true">
                                    <i class="material-icons">face</i> PROFILE
                                </a>
                            </li>
                            <li role="presentation" class="">
                                <a href="#vitae" data-toggle="tab" aria-expanded="false">
                                    <i class="material-icons">art_track</i> MENTOR VITAE
                                </a>
                            </li>
                            <li role="presentation" class="">
                                <a href="#works" data-toggle="tab" aria-expanded="false">
                                    <i class="material-icons">photo_library</i> PORTFOLIO
                                </a>
                            </li>
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane fade active in" id="profile">
                                <h2 class="card-inside-title">Teaching Topic Strengths</h2>
                                <p>
                                    <asp:Literal ID="ltTeaching" runat="server" />
                                </p>
                                <h2 class="card-inside-title">Research Topic Guidance Strengths</h2>
                                <p>
                                    <asp:Literal ID="ltResearch" runat="server" />
                                </p>
                                <h2 class="card-inside-title">Direction and Platform Affiliations</h2>
                                <p>
                                    <ul>
                                        <asp:ListView ID="lvAffiliations" runat="server">
                                            <ItemTemplate>
                                                <li><%# Eval("Name") %></li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ul>
                                </p>
                                <h2 class="card-inside-title">Professional Statement</h2>
                                <p>
                                    <asp:Literal ID="ltStatement" runat="server" />
                                </p>
                            </div>
                            <div role="tabpanel" class="tab-pane fade" id="vitae">
                                <h2 class="card-inside-title">Summary</h2>
                                <p>
                                    <asp:Literal ID="ltResume" runat="server" />
                                </p>
                                <h2 class="card-inside-title">General Studio Agenda</h2>
                                <p>
                                    <asp:Literal ID="ltAgenda" runat="server" />
                                </p>
                            </div>
                            <div role="tabpanel" class="tab-pane fade" id="works">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="card">
                    <div class="body">
                        <asp:LinkButton ID="btnSelect" runat="server" CssClass="btn btn-lg btn-success btn-block waves-effect">
                            <i class="material-icons">add_to_photos</i><span>CHOOSE AS ADVISER</span>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>


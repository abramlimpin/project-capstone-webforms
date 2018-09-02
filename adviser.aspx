<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="adviser.aspx.cs" Inherits="adviser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-12">
                <div id="error" runat="server" class="alert alert-danger" visible="false">
                    Mentor already selected. <a runat="server" href="~/Account/Application">View application here.</a>
                </div>
                <div id="add" runat="server" class="alert alert-success" visible="false">
                    Selected mentor. <a runat="server" href="~/Account/Application">View application here.</a>
                </div>
            </div>
            <div class="col-lg-9">
                <div class="card">
                    <div class="body clearfix">
                        <asp:Image ID="imgUser" runat="server" CssClass="img-responsive col-lg-2" />
                        <div class="col-lg-10">
                            <h2>
                                <asp:Literal ID="ltFacultyID" runat="server" Visible="false" />
                                <asp:Literal ID="ltName" runat="server" /></h2>
                            <h5>Email:
                                <asp:Literal ID="ltEmail" runat="server" /></h5>
                            <h5>Studio:
                                <asp:Literal ID="ltStudio" runat="server" /></h5>
                            <blockquote class="m-b-15">
                                <p>
                                    <asp:Literal ID="ltAgenda" runat="server" />
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
                                    <ul>
                                        <asp:ListView ID="lvTopics_Teaching" runat="server">
                                            <ItemTemplate>
                                                <li><%# Eval("Name") %></li>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <li>No records found.</li>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </ul>
                                </p>
                                <h2 class="card-inside-title">Research Topic Guidance Strengths</h2>
                                <p>
                                    <ul>
                                        <asp:ListView ID="lvTopics_Research" runat="server">
                                            <ItemTemplate>
                                                <li><%# Eval("Name") %></li>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <li>No records found.</li>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </ul>
                                </p>
                                <h2 class="card-inside-title">Directions</h2>
                                <p>
                                    <ul>
                                        <asp:ListView ID="lvDirections" runat="server">
                                            <ItemTemplate>
                                                <li><%# Eval("Name") %></li>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <li>No records found.</li>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </ul>
                                </p>
                                <h2 class="card-inside-title">Platform Affiliations</h2>
                                <p>
                                    <ul>
                                        <asp:ListView ID="lvAffiliations" runat="server">
                                            <ItemTemplate>
                                                <li><%# Eval("Name") %></li>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <li>No records found.</li>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </ul>
                                </p>
                                <h2 class="card-inside-title">Professional Statement</h2>
                                <p>
                                    <asp:Literal ID="ltStatement" runat="server" />
                                </p>
                                <h2 class="card-inside-title">Educational Background</h2>
                                <p>
                                    <ul>
                                        <asp:ListView ID="lvEducation" runat="server">
                                            <ItemTemplate>
                                                <li><%# Eval("Degree") %> (<%# Eval("Institution") %>) <span class="label label-info"><%# Eval("YearStart", "{0: yyyy}") %></span></li>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <li>No records found.</li>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </ul>
                                </p>
                            </div>
                            <div role="tabpanel" class="tab-pane fade" id="vitae">
                                <h2 class="card-inside-title">Summary</h2>
                                <p>
                                    <asp:Literal ID="ltResume" runat="server" />
                                </p>
                                <h2 class="card-inside-title">Abridged Manifesto</h2>
                                <p>
                                    <asp:Literal ID="ltManifesto" runat="server" />
                                </p>
                                <asp:Panel ID="pnlOthers" runat="server" Visible="false">
                                    <h2 class="card-inside-title">Additional Notes</h2>
                                    <p>
                                        <asp:Literal ID="ltOthers" runat="server" />
                                    </p>
                                </asp:Panel>
                            </div>
                            <div role="tabpanel" class="tab-pane fade" id="works">
                                <div id="animated-thumbnails" class="list-unstyled row clearfix">
                                    <asp:ListView ID="lvPortfolio" runat="server">
                                        <ItemTemplate>
                                            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                                <a runat="server" href='<%# string.Concat("~/images/portfolio/", Eval("Image").ToString()) %>' data-sub-html='<%# Eval("Title") %>'>
                                                    <img runat="server" class="img-responsive thumbnail" src='<%# string.Concat("~/images/portfolio/", Eval("Image")) %>'>
                                                </a>
                                            </div>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div class="col-lg-12">
                                                <div class="well">
                                                    <h2 class="text-center">No records found.</h2>
                                                </div>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="card">
                    <div class="body">

                        <h2 class="card-inside-title">Availability</h2>
                        <p>
                            <ul>
                                <asp:ListView ID="lvSchedule" runat="server">
                                    <ItemTemplate>
                                        <li><%# Eval("Day") %> (<%# Eval("StartTime", "{0:hh:mm tt}") %> - <%# Eval("EndTime", "{0:hh:mm tt}") %>)</li>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <li>No records found.</li>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </ul>
                        </p>
                        <asp:LinkButton ID="btnSelect" runat="server" CssClass="btn btn-lg btn-success btn-block waves-effect" OnClick="btnSelect_Click">
                            <i class="material-icons">add_to_photos</i><span>CHOOSE AS ADVISER</span>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>


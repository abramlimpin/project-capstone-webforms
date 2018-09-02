<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="student.aspx.cs" Inherits="student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-12">
            </div>
            <div class="col-lg-9">
                <div class="card">
                    <div class="body clearfix">
                        <asp:Image ID="imgUser" runat="server" CssClass="img-responsive col-lg-2" />
                        <div class="col-lg-10">
                            <h2>
                                <asp:Literal ID="ltStudentID" runat="server" Visible="false" />
                                <asp:Literal ID="ltName" runat="server" /></h2>
                            <h5>Course: <asp:Literal ID="ltCourse" runat="server" /></h5>
                            <h5>Email: <asp:Literal ID="ltEmail" runat="server" /></h5>
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
                                <a href="#works" data-toggle="tab" aria-expanded="false">
                                    <i class="material-icons">photo_library</i> PORTFOLIO
                                </a>
                            </li>
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane fade active in" id="profile">
                                <h2 class="card-inside-title">Research / Thesis Agenda</h2>
                                <p>
                                    <asp:Literal ID="ltAgenda" runat="server" />
                                </p>
                                <h2 class="card-inside-title">Related Files</h2>
                                <p>
                                    <ul>
                                        <asp:ListView ID="lvUploads" runat="server">
                                            <ItemTemplate>
                                                <li><a runat="server" href='<%# string.Concat("~/download?f=", Eval("Code").ToString()) %>' target="_blank">
                                                <%# Eval("Name") %>
                                            </a>
                                            <asp:Literal ID="ltCode" runat="server" Text='<%# Eval("Code") %>' Visible="false" />
                                            <asp:Literal ID="ltFileName" runat="server" Text='<%# Eval("FileName") %>' Visible="false" /></li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ul>
                                </p>
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
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>


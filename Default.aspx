<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <asp:Panel ID="stat_admin" runat="server" Visible="false">
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" onclick="location.href='users';">
                <div class="info-box-4 hover-zoom-effect">
                    <div class="icon">
                        <i class="material-icons col-red">account_box</i>
                    </div>
                    <div class="content">
                        <div class="text">TOTAL USERS</div>
                        <div id="count_users" runat="server" class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">
                            <asp:Literal ID="ltCount_Users" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" onclick="location.href='students';">
                <div class="info-box-4 hover-zoom-effect">
                    <div class="icon">
                        <i class="material-icons col-cyan">school</i>
                    </div>
                    <div class="content">
                        <div class="text"># OF STUDENTS</div>
                        <div id="count_students" runat="server" class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">
                            <asp:Literal ID="ltCount_Students" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" onclick="location.href='faculty';">
                <div class="info-box-4 hover-zoom-effect">
                    <div class="icon">
                        <i class="material-icons col-green">face</i>
                    </div>
                    <div class="content">
                        <div class="text">TOTAL FACULTY</div>
                        <div id="count_faculty" runat="server" class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">
                            <asp:Literal ID="ltCount_Faculty" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" onclick="location.href='enlistment';">
                <div class="info-box-4 hover-zoom-effect">
                    <div class="icon">
                        <i class="material-icons col-orange">assignment</i>
                    </div>
                    <div class="content">
                        <div class="text">TOTAL ENLISTED STUDENTS</div>
                        <div id="count_enlist" runat="server" class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">
                            <asp:Literal ID="ltCount_Enlist" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="stat_faculty" runat="server" Visible="false">
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="info-box-4 hover-zoom-effect">
                    <div class="icon">
                        <i class="material-icons col-cyan">school</i>
                    </div>
                    <div class="content">
                        <div class="text"># OF ADVISEES</div>
                        <div id="count_advisees" runat="server" class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">
                            <asp:Literal ID="ltCount_Advisees" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="row clearfix">
        <div class="block-header">
            <h3>Latest News</h3>
        </div>
        <asp:ListView ID="lvNews" runat="server">
            <ItemTemplate>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <div class="card">
                        <div class="header">
                            <h2><%# Eval("Title") %>
                                <small>
                                    <span class="pull-left">Date Posted: <%# Eval("DateAdded", "{0: MM/dd/yyyy hh:mm tt}") %></span>
                                    <span class="pull-right">Last Modified: <%# Eval("DateModified", "{0: MM/dd/yyyy hh:mm tt}") %>
                                </small>
                            </h2>
                        </div>
                        <div class="body">
                            <%# Server.HtmlDecode(Eval("Post").ToString()) %>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>


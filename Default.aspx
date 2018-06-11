﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
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
    </div>
</asp:Content>

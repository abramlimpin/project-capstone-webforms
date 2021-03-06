﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Faculty_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>MY PORTFOLIO</h2>
                    <ul class="header-dropdown m-r--5">
                        <li class="dropdown">
                            <a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                <i class="material-icons">more_vert</i>
                            </a>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="Add">Add a Record</a></li>
                        </li>
                    </ul>
                </div>
                <div class="body">
                    <div id="update" runat="server" class="alert alert-success" visible="false">
                        Record updated
                    </div>
                    <div class="table-responsive">
                        <table class="table table-striped table-hover my-table dataTable">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Title</th>
                                    <th>Keywords</th>
                                    <th>Status</th>
                                    <th>Date Added</th>
                                    <th>Last Modified</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th></th>
                                    <th>Title</th>
                                    <th>Keywords</th>
                                    <th>Status</th>
                                    <th>Date Added</th>
                                    <th>Last Modified</th>
                                </tr>
                            </tfoot>
                            <tbody>
                                <asp:ListView ID="lvRecords" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><a class="link" href='Details?no=<%# Eval("Code") %>'>
                                                <img runat="server" src='<%# string.Concat("~/images/portfolio/", Eval("Image").ToString()) %>' width="100" />
                                                </a></td>
                                            <td><a class="link" href='Details?no=<%# Eval("Code") %>'><%# Eval("Title") %></a></td>
                                            <td><%# Eval("Keywords") %></td>
                                            <td><%# Eval("Status") %></td>
                                            <td><%# Eval("DateAdded") %></td>
                                            <td><%# Eval("DateModified") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


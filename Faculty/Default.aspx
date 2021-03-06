﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Faculty_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>FACULTY LIST
                            </h2>
                    <ul class="header-dropdown m-r--5">
                        <li class="dropdown">
                            <a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                <i class="material-icons">more_vert</i>
                            </a>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="Add">Add a Record</a></li>
                                <li><a href="Logs">View Logs</a></li>
                                <li class="divider"></li>
                                <li><a href="Advising">Manage Advising</a></li>
                            </ul>
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
                                    <th>Account #</th>
                                    <th>Name</th>
                                    <th>Email Address</th>
                                    <th>Program</th>
                                    <th>Date Added</th>
                                    <th>Last Modified</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th></th>
                                    <th>Account #</th>
                                    <th>Name</th>
                                    <th>Email Address</th>
                                    <th>Program</th>
                                    <th>Date Added</th>
                                    <th>Last Modified</th>
                                    <th>Status</th>
                                </tr>
                            </tfoot>
                            <tbody>
                                <asp:ListView ID="lvRecords" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <img runat="server" src='<%# Eval("Image").ToString() == "" ? string.Concat("~/images/user-placeholder.jpg") : string.Concat("~/images/users/", Eval("Image").ToString()) %>' width="100" />
                                            </td>
                                            <td><a class="link" href='Details?no=<%# Eval("Code") %>'><%# Eval("AccountNo") %></a></td>
                                            <td><%# Eval("Name") %></td>
                                            <td><%# Eval("Email") %></td>
                                            <td><%# Eval("Program") %></td>
                                            <td><%# Eval("DateAdded") %></td>
                                            <td><%# Eval("DateModified") %></td>
                                            <td><%# Eval("Status") %></td>
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


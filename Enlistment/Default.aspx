<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Enlistment_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>ENLISTMENT
                            </h2>
                    <%--<ul class="header-dropdown m-r--5">
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
                    </ul>--%>
                </div>
                <div class="body">
                    <div id="update" runat="server" class="alert alert-success" visible="false">
                        Record updated
                    </div>
                    <div class="table-responsive">
                        <table class="table table-striped table-hover my-table dataTable">
                            <thead>
                                <tr>
                                    <th>Date Added</th>
                                    <th>Student Name</th>
                                    <th>Adviser</th>
                                    <th>Status</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th>Date Added</th>
                                    <th>Student Name</th>
                                    <th>Adviser</th>
                                    <th>Status</th>
                                    <th></th>
                                </tr>
                            </tfoot>
                            <tbody>
                                <asp:ListView ID="lvRecords" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("DateAdded") %></td>
                                            <td><a runat="server" target="_blank" class="link" href='<%# string.Concat("~/student?u=", Eval("Code_Student")) %>'><%# Eval("Student") %></a></td>
                                            <td><a runat="server" target="_blank" class="link" href='<%# string.Concat("~/adviser?u=", Eval("Code_Faculty")) %>'><%# Eval("Faculty") %></a></td>
                                            <td><%# Eval("Status") %></td>
                                            <td></td>
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


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="News_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>NEWS LIST
                            </h2>
                    <ul class="header-dropdown m-r--5">
                        <li class="dropdown">
                            <a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                <i class="material-icons">more_vert</i>
                            </a>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="Add">Add a Record</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div class="body">
                    <div class="table-responsive">
                        <table class="table table-striped table-hover my-table dataTable">
                            <thead>
                                <tr>
                                    <th>Type</th>
                                    <th>Title</th>
                                    <th>Posted By</th>
                                    <th>Date Added</th>
                                    <th>Date Modified</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th>Type</th>
                                    <th>Title</th>
                                    <th>Posted By</th>
                                    <th>Date Added</th>
                                    <th>Date Modified</th>
                                    <th>Status</th>
                                </tr>
                            </tfoot>
                            <tbody>
                                <asp:ListView ID="lvRecords" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                             <td><%# Eval("Type") %></td>
                                            <td><a class="link" href='Details?id=<%# Eval("Code") %>'><%# Eval("Title") %></a></td>
                                            <td><%# Eval("Name") %></td>
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


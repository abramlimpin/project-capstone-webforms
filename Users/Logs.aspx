<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Logs.aspx.cs" Inherits="Users_Logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>AUDIT LOGS
                            </h2>
                    <ul class="header-dropdown m-r--5">
                        <li class="dropdown">
                            <a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                <i class="material-icons">more_vert</i>
                            </a>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="#">Generate Report</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div class="body">
                    <div class="table-responsive">
                        <table class="table table-striped table-hover my-table dataTable">
                            <thead>
                                <tr>
                                    <th>Account #</th>
                                    <th>Type</th>
                                    <th>Description</th>
                                    <th>Timestamp</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th>Account #</th>
                                    <th>Type</th>
                                    <th>Description</th>
                                    <th>Timestamp</th>
                                </tr>
                            </tfoot>
                            <tbody>
                                <asp:ListView ID="lvRecords" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Helper.Decrypt(Eval("AccountNo").ToString() == "0" ? "Administrator" : Eval("AccountNo").ToString()) %></td>
                                            <td><%# Helper.Decrypt(Eval("LogType").ToString()) %></td>
                                            <td><%# Helper.Decrypt(Eval("Description").ToString()) %></td>
                                            <td><%# Eval("LogDate") %></td>
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


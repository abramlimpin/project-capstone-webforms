<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Students_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="header">
                        <h2>STUDENTS LIST
                            </h2>
                        <ul class="header-dropdown m-r--5">
                            <li class="dropdown">
                                <a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                    <i class="material-icons">more_vert</i>
                                </a>
                                <ul class="dropdown-menu pull-right">
                                    <li><a href="Add">Add a Record</a></li>
                                    <li><a href="Logs">View Logs</a></li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                    <div class="body">
                        <div id="update" runat="server" class="alert alert-success" visible="false">
                            Records updated.
                        </div>
                        <div class="table-responsive">
                            <table class="table table-striped table-hover my-table dataTable">
                                <thead>
                                    <tr>
                                        <th>Student #</th>
                                        <th>Name</th>
                                        <th>Email Address</th>
                                        <th>Gender</th>
                                        <th>Date Added</th>
                                        <th>Last Modified</th>
                                        <th>Status</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tfoot>
                                    <tr>
                                        <th>Student #</th>
                                        <th>Name</th>
                                        <th>Email Address</th>
                                        <th>Gender</th>
                                        <th>Date Added</th>
                                        <th>Last Modified</th>
                                        <th>Status</th>
                                        <th></th>
                                    </tr>
                                </tfoot>
                                <tbody>
                                    <asp:ListView ID="lvStudents" runat="server" OnItemCommand="lvStudents_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><a class="link" href='Details?no=<%# Eval("Code") %>'>
                                                    <asp:Literal ID="ltAccountNo" runat="server" Text='<%# Eval("AccountNo") %>' />
                                                    </a></td>
                                                <td><%# Eval("Name") %></td>
                                                <td><asp:Literal ID="ltEmail" runat="server" Text='<%# Eval("Email") %>' /></td>
                                                <td><%# Eval("Gender") %></td>
                                                <td><%# Eval("DateAdded") %></td>
                                                <td><%# Eval("DateModified") %></td>
                                                <td><%# Eval("Status") %></td>
                                                <td>
                                                    <asp:LinkButton ID="btnActivate" runat="server" CssClass="btn btn-xs btn-info" 
                                                        Visible='<%# Eval("Status").ToString() == "Pending" ? true : false %>'
                                                        ToolTip="Activate Account" OnClientClick='return confirm("Activate account?");'
                                                        CommandName="activate">
                                                    <i class="material-icons">lock_open</i>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>


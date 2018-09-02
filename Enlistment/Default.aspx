<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Enlistment_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
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
                    <form runat="server">
                        <div id="update" runat="server" class="alert alert-success" visible="false">
                            Record updated
                        </div>
                        <div id="error" runat="server" class="alert alert-success" visible="false">
                            Slots are full.
                        </div>
                        <div id="blank" runat="server" class="alert alert-success" visible="false">
                            Slots are full.
                        </div>
                        <div class="table-responsive">
                            <table class="table table-striped table-hover my-table dataTable">
                                <thead>
                                    <tr>
                                        <th>Date Added</th>
                                        <th>Course</th>
                                        <th>Student #</th>
                                        <th>Student Name</th>
                                        <th>Adviser</th>
                                        <th>Status</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tfoot>
                                    <tr>
                                        <th>Date Added</th>
                                        <th>Course</th>
                                        <th>Student #</th>
                                        <th>Student Name</th>
                                        <th>Adviser</th>
                                        <th>Status</th>
                                        <th></th>
                                    </tr>
                                </tfoot>
                                <tbody>
                                    <asp:ListView ID="lvRecords" runat="server" OnItemCommand="lvRecords_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <asp:Literal ID="ltEnlistID" runat="server" Text='<%# Eval("EnlistID") %>' Visible="false" />
                                                <td><%# Eval("DateAdded") %></td>
                                                <td><%# Eval("Course") %></td>
                                                 <td><a runat="server" target="_blank" class="link" href='<%# string.Concat("~/student?u=", Eval("Code_Student")) %>'><asp:Literal ID="ltAccountNo" runat="server" Text='<%# Eval("AccountNo") %>' /></a></td>
                                                <td><a runat="server" target="_blank" class="link" href='<%# string.Concat("~/student?u=", Eval("Code_Student")) %>'><%# Eval("Student") %></a></td>
                                                <td><a runat="server" target="_blank" class="link" href='<%# string.Concat("~/adviser?u=", Eval("Code_Faculty")) %>'><%# Eval("Faculty") %></a></td>
                                                <td><%# Eval("Status") %></td>
                                                <td>
                                                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success btn-xs" CommandName="approve" Visible='<%# Eval("Status").ToString() != "Active" ? false : true %>'
                                                        OnClientClick='return confirm("Are you sure?");' Text="Approve" formnovalidate />
                                                    <a runat="server" class="btn btn-danger btn-xs" data-toggle="modal" data-target="#remarks" Visible='<%# Eval("Status").ToString() != "Active" ? false : true %>'>
                                                        Disapprove
                                                    </a>
                                                </td>
                                            </tr>
                                            <div class="modal fade" id="remarks" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                            <h4 class="modal-title">Disapprove Enlistment</h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Input your remarks here..." />
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button ID="btnDisapprove" runat="server" CssClass="btn btn-success" Text="Proceed" formnovalidate />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- /.modal -->
                                        </ItemTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


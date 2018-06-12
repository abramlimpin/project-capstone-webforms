<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Programs.aspx.cs" Inherits="Admin_Programs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

    <div class="row clearfix">
        <div class="col-lg-12">
            <div class="header">
                <h2>MANAGE PROGRAMS</h2>
            </div>
        </div>
    </div>
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-4">
                <div class="card">
                    <div class="body">
                        <div id="status" runat="server" class="form-group" visible="false">
                            <div class="form-group">
                                <asp:CheckBox ID="cboStatus" runat="server" Text="Active" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlSchools" runat="server" CssClass="form-control show-tick" required />
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" MaxLength="50" required autofocus autocomplete="off" />
                                <label class="form-label">Program Code</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:Literal ID="ltProgram" runat="server" Visible="false" />
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="50" required autocomplete="off" />
                                <label class="form-label">Program Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" />
                                <label class="form-label">Description</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-lg btn-success waves-effect" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-lg btn-success waves-effect" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-lg btn-default waves-effect" Text="Cancel" Visible="false" OnClick="btnCancel_Click" />
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-8">
                <div class="card">
                    <div class="body">
                        <div id="success" runat="server" class="alert alert-success" visible="false">
                            <asp:Literal ID="ltSuccess" runat="server" />
                        </div>
                        <div id="error" runat="server" class="alert alert-danger" visible="false">
                            <asp:Literal ID="ltError" runat="server" />
                        </div>
                        <div class="table-responsive">
                            <table class="table table-responsive">
                                <thead>
                                    <th>#</th>
                                    <th>Name</th>
                                    <th>Status</th>
                                    <th>Date Added</th>
                                    <th></th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvPrograms" runat="server" OnItemCommand="lvPrograms_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="ltProgramID" runat="server" Text='<%# Eval("ProgramID") %>' /></td>
                                                <td><%# Eval("Name") %> (<%# Eval("ProgramCode") %>)</td>
                                                <td><%# Eval("Status") %></td>
                                                <td><%# Eval("DateAdded") %></td>
                                                <td>
                                                    <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-xs btn-info" CommandName="manage">
                                          <i class="material-icons">edit</i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandName="archive"
                                                        OnClientClick='return confirm("Archive record?");'>
                                          <i class="material-icons">delete</i>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="6">
                                                    <h3 class="text-center">No records found.</h3>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
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


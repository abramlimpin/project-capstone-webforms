<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Directions.aspx.cs" Inherits="Admin_Directions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

    <div class="row clearfix">
        <div class="col-lg-12">
            <div class="header">
                <h2>MANAGE DIRECTIONS</h2>
            </div>
        </div>
    </div>
    <div class="row clearfix">
        <div class="col-lg-4">
            <div class="card">
                <div class="body">
                    <form runat="server" class="form-validate">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="50" required autofocus autocomplete="off" />
                                <label class="form-label">Name</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-lg btn-success waves-effect" Text="Add" OnClick="btnAdd_Click" />
                        </div>
                    </form>
                </div>

            </div>
        </div>
        <div class="col-lg-8">
            <div class="card">
                <div class="body">
                    <div id="update" runat="server" class="alert alert-success" visible="false">
                        Record updated.
                    </div>
                    <table class="table table-responsive">
                        <thead>
                            <th>#</th>
                            <th>Name</th>
                            <th>Date Added</th>
                            <th>Date Modified</th>
                            <th>Status</th>
                            <th></th>
                        </thead>
                        <tbody>
                            <asp:ListView ID="lvRecords" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("DirectID") %></td>
                                        <td><%# Eval("Name") %></td>
                                        <td><%# Eval("DateAdded") %></td>
                                        <td><%# Eval("DateModified") %></td>
                                        <td><%# Eval("Status") %></td>
                                        <td></td>
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
</asp:Content>


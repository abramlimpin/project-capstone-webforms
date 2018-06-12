﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Advising.aspx.cs" Inherits="Faculty_Advising" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

    <div class="row clearfix">
        <div class="col-lg-12">
            <div class="header">
                <h2>MANAGE ADVISING</h2>
            </div>
        </div>
    </div>
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-4">
                <div class="card">
                    <div class="body">
                        <div id="existing" runat="server" class="alert alert-danger" visible="false">
                            Faculty record already existing.
                        </div>
                        <div id="status" runat="server" class="form-group" visible="false">
                            <div class="form-group">
                                <asp:CheckBox ID="cboStatus" runat="server" Text="Active" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control show-tick" required />
                            <asp:Literal ID="ltRecord" runat="server" Visible="false" />
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlSY" runat="server" CssClass="form-control show-tick" required>
                                <asp:ListItem Text="Select school year..." Value="" />
                                <asp:ListItem>2018-2019</asp:ListItem>
                                <asp:ListItem>2019-2020</asp:ListItem>
                                <asp:ListItem>2020-2021</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlTerms" runat="server" CssClass="form-control show-tick" required>
                                <asp:ListItem Text="Select a term..." Value="" />
                                <asp:ListItem>1st</asp:ListItem>
                                <asp:ListItem>2nd</asp:ListItem>
                                <asp:ListItem>3rd</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtSlot" runat="server" CssClass="form-control" type="number" min="1" max="15" Text="1" autofocus autocomplete="off" />
                                <label class="form-label"># of Slots</label>
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
                            <table class="table table-responsive my-table dataTable">
                                <thead>
                                    <th>#</th>
                                    <th>Faculty</th>
                                    <th>Term</th>
                                    <th>Slot</th>
                                    <th>Date Added</th>
                                    <th>Status</th>
                                    <th></th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvSlots" runat="server" OnItemCommand="lvSlots_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="ltRecordID" runat="server" Text='<%# Eval("RecordID") %>' /></td>
                                                <td><%# Eval("Name") %></td>
                                                <td><%# Eval("Term") %> Term, <%# Eval("SchoolYear") %></td>
                                                <td><%# Eval("Slot") %></td>
                                                <td><%# Eval("DateAdded") %></td>
                                                <td><%# Eval("Status") %></td>
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


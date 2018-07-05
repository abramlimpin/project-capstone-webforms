<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Users_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <div class="col-lg-6">
            <div class="card">
                <div class="header">
                    <h2>USER DETAILS</h2>
                </div>
                <div class="body">
                    <form runat="server" class="form-validate">
                        <div class="form-group">
                            <asp:CheckBox ID="cboStatus" runat="server" Text="Active" />
                        </div>
                        <h2 class="card-inside-title">Basic Information</h2>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtUserType" runat="server" CssClass="form-control" disabled />
                                <label class="form-label">Account Type</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" disabled />
                                <asp:Literal ID="ltAccountNo" runat="server" Visible="false" />
                                <label class="form-label">InfoNet Account</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email" disabled />
                                <label class="form-label">Email Address</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtFN" runat="server" CssClass="form-control" disabled />
                                <label class="form-label">First Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtLN" runat="server" CssClass="form-control" disabled />
                                <label class="form-label">Last Name</label>
                            </div>
                        </div>
                        <h2 class="card-inside-title">Role</h2>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control show-tick" />
                        </div>
                        <h2 class="card-inside-title">Other Information</h2>
                        <div id="program" runat="server" class="form-group form-float" visible="false">
                            <div class="form-line">
                                <asp:TextBox ID="txtProgram" runat="server" CssClass="form-control" disabled />
                                <label class="form-label">Program</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-lg btn-success waves-effect" Text="Update" OnClick="btnUpdate_Click" />
                            <a runat="server" href="~/Users" class="btn btn-lg btn-default waves-effect">Back to List</a>
                            <asp:Button ID="btnArchive" runat="server" CssClass="btn btn-lg btn-danger waves-effect pull-right" Text="Archive" OnClientClick='return confirm("Archive record?")' OnClick="btnArchive_Click" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


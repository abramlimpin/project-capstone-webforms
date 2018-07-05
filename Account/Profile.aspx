<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Account_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    My Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-6">
                <div class="card">
                    <div class="body">
                        <h2 class="card-inside-title">Basic Information</h2>
                        <div id="update" runat="server" class="alert alert-success" visible="false">
                            Profile updated.
                        </div>
                        <div class="form-group">
                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                <div class="fileinput-new thumbnail" style="width: 200px; height: 150px;">
                                    <asp:Image ID="imgUser" runat="server" />
                                </div>
                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 200px; max-height: 150px;"></div>
                                <div>
                                    <span class="btn btn-info btn-file"><span class="fileinput-new">Select image</span><span class="fileinput-exists">Change</span>
                                        <asp:FileUpload ID="fuImage" runat="server" accept="image/x-png,image/jpeg" />
                                    </span>
                                    <a href="#" class="btn btn-danger fileinput-exists" data-dismiss="fileinput">Remove</a>
                                </div>
                            </div>
                        </div>
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
                                <asp:TextBox ID="txtMN" runat="server" CssClass="form-control" MaxLength="20" autofocus autocomplete="off" />
                                <label class="form-label">Middle Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtLN" runat="server" CssClass="form-control" disabled />
                                <label class="form-label">Last Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtNick" runat="server" CssClass="form-control" MaxLength="20" autofocus autocomplete="off" />
                                <label class="form-label">Nickname</label>
                            </div>
                        </div>
                        <h2 class="card-inside-title">Other Information</h2>
                        <div id="program" runat="server" class="form-group form-float" visible="false">
                            <div class="form-line">
                                <asp:TextBox ID="txtProgram" runat="server" CssClass="form-control" disabled />
                                <label class="form-label">Program</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="material-icons">date_range</i>
                                </span>
                                <asp:TextBox ID="txtBirthdate" runat="server" type="date" placeholder="yyyy-MM-dd" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-lg btn-success waves-effect" Text="Update" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="card">
                    <div class="body">
                        <h2 class="card-inside-title">Account Information</h2>
                        <div id="password" runat="server" class="alert alert-danger" visible="false">
                            Incorrect old password.
                        </div>
                        <div id="account" runat="server" class="alert alert-success" visible="false">
                            Password changed.
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtPassword_Old" runat="server" type="password" MaxLength="30" CssClass="form-control" />
                                <label class="form-label">Old Password</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtPassword_New" runat="server" type="password" MaxLength="30" CssClass="form-control" />
                                <label class="form-label">New Password</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnChangePassword" runat="server" CssClass="btn btn-lg btn-success waves-effect" Text="Change Password" OnClick="btnChangePassword_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>


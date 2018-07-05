<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Faculty_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <div class="col-lg-6">
            <div class="card">
                <div class="header">
                    <h2>ADD A FACULTY</h2>
                </div>
                <div class="body">
                    <form runat="server" class="form-validate">
                        <div id="error" runat="server" class="alert alert-danger" visible="false">
                            Account Name already existing.
                        </div>
                        <h2 class="card-inside-title">Basic Information</h2>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" required autofocus autocomplete="off" />
                                <label class="form-label">InfoNet Account</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email" required autocomplete="off" />
                                <label class="form-label">Email Address</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtFN" runat="server" CssClass="form-control" required autocomplete="off" />
                                <label class="form-label">First Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtMN" runat="server" CssClass="form-control" autocomplete="off" />
                                <label class="form-label">Middle Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtLN" runat="server" CssClass="form-control" required autocomplete="off" />
                                <label class="form-label">Last Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtNickname" runat="server" CssClass="form-control" required autocomplete="off" />
                                <label class="form-label">Nickname</label>
                            </div>
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
                        <h2 class="card-inside-title">Other Information</h2>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlPrograms" runat="server" CssClass="form-control show-tick" required />
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
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-lg btn-success waves-effect" Text="Add" OnClick="btnAdd_Click" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Users_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <div class="col-lg-6">
            <div class="card">
                <div class="header">
                    <h2>ADD A USER</h2>
                </div>
                <div class="body">
                    <form runat="server" class="form-validate">
                        <div id="error" runat="server" class="alert alert-danger" visible="false">
                            Account Name already existing.
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlTypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTypes_SelectedIndexChanged" CssClass="form-control show-tick" required />
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
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email" autofocus required autocomplete="off" />
                                <label class="form-label">Email Address</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtEmail_Repeat" runat="server" CssClass="form-control" type="email" oncopy="return false" onpaste="return false" 
                                    autofocus required autocomplete="off" />
                                <label class="form-label">Repeat Email Address</label>
                                <asp:CompareValidator ID="cv_Email" runat="server" ControlToValidate="txtEmail_Repeat" ControlToCompare="txtEmail"
                                    SetFocusOnError="true" />
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtFN" runat="server" CssClass="form-control" required autofocus autocomplete="off" />
                                <label class="form-label">First Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtLN" runat="server" CssClass="form-control" required autofocus autocomplete="off" />
                                <label class="form-label">Last Name</label>
                            </div>
                        </div>
                        <h2 class="card-inside-title">Other Information</h2>
                        <div id="program" runat="server" class="form-group" visible="false">
                            <asp:DropDownList ID="ddlPrograms" runat="server" CssClass="form-control show-tick" required />
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control show-tick" required>
                                <asp:ListItem Text="Select gender..." Value="" />
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem Value="N/A">Not Specified</asp:ListItem>
                            </asp:DropDownList>
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


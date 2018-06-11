<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="News_Add" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <div class="row clearfix">
        <div class="col-lg-6">
            <div class="card">
                <div class="header">
                    <h2>ADD A POST</h2>
                </div>
                <div class="body">
                    <form runat="server" class="form-validate">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlTypes" runat="server" CssClass="form-control show-tick" required>
                                <asp:ListItem Text="Select a post type..." Value=""></asp:ListItem>
                                <asp:ListItem>General Announcement</asp:ListItem>
                                <asp:ListItem>Updates</asp:ListItem>
                                <asp:ListItem>Events</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="50" required autofocus autocomplete="off" />
                                <label class="form-label">Title</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPost" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="15" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnPublish" runat="server" CssClass="btn btn-lg btn-success waves-effect" 
                                Text="Publish" OnClientClick='return confirm("Publish post?");' OnClick="btnPublish_Click" />
                            <asp:Button ID="btnDraft" runat="server" CssClass="btn btn-lg btn-warning waves-effect pull-right" 
                                Text="Save as Draft" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


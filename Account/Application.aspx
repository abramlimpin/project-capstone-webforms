<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Application.aspx.cs" Inherits="Account_Application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    STUDENT PROFILE
    <small>
        <asp:HyperLink ID="hlkProfile" runat="server" Target="_blank" CssClass="btn btn-xs btn-info" Text="View Public Profile" /></small>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-6">
                <div class="card">
                    <div class="body clearfix">
                        <h2 class="card-inside-title">Basic Information</h2>
                        <div id="update" runat="server" class="alert alert-success" visible="false">
                            Profile updated.
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" disabled />
                                <asp:Literal ID="ltStudentID" runat="server" Visible="false" />
                                <label class="form-label">Student Name</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="body clearfix">
                        <h2 class="card-inside-title">Application Information</h2>
                        <div class="form-group">
                            <label>Top 3 Preferred Mentors</label>
                            <ul>
                                <asp:ListView ID="lvMentors" runat="server">
                                    <ItemTemplate>
                                        <li><%# Eval("Name") %></li>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <li>No records found.</li>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                        <br />
                        <div class="form-group">
                            <label>Research / Thesis Agenda</label>
                            <div class="form-line">
                                <asp:TextBox ID="txtAgenda" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control no-resize" placeholder="Please write a 3-5 sentence Abstract on the proposed research proposal (Arcdes9), or the terminal thesis proposal (Arcds10), below" required />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-lg btn-success waves-effect pull-right" Text="Update" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="card">
                    <div class="body clearfix">
                        <div id="add" runat="server" class="alert alert-success" visible="false">
                            Record updated.
                        </div>
                        <h2 class="card-inside-title">Uploads</h2>
                        <table class="table">
                            <thead>
                                <th>File</th>
                                <th>Date Added</th>
                                <th></th>
                            </thead>
                            <asp:ListView ID="lvUploads" runat="server" OnItemCommand="lvUploads_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <a runat="server" href='<%# string.Concat("~/download?f=", Eval("Code").ToString()) %>' target="_blank">
                                                <%# Eval("Name") %>
                                            </a>
                                            <asp:Literal ID="ltCode" runat="server" Text='<%# Eval("Code") %>' Visible="false" />
                                            <asp:Literal ID="ltFileName" runat="server" Text='<%# Eval("FileName") %>' Visible="false" />
                                        </td>
                                        <td><%# Eval("DateAdded") %></td>
                                        <td>
                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-xs btn-danger" 
                                                ToolTip="Delete File" OnClientClick='return confirm("Delete file?");'
                                                CommandName="remove">
                                            <i class="material-icons">delete_forever</i>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <tr>
                                        <td colspan="3">
                                            <h3 class="text-center">No records found.</h3>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <tr>
                                <td>
                                <asp:FileUpload ID="fuFile" runat="server" CssClass="form-control"
                                    accept=".pdf" />
                                    <asp:RegularExpressionValidator ID="rev_File" runat="server"
                                        ControlToValidate="fuFile" SetFocusOnError="true" Display="Dynamic" />
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-info" Text="Upload" formnovalidate OnClick="btnUpload_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script>
        $(function () {
            //CKEditor
            CKEDITOR.replace('content_txtAgenda');
            CKEDITOR.config.height = 300;
            CKEDITOR.config.toolbar = [
                ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent'],
                ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ];
        });
    </script>
</asp:Content>

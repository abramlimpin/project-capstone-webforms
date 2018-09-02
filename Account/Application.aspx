<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Application.aspx.cs" Inherits="Account_Application" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    STUDENT PROFILE
    <small>
        <asp:HyperLink ID="hlkProfile" runat="server" Visible="false" Target="_blank" CssClass="btn btn-xs btn-info" Text="View Public Profile" /></small>
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
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtStudentNo" runat="server" CssClass="form-control" disabled />
                                <label class="form-label">Student Number</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <label>Course</label>
                            <div class="switch">
                                <label>ARCDES9<asp:CheckBox ID="cbCourse" runat="server" /><span class="lever"></span>ARCDS10</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="body clearfix">
                        <h2 class="card-inside-title">Application Information</h2>
                        <div class="form-group">
                            <label>Top Preferred Mentors</label>
                            <ul>
                                <asp:ListView ID="lvMentors" runat="server" OnItemCommand="lvMentors_ItemCommand">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltEnlistID" runat="server" Text='<%# Eval("EnlistID") %>' Visible="false" />
                                        <li><%# Eval("Name") %>
                                             <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-xs btn-danger btn-xs" Visible='<%# Eval("Status").ToString() == "Active" ? true : false %>' CommandName="remove" OnClientClick='return confirm("Are you sure?")' Text="Remove" formnovalidate />
                                        </li>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <li>No records found. View the <a runat="server" href="~/Directory/Advisers">Directory</a>.</li>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                        <br />
                        <div class="form-group">
                            <label>Research / Thesis Agenda</label><br />
                            <small>Please write a 3-5 sentence Abstract on the proposed research proposal (Arcdes9), or the terminal thesis proposal (Arcds10)</small>
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
                        <small>
                            <strong>For ArcDes9:</strong> Applicant to submit a one-page (or no more than 3 pages) poster of his/her best work from ARCDES 5 - 8. This shall form part of the mentors’ screening mechanism during the studio-drafting session.
                            <br /><br />
                            <strong>For Arcds10:</strong> In addition to above, please submit additional poster/s of your jury-approved Thesis Proposal.
                        </small>
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
                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-xs btn-danger btn-xs" 
                                                ToolTip="Delete File" OnClientClick='return confirm("Delete file?");'
                                                CommandName="remove" Text="Remove" />
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
                                    <small>File Size Limit: 5MB</small>
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

        var uploadField = document.getElementById("content_fuFile");

        uploadField.onchange = function() {
            if(this.files[0].size > 5242880){
                alert("File is too big!");
                this.value = "";
            }
        };
    </script>
</asp:Content>

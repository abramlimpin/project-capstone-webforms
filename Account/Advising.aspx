<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Advising.aspx.cs" Inherits="Account_Advising" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    ADVISER PROFILE
    <small>
        <asp:HyperLink ID="hlkProfile" runat="server" Target="_blank" CssClass="btn btn-xs btn-info" Text="View Public Profile" /></small>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <asp:ScriptManager runat="server" />
            <div class="col-lg-6">
                <div class="card">
                    <div class="body">
                        <h2 class="card-inside-title">Basic Information</h2>
                        <div id="update" runat="server" class="alert alert-success" visible="false">
                            Profile updated.
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" disabled />
                                <asp:Literal ID="ltFacultyID" runat="server" Visible="false" />
                                <label class="form-label">Mentor Name</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtStudio" runat="server" CssClass="form-control" MaxLength="100" required autofocus autocomplete="off" />
                                <label class="form-label">Studio Name</label>
                            </div>
                        </div>

                        <h2 class="card-inside-title">Mentor Profile</h2>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Teaching Topic Strengths</label>
                                <div class="checkbox">
                                    <asp:CheckBoxList ID="cbTopics_Teaching" runat="server" RepeatLayout="Table" RepeatColumns="2" required />
                                    <div class="form-group">
                                        <asp:CheckBox ID="cbTopics_Teaching_Other" runat="server" Text="Others:" />
                                        <asp:TextBox ID="txtTopics_Teaching_Other" runat="server" class="form-control" data-role="tagsinput" placeholder="Write something..." />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Research Topic Guidance Strengths</label>
                                <div class="checkbox">
                                    <asp:CheckBoxList ID="cbTopics_Research" runat="server" RepeatLayout="Table" RepeatColumns="2" required />
                                    <div class="form-group">
                                        <asp:CheckBox ID="cbTopics_Research_Others" runat="server" Text="Others:" />
                                        <asp:TextBox ID="txtTopics_Research_Others" runat="server" class="form-control" data-role="tagsinput" placeholder="Write something..." />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Directions:</label>
                            <div class="checkbox">
                                <asp:CheckBoxList ID="cbDirections" runat="server" RepeatLayout="Table" required />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Platform Affiliations:</label>
                            <div class="checkbox">
                                <asp:CheckBoxList ID="cbAffiliations" runat="server" RepeatLayout="Table" RepeatColumns="2" required />
                                <div class="form-group">
                                    <asp:TextBox ID="txtAffiliations_Others" runat="server" class="form-control" placeholder="Write something..." />
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label>Professional Statement</label>
                            <div class="form-line">
                                <asp:TextBox ID="txtStatement" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control no-resize" placeholder="A short, 5-7 sentence write-up of the your professional, or academic/research work profile, professional or academic practice, teaching strengths and studio pedagogical agenda." required />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="header">
                        <h2>Educational Background</h2>
                        <ul class="header-dropdown m-r--5">
                            <li class="dropdown">
                                <a href="#" data-toggle="modal" data-target="#addEducation">
                                    <i class="material-icons">add</i>
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div class="body">
                        <table class="table table-hover">
                            <thead>
                                <th>Institution</th>
                                <th>Degree</th>
                                <th>Date</th>
                                <th></th>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvEducation" runat="server" OnItemCommand="lvEducation_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="ltRecordID" runat="server" Text='<%# Eval("RecordID") %>' Visible="false" />
                                                <%# Eval("Institution") %></td>
                                            <td><%# Eval("Degree") %></td>
                                            <td><%# Eval("YearStart", "{0: MM/dd/yy}") %> - <%# Eval("YearEnd", "{0: MM/dd/yy}") %></td>
                                            <td>
                                                <asp:LinkButton ID="btnRemove" runat="server" CssClass="btn btn-danger btn-xs" CommandName="remove">
                                                    <i class="material-icons">delete</i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="5">
                                                <h3 class="text-center">No records found.</h3>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="card">
                    <div class="header">
                        <h2>Availability</h2>
                        <ul class="header-dropdown m-r--5">
                            <li class="dropdown">
                                <a href="#" data-toggle="modal" data-target="#addSchedule">
                                    <i class="material-icons">add</i>
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div class="body">
                        <table class="table table-hover">
                            <thead>
                                <th>Day</th>
                                <th>Start</th>
                                <th>End</th>
                                <th></th>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvSchedule" runat="server" OnItemCommand="lvSchedule_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="ltRecordID" runat="server" Text='<%# Eval("RecordID") %>' Visible="false" />
                                                <%# Eval("Day") %></td>
                                            <td><%# Eval("StartTime", "{0: hh:mm tt}") %></td>
                                            <td><%# Eval("EndTime", "{0: hh:mm tt}") %></td>
                                            <td>
                                                <asp:LinkButton ID="btnRemove" runat="server" CssClass="btn btn-danger btn-xs" CommandName="remove">
                                                    <i class="material-icons">delete</i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="5">
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
            <div class="col-lg-6">
                <div class="card">
                    <div class="body">
                        <h2 class="card-inside-title">Mentor Vitae</h2>
                        <div class="form-group">
                            <label>Mentor Resume <br />
                                <small>(A 300-word summary of the relevant experience to help students understand better how you can help with their proposals.)</small></label>
                            <div class="form-line">
                                <asp:TextBox ID="txtResume" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control no-resize" placeholder="A summary of the relevant experience to help students understand better how you can help their proposals." required />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>General Studio Agenda<br />
                                <small>(A 300-word summary of what you want to offer students to help them with their research and/or projects.)</small>
                            </label>
                            <div class="form-line">
                                <asp:TextBox ID="txtAgenda" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control no-resize" placeholder="What you want to offer students to help them with research and/or projects --- in bullet narratives, for easy reading." required />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Abridged Manifesto<br />
                                <small>(A set of declarations directed towards an intent, perceived worldviews, objectives, advocacies, ethos, and the like, that determine the way you practice architecture and produce architectural knowledge.)</small>
                            </label>
                            <div class="form-line">
                                <asp:TextBox ID="txtManifesto" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control no-resize" placeholder="A summary of the relevant experience to help students understand better how you can help their proposals." />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="body clearfix">
                        
                        <div class="form-group">
                            <label>Additional Notes</label>
                            <div class="form-line">
                                <asp:TextBox ID="txtOthers" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control no-resize" placeholder="Write something here..." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-lg btn-success waves-effect pull-right" Text="Update" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="addEducation" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Add Educational Background</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtInstitution" runat="server" CssClass="form-control" MaxLength="100" required autofocus autocomplete="off" />
                                    <label class="form-label">Institution</label>
                                </div>
                            </div>
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtDegree" runat="server" CssClass="form-control" MaxLength="100" required autofocus autocomplete="off" />
                                    <label class="form-label">Degree</label>
                                </div>
                            </div>
                            Year Start - End
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtStart" runat="server" CssClass="form-control" type="date" required autofocus autocomplete="off" />
                                    <asp:TextBox ID="txtEnd" runat="server" CssClass="form-control" type="date" required autofocus autocomplete="off" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-link waves-effect" Text="ADD RECORD" OnClick="btnAdd_Click" />
                            <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="addSchedule" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Add Schedule</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtDay" runat="server" CssClass="form-control" MaxLength="100" required autofocus autocomplete="off" />
                                    <label class="form-label">Day/s</label>
                                </div>
                            </div>
                            Start & End Time
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtStart_Time" runat="server" CssClass="form-control" type="time" required autofocus autocomplete="off" />
                                    <asp:TextBox ID="txtEnd_Time" runat="server" CssClass="form-control" type="time" required autofocus autocomplete="off" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAddSched" runat="server" CssClass="btn btn-link waves-effect" Text="ADD RECORD" OnClick="btnAddSched_Click" />
                            <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script>
        $(function () {
            //CKEditor
            CKEDITOR.replace('content_txtResume');
            CKEDITOR.config.height = 300;
            CKEDITOR.config.toolbar = [
                ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent'],
                ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ];
        });

        $(function () {
            //CKEditor
            CKEDITOR.replace('content_txtAgenda');
            CKEDITOR.config.height = 300;
            CKEDITOR.config.toolbar = [
                ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent'],
                ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ];
        });

        $(function () {
            //CKEditor
            CKEDITOR.replace('content_txtManifesto');
            CKEDITOR.config.height = 150;
            CKEDITOR.config.toolbar = [
                ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent'],
                ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ];
        });
    </script>
</asp:Content>


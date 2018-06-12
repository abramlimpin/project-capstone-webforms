<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Advising.aspx.cs" Inherits="Account_Advising" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    ADVISER PROFILE
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
                                <asp:TextBox ID="txtTeaching" runat="server" CssClass="form-control" MaxLength="100" required autocomplete="off" />
                                <label class="form-label">Teaching Topic Strengths</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtResearch" runat="server" CssClass="form-control" MaxLength="100" required autocomplete="off" />
                                <label class="form-label">Research Topic Guidance Strengths</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Direction and Platform Affiliations:</label>
                            <div class="checkbox">
                                <asp:CheckBoxList ID="cbAffiliations" runat="server" RepeatLayout="Table" RepeatColumns="2" required />
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
                    <div class="body">
                        <h2 class="card-inside-title">Availability</h2>
                        <div class="form-group">
                            <div class="form-line">
                                <asp:TextBox ID="txtAvailablity" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control no-resize" placeholder="Input your availability schedule..." required />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="card">
                    <div class="body">
                        <h2 class="card-inside-title">Mentor Vitae</h2>
                        <div class="form-group">
                            <label>Mentor Resume</label>
                            <div class="form-line">
                                <asp:TextBox ID="txtResume" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control no-resize" placeholder="A summary of the relevant experience to help students understand better how you can help their proposals." required />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>General Studio Agenda</label>
                            <div class="form-line">
                                <asp:TextBox ID="txtAgenda" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control no-resize" placeholder="What you want to offer students to help them with research and/or projects --- in bullet narratives, for easy reading." required />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="body clearfix">
                        <h2 class="card-inside-title">Optional</h2>
                        <div class="form-group">
                            <label>Abridged Manifesto</label>
                            <div class="form-line">
                                <asp:TextBox ID="txtManifesto" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control no-resize" placeholder="A summary of the relevant experience to help students understand better how you can help their proposals." />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Other Statements</label>
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
        </form>
    </div>
</asp:Content>


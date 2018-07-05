<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Advisers.aspx.cs" Inherits="Directory_Advisers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Directory - Advisers
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-3">
                <div class="card">
                    <div class="body">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Teaching Topic Strengths</label>
                                <div class="checkbox">
                                    <asp:CheckBoxList ID="cbTopics_Teaching" runat="server" RepeatLayout="Table" required />
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Research Topic Guidance Strengths</label>
                                <div class="checkbox">
                                    <asp:CheckBoxList ID="cbTopics_Research" runat="server" RepeatLayout="Table" required />
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Direction and Platform Affiliations</label>
                                <div class="checkbox">
                                    <asp:CheckBoxList ID="cbAffiliations" runat="server" RepeatLayout="Table" required />
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-lg btn-block" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-lg-9">
                <asp:ListView ID="lvAdvisers" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-4 co-xs-12">
                            <div class="thumbnail">
                                <a runat="server" href='<%# string.Concat("~/adviser?u=", Eval("AccountNo")) %>' target="_blank">
                                    <img runat="server" src='<%# Eval("Image").ToString() == "" ? "~/images/user-placeholder.jpg" : string.Concat("~/image/users/", Eval("Image")) %>' /></a>
                                <div class="caption">
                                    <h3 class="text-center"><%# Eval("Name") %></h3>
                                    <hr />
                                    <a href='<%# string.Concat("~/adviser?u=", Eval("AccountNo")) %>' target="_blank" class="btn btn-info btn-lg waves-effect">View Profile</a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>

        </form>
    </div>
</asp:Content>


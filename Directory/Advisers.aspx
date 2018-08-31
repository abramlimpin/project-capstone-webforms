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
                                <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control" placeholder="Keyword..." />
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Mentor Teaching Topics</label>
                                <div class="checkbox">
                                    <asp:CheckBoxList ID="cbTopics_Teaching" runat="server" RepeatLayout="Table" required />
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Mentor Research Topics</label>
                                <div class="checkbox">
                                    <asp:CheckBoxList ID="cbTopics_Research" runat="server" RepeatLayout="Table" required />
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Directions</label>
                                <div class="checkbox">
                                    <asp:CheckBoxList ID="cbDirections" runat="server" RepeatLayout="Table" required />
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Platform Affiliations</label>
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
                <asp:ListView ID="lvAdvisers" runat="server" OnItemDataBound="lvAdvisers_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-lg-3 co-xs-12">
                            <div class="thumbnail">
                                <a runat="server" href='<%# string.Concat("~/adviser?u=", Eval("AccountNo")) %>' target="_blank">
                                    <div id="ratio" class="ratio" runat="server">
                                    </div>
                                    <asp:Literal ID="ltImage" runat="server" Visible="false" Text='<%# Eval("Image") %>' />
                                </a>
                                <div class="caption">
                                    <h3 class="text-center"><%# Eval("Name") %></h3>
                                    <hr />
                                    <a runat="server" href='<%# string.Concat("~/adviser?u=", Eval("AccountNo")) %>' target="_blank" class="btn btn-info btn-block btn-lg waves-effect">View Profile</a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>

        </form>
    </div>
</asp:Content>


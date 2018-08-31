<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Account_Porfolio_Add" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Add a Portfolio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="row clearfix">
        <form runat="server" class="form-validate">
            <div class="col-lg-9">
                <div class="card clearfix">
                    <div class="body">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="100" required />
                                <label class="form-label">Title</label>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" MaxLength="500" required />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-line">
                                <label class="form-label">Keywords</label>
                                <asp:TextBox ID="txtKeywords" runat="server" data-role="tagsinput" CssClass="form-control" MaxLength="100" />
                            </div>
                        </div>

                    </div>
                </div>
                <div class="card col-lg-6">
                    <div class="body">
                        <h4>Specifications</h4>
                        <table class="table">
                            <tr>
                                <td>Typology</td>
                                <td>
                                    <asp:TextBox ID="txtTypology" runat="server" CssClass="form-control" /></td>
                            </tr>
                            <tr>
                                <td>Function</td>
                                <td>
                                    <asp:TextBox ID="txtFunction" runat="server" CssClass="form-control" /></td>
                            </tr>
                            <tr>
                                <td>Location</td>
                                <td>
                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCustom1" runat="server" CssClass="form-control" Text="Custom 1" /></td>
                                <td>
                                    <asp:TextBox ID="txtCustom1_Value" runat="server" CssClass="form-control" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCustom2" runat="server" CssClass="form-control" Text="Custom 2" /></td>
                                <td>
                                    <asp:TextBox ID="txtCustom2_Value" runat="server" CssClass="form-control" /></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="card">
                    <div class="body">
                        <div class="form-group form-float">
                            <label>Status</label>
                            <div class="switch">
                                <label>Draft<asp:CheckBox ID="cbStatus" runat="server" /><span class="lever switch-col-red"></span>Published</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Featured Image</label><br />
                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                <div class="fileinput-new thumbnail" style="width: 200px; height: 200px;">
                                    <asp:Image ID="imgUser" runat="server" />
                                </div>
                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 200px; max-height: 150px;"></div>
                                <div>
                                    <span class="btn btn-info btn-file"><span class="fileinput-new">Select image</span><span class="fileinput-exists">Change</span>
                                        <asp:FileUpload ID="fuImage" runat="server" accept="image/x-png,image/jpeg" required />
                                    </span>
                                    <a href="#" class="btn btn-danger fileinput-exists" data-dismiss="fileinput">Remove</a>
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Date Start</label>
                                <asp:TextBox ID="txtStart" runat="server" CssClass="form-control" type="date" required />
                            </div>
                        </div>
                        <div class="form-group form-float">
                            <div class="form-line">
                                <label>Date End</label>
                                <asp:TextBox ID="txtEnd" runat="server" CssClass="form-control" type="date" required />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-lg btn-success btn-block waves-effect" Text="Submit" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script>
        $(function () {
            //CKEditor
            CKEDITOR.replace('content_txtDesc');
            CKEDITOR.config.height = 300;

        });
    </script>
</asp:Content>


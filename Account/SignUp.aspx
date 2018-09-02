<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="Account_SignUp" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Sign Up | Project Capstone</title>
    <!-- Favicon-->
    <link rel="icon" href="../favicon.png">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css">

    <!-- Bootstrap Core Css -->
    <link href="../plugins/bootstrap/css/bootstrap.css" rel="stylesheet">

    <!-- Waves Effect Css -->
    <link href="../plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="../plugins/animate-css/animate.css" rel="stylesheet" />

    <!-- Custom Css -->
    <link href="../css/style.css" rel="stylesheet">
</head>

<body class="signup-page">
    <div class="signup-box">
        <div class="logo">
            <a href="javascript:void(0);">Project<b>Capstone</b></a>
            <small>For SDA-ARCH Program</small>
        </div>
        <div class="card">
            <div class="body">
                <form id="sign_up" runat="server">
                    <asp:ScriptManager runat="server" />
                    <div class="msg">Register a new membership</div>
                    <asp:UpdatePanel ID="upSignUp" runat="server">
                        <ContentTemplate>
                            <div id="error" runat="server" class="alert alert-danger" visible="false">
                                Account Name already existing.
                            </div>
                            <div id="email" runat="server" class="alert alert-danger" visible="false">
                                Incorrect email format.
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="material-icons">person</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtUsername" runat="server" type="number" MaxLength="8" CssClass="form-control" placeholder="InfoNet Account" required autofocus />
                                </div>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="material-icons">person</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtFN" runat="server" CssClass="form-control" placeholder="First Name" required />
                                </div>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="material-icons">person</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtLN" runat="server" CssClass="form-control" placeholder="Last Name" required />
                                </div>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="material-icons">email</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Benilde Email Address" pattern="^[A-Za-z0-9._%+-]+@benilde.edu.ph$" MaxLength="100" required />
                                </div>
                            </div>
                            <div class="form-group">
                                <input type="checkbox" name="terms" id="terms" class="filled-in chk-col-pink">
                                <label for="terms">I read and agree to the <a href="javascript:void(0);">terms of usage</a>.</label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnSignUp" runat="server" CssClass="btn btn-block bg-pink waves-effect"
                        Text="Sign Up" OnClick="btnSignUp_Click" />

                    <div class="m-t-25 m-b--5 align-center">
                        <a href="SignIn">Already have a membership?</a>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Jquery Core Js -->
    <script src="../plugins/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core Js -->
    <script src="../plugins/bootstrap/js/bootstrap.js"></script>

    <!-- Waves Effect Plugin Js -->
    <script src="../plugins/node-waves/waves.js"></script>

    <!-- Validation Plugin Js -->
    <script src="../plugins/jquery-validation/jquery.validate.js"></script>

    <!-- Custom Js -->
    <script src="../js/admin.js"></script>
    <script src="../js/pages/examples/sign-up.js"></script>
</body>

</html>

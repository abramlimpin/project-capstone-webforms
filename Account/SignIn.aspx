<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignIn.aspx.cs" Inherits="Account_SignIn" %>

<!DOCTYPE html>
<html>

<head runat="server">
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Sign In | Project Capstone</title>
    <!-- Favicon-->
    <link rel="icon" href="~/favicon.png">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css">

    <!-- Bootstrap Core Css -->
    <link href="~/plugins/bootstrap/css/bootstrap.css" rel="stylesheet">

    <!-- Waves Effect Css -->
    <link href="~/plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="~/plugins/animate-css/animate.css" rel="stylesheet" />

    <!-- Custom Css -->
    <link href="~/css/style.css" rel="stylesheet">
</head>

<body class="login-page">
    <div class="modal fade" id="forgotPassword" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Instructions</h4>
                </div>
                <div class="modal-body">
                    Contact Information Technology Department (ITD) for password reset.
                </div>
            </div>
        </div>
    </div>
    <div class="login-box">
        <div class="logo">
            <a href="javascript:void(0);">Project<b>Capstone</b></a>
            <small>For SDA-ARCH Program</small>
        </div>
        <div class="card">
            <div class="body">
                <form id="sign_in" runat="server">
                    <div class="msg">Sign in to start your session</div>
                    <div id="error" runat="server" class="alert alert-danger" visible="false">
                        Invalid credentials.
                    </div>
                    <div id="signout" runat="server" class="alert alert-info" visible="false">
                        Signed out successfully.
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">person</i>
                        </span>
                        <div class="form-line">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="InfoNet Account" required autofocus autocomplete="off" />
                        </div>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">lock</i>
                        </span>
                        <div class="form-line">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" type="password" placeholder="Password" required autocomplete="off" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-8 p-t-5">
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnSignIn" runat="server" CssClass="btn btn-block bg-pink waves-effect"
                                Text="Sign In" OnClick="btnSignIn_Click" />
                        </div>
                    </div>
                    <div class="row m-t-15 m-b--20">
                        <div class="col-xs-6">
                            <a href="SignUp">Register Now</a>
                        </div>
                        <div class="col-xs-6 align-right">
                            <a href="#" data-toggle="modal" data-target="#forgotPassword">Forgot Password?</a>
                        </div>
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
    <script src="../js/pages/examples/sign-in.js"></script>
</body>
</html>

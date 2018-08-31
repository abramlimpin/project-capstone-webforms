<%@ Page Language="C#" AutoEventWireup="true" CodeFile="runquery.aspx.cs" Inherits="runquery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Rows="10" Width="400" required />
        <br />
        <asp:Button ID="btnRun" runat="server" Text="Run" OnClick="btnRun_Click" OnClientClick='return confirm("Are you sure?");' />
        <br />
        <asp:GridView ID="gvResults" runat="server" />
    </div>
    </form>
</body>
</html>

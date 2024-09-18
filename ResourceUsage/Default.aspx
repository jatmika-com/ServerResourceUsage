<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResourceUsage.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table {
            border-collapse: collapse;
            border: 1px solid black;
        }
        table td, table th {
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family: sans-serif">
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <th>Parameter</th>
                        <th>Value</th>
                    </tr>
                    <tr>
                        <td style="font-weight:bold">CPU Usage</td>
                        <td style="text-align: right"><asp:Label ID="lblCPU" runat="server" /></td>
                    </tr>                    
                    <tr>
                        <td style="font-weight:bold">Memory Usage</td>
                        <td style="text-align: right"><asp:Label ID="lblRamUsage" runat="server" /></td>
                    </tr>                    
                    <tr>
                        <td style="font-weight:bold">Memory Available / Total</td>
                        <td style="text-align: right"><asp:Label ID="lblRam" runat="server" /></td>
                    </tr>
                </table>
                <br />
                <asp:Timer runat="server" OnTick="OnTick" Interval="1000"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

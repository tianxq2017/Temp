<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelList.aspx.cs" Inherits="join.pms.web.SysAdmin.TelList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�ޱ���ҳ</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <!--������Ϣ-->
        <div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
        <!--������Ϣ-->
        <div id="ec_bodyZone" class="part_bg2">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="small">
                <tr>
                    <td width="100" class="fb01" bgcolor="#F4FAFA">&nbsp;�����ţ�</td>
                    <td width="145" align="left" bgcolor="#F4FAFA">
                        <input id="txtBM" type="text" name="txtBM" size="20" runat="server" />
                    </td>
                    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
                    <td width="100" class="fb01" bgcolor="#F4FAFA">&nbsp;���û�����</td>
                    <td width="150" align="left" bgcolor="#F4FAFA">
                        <input id="txtYH" type="text" name="txtYH"  size="20" runat="server" />
                    </td>
                    <td width="20" bgcolor="#F4FAFA">&nbsp;</td>
                    <td align="left" bgcolor="#F4FAFA"><asp:Button ID="btnAdd" runat="server" Text="�� ��ѯ ��" class="cusersubmit" OnClick="btnAdd_Click"></asp:Button></td>
                </tr>
            </table>
            <br/>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td height="530" valign="top" class="zhengwen">
                                    <table width="95%" border="0" cellspacing="0" cellpadding="0" class="zhengwen">
                                        <tr>
                                            <td >
                                                <asp:Literal ID="LiteralResults" runat="server" EnableViewState="false"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
         </div>
    </form>
</body>
</html>

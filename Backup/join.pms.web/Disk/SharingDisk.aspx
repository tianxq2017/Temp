<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SharingDisk.aspx.cs" Inherits="join.pms.web.Disk.SharingDisk" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../includes/commOA.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
<!-- ҳ�浼�� -->
<asp:Literal ID="LiteralNav" runat="server" ></asp:Literal>
<!-- ҳ����� -->
<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
<br/>
<!-- ������ start -->
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
  <td width="8" height="8"><img src="../images/main_BK_LT.gif" width="8" height="8" /></td>
  <td background="../images/main_BK_T.gif"><img src="../images/main_BK_T.gif" width="2" height="8" /></td>
  <td width="8" height="8"><img src="../images/main_BK_RT.gif" width="8" height="8" /></td>
</tr>
<tr>
  <td background="../images/main_BK_L.gif"><img src="../images/main_BK_L.gif" width="8" height="2" /></td>
  <td align="center" class="pad5">
   <!-- ����Ϣ Start -->
    <table cellspacing="0" cellpadding="0" width="95%" border="0" align="center" class="marL15">
    <tr>
    <td style="height: 30px; width: 300px;">&nbsp;<input id="UploadFiles" contenteditable="false" name="UploadFiles" style="width: 287px; height: 26px;"
                        type="file" /></td>
    <td width="*" align="left" style="height: 30px">
     &nbsp;<asp:Button ID="ButUploadFile" runat="server" Height="26px" OnClick="ButUploadFile_Click"
                            OnLoad="ButUploadFile_Load" Text=" �ϴ� " Width="59px" />
        &nbsp;&nbsp;<asp:Literal ID="LiteralMsg" runat="server" ></asp:Literal></td>
    </tr>
    </table>	
    <!-- ����Ϣ End -->
  </td>
  <td background="../images/main_BK_R.gif"><img src="../images/main_BK_R.gif" width="8" height="2" /></td>
</tr>
<tr>
  <td height="8"><img src="../images/main_BK_LB.gif" width="8" height="8" /></td>
  <td background="../images/main_BK_B.gif"><img src="../images/main_BK_B.gif" width="2" height="8" /></td>
  <td><img src="../images/main_BK_RB.gif" width="8" height="8" /></td>
</tr>
</table>

<!-- ������ end -->
    </form>
</body>
</html>

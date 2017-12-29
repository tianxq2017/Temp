<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassMain.aspx.cs" Inherits="join.pms.web.SysAdmin.ClassMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>分类</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
<link href="../includes/dataGrid.css" rel="stylesheet" type="text/css" />
<link href="../includes/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<!-- 页面导航 -->
<table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
    <tr>
      <td width="3" style="height: 30px"><img src="../images/sysAdmin/R_l.gif" width="3" height="30" /></td>
      <td align="left" background="../images/sysAdmin/R_C.gif" style="height: 30px"> <img src="../images/sysAdmin/97.gif" align="absmiddle" align="absbottom" /> <span class="hz">您的位置:</span><asp:Label ID="labTitle" runat="server" EnableViewState="false"></asp:Label></td>
      <td width="3" style="height: 30px"><img src="../images/sysAdmin/R_R.gif" width="3" height="30" /></td>
    </tr>
  </table>

<!-- 页面功能区 -->
<table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr><td valign="top">
    <table width="800" border="0" cellpadding="0" cellspacing="0" class="tablemainbian">
      <tr>
        <td valign="top" bgcolor="#FFFFFF">
          <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" >
            <tr>
              <td width="786" height="20">请选择相应的首字母索引分类，添加项目：   </td>
              </tr>
            <tr>
              <td height="1" bgcolor="#BBD3EA"></td>
            </tr>
            <tr>
              <td style="padding-left:1px;">
                <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" style="padding-top:10px;">
                <tr>
                  <td width="200" height="550" valign="top" class="tdBorderLine">
				  <!-- 树菜单 -->
				  <iframe id="treeLeft"  name="treeLeft" src="inc_ClassTree.aspx?FuncCode=<%=m_FuncCode%>" frameBorder="0" scrolling="auto" height="100%" width="100%"></iframe>
				  </td>
                  <td width="10"></td>
                  <td height="550" valign="top">
				  <!-- 菜单编辑区 -->
				  <iframe id="treeEdit" name="treeEdit" src="about:blank" frameborder="0" scrolling="auto" height="100%" width="100%"></iframe>
				  </td>
                </tr>
              </table>
                <br/></td>
            </tr>
            <tr>
              <td height="1" bgcolor="#f0f0f0"></td>
            </tr>
          </table>
          </td>
      </tr>
    </table>
</td></tr></table>
<!-- 页脚信息 -->
</body>
</html>

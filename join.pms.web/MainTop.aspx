<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainTop.aspx.cs" Inherits="join.pms.web.MainTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>��������</title>
<link rel="Stylesheet" href="css/main.css"/>
<link href="css/top.css" type="text/css" rel="stylesheet" />
        <script language="javascript" type="text/javascript">
            function SetColorToMenu(objID) {
                SetColorToNull();
                document.getElementById(objID).className = "on";
            }
            function SetColorToNull() {
                var menuObj;
                for (i = 1; i < 9; i++) {
                    menuObj = document.getElementById("Menu0" + i);
                    if (menuObj != null) menuObj.className = "";
                }
            }
</script>
    <style type="text/css">
        .banner span
        {
            position:relative;
            top:-140px;
            left:25%;
            text-align:center;
            font-size:56px;
            color:Orange;
    </style>
</head>
<body>
<form id="form1" runat="server">
 <div>
    <div class="banner">
      <div style=" height:200px">
          <img src="images/bann.jpg" alt="" />
          <span>���ط�ƶ�ʽ�ȫ�̼��Ԥ������ϵͳ</span>
         </div>
    </div>
  <div class="header_01 clearfix">
       <div class="tab">
        <ul class="clearfix">    
            <li id="Li1" onclick="javascript:SetColorToMenu('Menu02');"><a href="MainLeft.aspx?c=00" target="leftFrame" >�ճ�����</a></li>
            <li id="Li2" onclick="javascript:SetColorToMenu('Menu05');"><a href="MainLeft.aspx?c=05" target="leftFrame" >��������</a></li>
            <li id="Li3" onclick="javascript:SetColorToMenu('Menu04');"><a href="MainLeft.aspx?c=04" target="leftFrame" >������Ϣ</a></li>
            <li id="Li4" onclick="javascript:SetColorToMenu('Menu06');"><a href="MainLeft.aspx?c=06" target="leftFrame" >ϵͳ����</a></li>
        </ul>
        </div>
      <div class="hea">
          <ul class="nav">
            <asp:Literal ID="LiteralUserInfo" runat="server" EnableViewState="false"></asp:Literal>
            <li><a href="MainDesk.aspx" target="mainFrame" onmouseover="self.status='��������' ;return true;">��������<img src="images/dian.png" alt=""/></a></li>
            <li><a href="MainTop.aspx?action=Logout" title="��ȫ�˳�">��ȫ�˳�<img src="images/tuichu.png" alt=""/></a></li>
          </ul>
        </div>
  </div>
 </div>
</form>
</body>
</html>


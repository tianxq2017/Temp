<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MainFrame.aspx.cs" Inherits="join.pms.web.MainFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>勉县扶贫资金全程监测预警管理系统</title>
        <script language="javascript" type="text/javascript">
        self.moveTo(0,0);
        self.resizeTo(screen.availwidth,screen.availheight);
        self.focus();
        // 兼容IE8
        resizeTo(screen.availWidth+8,screen.availHeight+8)
        //document.all.MaxWindow.Click();
        </script>
<frameset rows="240,*" frameborder="no" border="0" framespacing="0">
  <frame src="/MainTop.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
    <frameset cols="400,*" frameborder="no" border="0" framespacing="0" id="treeFrame">
	  <frame src="/MainLeft.aspx?c=00" name="leftFrame" scrolling="Auto" noresize id="leftFrame" title="leftFrame">
	    <frameset rows="*" cols="500,0" framespacing="0" frameborder="NO" border="0" id="treeFrame_r">
	        <frame src="/MainDesk.aspx" name="mainFrame" id="mainFrame" title="mainFrame" />
	    </frameset>
    </frameset>
</frameset>
<noframes>
  <body></body>
</noframes>
</html>

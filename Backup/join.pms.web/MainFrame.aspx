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
function window.onbeforeunload(){
    if(event.clientX>document.body.clientWidth&&event.clientY<0||event.altKey)
    {
        window.event.returnvalue = "";
    }
}
    </script>

<frameset rows="50,*" frameborder="no" border="0" framespacing="0">
  <frame src="/MainTop.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
    <frameset cols="242,*" frameborder="no" border="0" framespacing="0" id="treeFrame">
	  <frame src="/MainLeft.aspx?c=00" name="leftFrame" scrolling="Auto" noresize id="leftFrame" title="leftFrame">
	    <frameset rows="*" cols="*,269" framespacing="0" frameborder="NO" border="0" id="treeFrame_r">
	      <frameset rows="19,*" frameborder="no" border="0" framespacing="0">
	        <frame src="/MainTop2.aspx" name="topFrame2" scrolling="No" noresize="noresize" id="topFrame2" title="topFrame2" />
	        <frame src="/MainDesk.aspx" name="mainFrame" id="mainFrame" title="mainFrame" />
	      </frameset>
	      <frame src="/MainRight.aspx" name="rightFrame" scrolling="NO" noresize id="rightFrame" title="rightFrame">
	    </frameset>
    </frameset>
</frameset>
<noframes>
  <body></body>
</noframes>



    
    <!--
<frameset rows="80,*" frameborder="no" border="0" framespacing="0">
  <frame src="t" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
  <frameset cols="80,*" frameborder="no" border="0" framespacing="0">
		<frame src="l" name="leftFrame" scrolling="No" noresize="noresize" id="leftFrame" title="leftFrame" />
		<frameset cols="*,80" frameborder="no" border="0" framespacing="0">
		<frameset rows="80,*" frameborder="no" border="0" framespacing="0">
		  <frame src="tt" name="topFrame1" scrolling="No" noresize="noresize" id="topFrame1" title="topFrame1" />
		<frame src="m" name="mainFrame" id="mainFrame" title="mainFrame" />
	</frameset>
		<frame src="r" name="rightFrame" scrolling="No" noresize="noresize" id="rightFrame" title="rightFrame" />
	</frameset>
  </frameset>
</frameset>
<noframes><body>
</body>
</noframes>        
    
    -----old--------
       <frameset rows="100,*" frameborder="no" border="0" framespacing="0">
  <frame src="/MainTop.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
  <frameset cols="217,*" frameborder="no" border="0" framespacing="0" id="treeFrame">
		<frame src="/MainLeft.aspx" name="leftFrame" scrolling="Auto" noresize="noresize" id="leftFrame" title="leftFrame" />
		<frameset rows="*,35" cols="*" frameborder="no" border="0" framespacing="0">
		 <frame src="/MainDesk.aspx" name="mainFrame" id="mainFrame" title="mainFrame" />
	    <frame src="/MainBot.aspx" name="bottomFrame" scrolling="No" noresize="noresize" id="bottomFrame" title="bottomFrame" />
		</frameset>
  </frameset>
</frameset>
    <noframes>
        <body>
        </body>
    </noframes>
    -->
</html>

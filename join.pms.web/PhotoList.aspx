<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoList.aspx.cs" Inherits="join.pms.web.PhotoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>图片列表</title>
    <link href="/css/right.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/includes/datagrid.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 页面参数 -->
    <input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
    <input type="hidden" name="selectRowPara" id="selectRowPara" value="" style="display:none;"/>
    <input type="hidden" name="selectRowID" id="selectRowID" value=""style="display:none;" />
    
    <!-- 数据显示 -->
    <asp:Literal ID="LiteralDataList" runat="server" EnableViewState="False" />


<div class="photo_list" style="display:none;">
  <ul>
    <!--开始 -->
	<li>
	  <p class="pic"><a href="#"><span class="pic_center"><i></i><img src="http://192.168.1.200:8061/Files/2015/06/201506030947183400.jpg" alt="标题" /></span></a></p>
	  <p class="title"><a href="#">本人《居民身份证》或户口簿</a></p>
	  <p class="name"><span>2015-06-03</span><b>李丽丽</b></p>
	</li>
	<!--结束 -->
  </ul>
</div>

    </form>
</body>
</html>

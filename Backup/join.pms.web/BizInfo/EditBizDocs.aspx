<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBizDocs.aspx.cs" Inherits="join.pms.web.BizInfo.EditBizDocs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�ϴ�֤��</title>
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/Scripts/CommUpload.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/checkDocs.js" type="text/javascript"></script>
</head>
<body>
   <form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д������Ϣ�󣻵�����ύ����ť����</div>
<!-- ҳ����� -->
<input type="hidden" name="txtAreaCode" id="txtAreaCode" runat="server" style="display:none;"/>
<input type="hidden" name="txtRegAreaCodeA" id="txtRegAreaCodeA" runat="server" style="display:none;"/>
<input type="hidden" name="txtRegAreaCodeB" id="txtRegAreaCodeB" runat="server" style="display:none;"/>
<input type="hidden" name="txtIsInnerArea" id="txtIsInnerArea" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizCNum" id="txtBizCNum" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizGNum" id="txtBizGNum" runat="server" style="display:none;"/>

<input type="hidden" name="txtAttribs" id="txtAttribs" value="" runat="server" style="display:none;"/>

<input type="hidden" name="txtBizDocsIDiOld" id="txtBizDocsIDiOld" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizDocsIDOld" id="txtBizDocsIDOld" runat="server" style="display:none;"/>


<input type="hidden" name="txtBizDocsIDIsOld" id="txtBizDocsIDIsOld" runat="server" style="display:none;"/>

<input type="hidden" name="txtPersonID" id="txtPersonID" runat="server" style="display:none;"/>

<input type="hidden" name="txtAction" id="txtAction" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
<div class="column_00"> 
  <div class="column_c">
	<div class="apply_form">
	  <p class="column_title_b">�ύ����֤�����Ӱ�</p>
	  <div class="form_bg">
	    <div class="form_a form_b">
	      <p class="form_title"><b>������ʾ��</b><span>����֤�����ϰ�������ԭ����ԭ��ɨ��ĵ��Ӽ�,ԭ������ʱ���ֳ��ύ,ԭ��ɨ��ĵ��Ӽ�������ʱͨ�������ϴ�(�ɼ������İ���ʱ��),Ҳ���ύԭ��ʱ�ɼ������Ŵ�Ϊ�ϴ����������ͼƬ��ʽ��jpg��gif��bmp��png����</span></p>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <asp:Literal ID="LiteralBizCategoryLicense" runat="server" EnableViewState="false"></asp:Literal>
</table>
	    </div>
	  </div>	  
	  </div>
  </div>
</div>

<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��"   OnClientClick="return EditCheck();" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
<link rel="stylesheet" href="/scripts/lightbox/lightbox.css" type="text/css" media="screen" />
<script src="/scripts/lightbox/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="/scripts/lightbox/lightbox.js" type="text/javascript"></script>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaSelect.aspx.cs" Inherits="join.pms.web.BizInfo.AreaSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ѡ����������</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/Scripts/SearchData.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">

<%if (m_BizCode == "0101")%>
    <%{%>
ͨ���������������û�����������Ϊ��������ˣ�����ѭ����ԭ��ѡ�������ַ�������ɴ���ɵǼǲ���ʱ�ɱ��˳е���<br />
һ����˫������Ϊ����ͬ��ͬ��ģ������ѡ�񻧼��ء�<br />
������˫������Ϊ����ͬ����塢�������ģ����־�ס���ڷ�һ���Ļ����ؾ�ס�������ѡ���־�ס�أ����־�ס�ؾ����ڷ�˫�������أ������ѡ��Ů��������ַ��<br />
������˫������һ��Ϊ���ڻ����ģ������Ϊ���ڻ�����һ����<br />
�ġ���˫����Ϊ���⻧���ģ������Ϊ�־�ס�ء�
    <%}%>
    <%else if (m_BizCode == "0102")%>
    <%{%>
ͨ���������������û�����������Ϊ��������ˣ�����ѭ����ԭ��ѡ�������ַ�������ɴ������������֤��ȡ����ʱ�ɱ��˳е���<br />
һ����˫������Ϊ����ͬ��ͬ��ģ������ѡ�񻧼��ء�<br />
������˫������Ϊ����ͬ����塢�������ģ����־�ס���ڷ�һ���Ļ����ؾ�ס�������ѡ���־�ס�أ����־�ס�ؾ����ڷ�˫�������أ������ѡ��Ů��������ַ��<br />
������˫������һ��Ϊ���⻧���ģ������Ϊ���ڻ�����һ����
    <%}%>
    <%else if (m_BizCode == "0105")%>
    <%{%>
ͨ���������������û�����������Ϊ��������ˣ�����ѭ����ԭ��ѡ�������ַ�������ɴ������˲���ʱ�ɱ��˳е���<br />
һ����˫������Ϊ����ͬ��ͬ��ģ������ѡ�񻧼��ء�<br />
������˫������Ϊ����ͬ����塢�������ģ����־�ס���ڷ�һ���Ļ����ؾ�ס�������ѡ���־�ס�أ����־�ס�ؾ����ڷ�˫�������أ������ѡ��Ů��������ַ��<br />
������һ��Ϊ���⻧���ģ������Ϊ���ڻ�����һ����
    <%}%>
    <%else if (m_BizCode == "0107")%>
    <%{%>
ͨ���������������û�����������Ϊ��������ˣ������ַѡ��Ů���־�ס�ء������ɴ������˲���ʱ�ɱ��˳е���
    <%}%>
    <%else if (m_BizCode == "0108")%>
    <%{%>
ͨ���������������û�����������Ϊ��������ˣ�����ѭ����ԭ��ѡ�������ַ�������ɴ������˲���ʱ�ɱ��˳е���<br />
һ����˫������Ϊ����ͬ��ͬ��ģ������ѡ�񻧼��ء�<br />
������˫������Ϊ����ͬ����塢�������ģ����־�ס���ڷ�һ���Ļ����ؾ�ס�������ѡ���־�ס�أ����־�ס�ؾ����ڷ�˫�������أ������ѡ��Ů��������ַ��<br />
������˫������һ��Ϊ���ڻ����ģ������Ϊ���ڻ�����һ����
    <%}%>
    <%else if (m_BizCode == "0122")%>
    <%{%>
ͨ���������������û�����������Ϊ��������ˣ�����ѭ����ԭ��ѡ�������ַ�������ɴ������������֤��ȡ����ʱ�ɱ��˳е���<br />
һ����˫������Ϊ����ͬ��ͬ��ģ������ѡ�񻧼��ء�<br />
������˫������Ϊ����ͬ����塢�������ģ����־�ס���ڷ�һ���Ļ����ؾ�ס�������ѡ���־�ס�أ����־�ס�ؾ����ڷ�˫�������أ������ѡ��Ů��������ַ��<br />
������˫������һ��Ϊ���⻧���ģ������Ϊ���ڻ�����һ����
    <%}%>
    <%else%>
    <%{%>
ͨ���������������û�����������Ϊ��������ˣ������ѡ�������˻����ء������ɴ������˲���ʱ�ɱ��˳е���
    <%}%>

</div>

<!-- ҳ����� -->
<input type="hidden" name="txtCurrentClass" id="txtCurrentClass" runat="server" style="display:none;"/>
<input type="hidden" name="txtW" id="txtW" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeI" id="txtSelCodeI" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextI" id="txtSelTextI" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeII" id="txtSelCodeII" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextII" id="txtSelTextII" runat="server" style="display:none;" value="���ɹ������� &gt; ͨ����"/>

<input type="hidden" name="txtSelCodeIII" id="txtSelCodeIII" runat="server" style="display:none;" />
<input type="hidden" name="txtSelTextIII" id="txtSelTextIII" runat="server" style="display:none;" />

<input type="hidden" name="txtSelCodeIV" id="txtSelCodeIV" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextIV" id="txtSelTextIV" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeV" id="txtSelCodeV" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextV" id="txtSelTextV" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelectArea" id="txtSelectArea" runat="server" style="display:none;" value="����������"/>

<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>

<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
<div class="column_00">  
  <div class="column_c">
	<div class="area_list">
	  <div class="area_list_t">
	  <div style=" width:590px;">
	    <div class="area clearfix">
		  <ul>
		   <li>
		    <asp:Literal ID="LiteralIII" runat="server"></asp:Literal>
			</li>
		    <li id="ClassIV">
			<select size="4" name="SelClassI" id="SelClassI" style="width:147px;height:214px;" ><option value="">��ѡ���������</option></select>
			</li>
		    <li id="ClassV" class="off">
			<select size="4" name="SelClassII" id="SelClassII" style="width:147px;height:214px;" ><option value="">��ѡ����������</option></select>
			</li>
		  </ul>
		</div>
		</div>
	  </div>
	  <div class="area_list_c">
		<b>����ǰѡ����ǣ�</b><b id="SelectTexts">���ɹ������� &gt; ͨ���� &gt; </b>
	    <div id="SelectCodes" style="display:none;"></div><span><input type="button" name="butNextPage" id="butNextPage" value="��һ��" onclick="GotoSelectDo();"/></span>
	    
	  </div>
    </div>
  </div>
</div>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>

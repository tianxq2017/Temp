<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainDesk.aspx.cs" Inherits="join.pms.web.MainDesk" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>��������</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
<link href="/css/right.css" type="text/css" rel="stylesheet">
<link href="/css/index01.css" rel="stylesheet" type="text/css">
<script language="javascript" src="/includes/huadongmen.js" type="text/javascript"></script>
</head>
<body >
<form id="form1" runat="server">
<!--[if lte IE 6]>
<div class="ie6_bg">
  <div class="ie6 clearfix">
    <ul>
	  <li><a href="http://rj.baidu.com/soft/detail/23253.html" title="IE�����" target="_blank"></a></li>
	  <li><a href="http://www.firefox.com.cn/" title="��������" target="_blank"></a></li>
	  <li><a href="http://rj.baidu.com/soft/detail/14744.html?ald" title="�ȸ������" target="_blank"></a></li>
	  <li><a href="http://www.maxthon.cn/" title="���������" target="_blank"></a></li>
	  <li><a href="http://chrome.360.cn/" title="360���������" target="_blank"></a></li>
	  <li><a href="http://se.360.cn/index_main.html" title="360��ȫ�����" target="_blank"></a></li>
	</ul>
  </div>
</div>
<![endif]-->
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã���ʼҳ</div>
<!--������Ϣ-->
<asp:Literal ID="LiteralBiz" runat="server" EnableViewState="false" />
<asp:Literal ID="LiteralNHS1" runat="server" EnableViewState="false" />
<asp:Literal ID="LiteralNHS2" runat="server" EnableViewState="false" />

<div class="block_index" style="display:<%=none1 %>;">
    <!--div class="index_l" style="display:none">
        <div class="index_01">
            <div class="index_t"><p>��������</p></div>
            <div class="index_b">
                <div class="total">
                    <p class="title">��ϵͳ���ݹ���������</p>
                    <p class="number">201</p>
                </div>
                <div class="list">
                    <ul>
                        <li>���ƾ֣�<b>80</b></li>
                        <li>�����֣�<b>11</b></li>
                        <li>�����֣�<b>20</b></li>
                        <li>����֣�<b>40</b></li>
                        <li>����֣�<b>50</b></li>
                        <li>ͳ�ƾ֣�<b>60</b></li>
                    </ul>
                </div>
            </div>
        </div>
    </div-->
    <div>
        <div class="index_02">
            <div class="index_t"><p><asp:Literal runat="server" ID="LiteraAre" EnableViewState="false"></asp:Literal>ҵ������</p></div>
            <div class="index_b">
                <div class="index_b_l">
                    <table border="0" cellspacing="0" cellpadding="0">
	                    <tr>
		                    <th>ҵ������</th>
		                    <th>�����</th>
		                    <th>ԤԼ��</th>
	                    </tr>
	                    <tr>
		                    <td>������������֤����</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ015001" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ015002" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>һ�������Ǽ�</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010101" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010102" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>���������Ǽ�</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010201" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010202" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>��������������</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ012201" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ012202" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>�������֤��</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ011001" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ011002" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>��������Ů��ĸ����֤������</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010801" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010802" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>��ֹ������������</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ011101" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ011102" EnableViewState="false"></asp:Literal></td>
	                    </tr>
                    </table>
                </div>
                <div class="index_b_r">
                    <table border="0" cellspacing="0" cellpadding="0">
	                    <tr>
		                    <th>ҵ������</th>
		                    <th>�����</th>
		                    <th>ԤԼ��</th>
	                    </tr>
	                    <tr>
		                    <td>������ͥ�������������걨����</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010301" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010302" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>������ͥ�ر����������������</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010401" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010402" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>�������츻�������������� </td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010501" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010502" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>������ͥ������������</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010601" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010602" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>��һ���̡����������������</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010701" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010702" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>�������˿ڻ���֤������������ </td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010901" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010902" EnableViewState="false"></asp:Literal></td>
	                    </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="clr"></div>
<div class="clr10"></div>
</div>

<!--div class="block_index">
    <div class="index_03">
               
    </div>
</div>
<div class="clr10"></div-->

<%-- style="display:<%=none2 %>;"--%>
<div class="part_12_02" style="display:none;">
  <asp:Literal ID="LiteralCms" runat="server" EnableViewState="false"></asp:Literal>
<div class="clr10"></div>
</div>

<!--div class="block_index">
    <div class="index_04">
        <ul>
            <li class="a1"><b>8888</b><span>ȫ���ܻ���</span></li>
            <li class="a2"><b>88888</b><span>ȫ�����˿�</span></li>
            <li class="a3"><b>3333</b><span>ȫ��Ů������</span></li>
            <li class="a4"><b>2222</b><span>��������ȫ��<br />����֤�ѷ���</span></li>
            <li class="a5"><b>4444</b><span>���뱣����Ů��</span></li>
            <li class="a6"><b>555</b><span>���뱣����ͯ��</span></li>
        </ul>
    </div>
</div-->

<%--<div class="block_03">
	<div class="area_t">ͳ����Ϣ</div>
	<div class="area_l">
	    <div class="part_05">
	        <ul>
	            <li class="a1"><b>268963</b><span>ȫ���ܻ���</span></li>
	            <li class="a2"><b>268963</b><span>ȫ�����˿�</span></li>
	            <li class="a3"><b>268963</b><span>ȫ��Ů������</span></li>
	            <li class="a4"><b>268963</b><span>��������ȫ��<br />����֤�ѷ���</span></li>
	        </ul>
	    </div>
	</div>
	<div class="area_r">
	    <div class="part_06">
		    <ul>
	            <li><span>һ����������Ǽǣ�</span><b>15466</b></li>
	            <li><span>������������Ǽǣ�</span><b>15466</b></li>
	            <li><span>����������������</span><b>15466</b></li>
	            <li><span>�����˿ڻ���֤����</span><b>15466</b></li>
	            <li><span>�����˿ڻ���֤����</span><b>15466</b></li>
	            <li><span>������Ů��ĸ����֤��</span><b>15466</b></li>
	            <li><span>��ǰҽѧ���֤����</span><b>15466</b></li>
	            <li><span>ҽ�ƻ���ִҵ���֤��</span><b>15466</b></li>
	            <li><span>���������������֤��</span><b>15466</b></li>
	            <li><span>���������������֤��</span><b>15466</b></li>
	            <li><span>��������ˮ��ˮ��λ�������֤��</span><b>15466</b></li>
	            <li><span>�����˿ڻ������֤����</span><b>15466</b></li>
	        </ul>
	    </div>
	</div>
	<div class="clr"></div>
</div>--%>
<%--<div class="block_01">
	<div class="area_l">
	    <div class="index_01"><a href="#"><i>8</i><span>�ҵ���Ϣ</span></a></div>
	</div>
	<div class="area_c">
	    <div class="index_01 index_01b"><a href="#"><i>521</i><span>֪ͨ����</span></a></div>
	</div>
	<div class="area_r">
	    <div class="index_01 index_01c"><a href="#"><i>23</i><span>�����Ϣ</span></a></div>
	</div>
	<div class="clr"></div>
</div>--%>
<%--<div class="part_12_01">
  <ul>
    <li class="a1"><a href="#" onclick="parent.leftFrame.ShowSubMenu(2)">
      <p class="pic clearfix"><b></b><span>��������</span></p>
      <p class="content">���ݹ����Ǹ�ְ�ܲ������ύ�Ļ������ݣ����ڲ�ͬ�ط�ʹ�ò�ͬ������Ĳ�ͬ�û��ܹ��鿴�������ݲ����и��ֲ���...</p></a>
    </li>
    <li class="a2"><a href="#" onclick="parent.leftFrame.ShowSubMenu(4)">
      <p class="pic clearfix"><b></b><span>��������</span></p>
      <p class="content">���ݱȶԹ��򣬽���ְ�ܲ������ύ�Ĺ������ݺ��˿ڻ������ݽ�����ȡ����ϴ���ȶԷ������γɵĴ��ڲ��������...</p></a>
    </li>
    <li class="a3"><a href="#" onclick="parent.leftFrame.ShowSubMenu(3)">
      <p class="pic clearfix"><b></b><span>��׼����</span></p>
      <p class="content">��ְ�ܲ������ύ�Ĺ������ݺ��˿ڻ������ݽ�����ȡ����ϴ���ȶԷ������γɵ��޲������첻��С������...</p></a>
    </li>
    <li class="a4"><a href="#" onclick="parent.leftFrame.ShowSubMenu(5);parent.leftFrame.ShowSubMenu(6);">
      <p class="pic clearfix"><b></b><span>����Ӳ��</span></p>
      <p class="content">���̣��ֳ�����U�̡�����Ӳ�̣����ɹ���ƽ̨�ṩ�����ߴ洢�������û��ṩ�ļ��Ĵ洢�����ʡ�����...</p></a>
    </li>
  </ul>
  <div class="clr"></div>
</div>--%>
<%--<div class="block_02">
	<div class="area_l">
	    <div class="part_04">
	        <div class="part_t"><span><a href="#">���� &gt;&gt;</a></span><b>֪ͨ����</b></div>
	        <div class="part_c">
		        <ul>
		            <li><span class="time">��2014-11-25��</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		            <li><span class="time">��2014-11-25��</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		            <li><span class="time">��2014-11-25��</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		            <li><span class="time">��2014-11-25��</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		            <li><span class="time">��2014-11-25��</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		        </ul>
	        </div>
	    </div>
	</div>
	<div class="area_r">
	    <div class="part_04 part_04b">
	        <div class="part_t"><span><a href="#">���� &gt;&gt;</a></span><b>�����Ϣ</b></div>
	        <div class="part_c">
		        <ul>
		            <li><span class="time">���ǹ���</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		            <li><span class="time">���ǹ���</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		            <li><span class="time">���ǹ���</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		            <li><span class="time">���ǹ���</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		            <li><span class="time">���ǹ���</span><a href="#" title="����">��Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ������Ϣ����</a></li>
		        </ul>
	        </div>
	    </div>
	</div>
	<div class="clr"></div>
</div>--%>
<!--div class="block_01" style="display: none">
	<div class="area_l">
	    <div class="part_01">
	    
	    </div>
	</div>
	<div class="area_c">
	    <div class="part_02">
	    </div>
	</div>
	<div class="area_r">
	    <div class="part_03">
	    <aspLiteral ID="LiteralBaobiao" runat="server" EnableViewState="false" />
	    </div>
	</div>
	<div class="clr"></div>
</div-->
<div class="part_12_03" style="display:<%=none3 %>;">
  <div class="part_title">
    <p>ͳ�Ƹ���</p>
  </div>
  <div class="part_12_03a">
    <ul>
    <asp:Literal ID="LiteralTotalStats" runat="server" EnableViewState="false"></asp:Literal>
    </ul>
    <div class="clr"></div>
  </div>
  <div class="part_12_03b">
    <p>
      <asp:Literal ID="LiteralStats" runat="server" EnableViewState="false"></asp:Literal>
    </p>
  </div>
</div>
</form>
<br/>
</body>
</html>

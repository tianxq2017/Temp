<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BizPrt0150Menu.ascx.cs" Inherits="join.pms.web.userctrl.BizPrt0150Menu" %>
<style type="text/css">
#table_z td{ padding:10px 0; font-size:14px; line-height:30px}
.biaodianfuhao span{font-family:"Arial","����";  font-size:24px;  }
#tags,#tagsA {
	PADDING: 0px;
	MARGIN: 0px 0px 0px 0px;
	HEIGHT: 30px;
}
#tags LI {
	FLOAT: left;
	MARGIN-left:15px;
	LIST-STYLE-TYPE: none;
	HEIGHT: 30px;
	width:110px;
	text-align:center;
	font-size:12px;
}
#tags LI A {
	PADDING-RIGHT: 0px;
	PADDING-LEFT: 0px;
	PADDING-BOTTOM: 0px;
	COLOR: #333;
	LINE-HEIGHT: 30px;
	PADDING-TOP: 0px;
	HEIGHT: 30px;
	TEXT-DECORATION: none;
}

#tags LI.selectTag {
	FLOAT: left;
	LIST-STYLE-TYPE: none;
	HEIGHT: 30px;
	text-align:center;
	font-size:12px;
	font-weight:bold;
	text-decoration:underline
}
#tags LI.selectTag A {
	BACKGROUND-POSITION: right top;
	COLOR: #333333;
}
#tagsA LI {
	FLOAT: left;
	LIST-STYLE-TYPE: none;
	HEIGHT: 30px;
	width:25%;
	text-align:center;
	font-size:14px;
}
#tagsA LI A {
	PADDING-RIGHT: 0px;
	PADDING-LEFT: 0px;
	PADDING-BOTTOM: 0px;
	COLOR: #000000;
	font-size:12px;
	LINE-HEIGHT: 30px;
	PADDING-TOP: 0px;
	HEIGHT: 30px;
	TEXT-DECORATION: none;
}

#tagsA LI.selectTagA {
	FLOAT: left;
	LIST-STYLE-TYPE: none;
	HEIGHT: 30px;
	text-align:center;
	font-size:12px;
	font-weight:bold;
	text-decoration:underline
}
#tagsA LI.selectTagA A {
	BACKGROUND-POSITION: right top;
	COLOR: #FF0000;
	font-size:12px;
}
</style>
<%if (!string.IsNullOrEmpty(Request.QueryString["mp"])){ %>
<table width="758" border="0" cellpadding="5" cellspacing="1" bgcolor="#cccccc" align="center"><tr><td  bgcolor="#FFFFFF">
<UL id="tagsA">
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view1") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01501.aspx?action=view1&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >������������֤������Ϣ</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view21") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01502.aspx?action=view21&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ǰҽѧ�������֤��</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view22") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01502.aspx?action=view22&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ǰ�����������֤��</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view3") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01502.aspx?action=view3&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >�����ǼǺ�������������¼</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view41") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01503.aspx?action=view41&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >�в�����������</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view42") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01503.aspx?action=view42&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ǰ(����)��ü�¼</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view8") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01504.aspx?action=view8&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >�в���סԺ���俨</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view51") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01504.aspx?action=view51&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >���举Ů�������</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view52") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01504.aspx?action=view52&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >���д�ʩ֪��ѡ������¼</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view6") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01505.aspx?action=view6&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ͯ��������</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view71") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01505.aspx?action=view71&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ͯԤ�����ֻ�����Ϣ</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view72") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01506.aspx?action=view72&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ͯԤ�����ּ�¼��һ��</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view73") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01506.aspx?action=view73&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ͯԤ�����ּ�¼������</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view74") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01507.aspx?action=view74&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ͯԤ�����ּ�¼������</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view75") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01507.aspx?action=view75&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >��ͯԤ�����ּ�¼���ģ�</A></LI>
</UL></td></tr></table>
<%}%>
<script language=javascript>
function selectTag(showContent,selfObj){
        // ������ǩ
        var tag = document.getElementById("tags").getElementsByTagName("li");
        var taglength = tag.length;
        for(i=0; i<taglength; i++){
            tag[i].className = "";
        }
        selfObj.parentNode.className = "selectTag";
        // ��������
        for(i=0; j=document.getElementById("tagContent"+i); i++){
            j.style.display = "none";
        }
        document.getElementById(showContent).style.display = "block";
    }
</script>


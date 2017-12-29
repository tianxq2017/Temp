<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BizPrt0150Menu.ascx.cs" Inherits="join.pms.web.userctrl.BizPrt0150Menu" %>
<style type="text/css">
#table_z td{ padding:10px 0; font-size:14px; line-height:30px}
.biaodianfuhao span{font-family:"Arial","宋体";  font-size:24px;  }
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
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view1") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01501.aspx?action=view1&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >婚育健康服务证基本信息</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view21") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01502.aspx?action=view21&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >婚前医学健康检查证明</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view22") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01502.aspx?action=view22&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >孕前优生健康检查证明</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view3") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01502.aspx?action=view3&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >生育登记和再生育审批记录</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view41") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01503.aspx?action=view41&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >孕产妇保健服务</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view42") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01503.aspx?action=view42&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >产前(产后)随访记录</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view8") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01504.aspx?action=view8&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >孕产妇住院分娩卡</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view51") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01504.aspx?action=view51&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >育龄妇女健康检查</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view52") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01504.aspx?action=view52&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >避孕措施知情选择服务记录</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view6") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01505.aspx?action=view6&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >儿童保健服务</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view71") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01505.aspx?action=view71&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >儿童预防接种基本信息</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view72") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01506.aspx?action=view72&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >儿童预防接种记录（一）</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view73") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01506.aspx?action=view73&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >儿童预防接种记录（二）</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view74") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01507.aspx?action=view74&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >儿童预防接种记录（三）</A></LI>
<LI <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view75") > -1) { Response.Write("class=\"selectTagA\""); }%>><A href="/BizInfo/BizPrt01507.aspx?action=view75&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >儿童预防接种记录（四）</A></LI>
</UL></td></tr></table>
<%}%>
<script language=javascript>
function selectTag(showContent,selfObj){
        // 操作标签
        var tag = document.getElementById("tags").getElementsByTagName("li");
        var taglength = tag.length;
        for(i=0; i<taglength; i++){
            tag[i].className = "";
        }
        selfObj.parentNode.className = "selectTag";
        // 操作内容
        for(i=0; j=document.getElementById("tagContent"+i); i++){
            j.style.display = "none";
        }
        document.getElementById(showContent).style.display = "block";
    }
</script>


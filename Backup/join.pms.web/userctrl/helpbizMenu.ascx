<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="helpbizMenu.ascx.cs" Inherits="join.pms.web.userctrl.helpbizMenu" %>
<style type="text/css">
#table_z td{ padding:10px 0; font-size:14px; line-height:30px}
.biaodianfuhao span{font-family:"Arial","宋体";  font-size:24px;  }
#tags {
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
</style>
<div class="tagsA">
    <ul id="tagsA" class="clearfix">
        <li <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view1") > -1) { Response.Write("class=\"selectTagA\""); }%>><a href="/NHS/helpbiz01.aspx?action=view1&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >持证人基本信息</a></li>
        <li <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view2") > -1) { Response.Write("class=\"selectTagA\""); }%>><a href="/NHS/helpbiz02.aspx?action=view2&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >婚前(孕前)医学健康检查</a></li>
        <li <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view3") > -1) { Response.Write("class=\"selectTagA\""); }%>><a href="/NHS/helpbiz03.aspx?action=view3&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >生育登记及审批记录</a></li>
        <li <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view4") > -1) { Response.Write("class=\"selectTagA\""); }%>><a href="/NHS/helpbiz04.aspx?action=view4&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >孕产妇保健服务</a></li>
        <%--<li <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view8") > -1) { Response.Write("class=\"selectTagA\""); }%>><a href="/NHS/helpbiz08.aspx?action=view8&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >孕产妇住院分娩卡</a></li>--%>
        <li <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view5") > -1) { Response.Write("class=\"selectTagA\""); }%>><a href="/NHS/helpbiz05.aspx?action=view5&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >育龄妇女健康检查</a></li>
        <li <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view6") > -1) { Response.Write("class=\"selectTagA\""); }%>><a href="/NHS/helpbiz06.aspx?action=view6&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >儿童保健服务</a></li>
        <li <%if (Convert.ToString(Request.QueryString["action"]).IndexOf("view7") > -1) { Response.Write("class=\"selectTagA\""); }%>><a href="/NHS/helpbiz07.aspx?action=view7&mp=<%=Request.QueryString["mp"]%>&fwzh=<%=Request.QueryString["fwzh"]%>" >儿童预防接种记录</a></li>
    </ul>
</div>
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


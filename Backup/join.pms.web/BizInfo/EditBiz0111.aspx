<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBiz0111.aspx.cs" Inherits="join.pms.web.BizInfo.EditBiz0111" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>《终止妊娠》申请表</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtAttribs" id="txtAttribs" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
	  <div class="form_bg">	  
	    <div class="form_a">
	        <p class="form_title"><b>育龄妇女信息</b></p>
		    <div class="form_table">
		  <table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>民族：</th>
    <td class="select"><asp:Literal ID="LiteralNationsB" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsB" id="txtHNationsB" runat="server" style="display:none;"/></td>
  </tr>
      <tr>
    <th><span class="xing">*</span>末次月经日期：</th>
    <td class="text"><p><span><input id="txtFileds42" runat="server"  onclick="SelectDate(document.getElementById('txtFileds42'),'yyyy-MM-dd')"   /></span></p></td>
  </tr>
    <tr>
    <th><span class="xing">*</span>孕周：</th>
    <td class="select"><asp:DropDownList ID="ddlFileds43" runat="server">
        <asp:ListItem>1</asp:ListItem>
        <asp:ListItem>2</asp:ListItem>
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>4</asp:ListItem>
        <asp:ListItem>5</asp:ListItem>
        <asp:ListItem>6</asp:ListItem>
        <asp:ListItem>7</asp:ListItem>
        <asp:ListItem>8</asp:ListItem>
        <asp:ListItem>9</asp:ListItem>
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>11</asp:ListItem>
        <asp:ListItem>12</asp:ListItem>
        <asp:ListItem>13</asp:ListItem>
        <asp:ListItem>14</asp:ListItem>
        <asp:ListItem>15</asp:ListItem>
        <asp:ListItem>16</asp:ListItem>
        <asp:ListItem>17</asp:ListItem>
        <asp:ListItem>18</asp:ListItem>
        <asp:ListItem>19</asp:ListItem>
        <asp:ListItem>20</asp:ListItem>
        <asp:ListItem>21</asp:ListItem>
        <asp:ListItem>22</asp:ListItem>
        <asp:ListItem>23</asp:ListItem>
        <asp:ListItem>24</asp:ListItem>
        <asp:ListItem>25</asp:ListItem>
        <asp:ListItem>26</asp:ListItem>
        <asp:ListItem>27</asp:ListItem>
        <asp:ListItem>28</asp:ListItem>
        <asp:ListItem>29</asp:ListItem>
        <asp:ListItem>30</asp:ListItem>
        <asp:ListItem>31</asp:ListItem>
        <asp:ListItem>32</asp:ListItem>
        <asp:ListItem>33</asp:ListItem>
        <asp:ListItem>34</asp:ListItem>
        <asp:ListItem>35</asp:ListItem>
        <asp:ListItem>36</asp:ListItem>
        <asp:ListItem>37</asp:ListItem>
        <asp:ListItem>38</asp:ListItem>
        <asp:ListItem>39</asp:ListItem>
        <asp:ListItem>40</asp:ListItem>
        <asp:ListItem>41</asp:ListItem>
        <asp:ListItem>42</asp:ListItem>
        <asp:ListItem>43</asp:ListItem> 
        <asp:ListItem>44</asp:ListItem> 
        <asp:ListItem>45</asp:ListItem>                                                                                                               
      </asp:DropDownList></td>
  </tr>
     <tr>
    <th><span class="xing">*</span>政策属性：</th>
    <td class="select"><asp:DropDownList ID="ddlPolicyB" runat="server"   onchange="ShowFiled18();">
                            <asp:ListItem>政策内</asp:ListItem>
                            <asp:ListItem>政策外</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
   <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID('1','txtPersonCidB');"/></span></p></td>
  </tr>
    <tr>
    <th><span class="xing">*</span>婚姻状况：</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server">
                            <asp:ListItem>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem>  
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
    <tr>
    <th><span class="xing">*</span>户籍地址：</th>
        <td class="text"><p><span><asp:TextBox ID="txtAreaSelRegNameB" runat="server" EnableViewState="False" Width="300"/> </span></p>   
        <input type="hidden" name="txtAreaSelRegCodeB" id="txtAreaSelRegCodeB" runat="server" style="display:none;"/></td>
  </tr>   
    <tr>
    <th><span class="xing">*</span>户口性质：</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                                    <asp:ListItem>农业</asp:ListItem>
                                    <asp:ListItem>非农业</asp:ListItem>
                                    <asp:ListItem>其他</asp:ListItem>          
                       </asp:DropDownList></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>居住地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><input type="button" name="button" value="同户籍地" class="button" onclick="AreaCodeTB('txtAreaSelRegCodeB','txtAreaSelRegNameB','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea');"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>联系电话：</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
  </tr>
    <tr>
    <th><span class="xing">*</span>已生育子女数：</th>
     <td class="select"><asp:DropDownList ID="ddlBirthNum" runat="server" onchange="ShowBirth0101();">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                   </asp:DropDownList></td>
  </tr>  
</table>
		    </div>
        </div> 
	  
	    <div class="form_a">
	        <p class="form_title"><b>丈夫信息</b></p>
			<div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr>
   <tr>
        <th><span class="xing">*</span>民族：</th>
        <td class="select"><asp:Literal ID="LiteralNationsA" runat="server" EnableViewState="false"></asp:Literal>
        <input type="hidden" name="txtHNationsA" id="txtHNationsA" runat="server" style="display:none;"/></td>
  </tr>
     <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID('0','txtPersonCidA');"/></span></p></td>
  </tr>
   <tr>
        <th><span class="xing">*</span>户籍地址：</th>
        <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelRegA" runat="server" /></td>
  </tr> 
    <tr>
    <th><span class="xing">*</span>户口性质：</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                                    <asp:ListItem>农业</asp:ListItem>
                                    <asp:ListItem>非农业</asp:ListItem>
                                    <asp:ListItem>其他</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr>
   <tr>
    <th><span class="xing">*</span>居住地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="同户籍地" class="button" onclick="AreaCodeTB('UcAreaSelRegA_txtSelAreaCode','UcAreaSelRegA_txtSelectArea','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea');"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>联系电话：</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
  </tr> 

 
</table>
            </div>	    	    
	    </div>
              	
        <div id="panelChildren" style="display:none;" class="form_a">
          <p class="form_title"><b>双方生育子女情况</b></p>
          <div class="form_table">
          <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
            <tr><th><span class="xing">*</span>子女姓名</th><th><span class="xing">*</span>性别</th><th><span class="xing">*</span>出生日期</th><th>政策属性</th><th>备注</th></tr>
            <tr id="tableChild1">
                <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25" Width="80"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                       <asp:ListItem>男</asp:ListItem>
                       <asp:ListItem>女</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"   style="width:100px;"/></span></p></td> 
                <td class="select"><asp:DropDownList ID="ddlChildPolicy1" runat="server">
                       <asp:ListItem>政策内</asp:ListItem>
                       <asp:ListItem>政策外</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="text"><p><span><asp:TextBox ID="txtMemos1" runat="server" EnableViewState="False" MaxLength="18" Width="200"/></span></p></td>
            </tr>
            <input type="hidden" name="txtChildID1" id="txtChildID1" value="" runat="server" style="display:none;"/>
            <tr id="tableChild2"style="display:none;" >
                <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25" Width="80"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                                       <asp:ListItem>男</asp:ListItem>
                                       <asp:ListItem>女</asp:ListItem>           
                                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"   style="width:100px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy2" runat="server">
                                       <asp:ListItem>政策内</asp:ListItem>
                                       <asp:ListItem>政策外</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtMemos2" runat="server" EnableViewState="False" MaxLength="18" Width="200"/></span></p></td>              
            </tr>
            <input type="hidden" name="txtChildID2" id="txtChildID2" value="" runat="server" style="display:none;"/>       
          </table>         
            </div>
        </div>

        <div class="form_a">
          <p class="form_title"><b>申请理由</b></p>
          <div class="form_table">
            <table width="0" border="0" cellspacing="0" cellpadding="0">        
              <tr>
                <th><span class="xing">*</span>申请理由：</th>
                <td class="text"><p><span><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px"  Text ="政策内申请终止妊娠/政策外申请终止妊娠。"/></span></p></td>
              </tr>              
              </table>
          </div></div>
 
	  </div>
<div class="check"><asp:CheckBox ID="cbOk" runat="server" /> 承诺：本人保证以上情况及提供的相关材料属实。如有不实，愿意承担由此引起的相应法律责任，并将记入国家诚信系统。</div>	
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
<script>
 window.onload=function(){
 ShowBirth0111();ShowFiled18();
}
</script>
<script language="javascript" type="text/javascript">
        Sys.Application.add_load(
             function() {
                 var form = Sys.WebForms.PageRequestManager.getInstance()._form;
                 form._initialAction = form.action = window.location.href;
             }
         );
</script>

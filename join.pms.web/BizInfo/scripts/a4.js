  //定义浏览器页面设置———>页边距设置默认值（0.750000*25.4=19.05毫米）
   var top = '0.750000';
   var bottom = '0.750000';
   var left = '0.750000';
   var right = '0.750000';
   
   //调用js控件来修改IE的注册信息，设置网页打印的页眉页脚为空且把页边距(上下左右边界)修改为浏览器默认值：19.05毫米
  //设置网页打印的页眉页脚和页边距为默认值  
  var HKEY_Root,HKEY_Path,HKEY_Key;
  HKEY_Root="HKEY_CURRENT_USER";
  HKEY_Path="\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";
 
  var Wsh=new ActiveXObject("WScript.Shell");    //vbscript的控件核心文件
 
  HKEY_Key="header";
  Wsh.RegWrite(HKEY_Root+HKEY_Path+HKEY_Key, "");    //清空注册表信息中IE的页眉
 
  HKEY_Key="footer";
  Wsh.RegWrite(HKEY_Root+HKEY_Path+HKEY_Key, "");    //清空注册表信息中IE的页脚

  HKEY_Key="margin_top";
  Wsh.RegWrite(HKEY_Root+HKEY_Path+HKEY_Key,top); //键值设定--上边边界
 
  HKEY_Key="margin_bottom";
  Wsh.RegWrite(HKEY_Root+HKEY_Path+HKEY_Key,bottom); //键值设定--下边边界

  HKEY_Key="margin_left";
  Wsh.RegWrite(HKEY_Root+HKEY_Path+HKEY_Key,left); //键值设定--左边边界

   HKEY_Key="margin_right"
   Wsh.RegWrite(HKEY_Root+HKEY_Path+HKEY_Key,right); //键值设定--右边边界
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainDesk.aspx.cs" Inherits="join.pms.web.MainDesk" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head lang="en">
    <title>个人桌面</title>
    <link rel="stylesheet" href="css/main.css"/>
</head> 
<body>
<div class="bg">
    <div class="sec">
        <div class="sec-r">
            <div class="sec-r-t">
                <ul>
                    <li style="margin-left:80px;">
                        <a href="UnvCommList.aspx?1=1&FuncCode=0101">
                            <img src="images/zjgl.png" alt=""/>
                            <h4>资金管理</h4>
                        </a>
                    </li>
                    <li>
                        <a href="UnvCommChart.aspx?1=1&FuncCode=0201">
                            <img src="images/xiangmu.jpg" alt=""/>
                            <h4>项目管理</h4>
                        </a>
                    </li>
                </ul>
            </div>
            <div class="sec-r-f">
                <ul>
                    <li>
                        <a href="UnvCommChart.aspx?1=1&FuncCode=0203">
                            <img src="images/zdzx.png" alt=""/>
                            <h4>重点工作</h4>
                        </a>
                    </li>
                    <li>
                        <a href="UnvCommList.aspx?1=1&FuncCode=0401&FuncNa=通知公告">
                            <img src="images/shenji.png" alt=""/>
                            <h4>通知公告</h4>
                        </a>
                    </li>
                    <li>
                        <a href="UnvCommList.aspx?1=1&FuncCode=0402&FuncNa=政策法规">
                            <img src="images/dashuju.png" alt=""/>
                            <h4>政策法规</h4>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
 </div>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectInfoForMonth.aspx.cs" Inherits="join.pms.web.Chart.ProjectInfoForMonth" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/echarts/echarts.js" type="text/javascript"></script>
<!-- 引入 ECharts 文件 -->
<script type="text/javascript" src="/Scripts/echarts/echarts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="main1" style="width:300px;height:260px;">
    
    </div><script type="text/javascript">
                    // 基于准备好的dom，初始化echarts实例
                    var myChart = echarts.init(document.getElementById('main1'));

                    // 指定图表的配置项和数据
                    option = {
                        backgroundColor: '#14243e',//背景色
                        tooltip: {
                            trigger: 'item',
                            formatter: "{a} <br/>{b}: {c} ({d}%)"
                        },
                        color: ['#f69463', '#eee475', '#f5829f', '#57bc9a', '#43bee8', '#8b8edb', '#BB5662'],
                        legend: {
                            orient: 'horizontal',                            
                            bottom: '-5',
                            textStyle: { color: '#ffffff', fontSize: '10' },
                            width: '290',
                            itemWidth: 7,
                            icon: 'circle',
                            data: ['1', '2', '3', '4', '5', '6']
                        }, 
                        series: [
                            {
                                name: '按业务办理分析',
                                type: 'pie',
                                radius: ['50%', '70%'],
                                avoidLabelOverlap: false,                                
                                label: {
                                    normal: {
                                        show: false,
                                        position: 'center',                                      
                                    },
                                    emphasis: {
                                        show: true,
                                        textStyle: {
                                            fontSize: '14',
                                            fontWeight: 'bold'
                                        }
                                    }
                                },
                                labelLine: {
                                    normal: {
                                        show: false
                                    }
                                },
                                data: [100,200,300,400,500,600]
                            }
                        ]
       };


    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
    </script>
    </form>
</body>
</html>

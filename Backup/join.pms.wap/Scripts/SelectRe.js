//function $(p){return jQuery(p);}ע�͵���������jquery-1.4.2.min.js��ͻ

var xdmJS={
/*�ж������,���ݺ�������ֵ�ж�����ʲô�����,�����ж��Ƿ���IE��if(xdmJS.browser()=="ie6"){alert("this is ie6")}*/
browser:function(){
    var Sys = {};
    var ua = navigator.userAgent.toLowerCase();
    var s;
    var bro;
    (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
    (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
    (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
    (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
    (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
    //�����ж�
    if (Sys.ie){
        if(Sys.ie=="6.0"){bro="ie6";}
        else if(Sys.ie=="7.0"){bro="ie7";}
        else if(Sys.ie=="8.0"){bro="ie8";}
        else if(Sys.ie=="9.0"){bro="ie9";}}

    if (Sys.firefox){bro="firefox";}
    if (Sys.chrome){bro="chrome";}
    if (Sys.opera){bro="opera";}
    if (Sys.safari){bro="safari";}
    return bro;
},

/*����ԭ���������HTMLģ��select�����˵����������Ӧ�����ݶ�������Ӧ��ͬһҳ������������Ҳ����*/
selectAnalog:function(selectId,className){
/*selectId:����Ҫʹ��ģ���select��ID���ֻ���class����
className:������Ĭ�����õ�select-analog����ʽ��������Ԫ�ص���ʽ*/
$("#"+selectId).each(function(index){
    /*����select��ֵ������Ӧ��HTML*/
    var firstHtml=$(this).find("option:first").html();
    var html="<div class='select-analog";
    if(className){ /*����и�����ʽ�Ļ��ͼ�����ʽ�����򲻼�*/
    html+=" "+className;}
    html+="'><a class='title'>";
    //html+=firstHtml;
    html+="</a>";
    var htmlUl=$("<ul></ul>");
    var optionLength=$(this).find("option").length;

    for(var i=0;i<optionLength;i++){
    var htmlLi="<li><a href='"+$(this).find("option").eq(i).val()+"'>"+$(this).find("option").eq(i).html()+"</a></li>";
    htmlUl.append(htmlLi);
}

html+="<ul>"+htmlUl.html()+"</ul></div>";
$(this).after(html);
var selectW=$(this).width();
var arrWidth=$(".arr").eq(index).width();

//$(this).hide();

/*�����ɵ���Ӧ��HTML�ӷ���ģ��select������*/
var btnId=$(".select-analog").eq(index).find(".title");
var ul=$(".select-analog").eq(index).find("ul");
var arr="<span class='arr'></span>";

/*�ж������,��Ϊ���������select�Ŀ��ȡֵ��һ��*/

//if (xdmJS.browser()=="safari") {$(btnId).width(selectW+arrWidth+20);}
// else{$(btnId).width(selectW+arrWidth+2);}

var paddingW=parseInt($(btnId).css("padding-left"));
//ul.width($(btnId).width()+paddingW);
//$(".select-analog").width($(btnId).width()+paddingW);
$(".select-analog").mouseleave(function(){ul.hide();});

$(btnId).click(function(){
$(".select-analog").find("ul").hide().eq(index).show();
return false;
});

//һ�´��븳ֵʱʹ��
//ul.find("a").click(function(){
//$(btnId).html($(this).html()+arr);
//var aindex=ul.find("a").index($(this));
////��һ�ַ���

////$(this).parents(".select-analog").prev("select")[0].selectedIndex=aindex;

////�ڶ��ַ���
//$(this).parents(".select-analog").prev("select").find("option").eq(aindex).attr("selected",true);

//ul.hide();
//return false;
//});

});
}
}

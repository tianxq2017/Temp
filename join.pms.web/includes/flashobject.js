imgUrl1="images/01.jpg";
imgtext1="蔬菜广告创意01"
imgLink1=escape("#");
imgUrl2="images/02.jpg";
imgtext2="蔬菜广告创意02"
imgLink2=escape("#");
imgUrl3="images/03.jpg";
imgtext3="蔬菜广告创意03"
imgLink3=escape("#");
imgUrl4="images/04.jpg";
imgtext4="蔬菜广告创意04"
imgLink4=escape("#");
imgUrl5="images/05.jpg";
 var focus_width=280
 var focus_height=220
 var text_height=30
 var swf_height = focus_height+text_height
 
 var pics=imgUrl1+"|"+imgUrl2+"|"+imgUrl3+"|"+imgUrl4
 var links=imgLink1+"|"+imgLink2+"|"+imgLink3+"|"+imgLink4
 var texts=imgtext1+"|"+imgtext2+"|"+imgtext3+"|"+imgtext4
 
 document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="'+ focus_width +'" height="'+ swf_height +'">');
 document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="images/focus1.swf"><param name="quality" value="high"><param name="bgcolor" value="#F0F0F0">');
 document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
 document.write('<param name="FlashVars" value="pics='+pics+'&links='+links+'&texts='+texts+'&borderwidth='+focus_width+'&borderheight='+focus_height+'&textheight='+text_height+'">');
 document.write('</object>');
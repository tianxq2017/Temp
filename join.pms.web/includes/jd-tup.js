// JavaScript Document
function tabit1(id,cid) 
{
    var photoCount=document.getElementById('txtPhotoCount').value;
    if(photoCount!="" && photoCount!="0")
    {
        if(photoCount=="1")
        {
            document.getElementById("tab1").className="menu2";
            document.getElementById("tab2").className="menu2";
            id.className="menu1";
            ctab1.style.display="none";
            ctab2.style.display="none";
            cid.style.display="block";
        }
        else  if(photoCount=="2")
        {

            document.getElementById("tab1").className="menu2";
            document.getElementById("tab2").className="menu2";
            document.getElementById("tab3").className="menu2";
            id.className="menu1";
            ctab1.style.display="none";
            ctab2.style.display="none";
            ctab3.style.display="none";
            cid.style.display="block";
        }
        else  if(photoCount=="3")
        {

             document.getElementById("tab1").className="menu2";
            document.getElementById("tab2").className="menu2";
            document.getElementById("tab3").className="menu2";
             document.getElementById("tab4").className="menu2";
            id.className="menu1";
            ctab1.style.display="none";
            ctab2.style.display="none";
            ctab3.style.display="none";
            ctab4.style.display="none";
            cid.style.display="block";
        }
        else  if(photoCount=="4")
        {

                         document.getElementById("tab1").className="menu2";
            document.getElementById("tab2").className="menu2";
            document.getElementById("tab3").className="menu2";
             document.getElementById("tab4").className="menu2";
               document.getElementById("tab5").className="menu2";
            id.className="menu1";
            ctab1.style.display="none";
            ctab2.style.display="none";
            ctab3.style.display="none";
            ctab4.style.display="none";
            ctab5.style.display="none";
            cid.style.display="block";
        }
    }
}
//2011-1-21
//www.cnblogs.com/xxfss2

//function(event) { $(this).css({ 'cursor': '/web/Page/frameset/images/splith.cur' }
 var lineMove = false;
 var currTh = null;
 $(document).ready(function() {
     $("body").bind("selectstart", function() { return !lineMove; });
     $("body").append("<div id=\"line\" style=\"width:1px;height:200px;border-left:1px solid #00000000; position:absolute;display:none\" ></div> ");
     $("body").bind("mousemove", function(event) {
         if (lineMove == true) {
             $("#line").css({ "left": event.clientX }).show();
         }
     });

     $("th").bind("mousemove", function(event) {
         var th = $(this);
         if (th.prevAll().length <= 1 || th.nextAll().length < 1) {
             return;
         }
         var left = th.offset().left;
         if (event.clientX - left < 4 || (th.width() - (event.clientX - left)) < 4) {
             th.css({ 'cursor': 'url(/images/im_panh.cur),e-resize'});
             //this.cursor = 'url("/images/im_panh.cur"),e-resize';      
             //th.className="curs";
         }
         else {
             th.css({ 'cursor': 'default' });
         }
     });

     $("th").bind("mousedown", function(event) {
         var th = $(this);
         if (th.prevAll().length <= 1 || th.nextAll().length < 1) {
             return;
         }
         var pos = th.offset();
         if (event.clientX - pos.left < 4 || (th.width() - (event.clientX - pos.left)) < 4) {
             var height = th.parent().parent().height();
             var top = pos.top;
             $("#line").css({ "height": height, "top": top,"left":event .clientX,"display":"" });
             lineMove = true;
             if (event.clientX - pos.left < th.width() / 2) {
                 currTh = th.prev();
             }
             else {
                 currTh = th;
             }
         }
     });

     $("body").bind("mouseup", function(event) {
         if (lineMove == true) {
             $("#line").hide();
             lineMove = false;
             var pos = currTh.offset();
             var index = currTh.prevAll().length;
             currTh.width(event.clientX - pos.left);
             currTh.parent().parent().find("tr").each(function() {
                 $(this).children().eq(index).width(event.clientX - pos.left);
             }); //.children().eq(index).width(event.clientX - pos.left);
         }
     });
     $("th").bind("mouseup", function(event) {
         if (lineMove == true) {
             $("#line").hide();
             lineMove = false;
             var pos = currTh.offset();
             var index = currTh.prevAll().length;
             currTh.width(event.clientX - pos.left);
             currTh.parent().parent().find("tr").each(function() {
                 $(this).children().eq(index).width(event.clientX - pos.left);
             });
         }
     });
 });
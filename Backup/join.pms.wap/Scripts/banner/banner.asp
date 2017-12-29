<link href="/scripts/banner/common.css" rel="stylesheet" type="text/css">
<script type="text/javascript" src="/scripts/banner/jQuery.js"></script>
<script type="text/javascript" src="/scripts/banner/swipe.js"></script>

<div onselectstart="return true;" ondragstart="return false;" style="-webkit-transform:translate3d(0,0,0);">
	<div id="banner_box" class="box_swipe">
		<ul>
		  <li><a href="#"><img src="/images/banner/banner_01a.jpg" width="100%" /></a></li>
		  <li><a href="#"><img src="/images/banner/banner_01b.jpg" width="100%" /></a></li>
		  <li><a href="#"><img src="/images/banner/banner_01c.jpg" width="100%" /></a></li>
		</ul>
		<ol>
		  <li class="on"></li>
		  <li></li>
		  <li></li>
		</ol>
	</div>
</div>
<script>
	$(function(){
		new Swipe(document.getElementById('banner_box'), {
			speed:500,
			auto:5000,
			callback: function(){
				var lis = $(this.element).next("ol").children();
				lis.removeClass("on").eq(this.index).addClass("on");
			}
		});
	});
</script>
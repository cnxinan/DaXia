$(function() {
	var jsArgs = {};
	jsArgs['alllist'] = {
		head: true,
	};
	M.setRunMod(['alllist']);
	M.runner(jsArgs);
	
	$("#rootList li").on('touchstart', function(e) {
		var _index = $(this).index();
		$(this).addClass("current").siblings().removeClass("current");
		$("#category-content #category-cont").eq(_index).show().siblings().hide();
	})
});
// 列表滑动
var rootListBox;
var fenleiOne;
var fenleiTwo;
var fenleiThree;
var fenleiFour;
var fenleiFive;
var fenleiSix;
var fenleiSeven;
var fenleiEight;
function loaded() {
	rootListBox = new IScroll('#rootListBox', {
		keyBindings: true
	});
	fenleiOne   = new IScroll('#fenlei1');
	fenleiTwo   = new IScroll('#fenlei2');
	fenleiThree = new IScroll('#fenlei3');
	fenleiFour  = new IScroll('#fenlei4');
	fenleiFive  = new IScroll('#fenlei5');
	fenleiSix   = new IScroll('#fenlei6');
	fenleiSeven = new IScroll('#fenlei7');
	fenleiEight = new IScroll('#fenlei8');
}
document.addEventListener('touchmove', function(e) {
	e.preventDefault();
}, false);
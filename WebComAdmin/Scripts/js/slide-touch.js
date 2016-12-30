$(function($) {
	$('.slider').slider({
		loop: true,
		viewNum: 1
	});
    $('.productSliderOne').slider({
        loop: true,
        viewNum: 1
    });
}(Zepto));
// 下拉加载
$(function () {
    var PageNum = 0;
    //$(window).scroll(function () {
    //    var totalheight = parseFloat($(window).height()) + parseFloat($(window).scrollTop());//浏览器的高度加上滚动条的高度
    //    if ($(document).height() <= totalheight)//当文档的高度小于或者等于总的高度的时候，开始动态加载数据
    //    {
    //        $('#LoadingMsg').css('display', 'block');
    //    }else{
	//		$('#LoadingMsg').css('display', 'none');
	//	}
    //});
    // follow
    $(".seckill .follow span").on('click', function(e) {
        var _index = $(this).index();
        $(this).toggleClass("active");        
    })
    // product
    $(".productTop ul li").on('touchstart', function(e) {
        var _index = $(this).index();
        $(this).addClass("active").siblings().removeClass("active");
        $(".productBottom .productCont").eq(_index).show().siblings().hide();
    })
    // cart
    $(".productCont span").on('click', function(e) {
        var _index = $(this).index();
        $(this).toggleClass("active");
    })
    // edit
    $(".edit").on('touchstart', function(e) {
        $(".edit").hide();
        $(".follow").hide();
        $(".complete").show();
        $(".choice").show();
        $(".delete").show();
        $(".product").addClass("fr");
    })
    $(".complete").on('touchstart', function(e) {
        $(".edit").show();
        $(".follow").show();
        $(".complete").hide();
        $(".choice").hide();
        $(".delete").hide();
        $(".product").removeClass("fr");
    })
    // 全选
    $(".chooseall").on('click', function(e){
        if(this.checked){
            $("input[name='checkbox']").each(function(){this.checked=true;});
            $(".bgColor").prop("disabled", false);
            $(".bgColor").addClass("active");
        }else{
            $("input[name='checkbox']").each(function(){this.checked=false;});
            $(".bgColor").prop("disabled", true);
            $(".bgColor").removeClass("active");
        }
    });
    $(".choice input[name='checkbox']").on('click', function(e){
        $(".bgColor").prop("disabled", false);
        $(".bgColor").addClass("active");
    });
    // 充值
    $(".recharge span").on('click', function(e) {
        var _index = $(this).index();
        $(this).addClass("active").siblings().removeClass("active");
    })
    // 帐号明细
    $(".accountDetailsUl li").on('click', function(e) {
        var _index = $(this).index();
        $(this).addClass("active").siblings().removeClass("active");
        $(".accountDetailsBtm .accountDetailsCont").eq(_index).show().siblings().hide();
    })
    // 数量加减
    var t = $("#text_box");
    $("#add").click(function(){        
        t.val(parseInt(t.val())+1)
    })
    $("#min").click(function(){
        t.val(parseInt(t.val())-1)
    })
    // 一元夺宝记录
    $(".myPerCen li").on('click', function(e) {
        var _index = $(this).index();
        $(this).addClass("active").siblings().removeClass("active");
        $(".myPersonalCenter .myPerCenCont").eq(_index).show().siblings().hide();
    })
    // 产品详情
    $(".productUl li").on('click', function(e) {
        var _index = $(this).index();
        $(this).addClass("active").siblings().removeClass("active");
        $(".productCont .productContent").eq(_index).show().siblings().hide();
    })
    $(".mycart a").on('click', function(e) {
        var _index = $(this).index();
        $(this).toggleClass("active");
    })
})

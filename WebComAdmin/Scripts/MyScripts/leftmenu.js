$(function () {
    $(".firstMenu").each(function (i) {
        //鼠标滑过
        if ($(this).hasClass("first_cur") == false) {
            $(this).hover(function () {
                $(this).removeClass("first_ot").addClass("first_over");
            }, function () {
                $(this).removeClass("first_over").addClass("first_ot");
            })
        }

        //鼠标点击
        $(this).click(function () {
            if ($(this).hasClass("first_cur") == false) {
                ResetFirstMenu();
                $(".firstMenu").eq(i).removeClass("first_ot").addClass("first_cur");
                $(".SecondMenuCont").eq(i).slideDown(200);
            }
            else {
                ResetFirstMenu();
            }
        })
    })

    $(".secondMenu").each(function (i) {
        //鼠标滑过
        if ($(this).hasClass("second_cur") == false) {
            $(this).hover(function () {
                $(this).removeClass("second_ot").addClass("second_over");
            }, function () {
                $(this).removeClass("second_over").addClass("second_ot");
            })
        }

        //鼠标点击
        $(this).click(function () {
            if ($(this).hasClass("second_cur") == false) {
                ResetSecondMenu();
                $(".secondMenu").eq(i).removeClass("second_ot").addClass("second_cur");
            }
        })
    })

    //滑动显示样式
    $("#VSetting a").toggle(function () {
        $("#FrameSettingView").animate({ left: "0px" }, 200);
        $(this).addClass("close");
        $(this).attr("title", "关闭");
    }, function () {
        $("#FrameSettingView").animate({ left: "-58px" }, 200);
        $(this).removeClass("close");
        $(this).attr("title", "展开");
    })

    //显示样式切换
    $("#FrameSettingView span").each(function (i) {
        $(this).click(function () {
            ResetFrameView();
            $("#FrameSettingView span").eq(i).addClass("selected");
            chgFrameView(i);
        })
    })
})
//一级菜单复位
function ResetFirstMenu() {
    $(".firstMenu").removeClass("first_cur").addClass("first_ot");
    $(".SecondMenuCont").slideUp(200);
}

//二级菜单复位
function ResetSecondMenu() {
    $(".secondMenu").removeClass("second_cur").addClass("second_ot");
}

//框架显示样式复位
function ResetFrameView() {
    $("#FrameSettingView span").removeClass("selected");
}

//分栏、通栏切换
function chgFrameView(i) {
    if (i == 0) {
        //分栏
        $(".FrameLeftTD").show();
        $("#LeftMenuDiv").show();
        $("#FrameContent").removeClass("nobg").addClass("havebg");
    }
    else {
        //通栏
        $("#LeftMenuDiv").hide();
        $(".FrameLeftTD").hide();
        $("#FrameContent").removeClass("havebg").addClass("nobg");
    }
}
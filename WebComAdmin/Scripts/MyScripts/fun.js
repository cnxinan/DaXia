$(function () {
  AllHover();

  //$("#ReleaseDate").datepicker({
  //  changeMonth: true,
  //  changeYear: true,
  //  numberOfMonths: 1
  //});
})

function AllHover() {
  SelectAllCheckBox();
  SelectStyle();
  ButtonHover();
  TRHover();
}

//行与输入框效果
function TRHover() {
  $(".txt").focus(function () {
    $(this).addClass("addbg");
  })
  $(".txt").blur(function () {
    $(this).removeClass("addbg");
  })

  $(".tabTable .itemTD").each(function (i) {
    $(this).click(function () {
      $(".tabTable .itemTD").removeClass("cur").addClass("ot");
      $(".tabTable .itemTD").eq(i).removeClass("ot").addClass("cur");
      $(".ItemTable").hide();
      $(".ItemTable").eq(i).show();
    })
  })

  ////列表页面鼠标滑过行时变色
  //$(".dataGrid tr").each(function () {
  //    $(this).hover(function () {
  //        $(this).find("td").not("thead td").addClass("EvenBg");
  //    }, function () {
  //        $(this).find("td").not("thead td").removeClass("EvenBg");
  //    })
  //})

  //列表页面鼠标滑过行时变色
  $(".dataTable tbody tr").each(function () {
    $(this).hover(function () {
      $(this).find("td").addClass("EvenBg");
    }, function () {
      $(this).find("td").removeClass("EvenBg");
    })
  })
}

//按钮效果
function ButtonHover() {
  $(".addBtn").hover(function () {
    $(this).addClass("addHover");
  }, function () {
    $(this).removeClass("addDefault");
  })
  $(".addBtn").mousedown(function () {
    $(this).removeClass("addHover").addClass("addActive");
  })
  $(".addBtn").mouseout(function () {
    $(this).removeClass("addActive").removeClass("addHover").addClass("addDefault");
  })

  $(".delBtn").hover(function () {
    $(this).addClass("delHover");
  }, function () {
    $(this).removeClass("delDefault");
  })
  $(".delBtn").mousedown(function () {
    $(this).removeClass("delHover").addClass("delActive");
  })
  $(".delBtn").mouseout(function () {
    $(this).removeClass("delActive").removeClass("delHover").addClass("delDefault");
  })

  $(".refreshBtn").hover(function () {
    $(this).addClass("refreshHover");
  }, function () {
    $(this).removeClass("refreshDefault");
  })
  $(".refreshBtn").mousedown(function () {
    $(this).removeClass("refreshHover").addClass("refreshActive");
  })
  $(".refreshBtn").mouseout(function () {
    $(this).removeClass("refreshActive").removeClass("refreshHover").addClass("refreshDefault");
  })

  $(".editBtn").hover(function () {
    $(this).addClass("editHover");
  }, function () {
    $(this).removeClass("editDefault");
  })
  $(".editBtn").mousedown(function () {
    $(this).removeClass("editDefault").addClass("editHover");
  })
  $(".editBtn").mouseout(function () {
    $(this).removeClass("editHover").addClass("editDefault");
  })

  $(".searchBtn").hover(function () {
    $(this).addClass("searchHover");
  }, function () {
    $(this).removeClass("searchDefault");
  })
  $(".searchBtn").mousedown(function () {
    $(this).removeClass("searchDefault").addClass("searchHover");
  })
  $(".searchBtn").mouseout(function () {
    $(this).removeClass("searchHover").addClass("searchDefault");
  })

  $(".smallBtn").hover(function () {
    $(this).addClass("smallHover");
  }, function () {
    $(this).removeClass("smallDefault");
  })
  $(".smallBtn").mousedown(function () {
    $(this).removeClass("smallDefault").addClass("smallHover");
  })
  $(".smallBtn").mouseout(function () {
    $(this).removeClass("smallHover").addClass("smallDefault");
  })

  $(".backBtn").hover(function () {
    $(this).addClass("backHover");
  }, function () {
    $(this).removeClass("backDefault");
  })
  $(".backBtn").mousedown(function () {
    $(this).removeClass("backHover").addClass("backActive");
  })
  $(".backBtn").mouseout(function () {
    $(this).removeClass("backActive").removeClass("backHover").addClass("backDefault");
  })

  $(".submitBtn").hover(function () {
    $(this).removeClass("submitDefault").addClass("submitHover");
  }, function () {
    $(this).removeClass("submitHover").addClass("submitDefault");
  })

  $(".cancelBtn").hover(function () {
    $(this).removeClass("cancelDefault").addClass("cancelHover");
  }, function () {
    $(this).removeClass("cancelHover").addClass("cancelDefault");
  })

}


//美化下拉菜单
function SelectStyle() {
  $(".header_search").each(function () {
    $(this).selectBox();
  });
}

//全选
function SelectAllCheckBox() {
  //$("#cbAll").click(function () {
  //    if ($("#cbAll").attr("checked") == "checked") {
  //        $("#DataList input[type='checkbox']").each(function (i) {
  //            $(this).attr("checked", true);
  //        })
  //    } else {
  //        $("#DataList input[type='checkbox']").each(function (i) {
  //            $(this).attr("checked", false);
  //        })
  //    }
  //})

  //$("#DataList input[type='checkbox']").each(function (i) {
  //    $(this).click(function () {
  //        checkIsChose();
  //    });
  //})

  $("#cbAll").click(function () {
    if ($("#cbAll").attr("checked") == "checked") {
      $(".dataTable input[type='checkbox']").each(function (i) {
        $(this).attr("checked", true);
      })
    } else {
      $(".dataTable input[type='checkbox']").each(function (i) {
        $(this).attr("checked", false);
      })
    }
  })

  $(".dataTable input[type='checkbox']").each(function (i) {
    $(this).click(function () {
      checkIsChose();
    });
  })
}

function checkIsChose() {
  var _i = 0;
  $(".dataTable input[type='checkbox']").each(function (i) {
    if ($(this).attr("checked") == "checked") {
      _i++;
    }
  })
  if (_i == 0) {
    $("#cbAll").attr("checked", false);
  }
}

function JHshNumberText() {
  if (!(((window.event.keyCode >= 48) && (window.event.keyCode <= 57))
	|| (window.event.keyCode == 13) || (window.event.keyCode == 46)
	|| (window.event.keyCode == 45))) {
    window.event.keyCode = 0;
  }
}

function OnlyNum(obj) {
  var object = obj;
  var reg = /^[0-9]+$/;
  var reg2 = /[^0-9]+/;
  var val = $(object).val();
  if (!reg.test(val)) {
    $(object).val(val.replace(reg2, ""));
  }
}
//正整数检查
function checkNum(id) {
  var reg = /^[0-9]+$/;
  var val = $("#" + id).val();
  if (!reg.test(val)) {
    return false;
  }
  else {
    return true;
  }
}


//弹出层
function ShowIdeaDialog(_Title, _Txt, _Width, _Height, _YesBtn, callFn) {
    var tips_height = parseInt(_Height) + 36;
    var tips_width = parseInt(_Width);
    var margin_top = -tips_height / 2;
    var margin_left = -tips_width / 2;

    var _BtnText = "";
    if (_YesBtn) {
        _BtnText = "<div class=\"BtnDiv\"><a href=\"javascript:void(0)\" onclick=\"" + callFn + "\" class=\"btn\">确定</a>&nbsp;&nbsp;<a href=\"javascript:void(0)\" onclick=\"HideIdeaDialog();" + callFn + "\" class=\"btn\">取消</a></div>";
    }

    var _IdeaDialog = "";
    _IdeaDialog += "<div class=\"IdeaDialogMask\"></div>\n";
    _IdeaDialog += "<div class=\"IdeaDialogDiv\" style=\"width:" + tips_width + "px; margin-top:" + margin_top + "px; margin-left:" + margin_left + "px;\">\n";
    _IdeaDialog += "	<div class=\"DialogTitle\"><strong>" + _Title + "</strong><a href=\"javascript:void(0);\" class=\"Close\" onclick=\"HideIdeaDialog()\" title=\"关闭\"></a></div>\n";
    _IdeaDialog += "    <div class=\"DialogCont\" style=\"height:" + parseInt(_Height) + "px\">" + _Txt + _BtnText + "</div>\n";
    _IdeaDialog += "</div>\n";

    $("body").append(_IdeaDialog);
}

//关闭层
function HideIdeaDialog() {
    $(".IdeaDialogMask").remove();
    $(".IdeaDialogDiv").remove();
}
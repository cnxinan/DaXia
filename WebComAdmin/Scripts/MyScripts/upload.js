//图片上传
function UploadFy(_ctrl,_hf,i) {
    $("#" + _ctrl).uploadify({
        'onComplete': function (event, queueID, fileObj, response, data) {
            $("#" + _hf).val(response);
            PreviewImg(response, Number(i - 1));
        },
        'multi': false,
        'sizeLimit': 10737418240
    });
}

//图片预览
function PreviewImg(_ImgUrl, i) {
    $(".PreviewTD").eq(i).html("<a href=\"" + _ImgUrl + "\" target=\"_blank\"><img src=\"" + _ImgUrl + "\" /></a>");
    $(".UpAndCancel").eq(i).hide();
    $(".DelDiv").eq(i).show();
}

//删除图片
function DeleteImg(i,_ctrl) {
    $.ajax({
        "type": "get",
        "url": "/ds_admin/ashx/deletefile.ashx?file=" + $("#" + _ctrl).val() + "&" + Math.random(),
        "success": function (data) {
            if (data == "1") {
                $(".PreviewTD").eq(Number(i - 1)).html("");
                $(".UpAndCancel").eq(Number(i - 1)).show();
                $(".DelDiv").eq(Number(i - 1)).hide();
                $("#Upload" + i + "_UpLoadID").val("");
            }
            else if (data == "2") {
                $(".PreviewTD").eq(Number(i - 1)).html("");
                $(".UpAndCancel").eq(Number(i - 1)).show();
                $(".DelDiv").eq(Number(i - 1)).hide();
                $("#Upload" + i + "_UpLoadID").val("");
            }
        }
    })
}

//修改页面图片加载
function CheckUpload(i) {
    var _ImgUrl = $("#Upload" + i + "_UpLoadID").val();
    if (_ImgUrl != "") {
        $(".DelDiv").eq(i - 1).show();
        $(".UpAndCancel").eq(i - 1).hide();
        $(".PreviewTD").eq(i - 1).html("<a href=\"" + _ImgUrl + "\" target=\"_blank\"><img src=\"" + _ImgUrl + "\" /></a>");
    }
}

//文件上传
function UploadFileFy(_ctrl, _hf, i) {
    $("#" + _ctrl).uploadify({
        'onComplete': function (event, queueID, fileObj, response, data) {
            $("#" + _hf).val(response);
            ShowFileName(response, Number(i - 1));
        },
        'multi': false,
        'sizeLimit': 10737418240
    });
}

//上传文件名显示
function ShowFileName(Path, i) {
    $(".FileIcon").eq(i).show();
    $(".FileIcon").eq(i).html("<a href=\"" + Path + "\" target=\"_blank\">" + Path + "</a>");
    $(".FileUpAndCancel").eq(i).hide();
    $(".FileDelDiv").eq(i).show();
}

//删除文件
function DeleteFile(i, _ctrl) {
    $.ajax({
        "type": "get",
        "url": "/ds_admin/ashx/deletefile.ashx?file=" + $("#" + _ctrl).val() + "&" + Math.random(),
        "success": function (data) {
            if (data == "1") {
                $(".FileIcon").eq(Number(i - 1)).html("请选择文件");
                $(".FileIcon").eq(Number(i - 1)).hide();
                $(".FileUpAndCancel").eq(Number(i - 1)).show();
                $(".FileDelDiv").eq(Number(i - 1)).hide();
                $("#File" + i + "_File").val("");
            }
            else if (data == "2") {
                $(".FileIcon").eq(Number(i - 1)).html("请选择文件");
                $(".FileIcon").eq(Number(i - 1)).hide();
                $(".FileUpAndCancel").eq(Number(i - 1)).show();
                $(".FileDelDiv").eq(Number(i - 1)).hide();
                $("#File" + i + "_File").val("");
            }
        }
    })
}

//修改页面文件加载
function CheckUploadFile(i) {
    var _FileName = $("#File" + i + "_File").val();
    if (_FileName != "") {
        $(".FileDelDiv").eq(i - 1).show();
        $(".FileUpAndCancel").eq(i - 1).hide();
        $(".FileIcon").eq(i - 1).show();
        $(".FileIcon").eq(i - 1).html("<a href=\"" + _FileName + "\" target=\"_blank\">" + _FileName + "</a>");
    }
}
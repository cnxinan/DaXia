var btnFn = function (e) {
    alert(e.target);
    return true;
};

//提示
function AlertDialog(_txt) {
    easyDialog.open({
        container: {
            header: '系统提示',
            content: _txt,
            yesFn: false,
            noFn: true
        }
    });
}

//处理结束后跳转
function showDialog(_txt, _url) {
    easyDialog.open({
        container: {
            header: '系统提示',
            content: _txt,
            yesFn: function () { location.href = _url; },
            noFn: false
        },
        callback: function () { location.href = _url; }
    });
}

//询问
function DoDialog(_txt, _url) {
    easyDialog.open({
        container: {
            header: '系统提示',
            content: _txt,
            yesFn: function () { location.href = _url; },
            noFn: true
        }
    });
}

//处理等待
function showWorking() {
    easyDialog.open({
        container: {
            content: '系统处理中，请稍候...'
        },
        autoClose: false
    });
}

//+问询
function askDialog(_txt) {
    var _Count = 0;
    $(".dataTable input[type='checkbox']").each(function (i) {

        if ($(this).attr("checked")) {
          _Count++;
    
          }

      })
    
    if (_Count > 0) {
      easyDialog.open({
        container: {
          header: '系统提示',
          content: _txt,
          yesFn: function () {
           // RealDeleteBtn.click();
            document.getElementById("RealDeleteBtn").click()
          },
          noFn: true
        }
      });
    }
    else {
        easyDialog.open({
            container: {
                header: '系统提示',
                content: '您未选择任何项目！',
                yesFn: function () { },
                noFn: false
            }
        });
    }
}

//+问询审批
function askDialogIsShow(_txt,tt) {

    return false;
//    var Result = AddInfoCheck();

//    if (Result) {
//        easyDialog.open({
//            container: {
//                header: '系统提示',
//                content: _txt,
//                yesFn: function () {
//                    // RealDeleteBtn.click();
//                    document.getElementById("btnIsShow").click()
//                },
//                noFn: true
//            }
//        });
//    }
//    else {
//        easyDialog.open({
//            container: {
//                header: '系统提示',
//                content: '您未选择任何项目！',
//                yesFn: function () { },
//                noFn: false
//            }
//        });
//    }
    
}


//function askDialog(_txt) {
//  var _Count = 0;
//  $(".dataTable input").each(function (i) {
//    if ($(this).attr("checked")) {
//      _Count++;
//    }
//  })
//  if (_Count > 0) {
//    easyDialog.open({
//      container: {
//        header: '系统提示',
//        content: _txt,
//        yesFn: function () {
//          RealDeleteBtn.click();
//        },
//        noFn: true
//      }
//    });
//  }
//  else {
//    easyDialog.open({
//      container: {
//        header: '系统提示',
//        content: '您未选择任何项目！',
//        yesFn: function () { },
//        noFn: false
//      }
//    });
//  }
//}

//邮件发送问询
function askSendMail(_txt, _name) {
    var _Count = 0;
    $(".dataTable input[type='checkbox']").each(function (i) {
        if ($(this).attr("checked")) {
            _Count++;
        }
    })
    if (_Count > 0) {
        SendMailing();
        __doPostBack(_name, "");
    }
    else {
        easyDialog.open({
            container: {
                header: '系统提示',
                content: '您未选择任何项目！',
                yesFn: function () { },
                noFn: false
            }
        });
    }
}
//邮件发送处理等待
function SendMailing() {
    easyDialog.open({
        container: {
            content: '邮件发送中，请稍候...'
        },
        autoClose: false
    });
}
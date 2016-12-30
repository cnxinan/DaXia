//新增信息验证
function AddInfoCheck() {
if ($("#InfoTitle").val() == "") {
AlertDialog("请输入岗位名称！");
$("#InfoTitle").focus();
return false;
}
showWorking();
return true;
}

//修改信息验证
function EditInfoCheck() {
if ($("#InfoTitle").val() == "") {
  AlertDialog("请输入岗位名称！");
$("#InfoTitle").focus();
return false;
}
showWorking();
return true;
}

//信息搜索
function InfoSearch() {
var _TypeID = $("#TypeID").val();
var _keyword = $("#KeyWords").val();
location.href = "index.aspx?Keyword=" + _keyword;

}


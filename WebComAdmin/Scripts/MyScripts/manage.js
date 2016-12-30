//新增管理员验证
function AddManageCheck() {
    if ($("#RoleID").val() == "") {
        AlertDialog("请选择所属角色！");
        $("#RoleID").focus();
        return false;
    }
    if ($("#UserName").val() == "") {
        AlertDialog("请输入用户名称！");
        $("#UserName").focus();
        return false;
    }
    if ($("#UserPwd").val() == "") {
        AlertDialog("请设置用户密码！");
        $("#UserPwd").focus();
        return false;
    }
    if ($("#UserPwd").val().length < 6 || $("#UserPwd").val().length > 20) {
        AlertDialog("密码长度为6～20个字符！");
        $("#UserPwd").focus();
        return false;
    }
    if ($("#ConfimPwd").val() == "") {
        AlertDialog("请再次输入新密码！");
        $("#ConfimPwd").focus();
        return false;
    }
    if ($("#UserPwd").val() != $("#ConfimPwd").val()) {
        AlertDialog("您两次输入的密码不一致！");
        $("#ConfimPwd").focus();
        return false;
    }
    if ($("#Email").val() != "") {
        if ($("#Email").val().search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) == -1) {
            AlertDialog("您输入的Email地址格式不正确！");
            $("#Email").focus();
            return false;
        }
    }
    showWorking();
    return true;
}

//修改管理员验证
function EditManageCheck() {
    if ($("#RoleID").val() == "") {
        AlertDialog("请选择所属角色！");
        $("#RoleID").focus();
        return false;
    }
    if ($("#UserName").val() == "") {
        AlertDialog("请输入用户名称！");
        $("#UserName").focus();
        return false;
    }
    if ($("#hfV").val() != "") {
        if ($("#UserPwd").val() == "") {
            AlertDialog("请设置用户密码！");
            $("#UserPwd").focus();
            return false;
        }
        if ($("#UserPwd").val().length < 6 || $("#UserPwd").val().length > 20) {
            AlertDialog("密码长度为6～20个字符！");
            $("#UserPwd").focus();
            return false;
        }
    }
    if ($("#Email").val() != "") {
        if ($("#Email").val().search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) == -1) {
            AlertDialog("您输入的Email地址格式不正确！");
            $("#Email").focus();
            return false;
        }
    }
    showWorking();
    return true;
}

//是否修改密码
function chgPassTR(i) {
    $("#hfV").val(i);
    if (i == 0) {
        $(".passTR").hide();
    }
    else {
        $(".passTR").show();
    }
}


//新增角色验证
function AddRoleCheck() {
    if ($("#RoleName").val() == "") {
        AlertDialog("请输入角色名称！");
        $("#RoleName").focus();
        return false;
    }
    showWorking();
    return true;
}

//修改角色验证
function EditRoleCheck() {
    if ($("#RoleName").val() == "") {
        AlertDialog("请输入角色名称！");
        $("#RoleName").focus();
        return false;
    }
    showWorking();
    return true;
}

//管理员搜索
function ManageUserSearch() {
    var _RoleID = $("#RoleID").val();
    var _keyword = $("#KeyWords").val();
    var _SortBy = $("#SortBy").val();
    var _SortType = $("#SortType").val();
    location.href = "user.aspx?RoleID=" + _RoleID + "&Keyword=" + _keyword + "&SortBy=" + _SortBy + "&SortType=" + _SortType;
}

//角色搜索
function ManageRoleSearch() {
    var _keyword = $("#KeyWords").val();
    var _SortBy = $("#SortBy").val();
    var _SortType = $("#SortType").val();
    location.href = "role.aspx?Keyword=" + _keyword + "&SortBy=" + _SortBy + "&SortType=" + _SortType;
}
//初始化绑定事件
$(function () {

})


//注销按钮
function loginOut() {
    $.messager.alert('提示', '确认注销？', 'error')
    $.cookie('username', null);
    $.cookie('password', null);
    CloseWebPage();
}

//点击查询数据
function btnGet() {
    $('#btnGet').bind('click', function () {
        
    });
}

function submit() {
    $('#submit').bind('click', function () {
        
    });
}
function cancleSubmit() {
    $('#cancleSubmit').bind('click', function () {
      
    });
}
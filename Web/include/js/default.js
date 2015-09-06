//此js文件中存放于页面有关的js文件，不可包含业务处理信息

function showmessager(title, message) {
    $.messager.show({
        title: title,
        msg: message,
        showType: 'show'
    });
}
//格式化文档中的日期控件
$.fn.datebox.defaults.formatter = function (date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
};
$.fn.datebox.defaults.parser = function (s) {
    if (!s) return new Date();
    var ss = s.split('-');
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
};
//全局操作
(function ($) {
    function getCacheContainer(t) {
        var view = $(t).closest('div.datagrid-view');
        var c = view.children('div.datagrid-editor-cache');
        if (!c.length) {
            c = $('<div class="datagrid-editor-cache" style="position:absolute;display:none"></div>').appendTo(view);
        }
        return c;
    }
    function getCacheEditor(t, field) {
        var c = getCacheContainer(t);
        return c.children('div.datagrid-editor-cache-' + field);
    }
    function setCacheEditor(t, field, editor) {
        var c = getCacheContainer(t);
        c.children('div.datagrid-editor-cache-' + field).remove();
        var e = $('<div class="datagrid-editor-cache-' + field + '"></div>').appendTo(c);
        e.append(editor);
    }

    var editors = $.fn.datagrid.defaults.editors;
    for (var editor in editors) {
        var opts = editors[editor];
        (function () {
            var init = opts.init;
            opts.init = function (container, options) {
                var field = $(container).closest('td[field]').attr('field');
                var ed = getCacheEditor(container, field);
                if (ed.length) {
                    ed.appendTo(container);
                    return ed.find('.datagrid-editable-input');
                } else {
                    return init(container, options);
                }
            }
        })();
        (function () {
            var destroy = opts.destroy;
            opts.destroy = function (target) {
                if ($(target).hasClass('datagrid-editable-input')) {
                    var field = $(target).closest('td[field]').attr('field');
                    setCacheEditor(target, field, $(target).parent().children());
                } else if (destroy) {
                    destroy(target);
                }
            }
        })();
    }
})(jQuery);

function removeSelections() {
    var rows = $('#sampleSourceDataGrid').datagrid("getSelections");
    var copyRows = [];
    for (var j = 0; j < rows.length; j++) { copyRows.push(rows[j]); }
    for (var i = 0; i < copyRows.length; i++) {
        var index = $('#sampleSourceDataGrid').datagrid('getRowIndex', copyRows[i]);
        $('#sampleSourceDataGrid').datagrid('deleteRow', index);
    }
}

function isEmptyStr(str) {
    if (str == '' || str == null) {
        return true;
    } else {
        return false;
    }
}

function CloseWebPage() {
    if (navigator.userAgent.indexOf("MSIE") > 0) {
        if (navigator.userAgent.indexOf("MSIE 6.0") > 0) {
            window.opener = null;
            window.close();
        } else {
            window.open('', '_top');
            window.top.close();
        }
    }
    else if (navigator.userAgent.indexOf("Firefox") > 0) {
        window.location.href = 'about:blank ';
    } else {
        window.opener = null;
        window.open('', '_self', '');
        window.close();
    }
}
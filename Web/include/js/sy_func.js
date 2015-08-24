//设置页面DataGrid分页
function pagerFilter(data) {
    if (typeof data.length == 'number' && typeof data.splice == 'function') {	// is array
        data = { total: data.length, rows: data }
    }
    var dg = $(this);
    var opts = dg.datagrid('options');
    var pager = dg.datagrid('getPager');
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            opts.pageNumber = pageNum;
            opts.pageSize = pageSize;
            pager.pagination('refresh', { pageNumber: pageNum, pageSize: pageSize });
            dg.datagrid('loadData', data);
        }
    });
    if (!data.originalRows) { data.originalRows = (data.rows); }
    var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
    var end = start + parseInt(opts.pageSize);
    data.rows = (data.originalRows.slice(start, end));
    return data;
}

//绑定数据
function querybycode() {
    clearForm();
    var codeType = $('#codeType').combobox('getValue');
    var code = $('#code').textbox('getValue');//获取数据源
    if (/.*[\u4e00-\u9fa5]+.*$/.test(code)) { $.messager.alert('错误', '不能输入中文', 'error'); $('#In_Code').textbox('clear'); return; }
    if (code.length > 14) { $.messager.alert('错误', '条码最高不能超过15位', 'error'); $('#In_Code').textbox('clear'); return; }
    if (isEmptyStr(codeType) || isEmptyStr(code)) { $.messager.alert('提示', '请检查条码类型和条码号', 'error'); }
    else {
        ajaxLoading();
        $.ajax({
            type: 'GET',
            url: '/Sever/EmpiInfo.ashx?mode=qry&Mzzybz=' + codeType + '&Mzhzyh=' + code,
            onSubmit: function () { },
            success: function (data) {
                ajaxLoadEnd();
                $('#code').textbox('setValue', '');
                clearForm();
                if (!data) { $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error') }
                else {
                    //测试代码
                    var obj = $.parseJSON(data);
                    if (obj.Statu == "err") {
                        $.messager.alert('提示', obj.Msg, 'error')
                        return;
                    }
                    else {
                        $("#BaseInfoForm").form("load", obj.Data);
                        $('#oldCode').textbox('setValue', code);
                        //   $('#oldCodeType').textbox('setValue', codeType);
                        GetNormalLisReportInfo();
                        GetPatientDiagnoseInfo();
                        //发送请求请求临床数据
                        //请求条件？1、条码号；2、日期
                    }
                }
            }
        });
    }
}
//增加月 
function AddMonths(d, value) {
    d.setMonth(d.getMonth() + value);
    return d;
}
//增加天 
function AddDays(d, value) {
    d.setDate(d.getDate() + value);
    return d;
}
//增加时
function AddHours(d, value) {
    d.setHours(d.getHours() + value);
    return d;
}


function GetNormalLisReportInfo() {
    //<Request>
    //  <hospnum></hospnum>
    //  <ksrq00></ksrq00>
    //  <jsrq00></jsrq00>
    //</Request>
    var code = $('#oldCode').textbox('getValue');
    if (code) {
        //开始查询日期为当前日期前五天
        var ksrq00 = AddDays(new Date(), -5);
        //结束日期为当前日期后一天
        var jsrq00 = AddDays(new Date(), 1);
        $.ajax({
            type: "POST",
            url: "/Sever/NormalLisReport.ashx",
            data: {
                "mode": "qry",
                "code": code,
                "ksrq00": ksrq00,
                "jsrq00": jsrq00
            },
            success: function (data) {
                $('#NormalLisReportDg').datagrid("loading");
                if (!data) { $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error') }
                else {
                    var obj = $.parseJSON(data);
                    if (obj.Statu == "err") {
                        $.messager.alert('提示', obj.Msg, 'error')
                        return;
                    }
                    else {
                        $('#NormalLisReportDg').datagrid("loadData", obj.Data);
                        var row = $('#NormalLisReportDg').datagrid('getRows');
                    }
                }
                $('#NormalLisReportDg').datagrid("loaded");
            }
        });
    }
}

function GetPatientDiagnoseInfo() {
    //<Request>
    //  <cardno></cardno>
    //  <cxrq00></cxrq00>
    //</Request>
    var code = $('#oldCode').textbox('getValue');
    if (code) {
        var cxrq00 = AddDays(new Date(), 0);
        $.ajax({
            type: "POST",
            url: "/Sever/PatientDiagnose.ashx",
            data: {
                "mode": "qry",
                "code": code,
                "cxrq00": cxrq00
            },
            success: function (data) {
                if (!data) { $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error') }
                else {
                    var obj = $.parseJSON(data);
                    if (obj.Statu == "err") {
                        $.messager.alert('提示', obj.msg, 'error')
                        return;
                    }
                    else {
                        $('#PatientDiagnoseForm').form("load", obj.Data);
                    }
                }
            }
        });
    }
}
//清除控件值
function clearForm() {
    $('#BaseInfoForm').form('clear');
    $('#NormalLisReportDg').datagrid('loadData', { total: 0, rows: [] });
    $('#NormalLisReportForm').form('clear');
    $('#NormalLisReportForm').form('clear');
}

//条码框按钮回车事件
$(function () {
    $("input", $("#code").next("span")).keydown(function (e) {
        if (e.keyCode == 13) { querybycode(); }
    });
});
//ESC事件,点击ESC后清空所有值
$(document).keyup(function (e) {
    var key = e.which;
    if (key == 27
        ) {
        clearForm();
    }
});
//F2快捷键
$(document).keyup(function (e) {
    var key = e.which;
    if (key == 113) {
        postPatientInfo();
    }
});
//点击确定按钮提交请求
function getdatabybarcode() {
    var code = $('#barcodebox').textbox('getValue');
    if ($.trim(code)) { barcode(code); var code = $('#barcodebox').textbox('clear'); }
    var code = $('#barcodebox').textbox('clear');
}
//POST数据
function postPatientInfo() {
    var name = $('#_80').textbox('getText');
    var hzid = $('#_91').textbox('getText');
    var username = $.cookie('username');
    var departments = $.cookie(username + 'department');
    if (!departments) { $.messager.alert('提示', '必须选择科室', 'error'); return; }
    if (name == "") { $.messager.alert('提示', '必须输入姓名', 'error'); return; }
    else
    {
        var _baseinfo = getBaseInfoFormData();
        //ClinicalInfoDg
        var _ClinicalInfoDg = $('#ClinicalInfoDg').datagrid('getChecked');
        if (_ClinicalInfoDg.length <= 0) {
            $.messager.alert('提示', '未选择诊断信息或诊断信息为空', 'error'); return;
        }
        if (_ClinicalInfoDg) {
            for (var i = 0; i < _ClinicalInfoDg.length - 1; i++) {
                if (_ClinicalInfoDg[i].DiagnoseDateTime == "") { $.messager.alert('提示', '请选择诊断日期', 'error'); return; }
            }
        }
        var rowClinicalInfoDg = JSON.stringify(_ClinicalInfoDg);
        var _sampleInfoForm = getSampleInfoFormData();
        var _dg_SampleInfoDg = $('#dg_SampleInfo').datagrid('getRows');
        if (!_dg_SampleInfoDg || _dg_SampleInfoDg == '[]' || _dg_SampleInfoDg.length <= 0) {
            $.messager.alert('提示', '请添加样本信息', 'error'); return;
        }
        if (_dg_SampleInfoDg.length > 0) {
            for (var i = 0; i < _dg_SampleInfoDg.length; i++) {
                _dg_SampleInfoDg[i].num = i + 1;
            }
        }
        var _dg_SampleInfo = JSON.stringify(_dg_SampleInfoDg);
        ajaxLoading();
        $.ajax({
            type: 'post',
            dataType: "json",
            url: '/Fp_Ajax/SubmitData.aspx?action=postPatientinfo',
            data: {
                departments: departments,
                baseinfo: _baseinfo,
                clinicalInfoDg: rowClinicalInfoDg,
                sampleInfoForm: _sampleInfoForm,
                dg_SampleInfo: _dg_SampleInfo
            },
            onSubmit: function () {
            },
            success: function (data) {
                ajaxLoadEnd();
                if (data) {
                    var baseinfoData = data._baseInfo;
                    var clinicalInfoData = data._clinicalInfo;
                    var dg_SampleInfoData = data._dg_SampleInfo;
                    var baseinfo;
                    var clinicalInfo;
                    var dg_SampleInfo;
                    if (baseinfoData) { baseinfo = $.parseJSON(baseinfoData); }
                    if (clinicalInfoData) { clinicalInfo = $.parseJSON(clinicalInfoData); }
                    if (dg_SampleInfoData) { dg_SampleInfo = $.parseJSON(dg_SampleInfoData); }
                    if (baseinfo.success == "true") {
                        if (clinicalInfo.success == "true") {
                            if (dg_SampleInfo.success == "true") {
                                $.messager.show({ title: '提示！', msg: '导入成功：' + dg_SampleInfoData.msg, showType: 'show' });
                            }
                            else {
                                $.messager.show({ title: '提示！', msg: '样本添加失败：' + dg_SampleInfoData.msg, showType: 'show' });
                            }
                        }
                        else {
                            $.messager.show({ title: '提示！', msg: '临床信息导入失败：' + clinicalInfoData.msg, showType: 'show' });
                        }
                    }
                    else {
                        $.messager.show({ title: '提示！', msg: '基本信息导入失败：' + baseinfoData.msg, showType: 'show' });
                    }
                }
                else { $.messager.alert('提示', '服务器未响应', 'error'); return; }
            }
        });
    }
}
function getBaseInfoFormData() {
    var baseInfoForm = $("#BaseInfoForm").serializeArray();
    var Tem;
    if (baseInfoForm) { Tem = JSON.stringify(baseInfoForm); }
    return Tem;
}
function getSampleInfoFormData() {
    var sampleInfo = $("#SampleInfoForm").serializeArray();
    var Tem;
    if (sampleInfo) { Tem = JSON.stringify(sampleInfo); }
    return Tem;
}
function getSampleInfoFormData() {
    var sampleinfo = $("#SampleInfoForm").serializeArray();
    var ii = $("#_116").combobox('getText');
    var base;
    if (sampleinfo) { base = JSON.stringify(sampleinfo); }
    return base;
}
//添加值到ClinicalInfoDg
function submitFormClinicalInfoDg() {
    //验证当前表单？？
    var isValid = $('#setClinicalInfoDg').form('validate');
    if (isValid) {
        var from = $('#setClinicalInfoDg').serializeArray();
        $('#ClinicalInfoDg').datagrid('insertRow', {
            index: 1,	// 索引从0开始
            row: {
                DiagnoseTypeFlag: from[0].value,
                DiagnoseDateTime: from[1].value,
                ICDCode: from[2].value,
                DiseaseName: from[3].value,
                Description: from[4].value
            }
        });
        $('#setClinicalInfoDg').form('clear');
        $('#w').window('close');
    }
}
function clearsetClinicalInfoDg() {
    $('#setClinicalInfoDg').form('clear');
}
//添加样本信息到Dg
function AddSampleInfoToDg() {
    var isValid = $('#sampleInfoFormToDg').form('validate');
    if (isValid) {
        var from = $('#sampleInfoFormToDg').serializeArray();
        $('#dg_SampleInfo').datagrid('insertRow', {
            index: 1,	// 索引从0开始
            row: {
                SampleType: from[0].value,
                Volume: from[1].value,
                Scount: from[2].value
                //Organ: from[3].value,
                //OrganSubdivision: from[4].value
            }
        });
        //$('#sampleInfoFormToDg').form('clear');
        //$('#addSampleForm').window('close');
    }
}
function clearSampleInfoAddForm() {
    $('#sampleInfoFormToDg').form('clear');
}


//采用jquery easyui loading css效果 
function ajaxLoading() {
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%", height: $(window).height() }).appendTo("body");
    $("<div class=\"datagrid-mask-msg\"></div>").html("正在处理，请稍候。。。").appendTo("body").css({ display: "block", left: ($(document.body).outerWidth(true) - 190) / 2, top: ($(window).height() - 45) / 2 });
}
function ajaxLoadEnd() {
    $(".datagrid-mask").remove();
    $(".datagrid-mask-msg").remove();
}
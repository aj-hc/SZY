//输入框默认值
$(function () {
    //设置时间
    var begindate = new Date();
    //采集日期
    $('#_103').textbox('setValue', myformatter(begindate))
    var time = begindate.getHours() + ":" + begindate.getMinutes()
    $('#_104').textbox('setValue', time);
});
//格式化日期控件
function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}
//设置页面DataGrid分页
function pagerFilter(data) {
    if (typeof data.length == 'number' && typeof data.splice == 'function') {	// is array
        data = {total: data.length,rows: data}
        }
    var dg = $(this);
    var opts = dg.datagrid('options');
    var pager = dg.datagrid('getPager');
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            opts.pageNumber = pageNum;
            opts.pageSize = pageSize;
            pager.pagination('refresh', {pageNumber: pageNum,pageSize: pageSize});
            dg.datagrid('loadData', data);
        }
    });
    if (!data.originalRows) {data.originalRows = (data.rows);}
    var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
    var end = start + parseInt(opts.pageSize);
    data.rows = (data.originalRows.slice(start, end));
    return data;
}
//格式化日期
function myparser(s) {
    if (!s) return new Date();
    var ss = (s.split('-'));
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) { return new Date(y, m - 1, d); }
    else {return new Date();}
}
//绑定数据
function querybycode() {
    var codeType = $('#codeType').combobox('getValue');
    var code = $('#code').textbox('getValue');//获取数据源
    if (/.*[\u4e00-\u9fa5]+.*$/.test(code)) { $.messager.alert('错误', '不能输入中文', 'error'); $('#In_Code').textbox('clear'); return; }
    if (code.length > 14) { $.messager.alert('错误', '条码最高不能超过15位', 'error'); $('#In_Code').textbox('clear'); return; }
    if (isEmptyStr(codeType) || isEmptyStr(code)) { $.messager.alert('提示', '请检查条码类型和条码号', 'error'); }
    else {
        ajaxLoading();
        $.ajax({
            type: 'GET',
            url: '/Sever/GetDataSever.ashx?action=gethisdata&Mzzybz=' + codeType + '&Mzhzyh=' + code,
            onSubmit: function () { },
            success: function (data) {
                ajaxLoadEnd();
                $('#code').textbox('setValue', '');
                clearForm();
                if (!data) { $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error')}
                else {
                    //测试代码
                    var obj = $.parseJSON(data);
                    if (!obj.success) {
                        $.messager.alert('提示', obj.msg, 'error')
                    }
                    if (obj._BaseInfo) {
                        if (obj._BaseInfo.ds) {
                            var ds = obj._BaseInfo.ds;
                            //$("#BaseInfoForm").form("load", ds[0]);
                            AddBaseInfoToForm(ds[0]);
                        }
                    }
                    if (obj._ClinicalInfo) {
                        if (obj._ClinicalInfo.ds) {
                            for (var i = 0; i < obj._ClinicalInfo.ds.length ; i++) {
                                var text = obj._ClinicalInfo.ds[i].DiagnoseDateTime.substring(0, 10);
                                obj._ClinicalInfo.ds[i].DiagnoseDateTime = text
                            }
                            var ds = obj._ClinicalInfo.ds
                            $('#ClinicalInfoDg').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', ds).datagrid('reload');
                        }
                    }
                    //测试end
                    //将数据转换成json对象 正式
                    //var obj = $.parseJSON(data);
                    //$('#In_Code').textbox('clear');
                    //if (obj._BaseInfo)
                    //{
                    //    var _BaseInfo = $.parseJSON(obj._BaseInfo);
                    //    if (_BaseInfo.ds)
                    //    {
                    //        var ds = _BaseInfo.ds;
                    //        if(ds[0]==undefined)
                    //        {
                    //            $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error');
                    //            return;
                    //        }
                    //        AddBaseInfoToForm(ds[0]);
                    //    }
                    //}
                    //if (obj._ClinicalInfo)
                    //{
                    //    var _ClinicalInfo = $.parseJSON(obj._ClinicalInfo);

                    //        if (_ClinicalInfo.ds) {
                    //            var ds = _ClinicalInfo.ds
                    //            if (ds[0].msg) {
                    //                $.messager.alert('提示', ds[0].msg);
                    //        }
                    //            else
                    //            {
                    //                for (var i = 0; i < ds.length; i++) {
                    //                    var text = _ClinicalInfo.ds[i].DiagnoseDateTime.substring(0, 10);
                    //                    _ClinicalInfo.ds[i].DiagnoseDateTime = text; 
                    //                    switch (_ClinicalInfo.ds[i].DiagnoseTypeFlag)
                    //                    {
                    //                        case 0: _ClinicalInfo.ds[i].DiagnoseTypeFlag = "门诊诊断"; break;
                    //                        case 1: _ClinicalInfo.ds[i].DiagnoseTypeFlag = "入院诊断"; break;
                    //                        case 2: _ClinicalInfo.ds[i].DiagnoseTypeFlag = "出院主要诊断"; break;
                    //                        case 3: _ClinicalInfo.ds[i].DiagnoseTypeFlag = "出院次要诊断"; break;
                    //                        case 12: _ClinicalInfo.ds[i].DiagnoseTypeFlag = "病理诊断"; break;
                    //                        default: _ClinicalInfo.ds[i].DiagnoseTypeFlag = "未知诊断"; break;
                    //                    };
                    //                }
                    //        $('#ClinicalInfoDg').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', ds).datagrid('reload');
                    //    }
                    //}
                    //}
                    //正式END
                }
            }
        });
    }
}
//清除控件值
function clearForm() {
    $('#BaseInfoForm').form('clear');
    $('#ClinicalInfoDg').datagrid('loadData', { total: 0, rows: [] });
}

//绑定数据到基本信息数据框
function AddBaseInfoToForm(_BaseInfo)
{
    if (_BaseInfo=="SEE")
    {
        $("#_80").textbox('readonly', false);
        $("#_81").textbox('readonly',false);
        $("#_82").textbox('readonly',false);
        $("#_115").textbox('readonly',false);
        $("#_84").textbox('readonly', false);
        $("#_116").textbox('readonly', false);
        $("#_86").textbox('readonly', false);
        $("#_87").textbox('readonly', false);
        $("#_88").textbox('readonly', false);
        $("#_89").textbox('readonly', false);
        $("#_90").textbox('readonly', false);
        $("#_91").textbox('readonly', false);
        $("#_92").textbox('readonly', false);
        $("#_93").textbox('readonly', false);
    }
    if (_BaseInfo)
    {
        if (_BaseInfo['PatientName'] && _BaseInfo['PatientName']!="")
        {
            $("#_80").textbox('setValue', $.trim(_BaseInfo['PatientName']));
            $("#_80").textbox('readonly');
        }
        if (_BaseInfo['IPSeqNoText'])
        {
            $("#_81").textbox('setValue', $.trim(_BaseInfo['IPSeqNoText']));
            $("#_81").textbox('readonly');
        }
        if (_BaseInfo['PatientCardNo'])
        {
            $("#_82").textbox('setValue', $.trim(_BaseInfo['PatientCardNo']));
            $("#_82").textbox('readonly');
        }
        if (_BaseInfo['SexFlag'] || _BaseInfo['SexFlag']==0)
        {
            var data = $('#_115').combobox('getData');
            var SexFlag = _BaseInfo['SexFlag'];
            if (data.length > 0)
            {
                for (var tem in data)
                {
                    if (data[tem].SexFlag == SexFlag) { $("#_115").combobox('select', data[tem].text); }
                }
            }
            $("#_115").textbox('readonly');
        }
        if (_BaseInfo['BirthDay'])
        {
            var Birthday = _BaseInfo['BirthDay'].substring(0, 10);
            $("#_84").datebox('setValue', Birthday);
            $("#_84").textbox('readonly');
        }
        if (_BaseInfo['BloodTypeFlag'] || _BaseInfo['BloodTypeFlag']==0)
        {
            var BloodTypeFlag = _BaseInfo['BloodTypeFlag'];
            if (BloodTypeFlag==0){BloodTypeFlag = 6;}
            var data = $('#_116').combobox('getData');
            if (data.length > 0)
            {
                for (var tem in data)
                {
                    if (data[tem].BloodTypeFlag == BloodTypeFlag) { $("#_116").combobox('select', data[tem].text); }
                    
                }
                $("#_116").textbox('readonly');
            }
        }
        if (_BaseInfo['Phone'])
        {
            $("#_86").textbox('setValue', $.trim(_BaseInfo['Phone']));
            $("#_86").textbox('readonly');
        }
        if (_BaseInfo['ContactPhone'])
        {
            $("#_87").textbox('setValue', $.trim(_BaseInfo['ContactPhone']));
            $("#_87").textbox('readonly');
        }
        if (_BaseInfo['ContactPerson'])
        {
            $("#_88").textbox('setValue', $.trim(_BaseInfo['ContactPerson']));
            $("#_88").textbox('readonly');
        }
        if (_BaseInfo['NativePlace'])
        {
            $("#_89").textbox('setValue', $.trim(_BaseInfo['NativePlace']));
            $("#_89").textbox('readonly');
        }
        if (_BaseInfo['RegisterSeqNO'])
        {
            $("#_90").textbox('setValue', $.trim(_BaseInfo['RegisterSeqNO']));
            $("#_90").textbox('readonly');
        }
        if (_BaseInfo['PatientID'])
        {
            $("#_91").textbox('setValue', $.trim(_BaseInfo['PatientID']));
            $("#_91").textbox('readonly');
        }
        if (_BaseInfo['RegisterID'])
        {
            $("#_92").textbox('setValue', $.trim(_BaseInfo['RegisterID']));
            $("#_92").textbox('readonly');
        }
        if (_BaseInfo['InPatientID'])
        {
            $("#_93").textbox('setValue', $.trim(_BaseInfo['InPatientID']));
            $("#_93").textbox('readonly');
        }
    }
    else {$.messager.alert('提示', '这个编号没有数据', 'error'); }
}
//条码框按钮回车事件
$(function () {
    $("input", $("#In_Code").next("span")).keydown(function (e) {
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
    if ($.trim(code)) {barcode(code);var code = $('#barcodebox').textbox('clear');}
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
        if (_ClinicalInfoDg.length <= 0)
        {
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
        if (!_dg_SampleInfoDg || _dg_SampleInfoDg == '[]' || _dg_SampleInfoDg.length<=0) {
            $.messager.alert('提示', '请添加样本信息', 'error'); return;
        }
        if (_dg_SampleInfoDg.length>0)
        {
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
                    if (baseinfoData) {baseinfo = $.parseJSON(baseinfoData);}
                    if (clinicalInfoData) {clinicalInfo = $.parseJSON(clinicalInfoData);}
                    if (dg_SampleInfoData) {dg_SampleInfo = $.parseJSON(dg_SampleInfoData);}
                    if (baseinfo.success == "true")
                    {
                        if (clinicalInfo.success == "true")
                        {
                            if (dg_SampleInfo.success == "true")
                                {
                                $.messager.show({ title: '提示！', msg: '导入成功：' + dg_SampleInfoData.msg, showType: 'show' });
                    }
                                    else
                                    {
                                $.messager.show({ title: '提示！', msg: '样本添加失败：' + dg_SampleInfoData.msg, showType: 'show' });
                }
                                    }
                            else
                            {
                            $.messager.show({ title: '提示！', msg: '临床信息导入失败：' + clinicalInfoData.msg, showType: 'show' });
                            }
                        }
                        else
                        {
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

//提交单条样品
function ForSubmitSampleInfo()
{
    var username = $.cookie('username');
    var departments = $.cookie(username + 'department');
    if (!departments) { $.messager.alert('提示', '必须选择科室', 'error'); return; }
    var _baseinfo = getBaseInfoFormData();
    //ClinicalInfoDg
    var _ClinicalInfoDg = $('#ClinicalInfoDg').datagrid('getChecked');
    var _sampleInfoForm = getSampleInfoFormData();
    var _dg_SampleInfoDg = $('#dg_SampleInfo').datagrid('getSelected');
    if (_dg_SampleInfoDg == null) { $.messager.alert('提示', '请选中提交行再进行提交', 'error'); return; };
   // var =_dg_SampleInfoDg.State.ind
    if (_dg_SampleInfoDg.State.indexOf("成功") > 0) { $.messager.alert('提示', '只能选取可以提交的数据，且每次只能一条！！', 'error'); return; }
    if (_dg_SampleInfoDg.State.indexOf("重新提交") > 0)
    {
        var num = $('#dg_SampleInfo').datagrid('getRowIndex', $('#dg_SampleInfo').datagrid('getSelected')) + 1;
        $.ajax({
            type: 'post',
            dataType: "json",
            url: '/Fp_Ajax/SubmitData.aspx?action=posSingleData',
            data: {
                departments: departments,
                baseinfo: _baseinfo,
                clinicalInfoDg: _ClinicalInfoDg,
                sampleInfoForm: _sampleInfoForm,
                dg_SampleInfo: _dg_SampleInfoDg
            },
            onSubmit: function () { },
            success: function (data) {
                if (data) {
                    if (data[0].success == "True")
                    {
                        msg = msg + "<br />" + "第" + data[0].num + "行：" + "导入成功";
                        $('#dg_SampleInfo').datagrid('updateRow', {
                            index: data[0].num - 1,
                            row:
                                {
                                    State: '成功',
                                    Msg: data[0].msg
                                }
                        });
                    }
                }
                else { $.messager.alert('提示', '服务器未响应', 'error'); return; }
            }
        });
    }

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
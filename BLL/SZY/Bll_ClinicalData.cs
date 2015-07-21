using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;

namespace RuRo.BLL
{
    public class Bll_ClinicalData
    {
        //Model.SZY_FpExtendEntities sZY_FpExtendEntities = new Model.SZY_FpExtendEntities();

        //DAL.ClinicalData ClinicalData = new DAL.ClinicalData();

        //GetDataFromHospital getDataFromHospital = new GetDataFromHospital();

        //SampleSocrce sampleSocrce = new SampleSocrce();

        //Samples samples = new Samples();
        ////00.调用方法获取FP中样本信息，并添加临床数据
        //string username = Common.UserHelper.GetUserName();
        ///// <summary>
        ///// 批量添加临床数据（先有样本再有数据）
        ///// </summary>
        //public void AddSample_Clinical_DataByDateQuery()
        //{
        //    DateTime datetime = new DateTime();
        //    datetime = DateTime.Parse(GetUserNameTime());//获取当前登录用户上次添加样本的时间--此时间主要用于避免重复从FP中获取数据
        //    string date = string.Format("{0},{1}", datetime.AddDays(-1).ToString("yyyy.MM.dd"), DateTime.Now.ToString("yyyy.MM.dd"));//上次查询的时间的前一天及当前日期的数据
        //    List<Model.Sample_Info> sampleByDate = GetSampleByDate(date);//使用日期查询FP中的样本数据
        //    //将数据保存至本地数据库
        //    foreach (Model.Sample_Info sample_Info in sampleByDate)
        //    {
        //        Model.Sample_Clinical_Data sample_Clinical_Data = ConvertSample_InfoToSample_Clinical_Data(sample_Info);
        //        bool checkReasult = CheckSample_Clinical_DataInLocalDatabase(sample_Clinical_Data, sample_Info);
        //        if (!checkReasult)//不存在
        //        {
        //            //本地数据库无数据
        //            int addSample_Clinical_DataReasult = ClinicalData.AddSample_Clinical_Data(sample_Clinical_Data);
        //        }
        //    }
        //    List<Model.Sample_Clinical_Data> kk = ClinicalData.GetSample_Clinical_DataListBy(a => a.statu != "OK" || a.statu != "NN");//ok有数据、NN无数据--针对查询10天都没有数据的样本
        //    if (kk != null)
        //    {
        //        if (kk.Count > 0)
        //        {
        //            foreach (Model.Sample_Clinical_Data sample_Clinical_Data in kk)
        //            {
        //                if (DateTime.Parse(sample_Clinical_Data.created_at) < DateTime.Now.AddDays(-5))//创建时间是5天以前
        //                {
        //                    //调用方法查询数据
        //                    if (!string.IsNullOrEmpty(sample_Clinical_Data.sample_source_id))//样本信息有样本源
        //                    {
        //                        string sampleSourceName = sampleSocrce.Get_Sample_Source_Info(Convert.ToInt32(sample_Clinical_Data.sample_source_id)).name;
        //                        List<Common.Record> recordList = GetNeedRecord(DateTime.Parse(sample_Clinical_Data.created_at), sampleSourceName);//根据样本创建时间前后五天以及样本对应的患者姓名查询临床数据
        //                        //判断该指标数据库中是否存在--根据日期指标名结果判断 
        //                        if (recordList!=null)
        //                        {
        //                            if (recordList.Count>0)
        //                            {
        //                                foreach (Common.Record item in recordList)
        //                                {
        //                                    bool ck = CheckNormalLisReportInLocalDatabase(item);
        //                                    if (!ck)//本地数据库中没有数据
        //                                    {
        //                                        //01.保存数据到本地数据库
        //                                        //02.将数据提交到FP
        //                                    }
        //                                }
        //                            }
        //                        }
                                
        //                    }

        //                    BLL.ClinicalData.PacsLisReportServices c = new BLL.ClinicalData.PacsLisReportServices();
        //                    c.GetPatientDiagnose("<Request><cardno>64494521</cardno><cxrq00>2015.04.21</cxrq00></Request>");//获取诊断数据
        //                    //调用方法提交数据--一次性查询（查询范围是创建时间的前后5天）
        //                    //调用方法保存数据到数据库、无数据就改变状态为"NN"(no data)
        //                    //本地数据库数据库如果有数据就改变状态为"OK"
        //                }
        //                else//样本创建时间是5天内的
        //                {
        //                    //调用方法查询数据
        //                    //调用方法提交数据--以后还要查询
        //                    //调用方法保存数据到数据库--添加或更新不改变状态
        //                }
        //            }
        //        }

        //    }

        //    #region MyRegion
        //    ////01.循环遍历每一个样本，将样本信息保存至本地数据库（添加或更新）
        //    //int result = AddSampleInfo(sample_Info);
        //    //if (!string.IsNullOrEmpty(sample_Info.source_id))
        //    //{
        //    //    //02.循环遍历时调用webservice查询临床检测指标，并查看本地数据库中是否有次指标（根据时间、指标名称、样本源名称判断）
        //    //    //02.0 获取异常指标
        //    //    //02.1 循环指标判断是否有--有就忽略，没有就添加
        //    //    string requestMzhzyh = sampleSocrce.Get_Sample_Source_Info(Convert.ToInt32(sample_Info.id)).name;
        //    //    List<Common.Record> recordList = GetNeedRecord(DateTime.Parse(sample_Info.created_at), requestMzhzyh);
        //    //    if (recordList.Count > 0)//有异常指标
        //    //    {
        //    //        foreach (Common.Record record in recordList)
        //    //        {
        //    //            Model.NormalLisReport normalLisReport = ConvertNeedRecordToNormalLisReport(record);
        //    //            List<Model.NormalLisReport> normalLisReportList = ClinicalData.GetNormalLisReporListByt(a => a.hospnum == normalLisReport.hospnum && a.check_date == normalLisReport.check_date && a.chinese == normalLisReport.chinese);
        //    //            if (normalLisReportList.Count == 0)//没有数据
        //    //            {
        //    //                int addNormalLisReportResult = ClinicalData.AddNormalLisReport(normalLisReport);//添加临床数据到数据库
        //    //                //调用fp方法将数据添加到临床数据模块

        //    //            }
        //    //        }
        //    //        //判断此指标在本地数据库中是否存在

        //    //    }
        //    //    //02.2 指标存在就将此条样本数据保存至本地数据库并且修改状态为Y--已添加
        //    //    //02.3 如果指标不存在就忽略
        //    //    //03.调用本地数据库中需要更新的数据更新临床数据
        //    //} 
        //    #endregion
        //}

        //private bool CheckSample_Clinical_DataInLocalDatabase(Model.Sample_Clinical_Data sample_Clinical_Data, Model.Sample_Info sample_Info)
        //{
        //    bool result = false; //不存在
        //    List<Model.Sample_Clinical_Data> sample_Clinical_DataList = ClinicalData.GetSample_Clinical_DataListBy(a => a.sample_id == sample_Info.id);
        //    if (sample_Clinical_DataList != null)
        //    {
        //        if (sample_Clinical_DataList.Count > 0)
        //        {
        //            result = true;
        //        }
        //    }
        //    return result;
        //}

        //private bool CheckNormalLisReportInLocalDatabase(Common.Record needRecord)
        //{
        //    bool result = false; //不存在
        //    List<Model.NormalLisReport> localNormalLisReportList = ClinicalData.GetNormalLisReporListBy(a=>a.hospnum==needRecord.hospnum&&a.ext_mthd==needRecord.ext_mthd&&a.chinese==needRecord.chinese&&a.check_date==needRecord.check_date);
        //    if (localNormalLisReportList != null)
        //    {
        //        if (localNormalLisReportList.Count > 0)
        //        {
        //            result = true;
        //        }
        //    }
        //    return result;
        //}
        //private Model.NormalLisReport ConvertNeedRecordToNormalLisReport(Common.Record record)
        //{
        //    Model.NormalLisReport normalLisReport = new Model.NormalLisReport();
        //    if (!string.IsNullOrEmpty(record.check_date))
        //    {
        //        normalLisReport.check_date = record.check_date;
        //    }
        //    normalLisReport.chinese = record.chinese;
        //    normalLisReport.ext_mthd = record.ext_mthd;
        //    normalLisReport.hospnum = record.hospnum;
        //    normalLisReport.patname = record.patname;
        //    normalLisReport.Sex = record.sex;
        //    return new Model.NormalLisReport();
        //}


        //private int AddSampleInfo(Model.Sample_Info sample_info)
        //{
        //    List<Model.Sample_Clinical_Data> list = ClinicalData.GetSample_Clinical_DataListBy(a => a.sample_id.ToString() == sample_info.id);
        //    int result = 0;
        //    if (list.Count == 0)
        //    {
        //        //本地数据库中不存在该条数据
        //        Model.Sample_Clinical_Data sample_Clinical_Data = new Model.Sample_Clinical_Data();
        //        sample_Clinical_Data = ConvertSample_InfoToSample_Clinical_Data(sample_info);
        //        result = ClinicalData.AddSample_Clinical_Data(sample_Clinical_Data);
        //    }
        //    return result;
        //}

        //private Model.Sample_Clinical_Data ConvertSample_InfoToSample_Clinical_Data(Model.Sample_Info sample_Info)
        //{
        //    Model.Sample_Clinical_Data sample_Clinical_Data = new Model.Sample_Clinical_Data();
        //    sample_Clinical_Data.sample_id = sample_Info.id;
        //    if (!string.IsNullOrEmpty(sample_Info.source_id))
        //    {
        //        sample_Clinical_Data.sample_source_id = sample_Info.source_id;//需要判断是否有值
        //    }
        //    sample_Clinical_Data.username = username;
        //    sample_Clinical_Data.created_at = sample_Info.created_at;
        //    return sample_Clinical_Data;
        //}



        ///// <summary>
        ///// 根据当前用户名获取最后一次回发的时间
        ///// </summary>
        ///// <returns></returns>
        //private string GetUserNameTime()
        //{
        //    string datetime = "";
        //    datetime = sZY_FpExtendEntities.Sample_Clinical_Data.Where(a => a.username == username).OrderByDescending(a => a.created_at).First().created_at;
        //    return datetime;
        //}
        //private List<Model.Sample_Info> GetSampleByDate(string date)
        //{
        //    //sample_by_date，根据日期获取“前一天（yesterday）”的样本,如果是调用当天的样本数据就是“today"或者是"2015.02.18,2015.02.19"
        //    //***此处应该是根据保存的日志文件中的日期然后根据保存的日期取数据
        //    //http://192.168.1.104/api?username=admin;password=admin;method=samples_by_date&date=2014.03.15,2015.03.16
        //    BLL.Samples sample = new Samples();
        //    List<Model.Sample> sample_by_dateList = sample.GetSamples_By_Date(date);
        //    List<Model.Sample_Info> sample_InfoList = new List<Model.Sample_Info>();
        //    if (sample_by_dateList.Count > 0)
        //    {
        //        foreach (Model.Sample item in sample_by_dateList)
        //        {
        //            //根据日期获取样本再根据id获取样本详细信息
        //            Model.Sample_Info sampleinfo = sample.GetSample_Info(item.id);
        //            sample_InfoList.Add(sampleinfo);
        //        }
        //    }
        //    return sample_InfoList;
        //}

        ////根据数据库中需要添加的样本数据查询获取临床数据，并添加临床数据保存记录

        ////01.调用webservice获取临床数据
        ///// <summary>
        ///// 获取异常指标集合
        ///// </summary>
        ///// <param name="datetime">起始日期</param>
        ///// <param name="requestMzhzyh">住院号或门诊号</param>
        ///// <param name="i">前后几天</param>
        ///// <returns></returns>
        //private List<Common.Record> GetNeedRecord(DateTime datetime, string requestMzhzyh, int i = 5)
        //{
        //    List<Common.Record> record = getDataFromHospital.GetClinicalDataList(datetime, requestMzhzyh, i);
        //    List<Common.Record> newRecord = new List<Common.Record>();
        //    if (record.Count > 0)
        //    {
        //        //获取ref_flag不为空的数据
        //        newRecord = record.Where(a => !string.IsNullOrEmpty(a.ref_flag)).ToList();
        //    }
        //    return newRecord;
        //}
        ////02.添加临床数据、保存添加的数据到数据库

        ///// <summary>
        ///// 将异常数据集合转换成Json格式的字符串
        ///// </summary>
        ///// <param name="record">集合</param>
        ///// <returns>Json格式的字符串</returns>
        //private string ConvertRecordToJsonStr(List<Common.Record> record)
        //{
        //    string jsonStr = "";
        //    jsonStr = Common.FpJsonHelper.ObjectToJsonStr<List<Common.Record>>(record);
        //    return jsonStr;
        //}


        ////03.更改状态

        ////将传入的对象转换成json字符串

    }
}

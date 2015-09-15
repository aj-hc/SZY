using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;

namespace RuRo.BLL
{
    public partial class PatientDiagnose
    {
        //创建获取数据对象
        private ClinicalData.PacsLisReportServices clinicalData = new ClinicalData.PacsLisReportServices();

        private Model.DTO.PatientDiagnoseResuest request = null;
        //private Model.QueryRecoder queryRecoderModel = null;
        //private List<string> requestStrList = null;

        ///// <summary>
        ///// 前台调用方法
        ///// </summary>
        ///// <returns></returns>
        //public string GetData(Model.DTO.PatientDiagnoseResuest request)
        //{
        //    //string xmlStr = GetWebServiceData(request);
        //    //Model.DTO.JsonModel jsonmodel = StrTObject(xmlStr, request);
        //    //return JsonConvert.SerializeObject(jsonmodel);
        //    return "";
        //}

        public string GetData(Model.QueryRecoder model, bool queryBycode)
        {
            BLL.Request.PatientDiagnoseResuest cq = new Request.PatientDiagnoseResuest(model);
            cq.CreatRequest(queryBycode);
            Model.QueryRecoder queryRecoderModel = cq.QueryRecoderModel;
            Model.DTO.JsonModel jsonmodel = new Model.DTO.JsonModel() { Statu = "err", Msg = "无数据", Data = "" };

            //保存记录（查询记录数据,更新或添加）  string.IsNullOrEmpty(cq.RequestStr)存在值 修改修！string.IsNullOrEmpty(cq.RequestStr) kaka
            if (cq.RequestStr != null && cq.RequestStr.Count > 0)
            {
                List<Model.PatientDiagnose> patientDiagnoseList = new List<Model.PatientDiagnose>();
                StringBuilder msg = new StringBuilder();
                //调用接口获取数据
                foreach (var item in cq.RequestStr)
                {
                    string xmlStr = GetWebServiceData(item);
                    string _Msg = "";
                    //返回数据缺少结束标记
                    if (!xmlStr.Contains("</Response>"))
                    {
                        xmlStr += "</Response>";
                    }
                    Model.PatientDiagnose patientDiagnose = StrTObject(xmlStr, out _Msg);
                    //nnn.Add(model.Code);
                    if (patientDiagnose != null)
                    {
                        if (!patientDiagnoseList.Contains(patientDiagnose))
                        {
                            bool check = CheckData(patientDiagnose);
                            if (!check)
                            {
                                patientDiagnoseList.Add(patientDiagnose);
                            }
                        }
                        if (!string.IsNullOrEmpty(_Msg))
                        {
                            msg.Replace(_Msg, "");
                            msg.Append(" &nbsp " + _Msg);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_Msg))
                        {
                            msg.Replace(_Msg, "");
                            msg.Append(" &nbsp " + _Msg);
                        }
                    }
                }
                if (patientDiagnoseList != null && patientDiagnoseList.Count > 0)
                {
                    //有数据
                    jsonmodel = CreatJsonMode("ok", msg.ToString(), patientDiagnoseList);
                    ChangeQueryRecordStatu(cq, msg.ToString());
                }
                else
                {
                    //无数据
                    jsonmodel = CreatJsonMode("err", msg.ToString(), patientDiagnoseList);
                    ChangeQueryRecordStatu(cq, msg.ToString());
                }
            }
            return JsonConvert.SerializeObject(jsonmodel);
        }

        //public Model.DTO.JsonModel GetData(Model.DTO.PatientDiagnoseResuest request, string codeType, string t)
        //{
        //    Model.DTO.JsonModel jsonmodel = new Model.DTO.JsonModel() { Statu = "err", Msg = "无数据", Data = "" };
        //    //保存记录（查询记录数据,更新或添加）
        //    bool b = SaveQueryRecord(ref request, "", codeType);
        //    if (b)
        //    {
        //        //调用接口获取数据
        //        string xmlStr = GetData(request);
        //        string Msg = "";
        //        //将xml数据转换成list集合会查询本地数据库去除重复项
        //        List<Model.PatientDiagnose> nnn = new List<Model.PatientDiagnose>();
        //        //List<Model.PatientDiagnose> nnn = this.GetList(xmlStr, out Msg);

        //        if (nnn != null && nnn.Count > 0)
        //        {
        //            //有数据
        //            jsonmodel = CreatJsonMode("ok", Msg, nnn);
        //        }
        //        else
        //        {
        //            //无数据
        //            jsonmodel = CreatJsonMode("err", Msg, nnn);
        //            bool bb = SaveQueryRecord(ref request, Msg, codeType);
        //        }
        //    }
        //    return jsonmodel;
        //}

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="formData">数据</param>
        /// <param name="code">条码</param>
        /// <param name="codeType">条码类型</param>
        /// <param name="username">登陆用户</param>
        /// <param name="isP">是否批量导入</param>
        /// <returns></returns>
        //public string PostData(string formData, string code, string codeType, string username, bool isP)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    Dictionary<string, string> newDic = new Dictionary<string, string>();
        //    //if (isP)
        //    //{
        //    //    dic = GetBaseInfoDic_Q(formData);
        //    //}
        //    //else
        //    //{
        //    dic = GetBaseInfoDic(formData);
        //    //}
        //    // newDic.Add("Name", code);
        //    newDic.Add("Sample Source", code);
        //    foreach (KeyValuePair<string, string> item in dic)
        //    {
        //        if (Common.MatchDic.PatientDiagnoseDic.ContainsKey(item.Key))
        //        {
        //            #region 匹配诊断信息

        //            switch (item.Key)
        //            {
        //                case "Type":
        //                    switch (item.Value)
        //                    {
        //                        case "1":
        //                            newDic.Add(Common.MatchDic.PatientDiagnoseDic[item.Key], "中医疾病");
        //                            break;

        //                        case "2":
        //                            newDic.Add(Common.MatchDic.PatientDiagnoseDic[item.Key], "中医症候");
        //                            break;

        //                        case "3":
        //                            newDic.Add(Common.MatchDic.PatientDiagnoseDic[item.Key], "西医主诊断");
        //                            break;

        //                        case "4":
        //                            newDic.Add(Common.MatchDic.PatientDiagnoseDic[item.Key], "西医其他诊断");
        //                            break;

        //                        default:
        //                            break;
        //                    }
        //                    break;

        //                case "Flag":
        //                    switch (item.Value)
        //                    {
        //                        case "1":
        //                            newDic.Add(Common.MatchDic.PatientDiagnoseDic[item.Key], "西医诊断");
        //                            break;

        //                        case "2":
        //                            newDic.Add(Common.MatchDic.PatientDiagnoseDic[item.Key], "中医诊断");
        //                            break;

        //                        default:
        //                            break;
        //                    }
        //                    break;

        //                default:
        //                    break;
        //            }

        //            #endregion 匹配诊断信息

        //            if (!newDic.Keys.Contains(Common.MatchDic.PatientDiagnoseDic[item.Key]))
        //            {
        //                newDic.Add(Common.MatchDic.PatientDiagnoseDic[item.Key], item.Value);
        //            }
        //        }
        //    }
        //    //调用方法提交数据
        //    string result = PostData(newDic);
        //    if (result.Contains("\"success\":true,") || result.Contains("should be unique."))
        //    {
        //        //Model.PatientDiagnose patientDiagnoseModel = new Model.PatientDiagnose();
        //        //string date = DateTime.Now.ToString("yyyy-MM-dd");
        //        //patientDiagnoseModel.Cardno = code;
        //        //patientDiagnoseModel.Csrq00 = date;
        //        //patientDiagnoseModel.Sex = dic["Sex"];
        //        //patientDiagnoseModel.CardId = dic["CardId"];
        //        //patientDiagnoseModel.Tel = dic["Tel"];
        //        //patientDiagnoseModel.Icd = dic["Icd"];
        //        //patientDiagnoseModel.Diagnose = dic["Diagnose"];
        //        //patientDiagnoseModel.Type = dic["Type"];
        //        //patientDiagnoseModel.Flag = dic["Flag"];
        //        //patientDiagnoseModel.DiagnoseDate = dic["DiagnoseDate"];
        //        //patientDiagnoseModel.IsDel = true;
        //        //PatientDiagnose eee = new PatientDiagnose();
        //        //bool i = eee.Add(patientDiagnoseModel);
        //        //BLL.SZY.QueryRecoder bll_Q = new SZY.QueryRecoder();
        //        //bll_Q.UpdataQueryRecoderIsDel_BLL(username, 0, code, "PatientDiagnose");//修改QueryRecoder表为true

        //        //把数据添加到数据库
        //        Model.PatientDiagnose model = new Model.PatientDiagnose();
        //        model = DicToNormalLisReportModel(dic);
        //        PatientDiagnose n = new PatientDiagnose();
        //        n.Add(model);
        //    }
        //    return result;
        //}
        public string PostData(string code, string codeType, string dataStr, string username, bool isP)
        {
            List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> newDicList = new List<Dictionary<string, string>>();
            string mes = "\"{\"success\":false,\"error\":true,\"message\":\"无数据导入\"}\"";
            //if (isP)
            //{
            //    dicList = GetClinicalInfoDgDicList_Q(dataStr);
            //}
            //else
            //{
            dicList = GetClinicalInfoDgDicList(dataStr);
            //}
            if (dicList != null && dicList.Count > 0)
            {
                List<Model.PatientDiagnose> list = new List<Model.PatientDiagnose>();
                for (int i = 0; i < dicList.Count; i++)
                {
                    //把数据添加到数据库
                    Model.PatientDiagnose model = new Model.PatientDiagnose();
                    model = DicToNormalLisReportModel(dicList[i]);
                    try
                    {
                        if (this.CheckData(model))
                        {
                            dicList.Remove(dicList[i]);
                        }
                        else
                        {
                            list.Add(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                        continue;
                    }
                }

                newDicList = MatchClinicalDic(dicList, codeType);
                newDicList.Insert(0, new Dictionary<string, string>() { { "Sample Source", code } });
                foreach (var item in newDicList)
                {
                    if (item.Keys.Contains("诊断类型"))
                    {
                        switch (item["诊断类型"])
                        {
                            case "1":
                                item["诊断类型"] = "中医疾病";
                                break;

                            case "2":
                                item["诊断类型"] = "中医症候";
                                break;

                            case "3":
                                item["诊断类型"] = "西医主诊断";
                                break;

                            case "4":
                                item["诊断类型"] = "西医其他诊断";
                                break;

                            default:
                                break;
                        }
                    }
                    if (item.Keys.Contains("诊断类别"))
                    {
                        switch (item["诊断类别"])
                        {
                            case "1":
                                item["诊断类别"] = "西医诊断";
                                break;

                            case "2":
                                item["诊断类别"] = "中医诊断";
                                break;

                            default:
                                break;
                        }
                    }
                }
                string strList = JsonConvert.SerializeObject(newDicList);
                mes = PostData(strList);
                if (mes.Contains("{\"success\":true,"))
                {
                    //for (int i = 0; i < dicList.Count; i++)
                    //{
                    //    //把数据添加到数据库
                    //    Model.PatientDiagnose model = new Model.PatientDiagnose();
                    //    model = DicToNormalLisReportModel(dicList[i]);
                    //    PatientDiagnose n = new PatientDiagnose();
                    //    n.Add(model);
                    //}
                    //BLL.SZY.QueryRecoder bll_Q = new SZY.QueryRecoder();
                    //bll_Q.UpdataQueryRecoderIsDel_BLL(username, 0, code, "NormalLisReport");//修改QueryRecoder表为true
                    foreach (var item in list)
                    {
                        PatientDiagnose n = new PatientDiagnose();
                        try
                        {
                            n.Add(item);
                        }
                        catch (Exception ex)
                        {
                            Common.LogHelper.WriteError(ex);
                            continue;
                        }
                    }
                }
            }
            return mes;
        }

        private List<Dictionary<string, string>> MatchClinicalDic(List<Dictionary<string, string>> clinicalDicList, string codeType)
        {
            Dictionary<string, string> dic = Common.MatchDic.PatientDiagnoseDic;
            List<Dictionary<string, string>> resDicList = new List<Dictionary<string, string>>();
            foreach (var clinicalDic in clinicalDicList)
            {
                Dictionary<string, string> resDic = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> item in clinicalDic)
                {
                    if (dic.ContainsKey(item.Key))
                    {
                        string key = dic[item.Key];
                        if (!resDic.ContainsKey(key))
                        {
                            resDic.Add(key, item.Value);
                        }
                    }
                }
                resDicList.Add(resDic);
            }
            return resDicList;
        }

        private List<Dictionary<string, string>> GetClinicalInfoDgDicList(string dataStr)
        {
            List<Model.NormalLisReport> pageClinicalInfoList = new List<Model.NormalLisReport>();
            Model.NormalLisReport cl = new Model.NormalLisReport();
            List<Dictionary<string, string>> ClinicalInfoDgDicList = new List<Dictionary<string, string>>();
            if (!string.IsNullOrEmpty(dataStr) && dataStr != "[]")
            {
                pageClinicalInfoList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Model.NormalLisReport>>(dataStr);//转换ok
            }
            foreach (Model.NormalLisReport item in pageClinicalInfoList)
            {
                //给对象拼接--临床数据中需要添加基本信息中的RegisterID,InPatientID
                ClinicalInfoDgDicList.Add(FormToDic.ConvertModelToDic(item));
            }
            return ClinicalInfoDgDicList;
        }

        #region 获取基本信息字典（样本源） +  private Dictionary<string, string> GetBaseInfoDic()

        //获取基本信息字典（样本源）
        //private Dictionary<string, string> GetBaseInfoDic_Q(string formStr)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    Model.PatientDiagnose data = new Model.PatientDiagnose();
        //    List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
        //    if (!string.IsNullOrEmpty(formStr) && formStr != "[]")
        //    {
        //        dicList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Dictionary<string, string>>>(formStr);
        //        data = FormToDic.GetFromInfo<Model.PatientDiagnose>(dicList);
        //        dic = FormToDic.ConvertModelToDic(data);
        //    }
        //    return dic;
        //}

        //获取基本信息字典（样本源）
        private Dictionary<string, string> GetBaseInfoDic(string formStr)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Model.PatientDiagnose data = new Model.PatientDiagnose();
            Dictionary<string, string> dicc = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(formStr) && formStr != "[]")
            {
                List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
                dicList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Dictionary<string, string>>>(formStr);
                dicc = dicList[0];
                string aa = JsonConvert.SerializeObject(dicc);
                data = JsonConvert.DeserializeObject<Model.PatientDiagnose>(aa);
                //data = FormToDic.GetFromInfo<Model.PatientDiagnose>(dicList);
                dic = FormToDic.ConvertModelToDic(data);
            }
            return dic;
        }

        #endregion 获取基本信息字典（样本源） +  private Dictionary<string, string> GetBaseInfoDic()

        private string PostData(Dictionary<string, string> dic)
        {
            UnameAndPwd up = new UnameAndPwd();
            string result = FreezerProUtility.Fp_BLL.TestData.ImportTestData(up.GetUp(), "诊断信息", dic);
            return result;
        }

        //private bool SaveQueryRecord(ref Model.DTO.PatientDiagnoseResuest resquet, string Msg, string codeType)
        //{
        //    bool result;
        //    QueryRecoder queryRecoder = new QueryRecoder();
        //    //根据传入的查询字符串创建的当此查询的记录model
        //    Model.QueryRecoder model = new Model.QueryRecoder();

        //    //model.AddDate = DateTime.Now;
        //    model.Code = resquet.cardno;
        //    model.CodeType = codeType;
        //    model.QueryType = "PatientDiagnose";
        //    model.Uname = Common.CookieHelper.GetCookieValue("username");

        //    List<Model.QueryRecoder> list = CheckQueryRecord(model);
        //    if (list != null && list.Count > 0)
        //    {
        //        //判断查询出来的数据是否满足要求（时间差距lastdate<dateNow-5）
        //        Model.QueryRecoder oldModel = list.OrderByDescending(a => a.LastQueryDate).FirstOrDefault();
        //        model.AddDate = oldModel.AddDate;
        //        model.Id = oldModel.Id;
        //        DateTime dtAdd = Convert.ToDateTime(oldModel.AddDate);
        //        DateTime dtLastQuery = Convert.ToDateTime(oldModel.LastQueryDate);
        //        DateTime dt = DateTime.Now;//当前时间
        //        int days = (dt - dtAdd).Days;//获取当前日期与添加日期时间差
        //        int getDays = 0;
        //        if (days > getDays)
        //        {
        //            model.IsDel = true;
        //            model.LastQueryDate = dtAdd.AddDays(5);
        //        }
        //        else
        //        {
        //            //添加日期是距离当前日期是5天内的
        //            //更改最后查询时间为今天
        //            model.IsDel = false;
        //            model.LastQueryDate = DateTime.Now;
        //            resquet.cxrq00 = DateTime.Now.ToString("yyyy-MM-dd");
        //            model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg + oldModel.QueryResult;
        //        }
        //        //本地数据库有数据
        //        result = queryRecoder.Update(model);
        //    }
        //    else
        //    {
        //        model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg.Trim();
        //        model.AddDate = DateTime.Now;
        //        model.LastQueryDate = DateTime.Now;
        //        result = queryRecoder.Add(model) > 0;
        //    }
        //    return result;
        //}

        //#region 解析xml获取数据并转换成list +private List<Model.NormalLisReport> GetList(string xmlStr, out string Msg)

        ///// <summary>
        ///// 解析xml获取数据并转换成list
        ///// </summary>
        ///// <param name="xmlStr"></param>
        ///// <param name="Msg">消息</param>
        ///// <returns></returns>
        //private List<Model.PatientDiagnose> GetList(string xmlStr, out string Msg, string code, bool queryBycode)
        //{
        //    List<Model.PatientDiagnose> list = new List<Model.PatientDiagnose>();
        //    XmlDocument xd = Common.XmlHelper.XMLLoad(xmlStr, Common.XmlHelper.XmlType.String);
        //    Msg = "无数据";
        //    if (xd == null)
        //    {
        //    }
        //    else
        //    {
        //        if (xd.HasChildNodes)
        //        {
        //            XmlNode xn = xd.SelectSingleNode("//ResultCode");
        //            if (xn != null)
        //            {
        //                if (xn.InnerText == "0")
        //                {
        //                    //有数据
        //                    XmlNodeList xnlReocrd = xd.SelectNodes("//reocrd");
        //                    XmlNodeList xnll = xd.SelectNodes("//DiagnoseInfo");
        //                    if (xnlReocrd != null && xnlReocrd.Count > 0)
        //                    {
        //                        //foreach (XmlNode item in xnl)
        //                        //{
        //                        //    Model.PatientDiagnose nn = this.XmlTomModel(item);
        //                        //    nn.Cardno = code;
        //                        //    if (!Common.MatchDic.PatientDiagnoseDic.Keys.Contains(nn.CardId))//这里需要改
        //                        //    {
        //                        //        if (queryBycode)
        //                        //        {
        //                        //            list.Add(nn);
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            if (!this.CheckData(nn))
        //                        //            {
        //                        //                list.Add(nn);
        //                        //            }
        //                        //        }
        //                        //    }
        //                        //}
        //                        string strRecord = JsonConvert.SerializeXmlNode(xnlReocrd[0], Newtonsoft.Json.Formatting.None);

        //                        if (list.Count > 0)
        //                        {
        //                            Msg = "";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    //查询数据出错，联接无问题
        //                    Msg = xd.SelectSingleNode("//ErrorMsg").InnerText;
        //                }
        //            }
        //            else
        //            {
        //                //查询数据出错，联接无问题
        //                Msg = xd.InnerText;
        //                //保存查询记录
        //            }
        //        }
        //    }
        //    return list;
        //}

        //#endregion 解析xml获取数据并转换成list +private List<Model.NormalLisReport> GetList(string xmlStr, out string Msg)

        #region xmlNode转换成obj + Model.NormalLisReport XmlTomModel(XmlNode xd)

        ///// <summary>
        ///// xmlNode转换成obj
        ///// </summary>
        ///// <param name="xd"></param>
        ///// <returns></returns>
        //private Model.PatientDiagnose XmlTomModel(XmlNode xd)
        //{
        //    int id = 0;
        //    string strNode = JsonConvert.SerializeXmlNode(xd, Newtonsoft.Json.Formatting.None, true);
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    Model.PatientDiagnose nlr;
        //    try
        //    {
        //        dic = JsonToDictionary(strNode);
        //        if (dic.ContainsKey("DiagnoseInfo"))
        //        {
        //            string strdic = JsonConvert.SerializeObject(dic["DiagnoseInfo"]);//获取节点为DiagnoseInfo的JSON
        //            Dictionary<string, string> dicc = new Dictionary<string, string>();
        //            dicc = JsonConvert.DeserializeObject<Dictionary<string, string>>(strdic);//获取后转换为另外一个DIC
        //            foreach (KeyValuePair<string, string> item in dicc) //添加到原有的DIC中
        //            {
        //                dic.Add(item.Key, item.Value);
        //            }
        //            dic.Remove("DiagnoseInfo");
        //        }
        //        string strdnc = JsonConvert.SerializeObject(dic);
        //        nlr = JsonConvert.DeserializeObject<Model.PatientDiagnose>(strdnc);
        //        //if (!string.IsNullOrEmpty(nlr.Cardno) && Common.MatchDic.PatientDiagnoseDic.Keys.Contains(nlr.CardId))//这里需要改
        //        if (Common.MatchDic.PatientDiagnoseDic.Keys.Contains(nlr.CardId))//这里需要改
        //        {
        //            nlr.id = id;
        //            //id++;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        nlr = null;
        //        Common.LogHelper.WriteError(ex);
        //    }
        //    return nlr;
        //}

        /// <summary>
        /// 将json数据反序列化为Dictionary
        /// </summary>
        /// <param name="jsonData">json数据</param>
        /// <returns></returns>
        private Dictionary<string, object> JsonToDictionary(string jsonData)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, object>>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion xmlNode转换成obj + Model.NormalLisReport XmlTomModel(XmlNode xd)

        #region 检查数据对象在本地数据库是否存在 CheckData(Model.PatientDiagnose data)

        /// <summary>
        /// 检查数据对象在本地数据库是否存在 ,true--存在
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckData(Model.PatientDiagnose data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    string whereStr = string.Format("Cardno ='{0}' and Csrq00 ='{1}' and DiagnoseDate ='{2}' and Flag='{3}' and Type='{4}'  and Diagnose='{5}'", data.Cardno, data.Csrq00, data.DiagnoseDate, data.Flag, data.Type, data.Diagnose);
                    List<Model.PatientDiagnose> list = this.GetModelList(whereStr);
                    if (list != null && list.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
            }
            return result;
        }

        #endregion 检查数据对象在本地数据库是否存在 CheckData(Model.PatientDiagnose data)

        private Model.DTO.JsonModel CreatJsonMode(string statu, string msg, object data)
        {
            Model.DTO.JsonModel jsonModel = new Model.DTO.JsonModel();
            if (statu == "err")
            {
                jsonModel.Statu = statu;
                jsonModel.Msg = msg;
            }
            else
            {
                jsonModel.Statu = statu;
                jsonModel.Msg = msg;
                jsonModel.Data = data;
            }
            return jsonModel;
        }

        //private List<Model.QueryRecoder> CheckQueryRecord(Model.QueryRecoder model)
        //{
        //    QueryRecoder queryRecoder = new QueryRecoder();
        //    //查询本地数据库有没有数据
        //    StringBuilder strWhere = new StringBuilder();
        //    strWhere.AppendFormat("Uname = {0} and ", "'" + model.Uname + "'");
        //    strWhere.AppendFormat("Code = {0} and ", "'" + model.Code + "'");
        //    strWhere.AppendFormat("CodeType = {0} and ", "'" + model.CodeType + "'");
        //    strWhere.AppendFormat("IsDel = {0} and ", "'" + model.IsDel + "'");
        //    strWhere.AppendFormat("QueryType = {0}", "'" + model.QueryType + "'");

        //    //查询条件是，当前用户添加的卡号为X的卡号类型为Y的没有标记删除的并且临床数据类型为Z的数据
        //    return queryRecoder.GetModelList(strWhere.ToString());
        //}

        #region 查询完WebService之后更新记录表

        /// <summary>
        /// 查询完WebService之后更新记录表
        /// </summary>
        /// <param name="cq">记录表对象</param>
        /// <param name="msg">查询之后的消息</param>
        private void ChangeQueryRecordStatu(BLL.Request.Request cq, string msg)
        {
            Model.QueryRecoder queryRecoder = cq.QueryRecoderModel;
            int queryDateInterval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["QueryDateInterval"].Trim());
            if (queryRecoder != null)
            {
                try
                {
                    if ((DateTime.Now - queryRecoder.AddDate).Days > queryDateInterval)
                    {
                        //超时数据
                        queryRecoder.LastQueryDate = DateTime.Now;
                        queryRecoder.IsDel = true;
                    }
                    else
                    {
                        //不超时数据
                        queryRecoder.LastQueryDate = DateTime.Now;
                    }
                    queryRecoder.QueryResult = ("&nbsp" + DateTime.Now.ToLocalTime() + " " + msg + queryRecoder.QueryResult);
                    QueryRecoder q = new QueryRecoder();
                    q.Update(queryRecoder);
                }
                catch (Exception ex)
                {
                    Common.LogHelper.WriteError(ex);
                }
            }
        }

        #endregion 查询完WebService之后更新记录表

        #region 获取数据

        private string GetWebServiceData(string request)
        {
            try
            {
                // return Test("");
                return string.IsNullOrEmpty(request) ? "" : clinicalData.GetPatientDiagnose(request);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
                return ex.Message + "--" + DateTime.Now.ToLongTimeString();
            }
        }

        //private string GetWebServiceData(Model.DTO.PatientDiagnoseResuest request)
        //{
        //    try
        //    {
        //        // return TestAnd(new Model.DTO.PatientDiagnoseResuest("", "2015-01-01")).Replace("\r\n", "").Replace(" ", "");
        //        return string.IsNullOrEmpty(request.RequestStr) ? "" : clinicalData.GetPatientDiagnose(request.RequestStr);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHelper.WriteError(ex);
        //        return ex.Message + "--" + DateTime.Now.ToLongTimeString();
        //    }
        //}

        #endregion 获取数据

        #region 生成临时数据

        /// <summary>
        /// 生成临时数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string Test(string request)
        {
            string getDataFromHospitalStr = @"<Response>
                                                <ResultCode>0</ ResultCode>
                                                <ErrorMsg></ErrorMsg>
                                                <reocrd>
                                                  <PatientName>杨基</PatientName>
                                                  <Sex>男</Sex>
                                                  <Brithday>1999-01-01</Brithday>
                                                  <CardId>0272099</CardId>
                                                  <Tel>76</Tel>
                                                  <DiagnoseInfo>
                                                    <RegisterNo>11111111</RegisterNo>
                                                    <Icd>Icd123456</Icd>
                                                    <Diagnose>诊断名称</Diagnose>
                                                    <Type>3</Type>
                                                    <Flag>1</Flag>
                                                    <DiagnoseDate>2012-09-01</DiagnoseDate>
                                                    </DiagnoseInfo>
                                                 </reocrd>
                                                </Response>
                                                ";
            return getDataFromHospitalStr;
        }

        private string TestAnd(Model.DTO.PatientDiagnoseResuest request)
        {
            string getDataFromHospitalStr = @"<Response>
                                                <ResultCode>0</ ResultCode>
                                                <ErrorMsg></ErrorMsg>
                                                <reocrd>
                                                  <PatientName>张三</PatientName>
                                                  <Sex>男</Sex>
                                                  <Brithday>1999-01-01</Brithday>
                                                  <CardId>123456789</CardId>
                                                  <Tel>76</Tel>
                                                  <DiagnoseInfo>
                                                    <RegisterNo>11111111</RegisterNo>
                                                    <Icd>Icd123456</Icd>
                                                    <Diagnose>诊断名称</Diagnose>
                                                    <Type>3</Type>
                                                    <Flag>1</Flag>
                                                    <DiagnoseDate>2012-09-01</DiagnoseDate>
                                                    </DiagnoseInfo>
                                                 </reocrd>
                                                </Response>
                                                ";
            return getDataFromHospitalStr;
        }

        #endregion 生成临时数据

        #region 将数据转换成对象

        /// <summary>
        /// 将数据转换成对象
        /// </summary>
        /// <param name="xmlStr">要转换成对象的数据</param>
        /// <returns></returns>
        private Model.DTO.JsonModel StrTObject(string xmlStr, Model.DTO.PatientDiagnoseResuest request)
        {
            XmlDocument xd = HospitalXmlStrHelper.HospitalXmlStrToXmlDoc(xmlStr);
            Model.DTO.JsonModel jsonData = new Model.DTO.JsonModel() { Statu = "err", Data = "", Msg = "无数据" };
            if (xd == null)
            {
            }
            else
            {
                if (xd.HasChildNodes)
                {
                    XmlNode xn = xd.SelectSingleNode("//ResultCode");
                    if (xn != null)
                    {
                        if (xn.InnerText == "0")
                        {
                            string strNode = JsonConvert.SerializeXmlNode(xd.SelectSingleNode("//reocrd"), Newtonsoft.Json.Formatting.None, true);
                            Model.PatientDiagnose patientDiagnoseData = JsonConvert.DeserializeObject<Model.PatientDiagnose>(strNode);
                            string diagnoseInfoNode = JsonConvert.SerializeXmlNode(xd.SelectSingleNode("//DiagnoseInfo"), Newtonsoft.Json.Formatting.None, true);
                            Model.DTO.DiagnoseInfoModel dg = JsonConvert.DeserializeObject<Model.DTO.DiagnoseInfoModel>(diagnoseInfoNode);
                            patientDiagnoseData.RegisterNo = dg.RegisterNo;
                            patientDiagnoseData.Type = dg.Type;
                            patientDiagnoseData.Icd = dg.Icd;
                            patientDiagnoseData.Flag = dg.Flag;
                            patientDiagnoseData.DiagnoseDate = dg.DiagnoseDate;
                            patientDiagnoseData.Diagnose = dg.Diagnose;
                            patientDiagnoseData.Cardno = request.cardno;
                            patientDiagnoseData.Csrq00 = request.cxrq00;
                            if (patientDiagnoseData == null || patientDiagnoseData.PatientName == "")
                            {
                            }
                            else
                            {
                                jsonData.Data = patientDiagnoseData;
                                jsonData.Statu = "ok";
                                jsonData.Msg = "查询成功";
                            }
                        }
                        else
                        {
                            //查询数据出错，联接无问题
                            jsonData.Msg = xd.SelectSingleNode("//ErrorMsg").InnerText;
                            jsonData.Statu = "err";
                        }
                    }
                    else
                    {
                        //查询数据出错，联接无问题
                        jsonData.Msg = xd.InnerText;
                        jsonData.Statu = "err";
                    }
                }
            }
            return jsonData;
        }

        #endregion 将数据转换成对象

        private Model.PatientDiagnose StrTObject(string xmlStr, out string msg)
        {
            XmlDocument xd = HospitalXmlStrHelper.HospitalXmlStrToXmlDoc(xmlStr);
            Model.PatientDiagnose patientDiagnoseModel = null;
            msg = "";
            if (xd == null)
            {
            }
            else
            {
                if (xd.HasChildNodes)
                {
                    XmlNode xn = xd.SelectSingleNode("//ResultCode");
                    if (xn != null)
                    {
                        if (xn.InnerText == "0")
                        {
                            string strNode = JsonConvert.SerializeXmlNode(xd.SelectSingleNode("//reocrd"), Newtonsoft.Json.Formatting.None, true);
                            patientDiagnoseModel = JsonConvert.DeserializeObject<Model.PatientDiagnose>(strNode);
                            string diagnoseInfoNode = JsonConvert.SerializeXmlNode(xd.SelectSingleNode("//DiagnoseInfo"), Newtonsoft.Json.Formatting.None, true);
                            Model.DTO.DiagnoseInfoModel dg = JsonConvert.DeserializeObject<Model.DTO.DiagnoseInfoModel>(diagnoseInfoNode);
                            patientDiagnoseModel.RegisterNo = dg.RegisterNo;
                            patientDiagnoseModel.Type = dg.Type;
                            patientDiagnoseModel.Icd = dg.Icd;
                            patientDiagnoseModel.Flag = dg.Flag;
                            patientDiagnoseModel.DiagnoseDate = dg.DiagnoseDate;
                            patientDiagnoseModel.Diagnose = dg.Diagnose;
                            patientDiagnoseModel.Cardno = this.request.cardno;
                            patientDiagnoseModel.Csrq00 = this.request.cxrq00;
                        }
                        else
                        {
                            //查询数据出错，联接无问题
                            msg = xd.SelectSingleNode("//ErrorMsg").InnerText;
                        }
                    }
                    else
                    {
                        //查询数据出错，联接无问题
                        msg = xd.InnerText;
                    }
                }
            }
            return patientDiagnoseModel;
        }

        public Model.PatientDiagnose DicToNormalLisReportModel(Dictionary<string, string> dic)
        {
            string str = JsonConvert.SerializeObject(dic);
            Model.PatientDiagnose patientDiagnose = JsonConvert.DeserializeObject<Model.PatientDiagnose>(str);
            return patientDiagnose;
        }

        private string PostData(string str)
        {
            UnameAndPwd up = new UnameAndPwd();
            string result = FreezerProUtility.Fp_BLL.TestData.ImportTestData(up.GetUp(), "诊断信息", str);
            return result;
        }

        //获取数据
        //解析数据
        //返回数据对象
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Web.Configuration;

namespace RuRo.BLL
{
    public partial class PatientDiagnose
    {
        //创建获取数据对象
        ClinicalData.PacsLisReportServices clinicalData = new ClinicalData.PacsLisReportServices();
        /// <summary>
        /// 前台调用方法
        /// </summary>
        /// <returns></returns>
        public string GetData(Model.DTO.PatientDiagnoseResuest request)
        {
            //string xmlStr = GetWebServiceData(request);
            //Model.DTO.JsonModel jsonmodel = StrTObject(xmlStr, request);
            //return JsonConvert.SerializeObject(jsonmodel);
            return "";
        }

        public string GetData(Model.QueryRecoder model, bool queryBycode)
        {
            BLL.Request.PatientDiagnoseResuest cq = new Request.PatientDiagnoseResuest(model);
            cq.CreatRequest(true);
            Model.QueryRecoder queryRecoderModel = cq.QueryRecoderModel;
            Model.DTO.JsonModel jsonmodel = new Model.DTO.JsonModel() { Statu = "err", Msg = "无数据", Data = "" };
            //保存记录（查询记录数据,更新或添加）  string.IsNullOrEmpty(cq.RequestStr)存在值 修改修！string.IsNullOrEmpty(cq.RequestStr) kaka
            if (!string.IsNullOrEmpty(cq.RequestStr))
            {
                //调用接口获取数据
                string xmlStr = GetWebServiceData(cq.RequestStr);
                string Msg = "";
                //将xml数据转换成list集合会查询本地数据库去除重复项
                List<Model.PatientDiagnose> nnn = this.GetList(xmlStr, out Msg);
                if (nnn != null && nnn.Count > 0)
                {
                    //有数据
                    jsonmodel = CreatJsonMode("ok", Msg, nnn);
                    ChangeQueryRecordStatu(cq, Msg);
                }
                else
                {
                    //无数据
                    jsonmodel = CreatJsonMode("err", Msg, nnn);
                    ChangeQueryRecordStatu(cq, Msg);
                }
            }
            return JsonConvert.SerializeObject(jsonmodel);
        }

        public Model.DTO.JsonModel GetData(Model.DTO.PatientDiagnoseResuest request, string codeType, string t)
        {
            Model.DTO.JsonModel jsonmodel = new Model.DTO.JsonModel() { Statu = "err", Msg = "无数据", Data = "" };
            //保存记录（查询记录数据,更新或添加）
            bool b = SaveQueryRecord(ref request, "", codeType);
            if (b)
            {
                //调用接口获取数据
                string xmlStr = GetData(request);
                string Msg = "";
                //将xml数据转换成list集合会查询本地数据库去除重复项
                List<Model.PatientDiagnose> nnn = this.GetList(xmlStr, out Msg);

                if (nnn != null && nnn.Count > 0)
                {
                    //有数据
                    jsonmodel = CreatJsonMode("ok", Msg, nnn);
                }
                else
                {
                    //无数据
                    jsonmodel = CreatJsonMode("err", Msg, nnn);
                    bool bb = SaveQueryRecord(ref request, Msg, codeType);
                }
            }
            return jsonmodel;
        }


        public string PostData(string formData, string code, string codeType)
        {
            Dictionary<string, string> dic = GetBaseInfoDic(formData);
            Dictionary<string, string> newDic = new Dictionary<string, string>();
            newDic.Add("Name", code);
            newDic.Add("Sample Source", code);
            foreach (KeyValuePair<string, string> item in dic)
            {
                if (Common.MatchDic.PatientDiagnoseDic.ContainsKey(item.Key))
                {
                    newDic.Add(Common.MatchDic.PatientDiagnoseDic[item.Key], item.Value);
                }
            }
            //调用方法提交数据
            string result = PostData(newDic);
            if (result.Contains("\"success\":true,") || result.Contains("should be unique."))
            {
                Model.PatientDiagnose e = new Model.PatientDiagnose();
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                e.Cardno = code;
                e.Csrq00 = date;
                e.Sex = dic["Sex"];
                e.CardId = dic["CardId"];
                e.Tel = dic["Tel"];
                e.Icd = dic["Icd"];
                e.Diagnose = dic["Diagnose"];
                e.Type = dic["Type"];
                e.Flag = dic["Flag"];
                e.DiagnoseDate = dic["DiagnoseDate"];
                e.IsDel = true;
                //Model.PatientDiagnose e = JsonConvert.DeserializeObject<Model.PatientDiagnose>(JsonConvert.SerializeObject(dic));
                PatientDiagnose eee = new PatientDiagnose();
                bool i = eee.Add(e);
            }
            return result;
        }

        #region 获取基本信息字典（样本源） +  private Dictionary<string, string> GetBaseInfoDic()
        //获取基本信息字典（样本源）
        private Dictionary<string, string> GetBaseInfoDic(string formStr)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Model.PatientDiagnose data = new Model.PatientDiagnose();

            if (!string.IsNullOrEmpty(formStr) && formStr != "[]")
            {
                List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
                dicList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Dictionary<string, string>>>(formStr);
                data = FormToDic.GetFromInfo<Model.PatientDiagnose>(dicList);
                dic = FormToDic.ConvertModelToDic(data);
            }
            return dic;
        }
        #endregion

        private string PostData(Dictionary<string, string> dic)
        {
            UnameAndPwd up = new UnameAndPwd();
            string result = FreezerProUtility.Fp_BLL.TestData.ImportTestData(up.GetUp(), "诊断信息", dic);
            return result;
        }

        private bool SaveQueryRecord(ref Model.DTO.PatientDiagnoseResuest resquet, string Msg, string codeType)
        {
            bool result;
            QueryRecoder queryRecoder = new QueryRecoder();
            //根据传入的查询字符串创建的当此查询的记录model
            Model.QueryRecoder model = new Model.QueryRecoder();
            model.Code = resquet.cardno;
            model.CodeType = codeType;
            model.QueryType = "PatientDiagnose";
            model.Uname = Common.CookieHelper.GetCookieValue("username");

            List<Model.QueryRecoder> list = CheckQueryRecord(model);
            if (list != null && list.Count > 0)
            {
                //判断查询出来的数据是否满足要求（时间差距lastdate<dateNow-5）
                Model.QueryRecoder oldModel = list.OrderByDescending(a => a.LastQueryDate).FirstOrDefault();
                model.AddDate = oldModel.AddDate;
                model.Id = oldModel.Id;
                DateTime dtAdd = Convert.ToDateTime(oldModel.AddDate);
                DateTime dtLastQuery = Convert.ToDateTime(oldModel.LastQueryDate);
                DateTime dt = DateTime.Now;//当前时间
                int days = (dt - dtAdd).Days;//获取当前日期与添加日期时间差
                int getDays = 0;
                if (days > getDays)
                {
                    model.IsDel = true;
                    model.LastQueryDate = dtAdd.AddDays(5);
                }
                else
                {
                    //添加日期是距离当前日期是5天内的
                    //更改最后查询时间为今天
                    model.IsDel = false;
                    model.LastQueryDate = DateTime.Now;
                    resquet.cxrq00 = DateTime.Now.ToString("yyyy-MM-dd");
                    model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg + oldModel.QueryResult;
                }
                //本地数据库有数据
                result = queryRecoder.Update(model);
            }
            else
            {
                model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg.Trim();
                model.AddDate = DateTime.Now;
                model.LastQueryDate = DateTime.Now;
                result = queryRecoder.Add(model) > 0;
            }
            return result;
        }

        #region 解析xml获取数据并转换成list +private List<Model.NormalLisReport> GetList(string xmlStr, out string Msg)
        /// <summary>
        /// 解析xml获取数据并转换成list
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <param name="Msg">消息</param>
        /// <returns></returns>
        private List<Model.PatientDiagnose> GetList(string xmlStr, out string Msg)
        {
            List<Model.PatientDiagnose> list = new List<Model.PatientDiagnose>();
            XmlDocument xd = HospitalXmlStrHelper.HospitalXmlStrToXmlDoc(xmlStr);
            Msg = "无数据";
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
                            //有数据
                            XmlNodeList xnl = xd.SelectNodes("//reocrd");
                            if (xnl.Count > 0)
                            {
                                foreach (XmlNode item in xnl)
                                {
                                    Model.PatientDiagnose nn = this.XmlTomModel(item);
                                    if (Common.MatchDic.PatientDiagnoseDic.Keys.Contains(nn.CardId))//这里需要改
                                    //if (Common.MatchDic.PatientDiagnoseDic.Keys.Contains(nn.Cardno))//这里需要改
                                    {
                                        if (!this.CheckData(nn))
                                        {
                                            list.Add(nn);
                                        }
                                    }
                                }
                                if (list.Count > 0)
                                {
                                    Msg = "";
                                }
                            }
                        }
                        else
                        {
                            //查询数据出错，联接无问题
                            Msg = xd.SelectSingleNode("//ErrorMsg").InnerText;
                        }
                    }
                    else
                    {
                        //查询数据出错，联接无问题
                        Msg = xd.InnerText;
                        //保存查询记录
                    }
                }
            }
            return list;
        }
        #endregion

        #region xmlNode转换成obj + Model.NormalLisReport XmlTomModel(XmlNode xd)
        /// <summary>
        /// xmlNode转换成obj
        /// </summary>
        /// <param name="xd"></param>
        /// <returns></returns>
        private Model.PatientDiagnose XmlTomModel(XmlNode xd)
        {
            int id = 0;
            string strNode = JsonConvert.SerializeXmlNode(xd, Newtonsoft.Json.Formatting.None, true);
            Model.PatientDiagnose nlr;
            try
            {
                nlr = JsonConvert.DeserializeObject<Model.PatientDiagnose>(strNode);
                //if (!string.IsNullOrEmpty(nlr.Cardno) && Common.MatchDic.PatientDiagnoseDic.Keys.Contains(nlr.CardId))//这里需要改
                if (Common.MatchDic.PatientDiagnoseDic.Keys.Contains(nlr.CardId))//这里需要改
                {
                    nlr.id = id;
                    id++;
                    //switch (nlr.ref_flag)
                    //{
                    //    case "1":
                    //        nlr.ref_flag = "高";
                    //        break;
                    //    case "2":
                    //        nlr.ref_flag = "低";
                    //        break;
                    //    case "3":
                    //        nlr.ref_flag = "阳性";
                    //        break;
                    //}
                }
            }
            catch (Exception ex)
            {
                nlr = null;
                Common.LogHelper.WriteError(ex);
            }
            return nlr;
        }
        #endregion

        #region 检查数据对象在本地数据库是否存在 CheckData(Model.PatientDiagnose data)
        /// <summary>
        /// 检查数据对象在本地数据库是否存在 ,true--存在
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckData(Model.PatientDiagnose data)
        {
            bool result = false;
            if (data != null)
            {
                //string whereStr = string.Format("chinese ='{0}' and hospnum ='{1}' and check_date ='{2}' and patname='{3}'", data.Cardno, data.hospnum, data.check_date, data.patname);
                string whereStr = "";
                List<Model.PatientDiagnose> list = this.GetModelList(whereStr);
                if (list != null && list.Count > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion
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

        private List<Model.QueryRecoder> CheckQueryRecord(Model.QueryRecoder model)
        {
            QueryRecoder queryRecoder = new QueryRecoder();
            //查询本地数据库有没有数据
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("Uname = {0} and ", "'" + model.Uname + "'");
            strWhere.AppendFormat("Code = {0} and ", "'" + model.Code + "'");
            strWhere.AppendFormat("CodeType = {0} and ", "'" + model.CodeType + "'");
            strWhere.AppendFormat("IsDel = {0} and ", "'" + model.IsDel + "'");
            strWhere.AppendFormat("QueryType = {0}", "'" + model.QueryType + "'");
            //查询条件是，当前用户添加的卡号为X的卡号类型为Y的没有标记删除的并且临床数据类型为Z的数据
            return queryRecoder.GetModelList(strWhere.ToString());
        }
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
        #endregion

        #region 获取数据
        private string GetWebServiceData(string request)
        {
            try
            {
                return TestAnd(new Model.DTO.PatientDiagnoseResuest("", "2015-01-01")).Replace("\r\n", "").Replace(" ", "");
                //return string.IsNullOrEmpty(request.Request) ? "" : clinicalData.GetPatientDiagnose(request.Request);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
                return ex.Message + "--" + DateTime.Now.ToLongTimeString();
            }
        }
        #endregion
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
        #endregion

        #region 将数据转换成对象
        /// <summary>
        /// 将数据转换成对象
        /// </summary>
        /// <param name="xmlStr">要转换成对象的数据</param>
        /// <returns></returns>
        private Model.DTO.JsonModel StrTObject(string xmlStr , Model.DTO.PatientDiagnoseResuest request)
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
                            Model.PatientDiagnose data = JsonConvert.DeserializeObject<Model.PatientDiagnose>(strNode);
                            string strNode1 = JsonConvert.SerializeXmlNode(xd.SelectSingleNode("//DiagnoseInfo"), Newtonsoft.Json.Formatting.None, true);
                            Model.DTO.DiagnoseInfoModel dg = JsonConvert.DeserializeObject<Model.DTO.DiagnoseInfoModel>(strNode1);
                            data.RegisterNo = dg.RegisterNo;
                            data.Type = dg.Type;
                            data.Icd = dg.Icd;
                            data.Flag = dg.Flag;
                            data.DiagnoseDate = dg.DiagnoseDate;
                            data.Diagnose = dg.Diagnose;
                            data.Cardno = request.cardno;
                            data.Csrq00 = request.cxrq00;
                            if (data == null || data.PatientName == "")
                            {
                            }
                            else
                            {

                                jsonData.Data = data;
                                //PatientDiagnose bll = new PatientDiagnose();
                                //bll.Add(data);
                                //BLL.SZY.QueryRecoder bll_Q = new SZY.QueryRecoder();
                                //QueryRecoder bl_Q = new QueryRecoder();
                                ////更新记录表
                                //DataSet ds = bll_Q.GetReciprocalFirstData_BLL();
                                //if (ds.Tables[0].Rows.Count>0)
                                //{
                                    
                                //}
                                //Model.QueryRecoder model_Q = new Model.QueryRecoder();
                                //model_Q.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
                                //model_Q.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                                //model_Q.Uname = ds.Tables[0].Rows[0]["Uname"].ToString();
                                //model_Q.AddDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["AddDate"].ToString());
                                //model_Q.LastQueryDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["LastQueryDate"].ToString());
                                //model_Q.QueryType = "PatientDiagnose";
                                //model_Q.QueryResult = ds.Tables[0].Rows[0]["QueryResult"].ToString();
                                //model_Q.IsDel = false;
                                //bl_Q.Add(model_Q);
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
        #endregion
        //获取数据
        //解析数据
        //返回数据对象
    }
}

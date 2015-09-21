using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RuRo.BLL
{
    //查询临床检测记录前需要查询的检测项目数据。
    //此类中的方法需要返回该类的对象集合
    public partial class NormalLisItems
    {
        public List<string> RequestList { get; set; }
        private ClinicalData.PacsLisReportServices clinicalData = new ClinicalData.PacsLisReportServices();

        //private Model.DTO.NormalLisItemsRequest request;
        public List<Model.NormalLisItems> GetData(Model.QueryRecoder model, bool queryBycode)
        {
            BLL.Request.NormalLisItemsRequest normalLisItemsRequest = new Request.NormalLisItemsRequest(model);
            normalLisItemsRequest.CreatRequest(queryBycode);
            Model.QueryRecoder queryRecoderModel = normalLisItemsRequest.QueryRecoderModel;
            this.RequestList = normalLisItemsRequest.RequestStr;//临时变量
            List<Model.NormalLisItems> normalLisItems = new List<Model.NormalLisItems>();
            //   保存记录（查询记录数据,更新或添加）  string.IsNullOrEmpty(cq.RequestStr)存在值 修改修！string.IsNullOrEmpty(cq.RequestStr) kaka
            if (normalLisItemsRequest.RequestStr != null && normalLisItemsRequest.RequestStr.Count > 0)
            {
                //   调用接口获取数据
                //  正常这里只会传入一个连接字符串
                foreach (var item in normalLisItemsRequest.RequestStr)
                {
                    string xmlStr = GetWebServiceData(item);
                    string Msg = "";
                    List<Model.NormalLisItems> tem = new List<Model.NormalLisItems>();
                    //  将xml数据转换成list集合会查询本地数据库去除重复项
                    tem = this.GetList(xmlStr, out Msg);
                    if (string.IsNullOrEmpty(Msg))
                    {
                        normalLisItems.AddRange(tem);
                    }
                }
            }

            return normalLisItems;
        }

        /// <summary>
        /// 字典转化为model
        /// </summary>
        /// <returns></returns>
        public Model.NormalLisItems DicToNormalLisItemsModel(Dictionary<string, string> dic)
        {
            string str = JsonConvert.SerializeObject(dic);
            Model.NormalLisItems NormalLisItems = JsonConvert.DeserializeObject<Model.NormalLisItems>(str);
            return NormalLisItems;
        }

        private List<Dictionary<string, string>> GetClinicalInfoDgDicList(string dataStr)
        {
            string clinicalInfoDg = dataStr;//dg
            //页面上临床数据对象集合
            List<Model.NormalLisItems> pageClinicalInfoList = new List<Model.NormalLisItems>();
            List<Dictionary<string, string>> ClinicalInfoDgDicList = new List<Dictionary<string, string>>();
            //将页面上的临床信息转换成对象集合

            if (!string.IsNullOrEmpty(clinicalInfoDg) && clinicalInfoDg != "[]")
            {
                //转换页面上的clinicalInfoDg为对象集合
                pageClinicalInfoList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Model.NormalLisItems>>(clinicalInfoDg);//转换ok
            }
            Model.NormalLisItems cl = new Model.NormalLisItems();

            foreach (Model.NormalLisItems item in pageClinicalInfoList)
            {
                //给对象拼接--临床数据中需要添加基本信息中的RegisterID,InPatientID
                ClinicalInfoDgDicList.Add(FormToDic.ConvertModelToDic(item));
            }
            return ClinicalInfoDgDicList;
        }

        private List<Dictionary<string, string>> GetClinicalInfoDgDicList_Q(string dataStr)
        {
            List<Model.NormalLisItems> pageClinicalInfoList = new List<Model.NormalLisItems>();
            Model.NormalLisItems cl = new Model.NormalLisItems();
            List<Dictionary<string, string>> ClinicalInfoDgDicList = new List<Dictionary<string, string>>();
            if (!string.IsNullOrEmpty(dataStr) && dataStr != "[]")
            {
                //转换页面上的clinicalInfoDg为对象集合
                pageClinicalInfoList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Model.NormalLisItems>>(dataStr);//转换ok
            }
            foreach (Model.NormalLisItems item in pageClinicalInfoList)
            {
                //给对象拼接--临床数据中需要添加基本信息中的RegisterID,InPatientID
                ClinicalInfoDgDicList.Add(FormToDic.ConvertModelToDic(item));
            }
            return ClinicalInfoDgDicList;
        }

        private string PostData(string str)
        {
            UnameAndPwd up = new UnameAndPwd();
            string result = FreezerProUtility.Fp_BLL.TestData.ImportTestData(up.GetUp(), "临床检验数据", str);
            return result;
        }

        #region 获取数据 + private string GetData(Model.DTO.NormalLisItemsRequest request)

        private string GetWebServiceData(string request)
        {
            try
            {
                //   return Test("");
                return string.IsNullOrEmpty(request) ? "" : clinicalData.GetNormalLisItems(request);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
                return ex.Message + "--" + DateTime.Now.ToLongTimeString();
            }
        }

        #endregion 获取数据 + private string GetData(Model.DTO.NormalLisItemsRequest request)

        private string Test(string s)
        {
            string str = @"<Response>
                            <InterfaceCode>GetNormalLisItems</InterfaceCode>
                            <ResultCode>0</ResultCode>
                            <ErrorMsg></ErrorMsg>
                            <reocrd>
                              <hospnum>0272099</hospnum>
                              <patname>杨基</patname>
                              <sex>M</sex>
                              <age_month>1</age_month>
                              <age>15</age>
                              <ext_mthd>急诊生化(急,干)</ext_mthd>
                              <test_date>2015-3-14 8:01:52</test_date>
                              <location></location>
                              <DOC_NAME0>何某某</DOC_NAME0>
                              <check_by_name></check_by_name>
                              <check_date></check_date>
                            </reocrd>
                            <reocrd>
                              <hospnum>0272099</hospnum>
                              <patname>杨基</patname>
                              <sex>M</sex>
                              <age_month>1</age_month>
                              <age>15</age>
                              <ext_mthd>全血分析(五分类)</ext_mthd>
                              <test_date>2015-3-14 8:01:52</test_date>
                              <location>东区七楼皮门</location>
                              <DOC_NAME0>何某某</DOC_NAME0>
                              <check_by_name>何某某</check_by_name>
                              <check_date>2015-3-14 9:05:53</check_date>
                            </reocrd>
                        <Response>";
            return str;
        }

        #region xmlNode转换成obj + Model.NormalLisItems XmlTomModel(XmlNode xd)

        private Model.NormalLisItems XmlTomModel(XmlNode xd)
        {
            int id = 0;
            string strNode = JsonConvert.SerializeXmlNode(xd, Newtonsoft.Json.Formatting.None, true);
            Model.NormalLisItems nlr;
            try
            {
                nlr = JsonConvert.DeserializeObject<Model.NormalLisItems>(strNode);
                if (!string.IsNullOrEmpty(nlr.ext_mthd) && Common.MatchDic.NeedRecordDic.Values.Contains(nlr.ext_mthd))
                {
                    nlr.Id = id;
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
                    //        nlr.ref_flag = "#";
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

        #endregion xmlNode转换成obj + Model.NormalLisItems XmlTomModel(XmlNode xd)

        private bool CheckData(Model.NormalLisItems data)
        {
            bool result = false;
            if (data != null)
            {
                string whereStr = string.Format("ext_mthd ='{0}' and hospnum ='{1}' and check_date ='{2}' and patname='{3}'", data.ext_mthd, data.hospnum, data.check_date, data.patname);
                List<Model.NormalLisItems> list = this.GetModelList(whereStr);
                if (list != null && list.Count > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建返回数据对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Model.DTO.JsonModel CreatJsonModel(object obj, string msg)
        {
            Model.DTO.JsonModel jsonModel = new Model.DTO.JsonModel();
            if (string.IsNullOrEmpty(msg))
            {
                jsonModel.Data = obj;
                jsonModel.Msg = "查询成功";
                jsonModel.Statu = "ok";
            }
            else
            {
                jsonModel.Data = obj;
                jsonModel.Msg = msg;
                jsonModel.Statu = "err";
            }
            return jsonModel;
        }

        private List<Model.NormalLisItems> GetList(string xmlStr, out string Msg)
        {
            List<Model.NormalLisItems> list = new List<Model.NormalLisItems>();
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
                            //  有数据
                            XmlNodeList xnl = xd.SelectNodes("//reocrd");
                            if (xnl.Count > 0)
                            {
                                foreach (XmlNode item in xnl)
                                {
                                    Model.NormalLisItems nn = this.XmlTomModel(item);
                                    if (nn.ext_mthd != null && Common.MatchDic.NeedRecordDic.Values.Contains(nn.ext_mthd))
                                    {
                                        if (!string.IsNullOrEmpty(nn.ext_mthd))
                                        {
                                            if (!this.CheckData(nn))
                                            {
                                                list.Add(nn);
                                            }
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
                            //  查询数据出错，联接无问题
                            Msg = xd.SelectSingleNode("//ErrorMsg").InnerText;
                        }
                    }
                    else
                    {
                        // 查询数据出错，联接无问题
                        Msg = xd.InnerText;
                        //保存查询记录
                    }
                }
            }
            return list;
        }

        //private bool SaveQueryRecord(ref Model.DTO.NormalLisItemsRequest resquet, string Msg, string codeType)
        //{
        //    bool result = false;
        //    QueryRecoder queryRecoder = new QueryRecoder();
        //    //根据传入的查询字符串创建的当此查询的记录model
        //    Model.QueryRecoder model = new Model.QueryRecoder();

        //    //model.AddDate = DateTime.Now;
        //    model.Code = request.hospnum;
        //    model.CodeType = codeType;
        //    model.QueryType = "NormalLisItems";
        //    model.Uname = Common.CookieHelper.GetCookieValue("username");
        //    model.IsDel = false;

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
        //            int chaday = (dtLastQuery - dtAdd).Days;//获取最后查询日期与添加日期时间差
        //            int nowday = (dt - dtLastQuery).Days;//当前时间与最后查询时间差
        //            int addnowDay = (dt - dtAdd).Days;//当前时间与添加时间差
        //            if (nowday == 0)
        //            {
        //                //resquet.jsrq00 = "";
        //                //resquet.ksrq00 = "";
        //                result = true;
        //                model.IsDel = false;
        //                model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg + oldModel.QueryResult;
        //            }
        //            if (nowday > 0)//当前时间与最后查询时间差大于0
        //            {
        //                if (addnowDay == 0)
        //                {
        //                    result = true;
        //                    model.IsDel = false;
        //                    model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg + oldModel.QueryResult;
        //                }
        //                if (chaday >= getDays)
        //                {
        //                    model.IsDel = true;
        //                }
        //                else
        //                {
        //                    resquet.ksrq00 = dtLastQuery.ToString("yyyy-MM-dd");
        //                    resquet.jsrq00 = dtLastQuery.AddDays(1).ToString("yyyy-MM-dd");
        //                    result = true;
        //                    //添加日期是距离添加日期是5天内的
        //                    model.IsDel = false;
        //                    model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg + oldModel.QueryResult;
        //                }
        //            }
        //        }
        //        //本地数据库有数据
        //        result = queryRecoder.Update(model);
        //    }
        //    else
        //    {
        //        model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg.Trim();
        //        model.AddDate = DateTime.Now;
        //        model.LastQueryDate = DateTime.Now;
        //        bool bo = queryRecoder.Add(model) > 0;
        //        model.QueryType = "PatientDiagnose";
        //        bool boo = queryRecoder.Add(model) > 0;
        //        if (bo && boo)
        //        {
        //            result = true;
        //        }
        //        else { result = false; }
        //    }
        //    return result;
        //}

        private List<Model.QueryRecoder> CheckQueryRecord(Model.QueryRecoder model)
        {
            QueryRecoder queryRecoder = new QueryRecoder();
            //查询本地数据库有没有数据
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("Uname = {0} and ", "'" + model.Uname + "'");
            strWhere.AppendFormat("Code = {0} and ", "'" + model.Code + "'");
            strWhere.AppendFormat("CodeType = {0} and ", "'" + model.CodeType + "'");
            strWhere.AppendFormat("IsDel = {0} and ", "'" + model.IsDel + "'");
            strWhere.AppendFormat("QueryType = {0} and  ", "'" + model.QueryType + "'");
            strWhere.AppendFormat("IsDel = {0}", "'" + false + "'");

            //查询条件是，当前用户添加的卡号为X的卡号类型为Y的没有标记删除的并且临床数据类型为Z的数据
            return queryRecoder.GetModelList(strWhere.ToString());
        }

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
    }
}
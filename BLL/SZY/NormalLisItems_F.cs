using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RuRo.BLL.SZY
{
    public class NormalLisItems_F
    {
        public List<string> RequestList { get; set; }
        private ClinicalData.PacsLisReportServices clinicalData = new ClinicalData.PacsLisReportServices();

        //private Model.DTO.NormalLisItemsRequest request;
        public List<Model.NormalLisItems> GetData(Model.DTO.NormalLisReport_list_F model)
        {
            List<string> requestStr = new List<string>();
            requestStr = GetRequestStr(model);
            List<Model.NormalLisItems> normalLisItems = new List<Model.NormalLisItems>();
            //保存记录（查询记录数据,更新或添加）  string.IsNullOrEmpty(cq.RequestStr)存在值 修改修！string.IsNullOrEmpty(cq.RequestStr) kaka
            if (requestStr != null && requestStr.Count > 0)
            {
                //   调用接口获取数据
                //  正常这里只会传入一个连接字符串
                foreach (var item in requestStr)
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

        private List<string> GetRequestStr(Model.DTO.NormalLisReport_list_F model)
        {
            List<string> list = new List<string>();
            DateTime ksrq00 = new DateTime();
            DateTime jsrq00 = new DateTime();
            if (DateTime.TryParse(model.ksrq00, out ksrq00) && DateTime.TryParse(model.jsrq00, out jsrq00))
            {
                if (ksrq00 <= jsrq00)
                {
                    string str = CreatRequestStr(model.code, ksrq00, jsrq00);
                    list.Add(str);
                }
                if (ksrq00 > jsrq00)
                {
                    string str = CreatRequestStr(model.code, jsrq00, ksrq00);
                    list.Add(str);
                }
            }
            return list;
        }
        private string CreatRequestStr(string code, DateTime ksrq00, DateTime jsrq00)
        {
            return string.Format("<Request><hospnum>{0}</hospnum><ksrq00>{1}</ksrq00><jsrq00>{2}</jsrq00></Request>", code, ksrq00.ToString("yyyy-MM-dd"), jsrq00.ToString("yyyy-MM-dd"));
        }
        #region 获取数据 + private string GetData(Model.DTO.NormalLisItemsRequest request)

        private string GetWebServiceData(string request)
        {
            try
            {
                 //return Test("");
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
                              <ext_mthd>肝功6项</ext_mthd>
                              <test_date>2015-3-14 8:01:52</test_date>
                              <location>东区七楼皮门</location>
                              <DOC_NAME0>何某某</DOC_NAME0>
                              <check_by_name>何某某</check_by_name>
                              <check_date>2015-3-14 9:05:53</check_date>
                            </reocrd>
                        </Response>";
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
            BLL.NormalLisItems normalLisItems = new NormalLisItems();
            if (data != null)
            {
                string whereStr = string.Format("ext_mthd ='{0}' and hospnum ='{1}' and check_date ='{2}' and patname='{3}'", data.ext_mthd, data.hospnum, data.check_date, data.patname);
                List<Model.NormalLisItems> list = normalLisItems.GetModelList(whereStr);
                if (list != null && list.Count > 0)
                {
                    result = true;
                }
            }
            return result;
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
                                     //&& Common.MatchDic.NeedRecordDic.Values.Contains(nn.ext_mthd)
                                    if (nn.ext_mthd != null)
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
    }
}

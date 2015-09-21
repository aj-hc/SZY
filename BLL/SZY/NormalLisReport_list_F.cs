using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RuRo.BLL.SZY
{
    public class NormalLisReport_list_F
    {
        private ClinicalData.PacsLisReportServices clinicalData = new ClinicalData.PacsLisReportServices();
        private Model.QueryRecoder queryRecoderModel = new Model.QueryRecoder();
        private List<string> requestStrList = new List<string>();

        public string GetData(Model.DTO.NormalLisReport_list_F model)
        {
            //List<Model.NormalLisItems> normalLisItemsList = GetNormalLisItemsList(model, queryBycode);
            //requestStr = GetRequestList(normalLisItemsList);
            Model.DTO.JsonModel jsonmodel = new Model.DTO.JsonModel() { Statu = "err", Msg = "无数据", Data = "" };
            List<string> requestStr = new List<string>();
            requestStr = GetRequestStr(model);
            if (requestStr != null && requestStr.Count > 0)
            {
                List<Model.NormalLisReport> normalLisReportList = new List<Model.NormalLisReport>();
                StringBuilder msg = new StringBuilder();
                //调用接口获取数据
                foreach (var item in requestStr)
                {
                    string xmlStr = GetWebServiceData(item);
                    string Msg = "";
                    //将xml数据转换成list集合会查询本地数据库去除重复项
                    List<Model.NormalLisReport> nnn = this.GetList(xmlStr, out Msg);
                    if (nnn != null && nnn.Count > 0)
                    {
                        //有数据
                        normalLisReportList.AddRange(nnn);
                        if (!string.IsNullOrEmpty(Msg))
                        {
                            msg.Replace("&nbsp", "");
                            msg.Replace(" ", "");
                            msg.Append(" &nbsp " + Msg);
                        }
                    }
                    else
                    {
                        //无数据
                        jsonmodel = CreatJsonMode("err", Msg, nnn);
                        //ChangeQueryRecordStatu(this.queryRecoderModel, Msg);
                    }
                }
                if (normalLisReportList != null && normalLisReportList.Count > 0)
                {
                    jsonmodel = CreatJsonMode("ok", msg.ToString(), normalLisReportList);
                    //更新记录表状态
                    //ChangeQueryRecordStatu(this.queryRecoderModel, msg.ToString());
                }
                else
                {
                    //无数据
                    jsonmodel = CreatJsonMode("err", msg.ToString(), normalLisReportList);
                    //ChangeQueryRecordStatu(this.queryRecoderModel, msg.ToString());
                }
            }
            return JsonConvert.SerializeObject(jsonmodel);
        }

        private List<string> GetRequestStr(Model.DTO.NormalLisReport_list_F model)
        {
            BLL.SZY.NormalLisItems_F normalLisItems_F = new NormalLisItems_F();
            List<Model.NormalLisItems> list = new List<Model.NormalLisItems>();
            list = GetNormalLisItemsList(model);
            List<string> listStr = new List<string>();
            listStr = GetRequestList(list,model);
            return listStr;
        }
        private string GetWebServiceData(string request)
        {
            try
            {
                //return Test("");
                return string.IsNullOrEmpty(request) ? "" : clinicalData.NormalLisReport(request);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
                return ex.Message + "--" + DateTime.Now.ToLongTimeString();
            }
        }

        private Model.NormalLisReport XmlTomModel(XmlNode xd)
        {
            int id = 0;
            string strNode = JsonConvert.SerializeXmlNode(xd, Newtonsoft.Json.Formatting.None, true);
            Model.NormalLisReport nlr;
            try
            {
                nlr = JsonConvert.DeserializeObject<Model.NormalLisReport>(strNode);
                if (!string.IsNullOrEmpty(nlr.ref_flag) && Common.MatchDic.NeedRecordDic.Keys.Contains(nlr.chinese))
                {
                    nlr.Id = id;
                    id++;
                    switch (nlr.ref_flag)
                    {
                        case "1":
                            nlr.ref_flag = "高";
                            break;

                        case "2":
                            nlr.ref_flag = "低";
                            break;

                        case "3":
                            nlr.ref_flag = "#";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                nlr = null;
                Common.LogHelper.WriteError(ex);
            }
            return nlr;
        }

        private bool CheckData(Model.NormalLisReport data)
        {
            BLL.NormalLisReport normalLisReport = new BLL.NormalLisReport();
            bool result = false;
            if (data != null)
            {
                string whereStr = string.Format("chinese ='{0}' and hospnum ='{1}' and check_date ='{2}' and patname='{3}'", data.chinese, data.hospnum, data.check_date, data.patname);
                List<Model.NormalLisReport> list = normalLisReport.GetModelList(whereStr);
                if (list != null && list.Count > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        private List<Model.NormalLisReport> GetList(string xmlStr, out string Msg)
        {
            List<Model.NormalLisReport> list = new List<Model.NormalLisReport>();
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
                                    try
                                    {
                                        Model.NormalLisReport nn = this.XmlTomModel(item);
                                       // && Common.MatchDic.NeedRecordDic.Keys.Contains(nn.chinese)
                                        if (nn.chinese != null )
                                        {
                                            if (!string.IsNullOrEmpty(nn.ref_flag))
                                            {
                                                if (!this.CheckData(nn))
                                                {
                                                    list.Add(nn);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Common.LogHelper.WriteError(ex);
                                        continue;
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

        private List<Model.NormalLisItems> GetNormalLisItemsList(Model.DTO.NormalLisReport_list_F model)
        {
            BLL.SZY.NormalLisItems_F normalLisItems = new NormalLisItems_F();
            //normalLisItemsRequest.CreatRequest(queryByCode);
            //将前台页面传入的model和标识传入到NormalLisItems中产生request
            List<Model.NormalLisItems> list = normalLisItems.GetData(model);
            //查询完毕之后的QueryRecoder 对象
            return list;
        }

        private List<string> GetRequestList(List<Model.NormalLisItems> list, Model.DTO.NormalLisReport_list_F model)
        {
            List<string> temListStr = new List<string>();
            Model.DTO.NormalLisItemsRequest normalLisItemsRequestModel = new Model.DTO.NormalLisItemsRequest();
            if (list != null && list.Count > 0)
            {
                //生成查询检验信息的查询字符串
                foreach (var item in list)
                {
                    string request = string.Format("<Request><hospnum>{0}</hospnum><ksrq00>{1}</ksrq00><jsrq00>{2}</jsrq00><ext_mthd>{3}</ext_mthd></Request>", model.code, model.ksrq00, model.jsrq00, item.ext_mthd);
                    if (!string.IsNullOrEmpty(request) && !temListStr.Contains(request))
                    {
                        temListStr.Add(request);
                    }
                }
            }

            return temListStr;
        }

        private string Test(string s) 
        {
            string str = @"<Response>
                <ResultCode>0</ResultCode>
                <ErrorMsg></ErrorMsg>
                <reocrd>
                  <hospnum>64456507</hospnum>
                  <patname>林成某</patname>
                  <sex>M</sex>
                  <age_month>1</age_month>
                  <age>15</age>
                  <ext_mthd>肝功6项</ext_mthd>
                  <result>24</result>
                  <ref_flag>1</ref_flag>
                  <lowvalue>15</lowvalue>
                  <highvalue>40</highvalue>
                  <print_ref></print_ref>
                  <check_date>2015-03-14</check_date>
                  <check_by_name>李某某</check_by_name>
                  <units>U/L</units>
                  <prnt_order>5</prnt_order>
                  <chinese>谷草转氨酶(AST)</chinese>
                </reocrd>
                </Response>";
            return str;
        }
    }
}

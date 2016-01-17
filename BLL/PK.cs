using Newtonsoft.Json;
using RuRo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Xml;

namespace RuRo.BLL
{
    public class PK
    {


        //解析数据
        //返回数据对象
        public List<Model.PatientDiagnose> XmlStrToPatientDiagnoseResuestForZhuYuan(XmlNodeList xml, XmlNamespaceManager nsmgr)
        {
            Model.DTO.PatientDiagnoseResuest pdr = new Model.DTO.PatientDiagnoseResuest();
            List<Model.PatientDiagnose> list = new List<Model.PatientDiagnose>();
            Model.PatientDiagnose patient = new Model.PatientDiagnose();
            if (xml.Count > 0)
            {
                foreach (var item in xml)
                {
                    XmlElement xe = (XmlElement)item;
                    try
                    {
                        if (xe != null)
                        {
                            XmlNodeList xn = xe.SelectNodes("ab:diagnosis", nsmgr);
                            if (xn.Count > 0 && xn != null)
                            {
                                for (int i = 0; i < xn.Count; i++)
                                {
                                    //把数据赋值给PatientDiagnose
                                    patient.Cardno = xe.SelectSingleNode("ab:patientNo", nsmgr).InnerText;
                                    patient.PatientName = xe.SelectSingleNode("ab:name", nsmgr).InnerText;
                                    patient.Sex = xe.SelectSingleNode("ab:sexName", nsmgr).InnerText;
                                    DateTime dt = new DateTime();
                                    dt = Convert.ToDateTime(xe.SelectSingleNode("ab:birthday", nsmgr).InnerText);
                                    patient.Brithday = dt;
                                    patient.Csrq00 = DateTime.Now.ToString("yyyy-MM-dd");
                                    patient.Icd = xn[i].SelectSingleNode("ab:diagnosisCode", nsmgr).InnerText;
                                    patient.Diagnose = xn[i].SelectSingleNode("ab:diagnosisName", nsmgr).InnerText;
                                    patient.Type = xn[i].SelectSingleNode("ab:type", nsmgr).InnerText;
                                    if (xn[i].SelectSingleNode("ab:type", nsmgr).InnerText.Contains("中医"))
                                    {
                                        patient.Flag = "中医诊断";
                                    }
                                    if (xn[i].SelectSingleNode("ab:type", nsmgr).InnerText.Contains("西医"))
                                    {
                                        patient.Flag = "西医诊断";
                                    }
                                    patient.DiagnoseDate = xn[i].SelectSingleNode("ab:diagnosisDate", nsmgr).InnerText;
                                    list.Add(patient);
                                    patient = new Model.PatientDiagnose();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<Model.PatientDiagnose> GetHTTPWebServiceData(string request)
        {
            //方法1：通过HTTP获取数据
            //string strmethod = System.Configuration.ConfigurationManager.AppSettings["Pmethod"];
            //string strxml = PostData(request,strmethod);
            //方法2：通过服务引用获取数据
            //GetQueryPT.ChasCommonPTClient pt = new GetQueryPT.ChasCommonPTClient();//通过服务引用 一般不能用
            //string strxml = pt.queryPatient(request);
            //方法3：添加服务引用获取数据
            //PTtest.ChasCommonSvc ppt = new PTtest.ChasCommonSvc();//通过WEB服务获取
            //string strxml = ppt.queryPatient(request);
            //XmlDocument xm = new XmlDocument();
            //xm.LoadXml(strxml);
            //本地读取XML
            XmlDocument xm = RuRo.Common.XmlHelper.XMLLoad("XML//queryPatientResp.xml");
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xm.NameTable);
            nsmgr.AddNamespace("ab", "http://chas.hit.com/transport/integration/common/msg");
            XmlNodeList respHeaderlist = xm.SelectNodes("//ab:respHeader", nsmgr);
            XmlNodeList patientlist = null;
            List<Model.PatientDiagnose> patientDiagnoseList = new List<Model.PatientDiagnose>();
            if (respHeaderlist.Count > 0 && respHeaderlist[0].InnerText.Contains("000000"))
            {
                patientlist = xm.SelectNodes("//ab:patient", nsmgr);
                patientDiagnoseList = XmlStrToPatientDiagnoseResuestForZhuYuan(patientlist, nsmgr);
            }
            else
            {

            }
            return patientDiagnoseList;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetHTTPWebServiceDataold(string request) 
        {
            string strmethod = System.Configuration.ConfigurationManager.AppSettings["Pmethod"];
            //修改为读取HTTP请求WEBSERVICE
            //string xm = PostData(request, strmethod);//通过HTTP请求获取
            //GetQueryPT.ChasCommonPTClient pt = new GetQueryPT.ChasCommonPTClient();//通过服务引用 一般不能用
            //string add= pt.queryPatient(request);
            PTtest.ChasCommonSvc ppt = new PTtest.ChasCommonSvc();//通过WEB服务获取
            string xm = ppt.queryPatient(request);
            ////本地读取XML
            //XmlDocument xml = RuRo.Common.XmlHelper.XMLLoad("XML//queryPatientResp.xml");
            //XmlDocument xml = new XmlDocument();
            //xml.LoadXml(xm);
            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            //nsmgr.AddNamespace("ab", "http://chas.hit.com/transport/integration/common/msg");
            //XmlNodeList respHeaderlist = xml.SelectNodes("//ab:respHeader", nsmgr);
            //if (respHeaderlist.Count > 0 && respHeaderlist[0].InnerText.Contains("000000"))
            //{
            //    XmlNodeList patientlist = xml.SelectNodes("//ab:patient", nsmgr);
            //    if (patientlist.Count>0)
            //    {
            //        foreach (var item in patientlist)
            //        {
            //            XmlElement xe = (XmlElement)item;
            //            XmlDocument doc = new XmlDocument();
            //        }
            //    }
            //}
            return xm;
            //XmlNodeList rootNode = xm.GetElementsByTagName("QueryPatientResponse", "http://chas.hit.com/transport/integration/common/msg");
        }

        /// <summary>
        /// 使用HTTP上传
        /// </summary>
        /// <returns></returns>
        [WebMethod()]
        public string PostData(string request,string method)
        {
            RuRo.Common.HttpHelper http = new RuRo.Common.HttpHelper();
            RuRo.Common.HttpItem item = new RuRo.Common.HttpItem()
            {
                URL = System.Configuration.ConfigurationManager.AppSettings["webService2ZhuYuanZhenDuan"],//URL     必需项  
                Method = "post",//URL     可选项 默认为Get  
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                Cookie = "",//字符串Cookie     可选项  
                Referer = "",//来源URL     可选项  
                Postdata = request,//Post数据     可选项GET时不需要写  
                PostEncoding = Encoding.UTF8,
                Timeout = 100000,//连接超时时间     可选项默认为100000  
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000  
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值  
                ContentType = "text/xml",//返回类型    可选项有默认值
                Allowautoredirect = true,//是否根据301跳转     可选项  
                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数  
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024  
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数  
                //ProxyPwd = "123456",//代理服务器密码     可选项  
                //ProxyUserName = "administrator",//代理服务器账户名     可选项  
                ResultType = ResultType.String
            };
            item.Header.Add("SOAPAction", method);
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            string cookie = result.Cookie;
            return html;
        }
        /// <summary>
        /// 传入参数使用这个
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mothod"></param>
        /// <returns></returns>
        public List<string> GetRequestStr(Model.DTO.PatientDiagnose_list_F model,string mothod)
        {
            List<string> list = new List<string>();
            DateTime ksrq00 = new DateTime();
            DateTime jsrq00 = new DateTime();
            if (DateTime.TryParse(model.ksrq00, out ksrq00) && DateTime.TryParse(model.jsrq00, out jsrq00))
            {
                if (ksrq00 <= jsrq00)
                {
                    string str = CreatRequestStrmothod(model.code, ksrq00, jsrq00,mothod);
                    list.Add(str);
                }
                if (ksrq00 > jsrq00)
                {
                    string str = CreatRequestStrmothod(model.code, jsrq00, ksrq00, mothod);
                    list.Add(str);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取传参使用
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ksrq00"></param>
        /// <param name="jsrq00"></param>
        /// <param name="mothod"></param>
        /// <returns></returns>
        private string CreatRequestStrmothod(string code, DateTime ksrq00, DateTime jsrq00, string mothod)
        {
            StringBuilder sb = new StringBuilder();
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strxml = "<?xml version='1.0' encoding='UTF-8' standalone='yes'?>" +
                "<QueryPatientRequest xmlns='http://chas.hit.com/transport/integration/common/msg'>" +
                "<reqHeader>" +
                "<systemId>AJ</systemId>" +
                "<reqTimestamp>2015-12-31 16:33:45</reqTimestamp>" +
                "<terminalIp>192.168.1.40</terminalIp>" +
                "</reqHeader>" +
                "<patientNo>3027474</patientNo>" +
                "<startDate>2012-10-21</startDate>" +
                "<endDate>2012-10-23</endDate>" +
                "<dateField>"+mothod+"</dateField>" +
                "<queryMode>ALL</queryMode>" +
                "</QueryPatientRequest>";
            return strxml;
        }

        //解析数据
        //返回数据对象
        //老版本暂时用来参考
        public List<Model.PatientDiagnose> XmlStrToPatientDiagnoseResuestForZhuYuan(string xml, out string _msg)
        {
            List<Model.PatientDiagnose> list = new List<Model.PatientDiagnose>();
            _msg = "";
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(xml);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("ab", "http://chas.hit.com/transport/integration/common/msg");
                XmlNodeList respHeaderlist = xmldoc.SelectNodes("//ab:respHeader", nsmgr);
                if (respHeaderlist.Count > 0 && respHeaderlist[0].InnerText.Contains("000000"))//判断数据是否成功获取
                {
                    XmlNodeList paitentlist = xmldoc.SelectNodes("//ab:patient", nsmgr);//读取病人数据
                    if (paitentlist.Count > 0)
                    {
                        for (int i = 0; i < paitentlist.Count; i++)
                        {
                            XmlNode xn = paitentlist[i];//基本信息
                            XmlNodeList diagnosis = xn.SelectNodes("//ab:diagnosis", nsmgr);//读取诊断信息
                            if (diagnosis.Count > 0)
                            {
                                for (int j = 0; j < diagnosis.Count; j++)
                                {
                                    //按照类型需要添加进去就好了

                                    Model.PatientDiagnose model = new Model.PatientDiagnose();
                                    DateTime dt = Convert.ToDateTime(xn.SelectSingleNode("//ab:birthday", nsmgr).InnerText);
                                    //基本的
                                    model.Cardno = "";//查询卡号
                                    model.Csrq00 = "";//查询日期
                                    model.PatientName = xn.SelectSingleNode("//ab:name", nsmgr).InnerText;
                                    model.Sex = xn.SelectSingleNode("//ab:sexName", nsmgr).InnerText;
                                    model.Brithday = dt;
                                    model.CardId = xn.SelectSingleNode("//ab:idCardNo", nsmgr).InnerText;
                                    model.Tel = xn.SelectSingleNode("//ab:phone", nsmgr).InnerText;
                                    model.Icd = xn.SelectSingleNode("//ab:diagnosisCode", nsmgr).InnerText;
                                    //诊断的
                                    model.Diagnose = diagnosis[j].SelectSingleNode("//ab:diagnosisName", nsmgr).InnerText;
                                    model.Type = diagnosis[j].SelectSingleNode("//ab:type", nsmgr).InnerText;
                                    if (model.Type.Contains("中医"))
                                    {
                                        model.Flag = "中医诊断";
                                    }
                                    else if (model.Type.Contains("西医"))
                                    {
                                        model.Flag = "西医诊断";
                                    }
                                    model.DiagnoseDate = diagnosis[j].SelectSingleNode("//ab:diagnosisDate", nsmgr).InnerText;
                                    list.Add(model);
                                    model = new Model.PatientDiagnose();
                                }
                            }
                            else
                            {
                                _msg = "查询不到诊断数据";
                            }
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    _msg = respHeaderlist[0].InnerText.ToString();
                }
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
                _msg = ex.ToString();
            }
            return list;
        }
    }
}

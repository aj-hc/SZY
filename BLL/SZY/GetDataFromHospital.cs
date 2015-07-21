using RuRo.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Data;
namespace RuRo.BLL
{
    public class GetDataFromHospital
    {

        #region 1.0 获取基本数据
        #region 1.0.1 UI层调用--根据条码获取数据
        /// <summary>
        /// 使用条码获取数据，可能返回OPListForSpecimen单个字符串、正常数据、“”；
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <returns>Json格式的字符串（包含有隐藏域文件）</returns>
        public string GetDataByBarcode(string Mzhzyh, string Mzzybz)
        {
            //01.创建字典
            //基本数据
            Dictionary<string, string> BasicDataDic = new Dictionary<string, string>();
            //02.调用方法获取数据并将数据转换成字典
            BasicDataDic = GetBasicDataAndToDic(Mzhzyh, Mzzybz);
            //ClinicalDataDic = GetClinicalDataDic(Mzhzyh);//获取临床数据（页面上不需要所以不获取）
            string result = "";
            if (BasicDataDic != null)//链接无错误
            {
                if (BasicDataDic.Count > 0)//有获取到数据
                {
                    //03.调用方法将字典转换成JSON数据
                    if (BasicDataDic.Keys.Contains("ErrorMsg"))
                    {
                        string value = "";
                        if (BasicDataDic.TryGetValue("ErrorMsg", out value))
                        {
                            result = "{\"ErrorMsg:\":\"" + value + "\"}";
                        }
                    }
                    else if (BasicDataDic.Count > 1)
                    {
                        result = ConvertDicToJsonStr(BasicDataDic);
                    }
                }
            }
            return result;
        }
        #endregion

        #region 1.0.2 获取基本数据 +  private Dictionary<string, string> GetBasicDataAndToDic(string Mzhzyh, string Mzzybz)
        /// <summary>
        /// 获取基本数据
        /// </summary>
        /// <param name="Mzhzyh">住院号或者门诊号</param>
        /// <param name="Mzzybz">号码标识</param>
        /// <returns>获取到的数据</returns>
        private Dictionary<string, string> GetBasicDataAndToDic(string Mzhzyh, string Mzzybz)
        {
            string request = string.Format("<Request><Mzhzyh>{0}</Mzhzyh><Mzzybz>{1}</Mzzybz></Request>", Mzhzyh, Mzzybz);
            //01.创建字典
            Dictionary<string, string> hospitalDic = new Dictionary<string, string>();
            //02.创建webservice获取数据
            BasicData.EmpiService basicData = new BasicData.EmpiService();
            try
            {
                ////02.1 获取单个人的数据
                //string getDataFromHospitalStr = basicData.GetEmpiInfo(request);

                ////一下时产生随机的患者基本数据
                Common.RandomTest r = new RandomTest();
                string name = r.CreatName();
                string getDataFromHospitalStr = string.Format("<Response><InterfaceCode>GetEmpiInfo</InterfaceCode><ResultCode>{0}</ResultCode><ErrorMsg>出错了</ErrorMsg><EmpiInfo><EmpiId>{1}</EmpiId><PatientName>{2}</PatientName><Sex>{3}</Sex><Birthday>{4}</Birthday><CardId>{5}</CardId><Tel>{6}</Tel><Address>{7}</Address></EmpiInfo></Response>", "0", r.CreatNum(), r.CreatName(), r.CreatSex(), r.CreatBirthday().ToShortDateString(), "110", "100000000", "广州");

                if (getDataFromHospitalStr != null && getDataFromHospitalStr != "")//有数据并且不为null
                {
                    //03.将数据转换成xml数据有数据并且能转换成xml文档
                    XmlDocument getDataFromHospitalXml = new XmlDocument();
                    //转换失败时返回null
                    getDataFromHospitalXml = HospitalXmlStrHelper.HospitalXmlStrToXmlDoc(getDataFromHospitalStr);
                    //转换成功
                    if (getDataFromHospitalXml != null)
                    {
                        //有子元素
                        if (getDataFromHospitalXml.HasChildNodes)
                        {
                            //获取到能转换成XML文档的数据，判断文档中的ResultCode是否为0
                            if (getDataFromHospitalXml.SelectSingleNode("//ResultCode").InnerText == "0")
                            {
                                string ss = getDataFromHospitalXml.SelectSingleNode("//EmpiInfo").OuterXml;
                                hospitalDic = ConvertBasicXmlDataStrToDic(ss);

                                //0 门诊 1住院
                                if (Mzzybz == "0")
                                {
                                    hospitalDic.Add("MzhNo", Mzhzyh);
                                }
                                if (Mzzybz == "1")
                                {
                                    hospitalDic.Add("ZyhNo", Mzhzyh);
                                }
                            }
                            else
                            {
                                //获取数据成功，但是获取到的数据不是需要的病人数据
                                hospitalDic.Add("ErrorMsg", getDataFromHospitalXml.SelectSingleNode("/ErrorMsg").Value);
                            }
                        }
                    }
                }
                else //没数据返回
                {
                    hospitalDic.Clear();
                }
            }
            catch (Exception ex)
            {
                hospitalDic.Clear();
            }
            return hospitalDic;
        }
        #endregion

        #region 1.0.2.1 将基本数据转换成字典
        /// <summary>
        /// 将基本数据转换成字典
        /// </summary>
        /// <param name="basciDataEmpiInfoStr"></param>
        /// <returns></returns>
        private Dictionary<string, string> ConvertBasicXmlDataStrToDic(string basciDataEmpiInfoStr)
        {
            //BasicDataDic字典
            Dictionary<string, string> BasicDataDic = new Dictionary<string, string>();
            //将BasicDataDic  XML格式的数据转换成XML文档
            XmlDocument BasicDataEmpiInfoXmlDoc = new XmlDocument();
            BasicDataEmpiInfoXmlDoc = HospitalXmlStrHelper.HospitalXmlStrToXmlDoc(basciDataEmpiInfoStr);
            //能转换成xml文档
            if (BasicDataEmpiInfoXmlDoc != null)
            {
                //获取文档下的子节点集合
                XmlNodeList empiInfoNodeList = BasicDataEmpiInfoXmlDoc.SelectNodes("/EmpiInfo/*");
                //有子节点
                if (empiInfoNodeList.Count > 0)
                {
                    //循环节点
                    foreach (XmlNode item in empiInfoNodeList)
                    {
                        //字典中不包含当前节点name就加入到节点中
                        if (!BasicDataDic.Keys.Contains(item.Name))
                        {
                            BasicDataDic.Add(item.Name, item.InnerText);
                        }
                    }
                }
            }
            else
            {
                //xml文档为null
                BasicDataDic.Clear();
            }
            return BasicDataDic;
        }
        #endregion

        #region 1.0.3 将基本数据转换成前台页面需要的字符串
        /// <summary>
        /// 将基本数据转换成前台页面需要的字符串
        /// </summary>
        /// <param name="basicDataDic"></param>
        /// <returns></returns>
        private string ConvertDicToJsonStr(Dictionary<string, string> basicDataDic)
        {
            //创建前台页面DataGrid需要的行数据字典
            Dictionary<string, string> BasicDataDicResult = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> item in basicDataDic)
            {
                if (!BasicDataDicResult.Keys.Contains(item.Key))
                {
                    switch (item.Key)
                    {
                        case "ZyhNo":
                            if (!string.IsNullOrEmpty(item.Value))
                            {
                                BasicDataDicResult.Add("sampleSourceName", item.Value);//样本源名称(住院号)
                                BasicDataDicResult.Add("patientId", item.Value);
                            }
                            break;
                        case "MzhNo":
                            if (!string.IsNullOrEmpty(item.Value))
                            {
                                BasicDataDicResult.Add("sampleSourceName", item.Value);//样本源名称(门诊号)
                                BasicDataDicResult.Add("patientId", item.Value);
                            }
                            break;
                        case "PatientName"://患者姓名——对应的是样本源类型和样本源描述
                            BasicDataDicResult.Add("sampleSourceDescription", item.Value == "" ? "" : item.Value);
                            BasicDataDicResult.Add("patientName", item.Value);
                            break;
                        case "Sex":
                            BasicDataDicResult.Add("patientSex", item.Value);
                            break;
                        default:
                            break;
                    }
                }
            }
            //此处需要将整个的医院获取的字典加密之后传入隐藏字段中，直接传会是[object object]
            //0001.序列化oPListForSpecimenDic字典成Json对象
            //string tempHiddenStr = FpJsonHelper.DictionaryToJsonString(basicDataDic);
            //string hidden = EncodeAndDecodeString.Encode(tempHiddenStr);
            //0002.将对象加密成字符串
            BasicDataDicResult.Add("sampleSourceTypeName", "患者信息");
            BasicDataDicResult.Add("importStatus", "待导入");
            //BasicDataDicResult.Add("hidden", hidden);
            //将字典转换成Json对象
            //string oPListForSpecimenDicResultJsonStr = Common.FpJsonHelper.DictionaryToJsonString(BasicDataDicResult);
            //return oPListForSpecimenDicResultJsonStr;
            return "";
        }
        #endregion
        #endregion

        #region 2.0 获取临床数据检验

        #region 将xml转换成对象
        /// <summary>
        /// 将xml转换成对象
        /// </summary>
        /// <param name="xmlNode">xml节点</param>
        /// <returns>检测指标对象</returns>
        private Record XmlNodeToRecord(XmlNode xmlNode)
        {
            Record r = new Record();
            foreach (XmlNode item in xmlNode.ChildNodes)
            {
                switch (item.Name)
                {
                    case "chinese":
                        r.chinese = item.InnerText;
                        break;
                    case "age":
                        r.age = item.InnerText;
                        break;
                    case "age_month":
                        r.age_month = item.InnerText;
                        break;
                    case "check_by_name":
                        r.check_by_name = item.InnerText;
                        break;
                    case "check_date":
                        r.check_date = item.InnerText;
                        break;
                    case "ext_mthd":
                        r.ext_mthd = item.InnerText;
                        break;
                    case "highvalue":
                        r.highvalue = item.InnerText;
                        break;
                    case "hospnum":
                        r.hospnum = item.InnerText;
                        break;
                    case "lowvalue":
                        r.lowvalue = item.InnerText;
                        break;
                    case "patname":
                        r.patname = item.InnerText;
                        break;
                    case "print_ref":
                        r.print_ref = item.InnerText;
                        break;
                    case "ref_flag":
                        r.ref_flag = item.InnerText;
                        break;
                    case "result":
                        r.result = item.InnerText;
                        break;
                    case "sex":
                        r.sex = item.InnerText;
                        break;
                    case "units":
                        r.units = item.InnerText;
                        break;
                    default:
                        break;
                }
            }
            return r;
        }
        #endregion

        #region 从配置文件中获取需要的检测项目指标字典
        /// <summary>
        /// 从配置文件中获取需要的检测项目指标字典
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> XMDic()
        {
            //从配置文件中获取需要的检测项目指标字典
            Dictionary<string, string> xMDic = GetMatchFieldsXmlToDic("SZY\\configXML\\MatchFieldsWithClinicalData.xml", "/Matchings/*");
            return xMDic;
        }
        #endregion

        #region 获取临床数据并转换成list
        /// <summary>
        /// 获取临床数据并转换成list
        /// </summary>
        /// <param name="request">xml字符串</param>
        /// <returns></returns>
        private List<Record> GetClinicalDataToList(string request)
        {
            List<Record> recordList = new List<Record>();

            //正常调取web服务注销下面
            //ClinicalData.PacsLisReportServices pacsLisReportServices = new ClinicalData.PacsLisReportServices();
            //string normalLisReportStrs = "";
            //try
            //{
            //    normalLisReportStrs = pacsLisReportServices.NormalLisReport(request);
            //}
            //catch (Exception ex)
            //{

            //}
            //XmlDocument xd = new XmlDocument();
            //xd = HospitalXmlStrHelper.HospitalXmlStrToXmlDoc(normalLisReportStrs);

            ////一下是读取本地文件
            XmlDocument xd = new XmlDocument();
            //xd.Load(@"F:\01.anjie\03.客户文档\广东省中医院\对接\SZY_FpExtend4.0\FreezerProPlugin\configXML\XMLTestFile2.xml");
            xd = XmlHelper.XMLLoad("configXML\\XMLTestFile.xml");//读取xml文件
            if (xd != null && xd.HasChildNodes)
            {
                XmlNodeList nodlist = xd.SelectNodes("//reocrd");
                if (nodlist.Count > 0)
                {
                    foreach (XmlNode item in nodlist)
                    {
                        if (XmlNodeToRecord(item) != null && XmlNodeToRecord(item).ext_mthd != "")
                        {
                            //判断并获取临床数据对象
                            recordList.Add(XmlNodeToRecord(item));
                        }
                    }
                }
            }
            return recordList;
        }
        #endregion

        #region 获取指定配置文件中的匹配字典
        /// <summary>
        /// 获取指定配置文件中的匹配字典（临床数据字典和基本数据字典）
        /// </summary>
        /// <param name="xmlPath">Xml文件所在的位置</param>
        /// <param name="xPath">xpath路径</param>
        /// <returns>字段匹配字典</returns>
        private Dictionary<string, string> GetMatchFieldsXmlToDic(string xmlPath, string xPath)
        {
            Dictionary<string, string> MatchFieldDic = new Dictionary<string, string>();//创建医院数据字典
            try
            {
                XmlDocument xmlMatchFieldDc = XmlHelper.XMLLoad(xmlPath);//读取xml文件
                if (xmlMatchFieldDc.HasChildNodes)//判断是否包含节点
                {
                    XmlNodeList xmlnodelist = xmlMatchFieldDc.SelectNodes(xPath);//获取指定节点
                    if (xmlnodelist.Count > 0)
                    {
                        foreach (XmlNode item in xmlnodelist)
                        {
                            string KeyFieldName = item.SelectSingleNode("./KeyFieldName").InnerText;
                            string ValueFieldName = item.SelectSingleNode("./ValueFieldName").InnerText;
                            if (!MatchFieldDic.Keys.Contains(KeyFieldName))
                            {
                                MatchFieldDic.Add(KeyFieldName, ValueFieldName);
                            }
                        }
                    }
                }
                return MatchFieldDic;
            }
            catch (Exception ex) { return MatchFieldDic; }
        }
        #endregion

        //将从医院获取的检验数据转换成对象之后将对象转换成字段
        //recodeDic 最终需要生成的字典
        private Dictionary<string, string> RecodeListToDic(List<Record> record)
        {
            Dictionary<string, string> xMDic = XMDic();//需要的检测指标字典集合
            Dictionary<string, string> recodeDic = new Dictionary<string, string>();
            record.OrderBy(a => a.ext_mthd).ThenBy(a => a.check_date).ThenBy(a => a.chinese).ToList<Record>();
            foreach (KeyValuePair<string, string> xm in xMDic)
            {
                if (!recodeDic.Keys.Contains(xm.Key))
                {
                    foreach (Record re in record)
                    {
                        if (re.chinese == xm.Key)//需要的项目
                        {
                            if (!recodeDic.Keys.Contains(xm.Value))//最新一次。
                            {
                                recodeDic.Add(xm.Value, re.result);
                            }
                        }
                    }
                }
            }
            return recodeDic;
        }

        #region 获取临床检测指标数据
        /// <summary>
        /// 获取临床检验数据字典
        /// </summary>
        /// <param name="requestMzhzyh">住院号或门诊号</param>
        /// <returns>临床数据字典</returns>
        private Dictionary<string, string> GetClinicalDataDic(string requestMzhzyh)
        {
            string request = string.Format("<Request><hospnum>{0}</hospnum><ksrq00>{1}</ksrq00><jsrq00>{2}</jsrq00></Request>", requestMzhzyh, DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
            Dictionary<string, string> ClinicalDataDic = new Dictionary<string, string>();
            List<Record> re = GetClinicalDataToList(request);//将xml数据转换成记录对象
            if (re.Count > 0)
            {
                ClinicalDataDic = RecodeListToDic(re);
            }
            return ClinicalDataDic;
        }

        /// <summary>
        /// 根据日期和住院和门诊住院号获取临床检测数据
        /// </summary>
        /// <param name="datetime">输入的时间</param>
        /// <param name="requestMzhzyh">门诊或住院号</param>
        /// <param name="i">指定日期前后多少天，默认5天</param>
        /// <returns>记录集合</returns>
        public List<Record> GetClinicalDataList(DateTime datetime, string requestMzhzyh, int i)
        {
            string request = string.Format("<Request><hospnum>{0}</hospnum><ksrq00>{1}</ksrq00><jsrq00>{2}</jsrq00></Request>", requestMzhzyh, datetime.AddDays(-i).ToString("yyyy-MM-dd"), datetime.AddDays(i).ToString("yyyy-MM-dd"));
            List<Record> re = GetClinicalDataToList(request);//将xml数据转换成记录对象
            return re;
        }
        #endregion
        #endregion

        #region 3.0 获取临床诊断数据
        #region 调用webservice查询临床诊断数据
        /// <summary>
        /// 调用webservice查询临床诊断数据
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="date">日期</param>
        /// <returns>查询结果</returns>
        private string GetPatientDiagnoseData(string cardno, DateTime date)
        {
            string result = "";
            string request = string.Format("<Request><cardno>{0}</cardno><cxrq00>{1}</cxrq00></Request>", cardno, date.ToString("yyyy-MM-dd"));
            BLL.ClinicalData.PacsLisReportServices patientDiagnose = new ClinicalData.PacsLisReportServices();
            result = patientDiagnose.GetPatientDiagnose(request);
            return result;
        } 
        #endregion

        #region 根据日期查询前后5天的数据
        /// <summary>
        /// 根据日期查询前后5天的数据
        /// 此处的日期可以将数据保存至数据库，避免每次都要重复查询（保存字段为id、查询类型、日期）
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="date">日期</param>
        /// <returns>查询结果</returns>
        private string GetDataByDate(string cardno, DateTime date)
        {
            string result = "";
            //判断传入的日期和当前日期的范围
            //范围区间获取数据只获取取样日期的前后五天
            int day = Convert.ToInt32(TimeParser.DateDiffDays(date, DateTime.Now));

            //当前日期--前五天
            if (day == 0)
            {
                //当天的数据--查询前五天
                for (int i = 0; i < 5; i++)
                {
                    result = GetPatientDiagnoseData(cardno, date.AddDays(-(i + 1)));
                    if (!string.IsNullOrEmpty(result))
                    {
                        break;
                    }
                }
            }
            //5天内的数据--前几天？？
            if (day > 0 && day < 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    result = GetPatientDiagnoseData(cardno, date.AddDays(-(i + 1)));//前五天
                    if (!string.IsNullOrEmpty(result))
                    {
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(result))
                {
                    for (int i = 0; i < day; i++)
                    {
                        result = GetPatientDiagnoseData(cardno, date.AddDays((i + 1)));//当前日期后几天
                        if (!string.IsNullOrEmpty(result))
                        {
                            break;
                        }
                    }
                }
            }
            //5天前——循环10次取数据
            if (day > 5 || day == 5)
            {
                //当前日期的五天前的数据
                for (int i = 0; i < 5; i++)
                {
                    result = GetPatientDiagnoseData(cardno, date.AddDays(-(i + 1)));//前一天
                    if (!string.IsNullOrEmpty(result))
                    {
                        result = GetPatientDiagnoseData(cardno, date.AddDays((i + 1)));//后一天
                        if (!string.IsNullOrEmpty(result))
                        {
                            break;
                        }
                    }
                }
            }
            return result;
        }
        #endregion


        #endregion
    }
}

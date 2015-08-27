using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace RuRo.BLL
{
    public partial class NormalLisReport
    {
        //创建获取数据对象
        ClinicalData.PacsLisReportServices clinicalData = new ClinicalData.PacsLisReportServices();
        Model.DTO.NormalLisReportRequest request;
        /// <summary>
        /// 前台调用方法
        /// </summary>
        /// <returns></returns>
        public string GetData(Model.DTO.NormalLisReportRequest request, string codeType)
        {
            Model.DTO.JsonModel jsonmodel = new Model.DTO.JsonModel() {  Statu="err", Msg="无数据",Data=""};
            this.request = request;
            //保存记录（查询记录数据,更新或添加）
            bool b = SaveQueryRecord(ref request, "", codeType);
            if (b)
            {
                //调用接口获取数据
                string xmlStr = GetWebServiceData(request);
                string Msg = "";
                //将xml数据转换成list集合会查询本地数据库去除重复项
                List<Model.NormalLisReport> nnn = this.GetList(xmlStr, out Msg);
              
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
            return JsonConvert.SerializeObject(jsonmodel);
        }
        public Model.DTO.JsonModel GetData(Model.DTO.NormalLisReportRequest request, string codeType,string t)
        {
            Model.DTO.JsonModel jsonmodel = new Model.DTO.JsonModel() { Statu = "err", Msg = "无数据", Data = "" };
            this.request = request;
            //保存记录（查询记录数据,更新或添加）
            bool b = SaveQueryRecord(ref request, "", codeType);
            if (b)
            {
                //调用接口获取数据
                string xmlStr = GetWebServiceData(request);
                string Msg = "";
                //将xml数据转换成list集合会查询本地数据库去除重复项
                List<Model.NormalLisReport> nnn = this.GetList(xmlStr, out Msg);
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

        public string PostData(string code, string codeType, string dataStr)
        {
            List<Dictionary<string, string>> dicList = GetClinicalInfoDgDicList(dataStr);
            List<Dictionary<string, string>> newDicList = MatchClinicalDic(dicList, codeType);
            for (int i = 0; i < newDicList.Count; i++)
            {
                newDicList[i].Add("Sample Source",code);
                switch (newDicList[i]["性别"]) 
                {
                    case "M": newDicList[i]["性别"] = "男";
                    break;
                    case "F": newDicList[i]["性别"] = "女";
                    break;
                    default: newDicList[i].Remove("性别");
                    break;
                }
            }
           string strList= JsonConvert.SerializeObject(newDicList);
           string mes=  PostData(strList);
           if (mes.Contains("{\"success\":true,"))
           {
               for (int i = 0; i < dicList.Count; i++)
               {
                   //把数据添加到数据库
                   Model.NormalLisReport model = new Model.NormalLisReport();
                   model = DicToNormalLisReportModel(dicList[i]);
                   NormalLisReport n = new NormalLisReport();
                   n.Add(model);
               }
               //BLL.SZY.QueryRecoder bll_Q = new SZY.QueryRecoder();
               //List<Model.QueryRecoder> list= bll_Q.GetReciprocalFirstData_BLL();
               //if (list!=null||list.Count>0)
               //{
               //    Model.QueryRecoder = list[0];
               //}
              // bll_Q.UpdataQueryRecoderIsDel_BLL("user");
               //修改QueryRecoder表为true
           }
           return mes;
        }

        /// <summary>
        /// 字典转化为model
        /// </summary>
        /// <returns></returns>
        public Model.NormalLisReport DicToNormalLisReportModel(Dictionary<string,string> dic) 
        {
            string str = JsonConvert.SerializeObject(dic);
            Model.NormalLisReport normalLisReport = JsonConvert.DeserializeObject<Model.NormalLisReport>(str);
            return normalLisReport;
        }


        private List<Dictionary<string, string>> MatchClinicalDic(List<Dictionary<string, string>> clinicalDicList, string codeType)
        {
            Dictionary<string, string> dic = Common.MatchDic.NormalLisReportDic;
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
            string clinicalInfoDg = dataStr;//dg
            //页面上临床数据对象集合
            List<Model.NormalLisReport> pageClinicalInfoList = new List<Model.NormalLisReport>();
            List<Dictionary<string, string>> ClinicalInfoDgDicList = new List<Dictionary<string, string>>();
            //将页面上的临床信息转换成对象集合

            if (!string.IsNullOrEmpty(clinicalInfoDg) && clinicalInfoDg != "[]")
            {
                //转换页面上的clinicalInfoDg为对象集合
                pageClinicalInfoList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Model.NormalLisReport>>(clinicalInfoDg);//转换ok
            }
            Model.NormalLisReport cl = new Model.NormalLisReport();

            foreach (Model.NormalLisReport item in pageClinicalInfoList)
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
        #region 获取数据 + private string GetData(Model.DTO.NormalLisReportRequest request)
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="request">获取数据的参数</param>
        /// <returns>返回数据</returns>
        private string GetWebServiceData(Model.DTO.NormalLisReportRequest request)
        {
            try
            {
                return Test(request);
                // return string.IsNullOrEmpty(request.Request) ? "" : clinicalData.GetNormalLisItems(request.Request);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
                return ex.Message + "--" + DateTime.Now.ToLongTimeString();
            }
        }
        #endregion

        #region 生成临时数据 + private string Test(Model.DTO.NormalLisReportRequest request)
        /// <summary>
        /// 生成临时数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string Test(Model.DTO.NormalLisReportRequest request)
        {
            string str = @"<Response>
  <ResultCode>0</ResultCode>
  <ErrorMsg></ErrorMsg>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>急诊生化(急,干)</ext_mthd>
    <result>9.20</result>
    <ref_flag>1</ref_flag>
    <lowvalue>2.86</lowvalue>
    <highvalue>8.21</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>mmol/L</units>
    <prnt_order>606</prnt_order>
    <chinese>尿素(Urea)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>急诊生化(急,干)</ext_mthd>
    <result>87.22</result>
    <ref_flag>2</ref_flag>
    <lowvalue>99</lowvalue>
    <highvalue>110</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>mmol/L</units>
    <prnt_order>603</prnt_order>
    <chinese>氯离子(Cl-)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>输血4项</ext_mthd>
    <result>0.245</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>1</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>COI</units>
    <prnt_order>3</prnt_order>
    <chinese>HIV抗原抗体检测</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>112</prnt_order>
    <chinese>尿胆红素(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>112</result>
    <ref_flag>2</ref_flag>
    <lowvalue>130</lowvalue>
    <highvalue>175</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>g/L</units>
    <prnt_order>218</prnt_order>
    <chinese>血红蛋白测定(Hb)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>28.4</result>
    <ref_flag></ref_flag>
    <lowvalue>27</lowvalue>
    <highvalue>34</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>pg</units>
    <prnt_order>221</prnt_order>
    <chinese>平均红细胞Hb量(MCH)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>.61</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0.10</lowvalue>
    <highvalue>0.60</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>10^9/L</units>
    <prnt_order>212</prnt_order>
    <chinese>单核细胞计数(MONO)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>12.2</result>
    <ref_flag></ref_flag>
    <lowvalue>2.10</lowvalue>
    <highvalue>22.30</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>μmol/L</units>
    <prnt_order>13</prnt_order>
    <chinese>总胆红素(TBIL)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>38.3</result>
    <ref_flag>2</ref_flag>
    <lowvalue>40</lowvalue>
    <highvalue>55</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>g/L</units>
    <prnt_order>8</prnt_order>
    <chinese>白蛋白(ALB)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>急诊生化(急,干)</ext_mthd>
    <result>8.28</result>
    <ref_flag>1</ref_flag>
    <lowvalue>3.90</lowvalue>
    <highvalue>6.10</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>mmol/L</units>
    <prnt_order>605</prnt_order>
    <chinese>葡萄糖(Glu)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>急诊生化(急,干)</ext_mthd>
    <result>130.25</result>
    <ref_flag>2</ref_flag>
    <lowvalue>137</lowvalue>
    <highvalue>147</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>mmol/L</units>
    <prnt_order>601</prnt_order>
    <chinese>钠离子(Na+)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>急诊生化(急,干)</ext_mthd>
    <result>62.25</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>ml/min/1.73m2</units>
    <prnt_order>902</prnt_order>
    <chinese>肾小球滤过率估算值(eGFR)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>凝血4项</ext_mthd>
    <result>3.53</result>
    <ref_flag></ref_flag>
    <lowvalue>2</lowvalue>
    <highvalue>4</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>石汉振</check_by_name>
    <units>g/L</units>
    <prnt_order>217</prnt_order>
    <chinese>纤维蛋白原(FIB)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>109</prnt_order>
    <chinese>尿亚硝酸盐(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>3.96</result>
    <ref_flag>2</ref_flag>
    <lowvalue>4.30</lowvalue>
    <highvalue>5.80</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>10^12/L</units>
    <prnt_order>217</prnt_order>
    <chinese>红细胞计数(RBC)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>8.12</result>
    <ref_flag>1</ref_flag>
    <lowvalue>1.80</lowvalue>
    <highvalue>6.30</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>10^9/L</units>
    <prnt_order>210</prnt_order>
    <chinese>中性粒细胞计数(NEUT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>16</result>
    <ref_flag></ref_flag>
    <lowvalue>9</lowvalue>
    <highvalue>17</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>fl</units>
    <prnt_order>230</prnt_order>
    <chinese>血小板体积分布宽度(PDW)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>7.8</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>6.50</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>μmol/L</units>
    <prnt_order>14</prnt_order>
    <chinese>直接胆红素(DBIL)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>1.2</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units></units>
    <prnt_order>6</prnt_order>
    <chinese>谷草酶/谷丙酶(AST/ALT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>8</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>25</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>U/L</units>
    <prnt_order>2</prnt_order>
    <chinese>腺苷脱氨酶(ADA)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>30</result>
    <ref_flag></ref_flag>
    <lowvalue>20</lowvalue>
    <highvalue>60</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>U/L</units>
    <prnt_order>1</prnt_order>
    <chinese>亮氨酸氨基肽酶(LAP)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>凝血4项</ext_mthd>
    <result>15.8</result>
    <ref_flag></ref_flag>
    <lowvalue>14</lowvalue>
    <highvalue>21</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>石汉振</check_by_name>
    <units>s</units>
    <prnt_order>219</prnt_order>
    <chinese>凝血酶时间(TT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>/HP</units>
    <prnt_order>326</prnt_order>
    <chinese>小圆上皮细胞[镜检]</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>9.82</result>
    <ref_flag>1</ref_flag>
    <lowvalue>3.50</lowvalue>
    <highvalue>9.50</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>10^9/L</units>
    <prnt_order>203</prnt_order>
    <chinese>白细胞计数(WBC)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>33.2</result>
    <ref_flag>2</ref_flag>
    <lowvalue>40</lowvalue>
    <highvalue>50</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>%</units>
    <prnt_order>219</prnt_order>
    <chinese>红细胞比积测定(HCT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>351</result>
    <ref_flag>1</ref_flag>
    <lowvalue>125</lowvalue>
    <highvalue>350</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>10^9/L</units>
    <prnt_order>227</prnt_order>
    <chinese>血小板计数(PLT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>.1</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>1</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>%</units>
    <prnt_order>208</prnt_order>
    <chinese>嗜碱性粒细胞百分比(BASO%)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>17</result>
    <ref_flag></ref_flag>
    <lowvalue>15</lowvalue>
    <highvalue>40</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>U/L</units>
    <prnt_order>5</prnt_order>
    <chinese>谷草转氨酶(AST)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>5.4</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>10</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>μmol/L</units>
    <prnt_order>17</prnt_order>
    <chinese>总胆汁酸(TBA)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>粪便常规+潜血</ext_mthd>
    <result>软便</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>软便</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>庞鑫</check_by_name>
    <units></units>
    <prnt_order>140</prnt_order>
    <chinese>粪便性状</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>粪便常规+潜血</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>庞鑫</check_by_name>
    <units></units>
    <prnt_order>149</prnt_order>
    <chinese>潜血试验</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>血型检测(微柱法)</ext_mthd>
    <result>AB型</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>刘持翔</check_by_name>
    <units></units>
    <prnt_order>1</prnt_order>
    <chinese>ABO血型正反定型(微柱法)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>血淀粉酶(急,干)</ext_mthd>
    <result>
      小于30
    </result>
    <ref_flag>2</ref_flag>
    <lowvalue>30</lowvalue>
    <highvalue>110</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>U/L</units>
    <prnt_order></prnt_order>
    <chinese>血淀粉酶(AMY)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>急诊生化(急,干)</ext_mthd>
    <result>31.7</result>
    <ref_flag>1</ref_flag>
    <lowvalue>23</lowvalue>
    <highvalue>29</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>mmol/L</units>
    <prnt_order>604</prnt_order>
    <chinese>总二氧化碳(TCO2)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>淡黄色</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>淡黄色或黄色</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>101</prnt_order>
    <chinese>尿液颜色</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>108</prnt_order>
    <chinese>尿葡萄糖(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>106</prnt_order>
    <chinese>尿潜血(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>0</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>0</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>个/ul</units>
    <prnt_order>116</prnt_order>
    <chinese>尿病理管型计数</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>12.6</result>
    <ref_flag></ref_flag>
    <lowvalue>11.60</lowvalue>
    <highvalue>14.60</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>%</units>
    <prnt_order>225</prnt_order>
    <chinese>红细胞体积分布宽度(RDW)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>9</result>
    <ref_flag></ref_flag>
    <lowvalue>7.60</lowvalue>
    <highvalue>13.20</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>fl</units>
    <prnt_order>228</prnt_order>
    <chinese>平均血小板体积(MPV)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>6.2</result>
    <ref_flag></ref_flag>
    <lowvalue>3</lowvalue>
    <highvalue>10</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>%</units>
    <prnt_order>206</prnt_order>
    <chinese>单核细胞百分比(MONO%)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>1.4</result>
    <ref_flag></ref_flag>
    <lowvalue>1.20</lowvalue>
    <highvalue>2.40</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units></units>
    <prnt_order>10</prnt_order>
    <chinese>ALB/GLB</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>14</result>
    <ref_flag></ref_flag>
    <lowvalue>9</lowvalue>
    <highvalue>50</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>U/L</units>
    <prnt_order>4</prnt_order>
    <chinese>谷丙转氨酶(ALT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>心酶4项(急,干)</ext_mthd>
    <result>265</result>
    <ref_flag>1</ref_flag>
    <lowvalue>109</lowvalue>
    <highvalue>245</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>U/L</units>
    <prnt_order>613</prnt_order>
    <chinese>乳酸脱氢酶 (LDH)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>凝血4项</ext_mthd>
    <result>35.8</result>
    <ref_flag></ref_flag>
    <lowvalue>28</lowvalue>
    <highvalue>45</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>石汉振</check_by_name>
    <units>s</units>
    <prnt_order>218</prnt_order>
    <chinese>活化部分凝血活酶时间(APTT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>1.005</result>
    <ref_flag></ref_flag>
    <lowvalue>1.0030</lowvalue>
    <highvalue>1.03</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>103</prnt_order>
    <chinese>尿比重(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>1+</result>
    <ref_flag>3</ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性或±</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>111</prnt_order>
    <chinese>尿胆原(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>/HP</units>
    <prnt_order>324</prnt_order>
    <chinese>上皮细胞[镜检]</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>0</highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>/HP</units>
    <prnt_order>328</prnt_order>
    <chinese>真菌[镜检]</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>0</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>/HP</units>
    <prnt_order>328</prnt_order>
    <chinese>真菌[镜检]</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>83.8</result>
    <ref_flag></ref_flag>
    <lowvalue>82</lowvalue>
    <highvalue>100</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>fl</units>
    <prnt_order>220</prnt_order>
    <chinese>平均红细胞体积(MCV)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>.1</result>
    <ref_flag>2</ref_flag>
    <lowvalue>0.40</lowvalue>
    <highvalue>8</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>%</units>
    <prnt_order>207</prnt_order>
    <chinese>嗜酸性粒细胞百分比(EOSIN%)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>.317</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0.11</lowvalue>
    <highvalue>0.27</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>%</units>
    <prnt_order>229</prnt_order>
    <chinese>血小板压积(PCT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>54</result>
    <ref_flag></ref_flag>
    <lowvalue>10</lowvalue>
    <highvalue>60</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>U/L</units>
    <prnt_order>11</prnt_order>
    <chinese>γ-谷氨酰基转移酶（GGT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>粪便常规+潜血</ext_mthd>
    <result>未检出</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>庞鑫</check_by_name>
    <units></units>
    <prnt_order>146</prnt_order>
    <chinese>阿米巴原虫(粪便)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>心酶4项(急,干)</ext_mthd>
    <result>22</result>
    <ref_flag></ref_flag>
    <lowvalue>15</lowvalue>
    <highvalue>40</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>U/L</units>
    <prnt_order>615</prnt_order>
    <chinese>谷草转氨酶(AST)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>急诊生化(急,干)</ext_mthd>
    <result>100.6</result>
    <ref_flag></ref_flag>
    <lowvalue>59</lowvalue>
    <highvalue>104</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>μmol/L</units>
    <prnt_order>607</prnt_order>
    <chinese>肌酐(Cr)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>AFP定量</ext_mthd>
    <result>3.07</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>8.10</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>郑智明</check_by_name>
    <units>ng/ml</units>
    <prnt_order>95</prnt_order>
    <chinese>甲胎蛋白(AFP)定量</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>凝血4项</ext_mthd>
    <result>1.27</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0.80</lowvalue>
    <highvalue>1.20</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>石汉振</check_by_name>
    <units>R</units>
    <prnt_order>216</prnt_order>
    <chinese>凝血酶原国际标准化比值(INR)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>清</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>清晰</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>102</prnt_order>
    <chinese>尿透明度</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>107</prnt_order>
    <chinese>尿蛋白质(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>0.00</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>4</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>个/ul</units>
    <prnt_order>114</prnt_order>
    <chinese>尿红细胞计数</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>/HP</units>
    <prnt_order>326</prnt_order>
    <chinese>小圆上皮细胞[镜检]</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>82.7</result>
    <ref_flag>1</ref_flag>
    <lowvalue>40</lowvalue>
    <highvalue>75</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>%</units>
    <prnt_order>204</prnt_order>
    <chinese>中性粒细胞百分比(NEUT%)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>.01</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>0.06</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>10^9/L</units>
    <prnt_order>214</prnt_order>
    <chinese>嗜碱性粒细胞计数(BASO)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>.01</result>
    <ref_flag>2</ref_flag>
    <lowvalue>0.02</lowvalue>
    <highvalue>0.52</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>10^9/L</units>
    <prnt_order>213</prnt_order>
    <chinese>嗜酸性粒细胞计数(EOSIN计数)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>粪便常规+潜血</ext_mthd>
    <result>棕黄色</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>棕黄色</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>庞鑫</check_by_name>
    <units></units>
    <prnt_order>138</prnt_order>
    <chinese>粪便颜色</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>粪便常规+潜血</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>庞鑫</check_by_name>
    <units>/HP</units>
    <prnt_order>143</prnt_order>
    <chinese>粪便红细胞</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>CEA定量</ext_mthd>
    <result>339.5</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>5</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>郑智明</check_by_name>
    <units>ng/ml</units>
    <prnt_order>94</prnt_order>
    <chinese>癌胚抗原(CEA)定量</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>CA19-9</ext_mthd>
    <result>599.3</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>27</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>郑智明</check_by_name>
    <units>U/ml</units>
    <prnt_order>97</prnt_order>
    <chinese>糖链抗原19-9(CA19-9)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>CA242</ext_mthd>
    <result>362.017</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>20</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-16</check_date>
    <check_by_name>黄惠</check_by_name>
    <units>U/mL</units>
    <prnt_order>378</prnt_order>
    <chinese>糖链抗原242(CA242)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>凝血4项</ext_mthd>
    <result>15.9</result>
    <ref_flag>1</ref_flag>
    <lowvalue>11</lowvalue>
    <highvalue>14.50</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>石汉振</check_by_name>
    <units>s</units>
    <prnt_order>214</prnt_order>
    <chinese>凝血酶原时间(PT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>输血4项</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性</print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units></units>
    <prnt_order>315</prnt_order>
    <chinese>甲苯胺红不加热血清试验(TRUST)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>110</prnt_order>
    <chinese>尿酮体(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>2.64</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>5</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>个/ul</units>
    <prnt_order>113</prnt_order>
    <chinese>尿白细胞计数</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>0</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>1</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>个/ul</units>
    <prnt_order>115</prnt_order>
    <chinese>尿透明管型计数</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>/HP</units>
    <prnt_order>327</prnt_order>
    <chinese>结晶[镜检]</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>339</result>
    <ref_flag></ref_flag>
    <lowvalue>316</lowvalue>
    <highvalue>354</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>g/L</units>
    <prnt_order>223</prnt_order>
    <chinese>平均红细胞Hb浓度(MCHC)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>10.9</result>
    <ref_flag>2</ref_flag>
    <lowvalue>20</lowvalue>
    <highvalue>50</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>%</units>
    <prnt_order>205</prnt_order>
    <chinese>淋巴细胞百分比(LYM%)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>66.6</result>
    <ref_flag></ref_flag>
    <lowvalue>65</lowvalue>
    <highvalue>85</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>g/L</units>
    <prnt_order>7</prnt_order>
    <chinese>总蛋白(TP)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>28.3</result>
    <ref_flag></ref_flag>
    <lowvalue>20</lowvalue>
    <highvalue>40</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>g/L</units>
    <prnt_order>9</prnt_order>
    <chinese>球蛋白(GLB)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>75.36</result>
    <ref_flag>2</ref_flag>
    <lowvalue>180</lowvalue>
    <highvalue>390</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>mg/L</units>
    <prnt_order>3</prnt_order>
    <chinese>前白蛋白(PA)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>粪便常规+潜血</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>庞鑫</check_by_name>
    <units>/HP</units>
    <prnt_order>142</prnt_order>
    <chinese>粪便白细胞</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>TnI-Ultra</ext_mthd>
    <result>0.015</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>0.04</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>μg/L</units>
    <prnt_order>374</prnt_order>
    <chinese>超敏肌钙蛋白I(TnI-Ultra)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>心酶4项(急,干)</ext_mthd>
    <result>49</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>24</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>U/L</units>
    <prnt_order>612</prnt_order>
    <chinese>肌酸激酶同工酶(CKMB)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>心酶4项(急,干)</ext_mthd>
    <result>84</result>
    <ref_flag></ref_flag>
    <lowvalue>26</lowvalue>
    <highvalue>174</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>U/L</units>
    <prnt_order>611</prnt_order>
    <chinese>肌酸激酶(CK)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>急诊生化(急,干)</ext_mthd>
    <result>3.05</result>
    <ref_flag>2</ref_flag>
    <lowvalue>3.50</lowvalue>
    <highvalue>5.30</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>mmol/L</units>
    <prnt_order>602</prnt_order>
    <chinese>钾离子(K+)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>D二聚体</ext_mthd>
    <result>5430</result>
    <ref_flag>1</ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>500</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>石汉振</check_by_name>
    <units>ug/L FEU</units>
    <prnt_order>220</prnt_order>
    <chinese>D二聚体(D-Dimer)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>凝血4项</ext_mthd>
    <result>69</result>
    <ref_flag>2</ref_flag>
    <lowvalue>70</lowvalue>
    <highvalue>130</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>石汉振</check_by_name>
    <units>%</units>
    <prnt_order>215</prnt_order>
    <chinese>凝血酶原活动度(AT)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>输血4项</ext_mthd>
    <result>0.045</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>1</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>COI</units>
    <prnt_order>2</prnt_order>
    <chinese>丙肝抗体定量检测</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>输血4项</ext_mthd>
    <result>0.599</result>
    <ref_flag></ref_flag>
    <lowvalue>0</lowvalue>
    <highvalue>1</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>欧阳芬</check_by_name>
    <units>COI</units>
    <prnt_order>1</prnt_order>
    <chinese>乙肝表面抗原定量</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>6</result>
    <ref_flag></ref_flag>
    <lowvalue>4.50</lowvalue>
    <highvalue>8</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>104</prnt_order>
    <chinese>尿酸碱度(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units></units>
    <prnt_order>105</prnt_order>
    <chinese>尿白细胞酯酶(干化学)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>/HP</units>
    <prnt_order>324</prnt_order>
    <chinese>上皮细胞[镜检]</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>尿液分析+沉渣定量</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>卢妙莲</check_by_name>
    <units>/HP</units>
    <prnt_order>327</prnt_order>
    <chinese>结晶[镜检]</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>全血分析(五分类)</ext_mthd>
    <result>1.07</result>
    <ref_flag>2</ref_flag>
    <lowvalue>1.10</lowvalue>
    <highvalue>3.20</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>柳苗</check_by_name>
    <units>10^9/L</units>
    <prnt_order>211</prnt_order>
    <chinese>淋巴细胞计数(LYM)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>141</result>
    <ref_flag>1</ref_flag>
    <lowvalue>45</lowvalue>
    <highvalue>125</highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>U/L</units>
    <prnt_order>12</prnt_order>
    <chinese>碱性磷酸酶(ALP)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>肝功12项</ext_mthd>
    <result>4.4</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>李国华</check_by_name>
    <units>μmol/L</units>
    <prnt_order>16</prnt_order>
    <chinese>间接胆红素(IBIL)</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>粪便常规+潜血</ext_mthd>
    <result>未检出</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>庞鑫</check_by_name>
    <units></units>
    <prnt_order>145</prnt_order>
    <chinese>粪便寄生虫卵</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>粪便常规+潜血</ext_mthd>
    <result>阴性(-)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref>阴性(-)</print_ref>
    <check_date>2014-12-15</check_date>
    <check_by_name>庞鑫</check_by_name>
    <units></units>
    <prnt_order>141</prnt_order>
    <chinese>粪便粘液</chinese>
  </reocrd>
  <reocrd>
    <hospnum>0272099</hospnum>
    <patname>杨基</patname>
    <sex>M</sex>
    <age_month>1</age_month>
    <age>76</age>
    <ext_mthd>血型检测(微柱法)</ext_mthd>
    <result>阳性(+)</result>
    <ref_flag></ref_flag>
    <lowvalue></lowvalue>
    <highvalue></highvalue>
    <print_ref></print_ref>
    <check_date>2014-12-14</check_date>
    <check_by_name>刘持翔</check_by_name>
    <units></units>
    <prnt_order>2</prnt_order>
    <chinese>RhD血型(微柱法)</chinese>
  </reocrd>
</Response>";
            return str;
        }
        #endregion

        #region 将数据转换成对象 + private Model.DTO.JsonModel StrTObject(string xmlStr)
        /// <summary>
        /// 将数据转换成对象
        /// </summary>
        /// <param name="xmlStr">要转换成对象的数据</param>
        /// <returns></returns>
        private Model.DTO.JsonModel StrTObject(string xmlStr)
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
                            List<Model.NormalLisReport> list = new List<Model.NormalLisReport>();
                            //有数据
                            XmlNodeList xnl = xd.SelectNodes("//reocrd");
                            if (xnl.Count > 0)
                            {
                                foreach (XmlNode item in xnl)
                                {
                                    Model.NormalLisReport nn = this.XmlTomModel(item);
                                    if (Common.MatchDic.NeedRecordDic.Keys.Contains(nn.chinese))
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
                                jsonData.Data = list.OrderBy(a => a.chinese);
                                jsonData.Statu = "ok";
                                jsonData.Msg = "查询成功";
                            }
                            else
                            {
                                jsonData.Statu = "err";
                                jsonData.Msg = "无数据";
                            }
                        }
                        else
                        {
                            //查询数据出错，联接无问题
                            jsonData.Msg = xd.SelectSingleNode("//ErrorMsg").InnerText;
                            jsonData.Statu = "err";
                            //保存查询记录
                        }
                    }
                    else
                    {
                        //查询数据出错，联接无问题
                        jsonData.Msg = xd.InnerText;
                        jsonData.Statu = "err";
                        //保存查询记录
                    }
                }
            }
            return jsonData;
        }
        #endregion

        #region xmlNode转换成obj + Model.NormalLisReport XmlTomModel(XmlNode xd)
        /// <summary>
        /// xmlNode转换成obj
        /// </summary>
        /// <param name="xd"></param>
        /// <returns></returns>
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
                            nlr.ref_flag = "阳性";
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
        #endregion

        #region 检查数据对象在本地数据库是否存在 CheckData(Model.NormalLisReport data)
        /// <summary>
        /// 检查数据对象在本地数据库是否存在 ,true--存在
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckData(Model.NormalLisReport data)
        {
            bool result = false;
            if (data != null)
            {
                string whereStr = string.Format("chinese ='{0}' and hospnum ='{1}' and check_date ='{2}' and patname='{3}'", data.chinese, data.hospnum, data.check_date, data.patname);
                List<Model.NormalLisReport> list = this.GetModelList(whereStr);
                if (list != null && list.Count > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region 创建返回数据对象 + private Model.DTO.JsonModel CreatJsonModel(object obj, string msg)
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
        #endregion

        #region 解析xml获取数据并转换成list +private List<Model.NormalLisReport> GetList(string xmlStr, out string Msg)
        /// <summary>
        /// 解析xml获取数据并转换成list
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <param name="Msg">消息</param>
        /// <returns></returns>
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
                                    Model.NormalLisReport nn = this.XmlTomModel(item);
                                    if (Common.MatchDic.NeedRecordDic.Keys.Contains(nn.chinese))
                                    {
                                        if (!this.CheckData(nn))
                                        {
                                            list.Add(nn);
                                        }
                                    }
                                }
                                if (list.Count > 0)
                                {
                                    #region
                                    //for (int i = 0; i < list.Count; i++)
                                    //{
                                    //    //插入数据到数据库
                                    //    Model.NormalLisReport kk = new Model.NormalLisReport();
                                    //    kk.hospnum = list[i].hospnum;
                                    //    kk.patname = list[i].patname;
                                    //    kk.Sex = list[i].Sex;
                                    //    kk.Age = list[i].Age;
                                    //    kk.age_month = list[i].age_month;
                                    //    kk.ext_mthd = list[i].ext_mthd;
                                    //    kk.chinese = list[i].chinese;
                                    //    kk.result = list[i].result;
                                    //    kk.units = list[i].units;
                                    //    kk.ref_flag = list[i].ref_flag;
                                    //    kk.lowvalue = list[i].lowvalue;
                                    //    kk.highvalue = list[i].highvalue;
                                    //    kk.print_ref = list[i].print_ref;
                                    //    kk.check_date = list[i].check_date;
                                    //    kk.check_by_name = list[i].check_by_name;
                                    //    kk.prnt_order = list[i].prnt_order;
                                    //    kk.IsDel = false;
                                    //    NormalLisReport no = new NormalLisReport();
                                    //    no.Add(kk);
                                    //}
                                    //BLL.SZY.QueryRecoder bll_Q = new SZY.QueryRecoder();
                                    //QueryRecoder bl_Q = new QueryRecoder();
                                    ////更新记录表
                                    ////DataSet ds = bll_Q.GetReciprocalFirstData_BLL();
                                    ////Model.QueryRecoder model_Q = new Model.QueryRecoder();
                                    ////model_Q.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
                                    ////model_Q.Uname = ds.Tables[0].Rows[0]["Uname"].ToString();
                                    ////model_Q.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                                    ////model_Q.AddDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["AddDate"].ToString());
                                    ////model_Q.LastQueryDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["LastQueryDate"].ToString());
                                    ////model_Q.QueryType = "NormalLisReport";
                                    ////bl_Q.Update(model_Q);
                                    #endregion
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

        private bool SaveQueryRecord(ref Model.DTO.NormalLisReportRequest resquet, string Msg, string codeType)
        {
            bool result = false;
            QueryRecoder queryRecoder = new QueryRecoder();
            //根据传入的查询字符串创建的当此查询的记录model
            Model.QueryRecoder model = new Model.QueryRecoder();

            //model.AddDate = DateTime.Now;
            model.Code = request.hospnum;
            model.CodeType = codeType;
            model.QueryType = "NormalLisReport";
            model.Uname = Common.CookieHelper.GetCookieValue("username");
            model.IsDel = false;

            List<Model.QueryRecoder> list = CheckQueryRecord(model);
            if (list != null && list.Count > 0)
            {
                //判断查询出来的数据是否满足要求（时间差距lastdate<dateNow-5）
                Model.QueryRecoder oldModel = list.OrderByDescending(a => a.LastQueryDate).FirstOrDefault();
                model.AddDate = oldModel.AddDate;
                model.Id = oldModel.Id;
                DateTime dtAdd = Convert.ToDateTime(oldModel.AddDate);
                DateTime dtLastQuery = Convert.ToDateTime(oldModel.LastQueryDate);
                DateTime dt=DateTime.Now;//当前时间
                int days = (dt - dtAdd).Days;//获取当前日期与添加日期时间差
                int getDays = 0;
                if (days > getDays)
                {
                    model.IsDel = true;
                    model.LastQueryDate = dtAdd.AddDays(5);
                }
                else
                {
                    int chaday = (dtLastQuery - dtAdd).Days;//获取最后查询日期与添加日期时间差
                    int nowday = (dt - dtLastQuery).Days;//当前时间与最后查询时间差
                    int addnowDay = (dt - dtAdd).Days;//当前时间与添加时间差
                    if (nowday==0)
                    {
                        //resquet.jsrq00 = "";
                        //resquet.ksrq00 = "";
                        result = true;
                        model.IsDel = false;
                        model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg + oldModel.QueryResult;
                    }
                    if (nowday > 0)//当前时间与最后查询时间差大于0
                    {
                        if (addnowDay == 0)
                        {
                            result = true;
                            model.IsDel = false;
                            model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg + oldModel.QueryResult;
                        }
                        if (chaday >= getDays)
                        {
                            model.IsDel = true;
                        }
                        else
                        {
                            resquet.ksrq00 = dtLastQuery.ToString("yyyy-MM-dd");
                            resquet.jsrq00 = dtLastQuery.AddDays(1).ToString("yyyy-MM-dd");
                            result = true;
                            //添加日期是距离添加日期是5天内的
                            model.IsDel = false;
                            model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg + oldModel.QueryResult;
                        }
                    }
                }
                //本地数据库有数据
                result = queryRecoder.Update(model);
            }
            else
            {
                model.QueryResult += "&nbsp" + DateTime.Now.ToLocalTime() + " " + Msg.Trim();
                model.AddDate = DateTime.Now;
                model.LastQueryDate = DateTime.Now;
                bool bo = queryRecoder.Add(model) > 0;
                model.QueryType = "PatientDiagnose";
                bool boo = queryRecoder.Add(model) > 0;
                if (bo && boo)
                {
                    result = true;
                }
                else { result = false; }
                
            }
            return result;
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
    }
}

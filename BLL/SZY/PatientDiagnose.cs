using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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
        public string GetSampleSourceData(Model.DTO.PatientDiagnoseResuest request)
        {
            string xmlStr = GetData(request.Request);
            Model.DTO.JsonModel jsonmodel = StrTObject(xmlStr, request);
            return JsonConvert.SerializeObject(jsonmodel);
        }
        public string PostData(string formData, string code, string codeType)
        {
            Dictionary<string, string> dic = GetBaseInfoDic(formData);
            Dictionary<string, string> newDic = new Dictionary<string, string>();
            newDic.Add("Name", code);
            foreach (KeyValuePair<string, string> item in dic)
            {
                newDic.Add(Common.MatchDic.EmpiInfoDic[item.Key], item.Value);

            }
            //调用方法提交数据
            string result = PostData(newDic);
            if (result.Contains("\"success\":true,") || result.Contains("should be unique."))
            {
                Model.PatientDiagnose e = JsonConvert.DeserializeObject<Model.PatientDiagnose>(JsonConvert.SerializeObject(dic));
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
            string result = FreezerProUtility.Fp_BLL.SampleSocrce.ImportSampleSourceDataToFp(up.GetUp(), "患者信息", dic);
            return result;
        }

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="request">获取数据的参数</param>
        /// <returns>返回数据</returns>
        private string GetData(string request)
        {
            try
            {
                return Test(request).Replace("\r\n","").Replace(" ","");
                //return string.IsNullOrEmpty(request) ? "" : clinicalData.GetPatientDiagnose(request);
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

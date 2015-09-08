using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace RuRo.BLL
{
    public partial class EmpiInfo
    {
        //创建获取数据对象
        private BasicData.EmpiService empiService = new BasicData.EmpiService();

        /// <summary>
        /// 前台调用方法
        /// </summary>
        /// <returns></returns>
        public string GetSampleSourceData(Model.DTO.EmpiInfoRequest request)
        {
            string xmlStr = GetData(request);
            Model.DTO.JsonModel jsonmodel = StrTObject(xmlStr);
            return JsonConvert.SerializeObject(jsonmodel);
        }

        public string PostData(string formData, string code, string codeType)
        {
            Dictionary<string, string> dic = GetBaseInfoDic(formData);
            Dictionary<string, string> newDic = new Dictionary<string, string>();
            newDic.Add("Name", code);
            switch (codeType)
            {
                case "1":
                    newDic.Add("住院号", code);
                    break;

                case "0":
                    newDic.Add("卡号", code);
                    break;

                default:
                    break;
            }
            foreach (KeyValuePair<string, string> item in dic)
            {
                if (Common.MatchDic.EmpiInfoDic.Keys.Contains(item.Key))
                {
                    if (item.Key == "PatientName")
                    {
                        newDic.Add("Description", item.Value);
                        newDic.Add(Common.MatchDic.EmpiInfoDic[item.Key], item.Value);
                    }
                    else
                    {
                        newDic.Add(Common.MatchDic.EmpiInfoDic[item.Key], item.Value);
                    }
                }
            }
            //调用方法提交数据
            string result = PostData(newDic);
            if (result.Contains("\"success\":true,") || result.Contains("should be unique."))
            {
                Model.EmpiInfo e = JsonConvert.DeserializeObject<Model.EmpiInfo>(JsonConvert.SerializeObject(dic));
                EmpiInfo eee = new EmpiInfo();
                e.SourceType = "患者信息";
                int i = eee.Add(e);
            }
            return result;
        }

        #region 获取基本信息字典（样本源） +  private Dictionary<string, string> GetBaseInfoDic()

        //获取基本信息字典（样本源）
        private Dictionary<string, string> GetBaseInfoDic(string formStr)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //基本信息对象
            Model.EmpiInfo empiInfo = new Model.EmpiInfo();

            if (!string.IsNullOrEmpty(formStr) && formStr != "[]")
            {
                //转换页面上的baseinfo为对象
                List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();
                dicList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Dictionary<string, string>>>(formStr);
                empiInfo = FormToDic.GetFromInfo<Model.EmpiInfo>(dicList);
                dic = FormToDic.ConvertModelToDic(empiInfo);
            }
            return dic;
        }

        #endregion 获取基本信息字典（样本源） +  private Dictionary<string, string> GetBaseInfoDic()

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
        private string GetData(Model.DTO.EmpiInfoRequest request)
        {
            try
            {
                // return Test(request);
                return string.IsNullOrEmpty(request.Request) ? "" : empiService.GetEmpiInfo(request.Request);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
                return ex.Message + "--" + DateTime.Now.ToLongTimeString();
            }
        }

        #endregion 获取数据

        #region 生成临时数据

        /// <summary>
        /// 生成临时数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string Test(Model.DTO.EmpiInfoRequest request)
        {
            Common.RandomTest r = new Common.RandomTest();
            string name = r.CreatName();
            string getDataFromHospitalStr = string.Format("<Response><InterfaceCode>GetEmpiInfo</InterfaceCode><ResultCode>{0}</ResultCode><ErrorMsg>出错了</ErrorMsg><EmpiInfo><EmpiId>{1}</EmpiId><PatientName>{2}</PatientName><Sex>{3}</Sex><Birthday>{4}</Birthday><CardId>{5}</CardId><Tel>{6}</Tel><Address>{7}</Address></EmpiInfo></Response>", "0", r.CreatNum(), r.CreatName(), r.CreatSex(), r.CreatBirthday().ToShortDateString(), "110", "100000000", "广州");
            return getDataFromHospitalStr;
        }

        #endregion 生成临时数据

        #region 将数据转换成对象

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
                            string strNode = JsonConvert.SerializeXmlNode(xd.SelectSingleNode("//EmpiInfo"), Newtonsoft.Json.Formatting.None, true);
                            Model.EmpiInfo emp = JsonConvert.DeserializeObject<Model.EmpiInfo>(strNode);
                            if (!string.IsNullOrEmpty(emp.Birthday))
                            {
                                if (!emp.Birthday.Contains("-") && emp.Birthday.Length == 8)
                                {
                                    emp.Birthday = emp.Birthday.Insert(4, "-").Insert(7, "-");
                                }
                            }

                            if (emp == null || emp.PatientName == "")
                            {
                            }
                            else
                            {
                                jsonData.Data = emp;
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

        //获取数据
        //解析数据
        //返回数据对象
    }
}
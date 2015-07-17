using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FreezerProPlugin
{
    /// <summary>
    /// Import 的摘要说明
    /// </summary>
    public class Import : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //根据条码获取数据
            if (context.Request["GetType"] == "getdatabyMzhzyh")
            {

                string Mzzybz = context.Request["Mzzybz"];//0 门诊 1住院
                string Mzhzyh = context.Request["Mzhzyh"];//住院号或门诊号

                BLL.GetDataFromHospital hospitalDataByBarcode = new BLL.GetDataFromHospital();
                string jsonStrResult = hospitalDataByBarcode.GetDataByBarcode(Mzhzyh, Mzzybz);
                if (jsonStrResult == "")//直接无数据返回（可能是链接有问题）
                {
                    string result = "{\"success\":false,\"result\":\"获取数据失败，请检查住院号或门诊号\"}";
                    context.Response.Write(result);
                    context.Response.End();
                }
                if (jsonStrResult.Contains("ErrorMsg"))
                {
                    string oPListForSpecimen = Common.FpJsonHelper.GetStrFromJsonStr("ErrorMsg", jsonStrResult);
                    string result = "{\"success\":false,\"result\":" + oPListForSpecimen + "}";
                    context.Response.Write(result);
                    context.Response.End();
                }
                if (jsonStrResult.Contains("Name"))
                {
                    string result = "{\"success\":true,\"result\":" + jsonStrResult + "}";
                    context.Response.Write(result);
                    context.Response.End();
                }
                else
                {
                    string result = "";
                    context.Response.Write(result);
                    context.Response.End();
                }
            }

            //获取样本类型给下拉框
            if (context.Request["type"] == "getSampleSourceType")
            {
                ////样本源类型
                context.Response.Write(GetSampleSourceFromFp());
                context.Response.End();
                //context.Response.Write("[{\"sampleSourceTypeName\":\"口腔科\",\"sampleSourceTypeValue\":\"KQK\"}]");
            }

        }
        #region 从FP系统中获取样本源信息 +  private string GetSampleSourceFromFp()
        /// <summary>
        /// 从FP系统中获取样本源信息
        /// </summary>
        /// <returns>凡湖样本源描述和名称Json数据</returns>
        private string GetSampleSourceFromFp()
        {
            BLL.SampleSocrce sampleSource = new BLL.SampleSocrce();
            //创建业务层对象
            Dictionary<string, string> sampleSourceTypeNameAndDecDic = sampleSource.GetSampleSourceTypeNameAndDecToDic();
            //key：张三;value：zhangsan 获取样本源类型名称和描述字典
            Dictionary<string, string> sampleSourceTypeNameAndDecDicTemp = new Dictionary<string, string>();
            //创建临时字典存放key和value的值
            string result = "";
            if (sampleSourceTypeNameAndDecDic.Count > 0)
            {
                StringBuilder sampleSourceTypeNameAndDecStr = new StringBuilder();
                sampleSourceTypeNameAndDecStr.Append("[");
                foreach (KeyValuePair<string, string> item in sampleSourceTypeNameAndDecDic)
                {
                    sampleSourceTypeNameAndDecStr.Append("{");
                    sampleSourceTypeNameAndDecStr.AppendFormat("\"sampleSourceType\":\"{0}\",\"sampleSourceTypeName\":\"{1}\"", item.Value, item.Key);
                    sampleSourceTypeNameAndDecStr.Append("},");
                    #region 测试数据
                    ////循环key生成新的字典（将key和value分开）
                    //if (!sampleSourceTypeNameAndDecDicTemp.Keys.Contains(item.Key))
                    //{
                    //    sampleSourceTypeNameAndDecDicTemp.Add("sampleSourceType", item.Value);
                    //    sampleSourceTypeNameAndDecDicTemp.Add("sampleSourceTypeName", item.Key);
                    //}
                    //调用JsonNet序列化临时字典
                    //sampleSourceTypeNameAndDecStr.Append(Common.FpJsonHelper.DictionaryToJsonString(sampleSourceTypeNameAndDecDicTemp));
                    //sampleSourceTypeNameAndDecStr.Append(","); 
                    #endregion
                }
                result = sampleSourceTypeNameAndDecStr.ToString().TrimEnd(',') + "]";
            }
            else
            {
                result = "{\"sampleSourceType\":\"无样本源\",\"sampleSourceTypeName\":\"无样本源\"}";
            }

            return result;
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
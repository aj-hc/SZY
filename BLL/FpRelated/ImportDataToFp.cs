using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Common;
using System.IO;

namespace BLL
{
    public class ImportDataToFp
    {
        SampleSocrce sampleSouce = new SampleSocrce();

        #region 1.0 提交数据到FP并返回FP处理的结果
        /// <summary>
        /// 提交数据到FP并返回FP处理的结果
        /// </summary>
        /// <param name="sampleSourceTypeName">样本源类型名称</param>
        /// <param name="sampleSourceName">样本源名称</param>
        /// <param name="patientId">样本源唯一标识</param>
        /// <param name="sampleSourceDescription">样本源描述</param>
        /// <param name="hiddenEncodeStr">隐藏域中的加密字段</param>
        /// <returns></returns>
        public string ImportDataToFpAndRetunFpResult(string sampleSourceTypeName, string sampleSourceName, string patientId, string sampleSourceDescription, string hiddenEncodeStr)
        {
            string result = "";
            //获取样本源及描述字典
            Dictionary<string, string> sampleSourceNameAndDesDic = sampleSouce.GetSampleSourceTypeNameAndDecToDic();
            if (sampleSourceNameAndDesDic.Keys.Contains(sampleSourceTypeName))//系统中有此样本源
            {
                //01.接受前台页面发送回来的数据，并将数据转换成字典
                Dictionary<string, string> basicDataDic = new Dictionary<string, string>();
                basicDataDic = DecodeHiddenEncodeStr(hiddenEncodeStr);
                if (basicDataDic.Count > 0)//解码成功（）
                {
                    //02.获取用户自定义字段集合（根据指定的样本源类型名称）
                    //03.获取配置文件中的字段匹配字典
                    //04.根据用户自定义字段字典和字段匹配字典生成需要导入到Fp中的字段字典
                    Dictionary<string, string> sampleSourceFieldsDic = new Dictionary<string, string>();
                    sampleSourceFieldsDic = MathcSampleSourceFieldsWithConfigFile(sampleSourceTypeName, sampleSourceName, sampleSourceDescription, basicDataDic);
                    //06.调用API提交数据到Fp
                    result = sampleSouce.ImportSampleSourceDataToFp(sampleSourceTypeName, sampleSourceFieldsDic);
                    ImportReasult(result, sampleSourceName);
                }
                else
                {
                    //解析前台页面中的隐藏数据失败(隐藏域数据解析错误？-->加密出错？？-->数据来源错误？)
                    result = "{\"success\":\"失败\",\"patientId\":" + "\"" + patientId + "\",\"Reason\":\"导入错误\"}";
                }
            }
            else
            {
                //样本源类型不存在
                result ="{\"success\":\"失败\",\"patientId\":" + "\"" + patientId + "\",\"Reason\":\"样本源类型不存在\"}";
            }
            return result;
        }
        #endregion

        #region 1.0.1 当前样本元类型下的用户自定义字段和医院字段的匹配字典
        /// <summary>
        /// 根据样本元类型名称和配置文件以及医院数据生成最终的样本源字段名称和数据的字典
        /// </summary>
        /// <param name="sampleSourceTypeName">样本源类型名称</param>
        /// <param name="sampleSourceName">样本源Name</param>
        /// <param name="sampleSourceDescription">样本源Description</param>
        /// <param name="basicDataDic">医院数据</param>
        /// <returns>最终生成的的样本源字段名称和数据的字典</returns>
        private Dictionary<string, string> MathcSampleSourceFieldsWithConfigFile(string sampleSourceTypeName, string sampleSourceName, string sampleSourceDescription, Dictionary<string, string> basicDataDic)
        {
            //resultDic是提取当前样本源类型下面的所有字段字典
            Dictionary<string, string> resultDic = new Dictionary<string, string>();
            resultDic.Add("Name", sampleSourceName);
            resultDic.Add("Description", sampleSourceDescription);
            //获取用户自定义字段集合（根据指定的样本源类型名称）
            List<string> sampleSourceFieldsList = new List<string>();
            sampleSourceFieldsList = sampleSouce.GetSampleSourceTypeFieldByTypeName(sampleSourceTypeName);

            //获取配置文件中的字段匹配字典
            Dictionary<string, string> matchFieldsDic = new Dictionary<string, string>();
            matchFieldsDic = GetMatchFieldsXmlToDic("configXML\\MatchFieldWithBasicData.xml", "/Matchings/*");

            //循环遍历样本源自定义字段
            foreach (string item in sampleSourceFieldsList)
            {
                //检查配置文件中是否包含用户自定义字段
                if (matchFieldsDic.Keys.Contains(item))
                {
                    //确保当前字段在最终的自定字段名和value中不存在（防止重key）
                    if (!resultDic.Keys.Contains(item))
                    {
                        //医院数据中包含当前用户自定义字段对应的字段
                        if (basicDataDic.Keys.Contains(matchFieldsDic[item]))
                        {
                            resultDic.Add(item, basicDataDic[matchFieldsDic[item]]);
                        }
                    }
                }
            }
            return resultDic;
        }
        #endregion

        #region 1.0.2将页面传回的隐藏域中的数据解码并转换成字典
        /// <summary>
        /// 将页面传回的隐藏域中的数据解码并转换成字典
        /// </summary>
        /// <param name="hiddenEncodeStr">被加密的数据</param>
        /// <returns>字典</returns>
        private Dictionary<string, string> DecodeHiddenEncodeStr(string hiddenEncodeStr)
        {
            //创建从前台页面提交回来的数据
            Dictionary<string, string> basicDataDic = new Dictionary<string, string>();
            string hiddenDecodeStr = Common.EncodeAndDecodeString.Decode(hiddenEncodeStr);
            if (hiddenDecodeStr != "")
            {
                basicDataDic = Common.FpJsonHelper.JsonStrToDictionary<string, string>(hiddenDecodeStr);
            }
            return basicDataDic;
        }
        #endregion

        #region 1.0.3获取字段匹配文件中的数据到字典

        /// <summary>
        /// 获取字段匹配文件中的数据到字典
        /// </summary>
        /// <param name="xmlPath">路径</param>
        /// <param name="xPath">（默认是："/Matchings/*）</param>
        /// <returns>匹配好的字典</returns>
        public static Dictionary<string, string> GetMatchFieldsXmlToDic(string xmlPath, string xPath)
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
        private string ImportReasult(string result, string patientId)
        {
            string resultReason = "";
            if (FpJsonHelper.GetStrFromJsonStr("status", result) == "DONE")//导入成功
            {
                //获取并导入临床数据
                resultReason ="{\"success\":\"成功\",\"patientId\":" + "\"" + patientId + "\"}";
            }
            else if (FpJsonHelper.GetStrFromJsonStr("status", result) == "ERROR")//导入失败
            {
                string reason = FpJsonHelper.GetStrFromJsonStr("message", result).Replace(":", "-").Replace("\"", "");
                if (reason.Contains("should be unique"))//数据重复导入--已经包含此数据
                {
                    //获取并导入临床数据
                    resultReason ="{\"success\":\"失败\",\"patientId\":" + "\"" + patientId + "\",\"Reason\":\"" + "数据重复" + "\"}";
                }
                else
                {
                    resultReason ="{\"success\":\"失败\",\"patientId\":" + "\"" + patientId + "\",\"Reason\":\"" + reason + "\"}";
                }
            }
            return resultReason;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Common;

namespace BLL
{
    public class SampleSocrce
    {
        //创建数据层对象
        DataWithFP dataWithFP = new DataWithFP();
        #region 构造函数
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="username">用户名</param>
        ///// <param name="password">密码</param>
        //public SampleSocrce()
        //{
        //    dataWithFP = new DataWithFP();
        //}
        #endregion
        #region 获取Sample_Source_Types对象集合 + public List<Model.SampleSourceTypes> Sample_Source_Types()
        /// <summary>
        /// 获取Sample_Source_Types对象集合
        /// </summary>
        /// <returns> 获取Sample_Source_Types对象集合</returns>
        public List<Model.SampleSourceTypes> Sample_Source_Types()
        {
            List<Model.SampleSourceTypes> list_Sample_Source_Types = new List<Model.SampleSourceTypes>() { };
            string str_Json = dataWithFP.getDateFromFp(FpMethod.sample_source_types);
            if (ValidationData.checkTotal(str_Json))
            {
                list_Sample_Source_Types = FpJsonHelper.JObjectToList<Model.SampleSourceTypes>("SampleSourceTypes", str_Json);
            }
            return list_Sample_Source_Types;
        }
        #endregion

        #region 获取指定名称的samplesource 对象
        //获取指定名称的samplesource 对象
        /// <summary>
        /// 指定样本源类型名称获取样本源
        /// </summary>
        /// <param name="typeName">样本源类型名称</param>
        /// <returns>样本源类型</returns>
        public Model.SampleSourceTypes GetSampleSourceTypeByTypeName(string typeName)
        {
            Model.SampleSourceTypes sampleSourceType = new Model.SampleSourceTypes();
            if (Sample_Source_Types().Count > 0)
            {
                foreach (Model.SampleSourceTypes item in Sample_Source_Types())
                {
                    if (item.name == typeName)
                    {
                        sampleSourceType = item;
                        break;
                    }
                }
            }
            return sampleSourceType;
        }
        
        #endregion

        #region 获取样本源类型及描述字典 +  public Dictionary<string, string> GetSampleSourceTypeNameAndDecToDic()
        /// <summary>
        /// 获取样本源类型及描述字典 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetSampleSourceTypeNameAndDecToDic()
        {
            Dictionary<string, string> SampleSourceTypeNameAndDecDic = new Dictionary<string, string>();
           List<Model.SampleSourceTypes> SampleSourceTypes= Sample_Source_Types();
           if (SampleSourceTypes.Count > 0)
            {
                foreach (Model.SampleSourceTypes item in SampleSourceTypes)
                {
                    SampleSourceTypeNameAndDecDic.Add(item.name, item.descr);
                }
            }
            return SampleSourceTypeNameAndDecDic;
        } 
        #endregion

        #region 获取样本源类型中的字段 + public List<string> GetSampleSourceTypeFieldByTypeName(string typeName)

        /// <summary>
        /// 使用样本源类型名称获取样本源类型中的字段集合list<string>
        /// </summary>
        /// <param name="typeName">指定样本元类型名称</param>
        /// <returns>字段集合</returns>
        
        public List<string> GetSampleSourceTypeFieldByTypeName(string typeName)
        {
            List<string> sampleSourceTypeField = new List<string>();
            Model.SampleSourceTypes sampleSourceTypes = GetSampleSourceTypeByTypeName(typeName);
            if (sampleSourceTypes != null)
            {
                string fieldsStr = sampleSourceTypes.fields;
                if (fieldsStr!=null)
                {
                    string[] fields = ((fieldsStr.Replace("<br>", "$")).Replace("</br>", "$")).Split('$');
                    foreach (string item in fields)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            sampleSourceTypeField.Add(item);
                        }
                    }
                }
               
            }
            return sampleSourceTypeField;
        } 

        #endregion

        //导入数据到FreezerPro
        
        //public string ImportSampleSourceDataToFp(string jsonStr)
        //{
        //   return dateWithFP.postDateToFp(FpMethod.import_sources, jsonStr);
        //}

        public string ImportSampleSourceDataToFp(string sampleSourceTypeName, Dictionary<string, string> sampleSourceFieldsDic)
        {
            //01.将字典转换成json格式的字符串
            //02.将此字符串转换成Fp需要的格式
            //03.调用数据层方法提交数据到Fp，并接受返回值
            string sampleSourceFieldsJsonStr = FpJsonHelper.DictionaryToJsonString(sampleSourceFieldsDic);
            return dataWithFP.postDateToFp(FpMethod.import_sources, "&sample_source_type=" + sampleSourceTypeName + "&json=" + sampleSourceFieldsJsonStr);
        }
        public string UpdataSampleSourceDataToFp(string jsonStr)
        {
            return dataWithFP.postDateToFp(FpMethod.import_sources, jsonStr);
        }
        
        //查询样本源数据
        public string QuerySampleSourceByUniqueField(string queryParameter)
        {
            return dataWithFP.postDateToFp(FpMethod.sample_sources, queryParameter);
        }
        public Model.Sample_Source Get_Sample_Source_Info(int sample_source_id)
        {
            Model.Sample_Source samplesource = new Model.Sample_Source();
            string jsonStr = dataWithFP.postDateToFp(FpMethod.sample_source_info,string.Format("&id={0}",sample_source_id));
            samplesource = FpJsonHelper.JsonStrToObject<Model.Sample_Source>(jsonStr);
            return samplesource;
        }
        public Dictionary<string, string> Get_Sample_Source_Userfields(string sample_source_id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string jsonStr = dataWithFP.postDateToFp(FpMethod.sample_source_userfields, string.Format("&id={0}", sample_source_id));
            dic = FpJsonHelper.JsonStrToDictionary<string,string>(jsonStr);
            return dic;
        }
    }
}

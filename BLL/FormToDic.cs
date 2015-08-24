using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RuRo.BLL
{
    public class FormToDic
    {
        #region 转换患者基本信息为字典 +private Dictionary<string, string> ConvertBaseInfoObjToDic(PageBaseInfo pageBaseInfo)
        /// <summary>
        /// 转换患者基本信息为字典  包含Name和描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public  static Dictionary<string, string> ConvertBaseInfoObjToDic(object obj)
        {
            Dictionary<string, string> pageBaseInfoDic = new Dictionary<string, string>();
            Type type = obj.GetType();
            PropertyInfo[] propertys = type.GetProperties();
            foreach (PropertyInfo item in propertys)
            {
                try
                {
                    string value = Common.ReflectHelper.GetValue(obj, item.Name);
                    pageBaseInfoDic.Add(item.Name, value);
                }
                catch (Exception ex)
                {
                    Common.LogHelper.WriteError(ex);
                    continue;
                }
            }
            return pageBaseInfoDic;
        }
        #endregion
        #region 将前台返回的form字典转换成对象 + private T GetFromInfo<T>(List<Dictionary<string, string>> dicList) where T : class,new()
        /// <summary>
        /// 将前台返回的form字典转换成对象
        /// </summary>
        /// <typeparam name="T">要转换成的对象</typeparam>
        /// <param name="dicList"></param>
        /// <returns></returns>
        public static T GetFromInfo<T>(List<Dictionary<string, string>> dicList) where T : class, new()
        {
            T t = new T();
            foreach (var item in dicList)
            {
                string name = "";
                string value = "";
                foreach (var dic in item)
                {
                    if (dic.Key == "name")
                    {
                        name = dic.Value;
                    }
                    if (dic.Key == "value")
                    {
                        value = dic.Value;
                    }
                }
                try
                {
                    Common.ReflectHelper.SetValue(t, name, value);
                }
                catch (Exception ex)
                {
                    Common.LogHelper.WriteError(ex);
                    continue;
                }

            }
            return t;
        }
        #endregion

    }
}

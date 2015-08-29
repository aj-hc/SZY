using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace RuRo.BLL.SZY
{
    public partial class QueryRecoder
    {
        DAL.QueryRecoder dal = new DAL.QueryRecoder();
        /// <summary>
        /// 获取QueryRecoder可批量导入列表
        /// </summary>
        /// <param name="size">页面数</param>
        /// <param name="count">每个页面显示多少条</param>
        /// <param name="where">约束</param>
        /// <param name="strorder">排序</param>
        /// <returns></returns>
        public DataSet GetQueryRecoderTrue_bll(int size, int count, string where, string strorder)
        {
            return dal.GetQueryRecoderTrue(size, count, where, strorder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RuRo.Model.QueryRecoder> DataTableToList(DataTable dt)
        {
            List<RuRo.Model.QueryRecoder> modelList = new List<RuRo.Model.QueryRecoder>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                RuRo.Model.QueryRecoder model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="code">条码</param>
        /// <param name="codeType">条码的类型：0 卡号，1 住院号</param>
        /// <param name="dataStr">传入数据</param>
        /// <returns></returns>
        public string PostData(string dataStr)
        {
            List<Dictionary<string, string>> dicList = GetClinicalInfoDgDicList(dataStr);
            string mes = "";
            //读取code和codeType
            string listStr = JsonConvert.SerializeObject(dicList);
            List<Model.QueryRecoder> queryRecoderList = JsonConvert.DeserializeObject<List<Model.QueryRecoder>>(listStr);
            foreach (Model.QueryRecoder item in queryRecoderList)
            {
                if (item.QueryType == "NormalLisReport")
                {
                    BLL.Request.NormalLisReportRequest nrr = new Request.NormalLisReportRequest(item);
                    BLL.NormalLisReport nr = new NormalLisReport();
                    nrr.CreatRequest(false);
                    nr.GetData(item, false);
                }
                else if (item.QueryType == "PatientDiagnose")
                {
                    BLL.Request.PatientDiagnoseResuest nrr = new Request.PatientDiagnoseResuest(item);
                    //BLL.PatientDiagnoseResuest nr = new PatientDiagnoseResuest();
                    //nrr.CreatRequest(false);
                    //nr.GetData(item, false);
                }
            }
            for (int i = 0; i < dicList.Count; i++)
            {
                string code = dicList[i]["Code"];
                string codeType = dicList[i]["CodeType"];
                string date = dicList[i]["LastQueryDate"];
                Model.QueryRecoder model = new Model.QueryRecoder();
                model = DicToQueryRecoderModel(dicList[i]);
                BLL.NormalLisReport bll_N = new NormalLisReport();
                string result = bll_N.GetData(model, false);

                Model.DTO.NormalLisReportRequest mo = new Model.DTO.NormalLisReportRequest(code, date);
                Model.DTO.JsonModel jsonModel_N = bll_N.GetData(mo, codeType, "");
                if (jsonModel_N.Statu == "ok")
                {
                    mes = bll_N.PostData(code, codeType, JsonConvert.SerializeObject(jsonModel_N.Data));//导入到临床检验数据
                }
                else
                {
                    mes = mes + "," + jsonModel_N.Msg;
                }
                Model.DTO.PatientDiagnoseResuest po = new Model.DTO.PatientDiagnoseResuest(code, date);
                BLL.PatientDiagnose bll_P = new PatientDiagnose();
                Model.DTO.JsonModel jsonModel_P = bll_P.GetData(po, codeType, "");
                if (jsonModel_P.Statu == "ok")
                {
                    mes = bll_N.PostData(code, codeType, JsonConvert.SerializeObject(jsonModel_P.Data));//导入到患者信息
                }
                else
                {
                    mes = mes + "," + jsonModel_P.Msg;
                }
            }
            return mes;
        }

        /// <summary>
        /// 字典转化为model
        /// </summary>
        /// <returns></returns>
        public Model.QueryRecoder DicToQueryRecoderModel(Dictionary<string, string> dic)
        {
            string str = JsonConvert.SerializeObject(dic);
            Model.QueryRecoder queryRecoder = JsonConvert.DeserializeObject<Model.QueryRecoder>(str);
            return queryRecoder;
        }
        /// <summary>
        /// 将数据转化为List
        /// </summary>
        /// <param name="dataStr"></param>
        /// <returns></returns>
        private List<Dictionary<string, string>> GetClinicalInfoDgDicList(string dataStr)
        {
            string clinicalInfoDg = dataStr;//dg
            //页面上临床数据对象集合
            List<Model.QueryRecoder> pageClinicalInfoList = new List<Model.QueryRecoder>();
            List<Dictionary<string, string>> ClinicalInfoDgDicList = new List<Dictionary<string, string>>();
            //将页面上的临床信息转换成对象集合

            if (!string.IsNullOrEmpty(clinicalInfoDg) && clinicalInfoDg != "[]")
            {
                //转换页面上的clinicalInfoDg为对象集合
                pageClinicalInfoList = FreezerProUtility.Fp_Common.FpJsonHelper.JsonStrToObject<List<Model.QueryRecoder>>(clinicalInfoDg);//转换ok
            }
            Model.NormalLisReport cl = new Model.NormalLisReport();

            foreach (Model.QueryRecoder item in pageClinicalInfoList)
            {
                //给对象拼接--临床数据中需要添加基本信息中的RegisterID,InPatientID
                Dictionary<string, string> dic = FormToDic.ConvertModelToDic(item);
                if (dic.Keys.Contains("AddDate"))
                {
                    dic["AddDate"] = item.AddDate.ToString("yyyy-MM-dd");
                }
                ClinicalInfoDgDicList.Add(dic);
            }
            return ClinicalInfoDgDicList;
        }


        /// <summary>
        /// 返回倒数第二条
        /// </summary>
        /// <returns></returns>
        public List<RuRo.Model.QueryRecoder> GetLastSecondData_BLL()
        {
            DataSet ds = dal.GetLastSecondData();
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 返回倒数第一条
        /// </summary>
        /// <returns></returns>
        /// 
        public List<RuRo.Model.QueryRecoder> GetReciprocalFirstData_BLL()
        {
            DataSet ds = dal.GetReciprocalFirstData();
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 修改记录为true
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="adddate"></param>
        /// <param name="code"></param>
        /// <param name="queryType"></param>
        /// <returns></returns>
        public int UpdataQueryRecoderIsDel_BLL(string uname, string adddate, string code, string queryType)
        {
            return dal.UpdataQueryRecoderIsDel(uname, adddate, code, queryType);
        }
    }
}

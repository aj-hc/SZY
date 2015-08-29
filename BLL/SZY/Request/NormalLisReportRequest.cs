using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.BLL.Request
{
    public class NormalLisReportRequest : Request
    {

        /// <summary>
        /// datagrid查询数据
        /// </summary>
        public NormalLisReportRequest(Model.QueryRecoder queryRecoder)
        {
            this.QueryRecoderModel = queryRecoder;
        }
        /// <summary>
        /// 创建获取webservice数据的连接字符串
        /// </summary>
        /// <returns></returns>
        public override void CreatRequest(bool quertByCode)
        {
            //此方法this.RequestStr 赋值

            //01.按照code查询数据
            if (quertByCode)
            {
                //检查数据是否有记录
                //根据code、username、type、isdel 查询数据记录
                Model.QueryRecoder newModel = this.QueryRecoderModel;
                BLL.QueryRecoder queryRecoder = new QueryRecoder();

                List<Model.QueryRecoder> list = queryRecoder.CheckQueryRecord(newModel);
                if (list != null && list.Count > 0)
                {
                    //本地数据库有数据
                    Model.QueryRecoder oldModel = list.OrderByDescending(a => a.LastQueryDate).FirstOrDefault();
                    //对比数据库数据，并更新数据库数据
                    this.QueryRecoderModel = ContrastQueryRecoderModel(newModel, oldModel);
                }
                else
                {
                    //本地数据库无数据
                    this.QueryRecoderModel = ContrastQueryRecoderModel(newModel, null);
                }
            }
            //02.datagrid 提交过来的数据
            else
            {
                //数据肯定有记录
                Model.QueryRecoder oldModel = this.QueryRecoderModel;
                this.QueryRecoderModel = ContrastQueryRecoderModel(null, oldModel);
                //更新数据库的记录--更新X内容
            }
        }
        private Model.QueryRecoder CreatQueryRecoderModel()
        {
            Model.QueryRecoder model = new Model.QueryRecoder();
            model.Code = this.Code;
            model.CodeType = this.CodeType;
            model.QueryType = "NormalLisReport";
            model.Uname = Common.CookieHelper.GetCookieValue("username");
            model.IsDel = false;
            return model;
        }
        private Model.QueryRecoder ContrastQueryRecoderModel(Model.QueryRecoder newModel, Model.QueryRecoder oldModel)
        {
            this.RequestStr = string.Empty;
            Model.QueryRecoder resultModel = new Model.QueryRecoder();
            BLL.QueryRecoder queryRecoder = new QueryRecoder();
            //对比创建的model和数据库中的model。
            //对比方面：addDate、lastQueryDate、QueryResult
            int queryDateInterval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["QueryDateInterval"].Trim());
            if (newModel == null)
            {
                //datagrid里面的数据
                #region datagrid里面的数据
                if ((DateTime.Now - oldModel.AddDate).Days > queryDateInterval)
                {
                    //最后一次查询时间
                    string lastQueryDateStr = Convert.ToDateTime(oldModel.LastQueryDate).ToString("yyyy-MM-dd");
                    //更新最后一次查询时间
                    oldModel.LastQueryDate = DateTime.Now;
                    try
                    {
                        bool updateResult = queryRecoder.Update(resultModel);
                        if (updateResult)
                        {
                            //更新数据成功
                            // this.RequestStr = 最后一次查询时间到adddate+5
                            this.RequestStr = CreatRequestStr(lastQueryDateStr, oldModel.AddDate.AddDays(queryDateInterval).ToString("yyyy-MM-dd"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                    resultModel = oldModel;
                }
                else
                {
                    //5天范围内添加的数据
                    string lastQueryDateStr = Convert.ToDateTime(oldModel.LastQueryDate).ToString("yyyy-MM-dd");
                    if (oldModel.AddDate == oldModel.LastQueryDate && oldModel.AddDate == DateTime.Now)
                    {
                        //当天的数据
                    }
                    else if (oldModel.AddDate == oldModel.LastQueryDate && oldModel.AddDate < DateTime.Now)
                    {
                        oldModel.LastQueryDate = DateTime.Now;
                        try
                        {
                            bool updateResult = queryRecoder.Update(resultModel);
                            if (updateResult)
                            {
                                //更新数据成功
                                // this.RequestStr = 最后一次查询时间到adddate+5
                                this.RequestStr = CreatRequestStr(lastQueryDateStr, oldModel.AddDate.AddDays(queryDateInterval).ToString("yyyy-MM-dd"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Common.LogHelper.WriteError(ex);
                        }

                        resultModel = oldModel;
                    }
                }
                #endregion
            }
            else if (oldModel == null)
            {
                //本地数据库没数据，并且是用code添加信息
                #region 本地数据库没数据，并且是用code添加信息
                //超时的老数据第一次添加
                if ((DateTime.Now - newModel.AddDate).Days > queryDateInterval)
                {
                    newModel.LastQueryDate = DateTime.Now;
                    try
                    {
                        int res = queryRecoder.Add(newModel);
                        if (res > 0)
                        {
                            resultModel = queryRecoder.CheckQueryRecord(newModel).OrderByDescending(a => a.AddDate).FirstOrDefault();
                            string k = newModel.AddDate.AddDays(-queryDateInterval).ToString("yyyy-MM-dd");
                            string j = newModel.AddDate.AddDays(queryDateInterval).ToString("yyyy-MM-dd");
                            this.RequestStr = CreatRequestStr(k, j);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                }
                else
                {
                    //新添加的数据并且是在范围内
                    newModel.LastQueryDate = DateTime.Now;
                    try
                    {
                        int res = queryRecoder.Add(newModel);
                        if (res > 0)
                        {
                            resultModel = queryRecoder.CheckQueryRecord(newModel).OrderByDescending(a => a.AddDate).FirstOrDefault();
                            string k = newModel.AddDate.AddDays(-queryDateInterval).ToString("yyyy-MM-dd");
                            string j = DateTime.Now.ToString("yyyy-MM-dd");
                            //查询字符串的范围是当前日期的前几天，到当前日期
                            this.RequestStr = CreatRequestStr(k, j);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                }
                #endregion
            }
            else if (newModel != null && oldModel != null)
            {
                //当前时间和最后的查询时间的查值
                int dateDifWithOldLastDateAndDateNow = (DateTime.Now - Convert.ToDateTime(oldModel.LastQueryDate)).Days;
                int dateDifWithOldAddDateAndDateNow = (DateTime.Now - oldModel.AddDate).Days;
                //本地数据库有数据，并且是用code添加信息
                //01.判断lastquery 和当前日期
                if (oldModel.LastQueryDate == newModel.LastQueryDate && oldModel.LastQueryDate == DateTime.Now)
                {
                    //重复查询数据
                }
                else if (dateDifWithOldAddDateAndDateNow >= 2 * queryDateInterval)
                {
                    string lastQueryDateStr = Convert.ToDateTime(oldModel.LastQueryDate).ToString("yyyy-MM-dd");
                    //超时 并且是2倍时间间隔之前的数据——新建一条数据
                    Model.QueryRecoder newRecord = new Model.QueryRecoder()
                    {
                        AddDate = DateTime.Now.AddDays(-queryDateInterval),
                        Code = newModel.Code,
                        CodeType = newModel.CodeType,
                        IsDel = false,
                        LastQueryDate = DateTime.Now.AddDays(-queryDateInterval),
                        QueryType = newModel.QueryType,
                        Uname = newModel.Uname
                    };
                    oldModel.LastQueryDate = oldModel.AddDate.AddDays(queryDateInterval);
                    try
                    {
                        int add = queryRecoder.Add(newRecord);

                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                    resultModel = oldModel;
                    this.RequestStr = CreatRequestStr(lastQueryDateStr, oldModel.AddDate.AddDays(queryDateInterval).ToString("yyyy-MM-dd"));
                }
                else if (dateDifWithOldAddDateAndDateNow >= queryDateInterval && dateDifWithOldAddDateAndDateNow < 2 * queryDateInterval)
                {
                    //查询时间有重合
                    string lastQueryDateStr = Convert.ToDateTime(oldModel.LastQueryDate).ToString("yyyy-MM-dd");
                    this.RequestStr = CreatRequestStr(lastQueryDateStr, oldModel.AddDate.AddDays(queryDateInterval).ToString("yyyy-MM-dd"));
                    //超时 并且是2倍时间间隔之前的数据——新建一条数据
                    Model.QueryRecoder newRecord = new Model.QueryRecoder()
                    {
                        AddDate = oldModel.AddDate.AddDays(queryDateInterval),
                        Code = newModel.Code,
                        CodeType = newModel.CodeType,
                        IsDel = false,
                        LastQueryDate = oldModel.AddDate.AddDays(queryDateInterval),
                        QueryType = newModel.QueryType,
                        Uname = newModel.Uname
                    };
                    try
                    {
                        int add = queryRecoder.Add(newRecord);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                        throw;
                    }
                    oldModel.LastQueryDate = oldModel.AddDate.AddDays(queryDateInterval);
                    resultModel = oldModel;
                }
                else
                {
                    string lastQueryDateStr = Convert.ToDateTime(oldModel.LastQueryDate).ToString("yyyy-MM-dd");
                    //没超时，还在时间范围内的
                    oldModel.LastQueryDate = DateTime.Now;
                    this.RequestStr = CreatRequestStr(lastQueryDateStr, DateTime.Now.ToString("yyyy-MM-dd"));
                    resultModel = oldModel;
                }
            }
            return resultModel;
        }
        private string CreatRequestStr(string ksrq00, string jsrq00)
        {
            DateTime dateksrq00 = new DateTime();
            DateTime datekjsrq00 = new DateTime();
            if (DateTime.TryParse(ksrq00, out dateksrq00) && DateTime.TryParse(jsrq00, out datekjsrq00))
            {
                return string.Format("<Request><hospnum>{0}</hospnum><ksrq00>{1}</ksrq00><jsrq00>{2}</jsrq00></Request>", this.Code, dateksrq00.ToString("yyyy-MM-dd"), dateksrq00.ToString("yyyy-MM-dd"));
            }
            else
            {
                return "";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace RuRo.BLL.Request
{
    public class PatientDiagnoseResuest : Request
    {
        public PatientDiagnoseResuest(Model.QueryRecoder queryRecoder)
        {
            this.QueryRecoderModel = queryRecoder;
        }

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
            model.QueryType = "PatientDiagnose";
            model.Uname = Common.CookieHelper.GetCookieValue("username");
            model.IsDel = false;
            return model;
        }

        private Model.QueryRecoder ContrastQueryRecoderModel(Model.QueryRecoder newModel, Model.QueryRecoder oldModel)
        {
            this.RequestStr = new List<string>();
            Model.QueryRecoder resultModel = new Model.QueryRecoder();
            BLL.QueryRecoder queryRecoder = new QueryRecoder();
            //对比创建的model和数据库中的model。
            //对比方面：addDate、lastQueryDate、QueryResult
            int queryDateInterval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["QueryDateInterval"].Trim());
            if (newModel == null)
            {
                //datagrid里面的数据

                #region datagrid里面的数据

                int nowAndAddDate = (DateTime.Now - oldModel.AddDate).Days;
                if (nowAndAddDate > queryDateInterval)
                {
                    //最后一次查询时间
                    string lastQueryDateStr = Convert.ToDateTime(oldModel.LastQueryDate).ToString("yyyy-MM-dd");
                    DateTime lastDate = new DateTime();
                    try
                    {
                        lastDate = Convert.ToDateTime(lastQueryDateStr);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                    int lastDateAndAddDate = (lastDate - oldModel.AddDate).Days;
                    //更新最后一次查询时间
                    oldModel.LastQueryDate = DateTime.Now;
                    try
                    {
                        bool updateResult = queryRecoder.Update(resultModel);
                        if (updateResult && lastDate != null)
                        {
                            if (lastDateAndAddDate >= 0)
                            {
                                //正常情况
                                for (int i = 0; i < (queryDateInterval - lastDateAndAddDate); i++)
                                {
                                    string temStr = this.CreatRequestStr(oldModel.Code, lastDate.AddDays(i));
                                    this.RequestStr.Add(temStr);
                                }
                            }
                            else
                            {
                                //没有最后查询时间
                                for (int i = 0; i < (queryDateInterval - 0); i++)
                                {
                                    string temStr = this.CreatRequestStr(oldModel.Code, oldModel.AddDate.AddDays(i));
                                    this.RequestStr.Add(temStr);
                                }
                            }
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
                    DateTime lastDate = new DateTime();
                    try
                    {
                        lastDate = Convert.ToDateTime(lastQueryDateStr);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }

                    if ((oldModel.AddDate == oldModel.LastQueryDate && oldModel.AddDate == DateTime.Now) || (oldModel.LastQueryDate < oldModel.AddDate && oldModel.AddDate == DateTime.Now))
                    {
                        //当天的数据
                    }
                    else if ((oldModel.AddDate == oldModel.LastQueryDate && oldModel.AddDate < DateTime.Now) || (oldModel.LastQueryDate < oldModel.AddDate && oldModel.AddDate < DateTime.Now))
                    {
                        oldModel.LastQueryDate = DateTime.Now;
                        try
                        {
                            bool updateResult = queryRecoder.Update(resultModel);
                            if (updateResult)
                            {
                                //更新数据成功
                                // this.RequestStr = 最后一次查询时间到adddate+5
                                for (int i = 0; i < nowAndAddDate; i++)
                                {
                                    string temStr = CreatRequestStr(oldModel.Code, oldModel.AddDate.AddDays(i));
                                    this.RequestStr.Add(temStr);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Common.LogHelper.WriteError(ex);
                        }

                        resultModel = oldModel;
                    }
                }

                #endregion datagrid里面的数据
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
                            for (int i = 0; i < queryDateInterval; i++)
                            {
                                string temStr1 = this.CreatRequestStr(newModel.Code, newModel.AddDate.AddDays(-i).AddDays(-1));
                                string temStr2 = this.CreatRequestStr(newModel.Code, newModel.AddDate.AddDays(i));
                                this.RequestStr.Add(temStr1);
                                this.RequestStr.Add(temStr2);
                            }
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
                            string k = newModel.Code;
                            string j = DateTime.Now.ToString("yyyy-MM-dd");
                            for (int i = 0; i < queryDateInterval; i++)
                            {
                                string temStr = this.CreatRequestStr(newModel.Code, newModel.AddDate.AddDays(-i).AddDays(-1));
                                this.RequestStr.Add(temStr);
                            }
                            for (int i = 0; i < (DateTime.Now - newModel.AddDate).Days; i++)
                            {
                                string temStr = this.CreatRequestStr(newModel.Code, newModel.AddDate.AddDays(i));
                                this.RequestStr.Add(temStr);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                }

                #endregion 本地数据库没数据，并且是用code添加信息
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
                    string Code = oldModel.Code;
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
                    DateTime lastDate = new DateTime();
                    try
                    {
                        lastDate = Convert.ToDateTime(oldModel.LastQueryDate);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                    int lastDateAndAddDate = (lastDate - oldModel.AddDate).Days;
                    oldModel.LastQueryDate = oldModel.AddDate.AddDays(queryDateInterval);
                    try
                    {
                        bool q = queryRecoder.CheckRecord(newRecord);
                        if (!q)
                        {
                            //不存在
                            int add = queryRecoder.Add(newRecord);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                    resultModel = oldModel;
                    //最后查询时间到老model的添加时间加上时间间隔
                    if (lastDateAndAddDate >= 0)
                    {
                        //正常情况
                        for (int i = 0; i < (queryDateInterval - lastDateAndAddDate); i++)
                        {
                            string temStr = this.CreatRequestStr(oldModel.Code, lastDate.AddDays(i));
                            this.RequestStr.Add(temStr);
                        }
                    }
                    else
                    {
                        //没有最后查询时间
                        for (int i = 0; i < (queryDateInterval - 0); i++)
                        {
                            string temStr = this.CreatRequestStr(oldModel.Code, oldModel.AddDate.AddDays(i));
                            this.RequestStr.Add(temStr);
                        }
                    }
                }
                else if (dateDifWithOldAddDateAndDateNow >= queryDateInterval && dateDifWithOldAddDateAndDateNow < 2 * queryDateInterval)
                {
                    //查询时间有重合
                    // string lastQueryDateStr = Convert.ToDateTime(oldModel.LastQueryDate).ToString("yyyy-MM-dd");
                    DateTime lastDate = new DateTime();
                    try
                    {
                        lastDate = Convert.ToDateTime(oldModel.LastQueryDate);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                    int lastDateAndAddDate = (lastDate - oldModel.AddDate).Days;
                    //最后查询时间到老model的添加时间加上时间间隔
                    if (lastDateAndAddDate >= 0)
                    {
                        //正常情况
                        for (int i = 0; i < (queryDateInterval - lastDateAndAddDate); i++)
                        {
                            string temStr = this.CreatRequestStr(oldModel.Code, lastDate.AddDays(i));
                            this.RequestStr.Add(temStr);
                        }
                    }
                    else
                    {
                        //没有最后查询时间
                        for (int i = 0; i < (queryDateInterval - 0); i++)
                        {
                            string temStr = this.CreatRequestStr(oldModel.Code, oldModel.AddDate.AddDays(i));
                            this.RequestStr.Add(temStr);
                        }
                    }
                    // this.RequestStr.Add(CreatRequestStr(lastQueryDateStr, oldModel.AddDate.AddDays(queryDateInterval).ToString("yyyy-MM-dd")));
                    //超时 并且是时间间隔之前的数据——新建一条数据
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
                        bool q = queryRecoder.CheckRecord(newRecord);
                        if (!q)
                        {
                            //不存在
                            int add = queryRecoder.Add(newRecord);
                        }
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
                    string strCode = oldModel.Code;
                    //没超时，还在时间范围内的
                    DateTime lastDate = new DateTime();
                    try
                    {
                        lastDate = Convert.ToDateTime(oldModel.LastQueryDate);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHelper.WriteError(ex);
                    }
                    //最后查询时间和当天时间对比
                    int nowAndLastDate = (DateTime.Now - lastDate).Days;
                    //最后查询时间到老model的添加时间加上时间间隔
                    if (nowAndLastDate <= queryDateInterval)
                    {
                        //正常情况
                        for (int i = 0; i < (nowAndLastDate); i++)
                        {
                            string temStr = this.CreatRequestStr(oldModel.Code, lastDate.AddDays(i));
                            this.RequestStr.Add(temStr);
                        }
                    }

                    // this.RequestStr.Add(CreatRequestStr(strCode, DateTime.Now.ToString("yyyy-MM-dd")));
                    oldModel.LastQueryDate = DateTime.Now;
                    resultModel = oldModel;
                }
            }
            List<string> temResquestStrList = new List<string>();
            if (this.RequestStr != null && this.RequestStr.Count > 0)
            {
                foreach (string item in this.RequestStr)
                {
                    if (!temResquestStrList.Contains(item))
                    {
                        temResquestStrList.Add(item);
                    }
                }
            }

            this.RequestStr = temResquestStrList;
            return resultModel;
        }

        private string CreatRequestStr(string cardno, string cxrq00)
        {
            //DateTime datecxrq00 = new DateTime();
            //if (DateTime.TryParse(cxrq00, out datecxrq00))
            //{
            //    //return string.Format("<Request><hospnum>{0}</hospnum><ksrq00>{1}</ksrq00><jsrq00>{2}</jsrq00></Request>", this.Code, dateksrq00.ToString("yyyy-MM-dd"), dateksrq00.ToString("yyyy-MM-dd"));
            //    return string.Format("<Request><cardno>{0}</cardno><cxrq00>{1}</cxrq00></Request>", cardno, cxrq00);
            //}
            //else
            //{
            //    return "";
            //}

            DateTime datecxrq00 = new DateTime();
            if (DateTime.TryParse(cxrq00, out datecxrq00))
            {
                //return string.Format("<Request><hospnum>{0}</hospnum><ksrq00>{1}</ksrq00><jsrq00>{2}</jsrq00></Request>", this.Code, dateksrq00.ToString("yyyy-MM-dd"), dateksrq00.ToString("yyyy-MM-dd"));
                return string.Format("<Request><cardno>{0}</cardno><cxrq00>{1}</cxrq00></Request>", cardno, datecxrq00.ToString("yyyy-MM-dd"));
            }
            else
            {
                return "";
            }
        }

        private string CreatRequestStr(string cardno, DateTime cxrq00)
        {
            return string.Format("<Request><cardno>{0}</cardno><cxrq00>{1}</cxrq00></Request>", cardno, cxrq00.ToString("yyyy-MM-dd"));
        }
    }
}
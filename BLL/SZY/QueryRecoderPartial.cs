using System;
using System.Collections.Generic;
using System.Text;

namespace RuRo.BLL
{
    public partial class QueryRecoder
    {
        public List<Model.QueryRecoder> CheckQueryRecord(Model.QueryRecoder model)
        {
            BLL.QueryRecoder qr = new BLL.QueryRecoder();
            QueryRecoder queryRecoder = new QueryRecoder();
            //查询本地数据库有没有数据
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(model.Uname))
            {
                strWhere.AppendFormat("Uname = {0} and ", "'" + model.Uname + "'");
            }
            if (!string.IsNullOrEmpty(model.Code))
            {
                strWhere.AppendFormat("Code = {0} and ", "'" + model.Code + "'");
            }
            if (!string.IsNullOrEmpty(model.CodeType))
            {
                strWhere.AppendFormat("CodeType = {0} and ", "'" + model.CodeType + "'");
            }
            if (!string.IsNullOrEmpty(model.QueryType))
            {
                strWhere.AppendFormat("QueryType = {0} and  ", "'" + model.QueryType + "'");
            }
            if (!model.IsDel)
            {
                strWhere.AppendFormat("IsDel = {0}", "'" + model.IsDel + "'");
            }
            //查询条件是，当前用户添加的卡号为X的卡号类型为Y的没有标记删除的并且临床数据类型为Z的数据
            return qr.GetModelList(strWhere.ToString());
        }

        /// <summary>
        /// 检查数据是否存在，会检测添加日期 true存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckRecord(Model.QueryRecoder model)
        {
            BLL.QueryRecoder qr = new BLL.QueryRecoder();
            QueryRecoder queryRecoder = new QueryRecoder();
            //查询本地数据库有没有数据
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(model.Uname))
            {
                strWhere.AppendFormat("Uname = {0} and ", "'" + model.Uname + "'");
            }
            if (!string.IsNullOrEmpty(model.Code))
            {
                strWhere.AppendFormat("Code = {0} and ", "'" + model.Code + "'");
            }
            if (!string.IsNullOrEmpty(model.CodeType))
            {
                strWhere.AppendFormat("CodeType = {0} and ", "'" + model.CodeType + "'");
            }
            if (!string.IsNullOrEmpty(model.QueryType))
            {
                strWhere.AppendFormat("QueryType = {0} and  ", "'" + model.QueryType + "'");
            }
            if (!model.IsDel)
            {
                strWhere.AppendFormat("IsDel = {0} and ", "'" + model.IsDel + "'");
            }
            DateTime date = new DateTime();
            if (DateTime.TryParse(model.AddDate.ToString("yyyy-MM-dd"), out date))
            {
                // select * from dbo.QueryRecoder where REPLACE(CONVERT(Char(10),AddDate,111),'/','-')='2015-08-06'
                strWhere.AppendFormat("REPLACE(CONVERT(Char(10),AddDate,111),'/','-')={0}", "'" + model.AddDate.ToString("yyyy-MM-dd") + "'");
                //strWhere.AppendFormat("AddDate = {0}", "'" + model.AddDate + "'");
            }
            //查询条件是，当前用户添加的卡号为X的卡号类型为Y的没有标记删除的并且临床数据类型为Z的数据
            List<Model.QueryRecoder> list = new List<Model.QueryRecoder>();
            list = qr.GetModelList(strWhere.ToString());
            if (list !=null&&list.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
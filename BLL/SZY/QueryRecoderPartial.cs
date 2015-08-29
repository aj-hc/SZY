using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

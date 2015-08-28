using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.BLL.Request
{
    public class NormalLisReportRequest : Request
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public string ksrq00 { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string jsrq00 { get; set; }
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
                BLL.QueryRecoder queryRecoder = new QueryRecoder();
                Model.QueryRecoder newModel = this.QueryRecoderModel;
                List<Model.QueryRecoder> list = CheckQueryRecord(newModel);
                if (list != null && list.Count > 0)
                {
                    //本地数据库有数据
                    Model.QueryRecoder oldModel = list.OrderByDescending(a => a.LastQueryDate).FirstOrDefault();
                    //对比数据库数据，并更新数据库数据
                }
                else
                {
                    //本地数据库无数据
                }
            }
            else if(this.QueryRecoderModel!=null)
            {
                //数据肯定有记录
            }
            else
            {

            }
            //02.列表数据
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
        private List<Model.QueryRecoder> CheckQueryRecord(Model.QueryRecoder model)
        {
            QueryRecoder queryRecoder = new QueryRecoder();
            //查询本地数据库有没有数据
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("Uname = {0} and ", "'" + model.Uname + "'");
            strWhere.AppendFormat("Code = {0} and ", "'" + model.Code + "'");
            strWhere.AppendFormat("CodeType = {0} and ", "'" + model.CodeType + "'");
            strWhere.AppendFormat("QueryType = {0} and  ", "'" + model.QueryType + "'");
            strWhere.AppendFormat("IsDel = {0}", "'" + model.IsDel + "'");

            //查询条件是，当前用户添加的卡号为X的卡号类型为Y的没有标记删除的并且临床数据类型为Z的数据
            return queryRecoder.GetModelList(strWhere.ToString());
        }
        private Model.QueryRecoder ContrastQueryRecoderModel(Model.QueryRecoder newModel, Model.QueryRecoder oldModel)
        {
            //对比创建的model和数据库中的model。
            //对比方面：addDate、lastQueryDate、QueryResult
            int QueryDateInterval = 5;
            var nModel = newModel;
            int addDateDifference = (nModel.AddDate - oldModel.AddDate).Days;
            if (addDateDifference > QueryDateInterval)
            {
                //当前添加日期大于记录数据的5天
                
            }
            return new Model.QueryRecoder();
        }
    }
}

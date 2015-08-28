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
        /// 构造函数 按照code查询数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        public NormalLisReportRequest(string code, DateTime date)
        {
            this.Date = date;
            this.Code = code;
        }
        /// <summary>
        /// datagrid查询数据
        /// </summary>
        /// <param name="ksrq00"></param>
        /// <param name="jsrq00"></param>
        public NormalLisReportRequest(string ksrq00, string jsrq00)
        {
            this.ksrq00 = ksrq00;
            this.jsrq00 = jsrq00;
        }

        /// <summary>
        /// 创建获取webservice数据的连接字符串
        /// </summary>
        /// <returns></returns>
        public override void CreatRequest()
        {
            //此方法this.RequestStr 赋值
            
            //01.按照code查询数据--会传入查询日期
            if (this.Date!=null)
            {
               //检查数据是否有记录
                //根据code、username、type、isdel 查询数据记录
                BLL.QueryRecoder queryRecoder = new QueryRecoder();
                Model.QueryRecoder model = CreatQueryRecoderModel();
                List<Model.QueryRecoder> list = CheckQueryRecord(model);
                if (list != null && list.Count > 0)
                {
                    //本地数据库有数据
                    Model.QueryRecoder oldModel = list.OrderByDescending(a => a.LastQueryDate).FirstOrDefault();

                }
                else
                {
                    //本地数据库无数据
                }
            }
            else
            {
                //日期为空
                //数据肯定有记录----需要更新查询记录信息
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
            strWhere.AppendFormat("IsDel = {0}", "'" + false + "'");

            //查询条件是，当前用户添加的卡号为X的卡号类型为Y的没有标记删除的并且临床数据类型为Z的数据
            return queryRecoder.GetModelList(strWhere.ToString());
        }
    }
}

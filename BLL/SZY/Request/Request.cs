using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.BLL.Request
{
    public abstract class Request
    {
        /// <summary>
        /// 查询卡号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 条码类型
        /// </summary>
        public string CodeType { get; set; }
        /// <summary>
        /// 查询日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 查询字符串
        /// </summary>
        public string  RequestStr { get; set; }
        public Model.QueryRecoder  QueryRecoderModel{ get; set; }
        /// <summary>
        /// 创建查询字符串
        /// </summary>
        /// <returns></returns>
        public abstract void CreatRequest();
    }
}

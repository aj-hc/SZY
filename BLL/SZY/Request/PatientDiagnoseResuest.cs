using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.BLL.Request
{
    public class PatientDiagnoseResuest:Request
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardno { get; set; }
        /// <summary>
        /// 查询日期
        /// </summary>
        public string cxrq00 { get; set; }

        /// <summary>
        /// datagrid查询数据
        /// </summary>
        public PatientDiagnoseResuest(Model.QueryRecoder queryRecoder)
        {
            this.QueryRecoderModel = queryRecoder;
        }

        //public PatientDiagnoseResuest(string cardno, string cxrq00)
        //{
        //    this.RequestStr = string.Format("<Request><cardno>{0}</cardno><cxrq00>{1}</cxrq00></Request>", cardno, cxrq00);
        //}
        public override void CreatRequest(bool quertByCode)
        {
            throw new NotImplementedException();
        }
    }
}

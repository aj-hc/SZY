using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.Model.DTO
{
    public class PatientDiagnoseResuest 
    {
        /// <summary>
        /// 查询卡号
        /// </summary>
        public string cardno { get; set; }
        /// <summary>
        /// 查询日期
        /// </summary>
        public string cxrq00 { get; set; }
        /// <summary>
        /// 查询字符串
        /// </summary>
        public string RequestStr { get; set; }
        public PatientDiagnoseResuest(string cardno, string cxrq00)
        {
            this.cardno = cardno;
            this.cxrq00 = cxrq00;
            this.RequestStr = string.Format("<Request><cardno>{0}</cardno><cxrq00>{1}</cxrq00></Request>", cardno, cxrq00);
        }
    }
}

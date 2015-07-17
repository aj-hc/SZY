using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Record
    {
        /// <summary>
        /// 病人门诊号、住院号
        /// </summary>
        public string  hospnum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string patname { get; set; }
        public string sex { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public string age_month { get; set; }
        public string age { get; set; }
        /// <summary>
        /// 项目总称
        /// </summary>
        public string ext_mthd { get; set; }
        public string result { get; set; }
        /// <summary>
        /// 1：高；2：低；3：阳性
        /// </summary>
        public string ref_flag { get; set; }
        /// <summary>
        /// 正常低值
        /// </summary>
        public string lowvalue { get; set; }
        /// <summary>
        /// 正常高值
        /// </summary>
        public string highvalue { get; set; }
        /// <summary>
        /// 正常范围
        /// </summary>
        public string print_ref { get; set; }
        /// <summary>
        /// 批准时间
        /// </summary>
        public string check_date { get; set; }
        /// <summary>
        ///批准人
        /// </summary>
        public string check_by_name { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string units { get; set; }
        /// <summary>
        /// 单项名称
        /// </summary>
        public string chinese { get; set; }

    }
}

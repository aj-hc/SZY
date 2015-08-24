using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.Model.DTO
{
    public class EmpiInfoRequest
    {
        // string request = string.Format("<Request><Mzhzyh>{0}</Mzhzyh><Mzzybz>{1}</Mzzybz></Request>", Mzhzyh, Mzzybz);
        /// <summary>
        /// 门诊、住院号
        /// </summary>
        public string Mzhzyh { get; set; }
        /// <summary>
        /// 门诊住院号标识 0-卡号，1-住院号
        /// </summary>
        public string Mzzybz { get; set; }
        /// <summary>
        /// 查询字符串
        /// </summary>
        public string Request { get; set; }
        public EmpiInfoRequest(string mzhzyh, string mzzybz)
        {
            this.Mzhzyh = mzhzyh;
            this.Mzzybz = mzzybz;
            this.Request = string.Format("<Request><Mzhzyh>{0}</Mzhzyh><Mzzybz>{1}</Mzzybz></Request>", mzhzyh, mzzybz);
        }
    }
}

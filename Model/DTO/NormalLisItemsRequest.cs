namespace RuRo.Model.DTO
{
    public class NormalLisItemsRequest
    {
        //<Request>
        //  <hospnum>6445658</hospnum>
        //  <ksrq00>2015-03-13</ksrq00>
        //  <jsrq00>2015-03-15</jsrq00>
        //</Request>
        /// <summary>
        /// 病人门诊号、住院号
        /// </summary>
        public string hospnum { get; set; }

        /// <summary>
        /// 采集开始日期
        /// </summary>
        public string ksrq00 { get; set; }

        /// <summary>
        /// 采集结束日期
        /// </summary>
        public string jsrq00 { get; set; }
    }
}
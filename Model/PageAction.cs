namespace RuRo.Model
{
    public class PageAction
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int CurPage { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }

        public string TabName { get; set; }

        /// <summary>
        /// 分页字段
        /// </summary>
        public string Fields { get; set; }

        public string PkField { get; set; }
        public string Condition { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
    }
}
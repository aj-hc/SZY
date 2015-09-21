using System;

namespace RuRo.Model
{
    /// <summary>
    /// NormalLisItems:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class NormalLisItems
    {
        public NormalLisItems()
        { }

        #region Model

        private int _id;
        private string _hospnum;
        private string _patname;
        private string _sex;
        private string _age;
        private string _age_month;
        private string _ext_mthd;
        private string _location;
        private string _doc_name0;
        private DateTime? _test_date;
        private string _check_by_name;
        private DateTime? _check_date;
        private bool _isdel = false;

        /// <summary>
        ///
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 门诊号/住院号
        /// </summary>
        public string hospnum
        {
            set { _hospnum = value; }
            get { return _hospnum; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string patname
        {
            set { _patname = value; }
            get { return _patname; }
        }

        /// <summary>
        /// 性别   取值为：M 、F
        /// </summary>
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public string age
        {
            set { _age = value; }
            get { return _age; }
        }

        /// <summary>
        /// 月
        /// </summary>
        public string age_month
        {
            set { _age_month = value; }
            get { return _age_month; }
        }

        /// <summary>
        /// 项目总称
        /// </summary>
        public string ext_mthd
        {
            set { _ext_mthd = value; }
            get { return _ext_mthd; }
        }

        /// <summary>
        /// 申请科室
        /// </summary>
        public string location
        {
            set { _location = value; }
            get { return _location; }
        }

        /// <summary>
        /// 申请医生
        /// </summary>
        public string DOC_NAME0
        {
            set { _doc_name0 = value; }
            get { return _doc_name0; }
        }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? test_date
        {
            set { _test_date = value; }
            get { return _test_date; }
        }

        /// <summary>
        /// 报告医生
        /// </summary>
        public string check_by_name
        {
            set { _check_by_name = value; }
            get { return _check_by_name; }
        }

        /// <summary>
        /// 报告日期
        /// </summary>
        public DateTime? check_date
        {
            set { _check_date = value; }
            get { return _check_date; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }

        #endregion Model
    }
}
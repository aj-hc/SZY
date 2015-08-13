
using System;
namespace RuRo.Model
{
	/// <summary>
	/// NormalLisReport:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class NormalLisReport
	{
		public NormalLisReport()
		{}
		#region Model
		private int _id;
		private string _hospnum;
		private string _patname;
		private string _sex;
		private string _age;
		private string _age_month;
		private string _ext_mthd;
		private string _chinese;
		private string _result;
		private string _units;
		private string _ref_flag;
		private string _lowvalue;
		private string _highvalue;
		private string _print_ref;
		private string _check_date;
		private string _check_by_name;
		private string _prnt_order;
		private bool _isdel;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 病人门诊号、住院号
		/// </summary>
		public string hospnum
		{
			set{ _hospnum=value;}
			get{return _hospnum;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
		public string patname
		{
			set{ _patname=value;}
			get{return _patname;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public string sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 年龄
		/// </summary>
		public string age
		{
			set{ _age=value;}
			get{return _age;}
		}
		/// <summary>
		/// 月
		/// </summary>
		public string age_month
		{
			set{ _age_month=value;}
			get{return _age_month;}
		}
		/// <summary>
		/// 项目总称
		/// </summary>
		public string ext_mthd
		{
			set{ _ext_mthd=value;}
			get{return _ext_mthd;}
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string chinese
		{
			set{ _chinese=value;}
			get{return _chinese;}
		}
		/// <summary>
		/// 结果
		/// </summary>
		public string result
		{
			set{ _result=value;}
			get{return _result;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string units
		{
			set{ _units=value;}
			get{return _units;}
		}
		/// <summary>
		/// 高低 ，1：高；2：低；3：阳性
		/// </summary>
		public string ref_flag
		{
			set{ _ref_flag=value;}
			get{return _ref_flag;}
		}
		/// <summary>
		/// 正常低值
		/// </summary>
		public string lowvalue
		{
			set{ _lowvalue=value;}
			get{return _lowvalue;}
		}
		/// <summary>
		/// 正常高值
		/// </summary>
		public string highvalue
		{
			set{ _highvalue=value;}
			get{return _highvalue;}
		}
		/// <summary>
		/// 正常范围
		/// </summary>
		public string print_ref
		{
			set{ _print_ref=value;}
			get{return _print_ref;}
		}
		/// <summary>
		/// 批准时间
		/// </summary>
		public string check_date
		{
			set{ _check_date=value;}
			get{return _check_date;}
		}
		/// <summary>
		/// 批准人
		/// </summary>
		public string check_by_name
		{
			set{ _check_by_name=value;}
			get{return _check_by_name;}
		}
		/// <summary>
		/// 打印顺序序号
		/// </summary>
		public string prnt_order
		{
			set{ _prnt_order=value;}
			get{return _prnt_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool isDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		#endregion Model

	}
}


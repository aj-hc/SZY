
using System;
namespace RuRo.Model
{
	/// <summary>
	/// EmpiInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EmpiInfo
	{
		public EmpiInfo()
		{}
		#region Model
		private int _id;
		private string _patientname;
		private string _sex;
		private string _birthday;
		private string _cardid;
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
		/// 姓名
		/// </summary>
		public string PatientName
		{
			set{ _patientname=value;}
			get{return _patientname;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 出生日期
		/// </summary>
		public string Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 身份证号
		/// </summary>
		public string CardId
		{
			set{ _cardid=value;}
			get{return _cardid;}
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


using System;
namespace RuRo.Model
{
	/// <summary>
	/// PatientDiagnose:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PatientDiagnose
	{
		public PatientDiagnose()
		{}
		#region Model
		private int _id;
		private string _cardno;
		private string _csrq00;
		private string _patientname;
		private string _sex;
		private DateTime? _brithday;
		private string _cardid;
		private string _tel;
		private string _registerno;
		private string _icd;
		private string _diagnose;
		private string _type;
		private string _flag;
		private string _diagnosedate;
		private bool _isdel;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 卡号
		/// </summary>
		public string Cardno
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 查询日期
		/// </summary>
		public string Csrq00
		{
			set{ _csrq00=value;}
			get{return _csrq00;}
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
		public DateTime? Brithday
		{
			set{ _brithday=value;}
			get{return _brithday;}
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
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegisterNo
		{
			set{ _registerno=value;}
			get{return _registerno;}
		}
		/// <summary>
		/// ICD码
		/// </summary>
		public string Icd
		{
			set{ _icd=value;}
			get{return _icd;}
		}
		/// <summary>
		/// 诊断名称
		/// </summary>
		public string Diagnose
        {
			set{ _diagnose=value;}
			get{return _diagnose;}
		}
		/// <summary>
		/// 诊断类型:1：中医疾病 2：中医症候 3：西医主诊断 4 西医其他诊断
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 诊断类别:1：西医诊断 2 中医诊断
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 诊断日期
		/// </summary>
		public string DiagnoseDate
		{
			set{ _diagnosedate=value;}
			get{return _diagnosedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		#endregion Model

	}
}


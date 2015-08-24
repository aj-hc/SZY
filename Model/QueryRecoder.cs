using System;
namespace RuRo.Model
{
	/// <summary>
	/// QueryRecoder:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class QueryRecoder
	{
		public QueryRecoder()
		{}
		#region Model
		private int _id;
		private string _uname;
		private DateTime _adddate;
		private DateTime? _lastquerydate;
		private string _code;
		private string _codetype;
		private string _querytype;
		private string _queryresult;
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
		/// 查询的用户
		/// </summary>
		public string Uname
		{
			set{ _uname=value;}
			get{return _uname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		/// <summary>
		/// 最后一次查询日期
		/// </summary>
		public DateTime? LastQueryDate
		{
			set{ _lastquerydate=value;}
			get{return _lastquerydate;}
		}
		/// <summary>
		/// 查询的条码号
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 条码号类型
		/// </summary>
		public string CodeType
		{
			set{ _codetype=value;}
			get{return _codetype;}
		}
		/// <summary>
		/// 查询的数据类型
		/// </summary>
		public string QueryType
		{
			set{ _querytype=value;}
			get{return _querytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QueryResult
		{
			set{ _queryresult=value;}
			get{return _queryresult;}
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


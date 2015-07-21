/**  版本信息模板在安装目录下，可自行修改。
* QueryRecoder.cs
*
* 功 能： N/A
* 类 名： QueryRecoder
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/21 14:38:51   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
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
		private string _lastquerydate;
		private string _code;
		private string _codetype;
		private string _querytype;
		private string _queryresult;
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
		/// 最后一次查询日期
		/// </summary>
		public string LastQueryDate
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
		#endregion Model

	}
}


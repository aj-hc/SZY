/**  版本信息模板在安装目录下，可自行修改。
* EmpiInfo.cs
*
* 功 能： N/A
* 类 名： EmpiInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/21 16:33:11   N/A    初版
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
		#endregion Model

	}
}


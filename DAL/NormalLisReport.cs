
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace RuRo.DAL
{
	/// <summary>
	/// 数据访问类:NormalLisReport
	/// </summary>
	public partial class NormalLisReport
	{
		public NormalLisReport()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "NormalLisReport"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from NormalLisReport");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(RuRo.Model.NormalLisReport model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into NormalLisReport(");
			strSql.Append("hospnum,patname,Sex,Age,age_month,ext_mthd,chinese,result,units,ref_flag,lowvalue,highvalue,print_ref,check_date,check_by_name,prnt_order)");
			strSql.Append(" values (");
			strSql.Append("@hospnum,@patname,@Sex,@Age,@age_month,@ext_mthd,@chinese,@result,@units,@ref_flag,@lowvalue,@highvalue,@print_ref,@check_date,@check_by_name,@prnt_order)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@hospnum", SqlDbType.NVarChar,50),
					new SqlParameter("@patname", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NChar,10),
					new SqlParameter("@Age", SqlDbType.NChar,10),
					new SqlParameter("@age_month", SqlDbType.NChar,10),
					new SqlParameter("@ext_mthd", SqlDbType.NVarChar,50),
					new SqlParameter("@chinese", SqlDbType.NVarChar,50),
					new SqlParameter("@result", SqlDbType.NVarChar,-1),
					new SqlParameter("@units", SqlDbType.NChar,10),
					new SqlParameter("@ref_flag", SqlDbType.NChar,10),
					new SqlParameter("@lowvalue", SqlDbType.NVarChar,50),
					new SqlParameter("@highvalue", SqlDbType.NVarChar,50),
					new SqlParameter("@print_ref", SqlDbType.NVarChar,50),
					new SqlParameter("@check_date", SqlDbType.NVarChar,50),
					new SqlParameter("@check_by_name", SqlDbType.NVarChar,50),
					new SqlParameter("@prnt_order", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.hospnum;
			parameters[1].Value = model.patname;
			parameters[2].Value = model.sex;
			parameters[3].Value = model.age;
			parameters[4].Value = model.age_month;
			parameters[5].Value = model.ext_mthd;
			parameters[6].Value = model.chinese;
			parameters[7].Value = model.result;
			parameters[8].Value = model.units;
			parameters[9].Value = model.ref_flag;
			parameters[10].Value = model.lowvalue;
			parameters[11].Value = model.highvalue;
			parameters[12].Value = model.print_ref;
			parameters[13].Value = model.check_date;
			parameters[14].Value = model.check_by_name;
			parameters[15].Value = model.prnt_order;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(RuRo.Model.NormalLisReport model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update NormalLisReport set ");
			strSql.Append("hospnum=@hospnum,");
			strSql.Append("patname=@patname,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("Age=@Age,");
			strSql.Append("age_month=@age_month,");
			strSql.Append("ext_mthd=@ext_mthd,");
			strSql.Append("chinese=@chinese,");
			strSql.Append("result=@result,");
			strSql.Append("units=@units,");
			strSql.Append("ref_flag=@ref_flag,");
			strSql.Append("lowvalue=@lowvalue,");
			strSql.Append("highvalue=@highvalue,");
			strSql.Append("print_ref=@print_ref,");
			strSql.Append("check_date=@check_date,");
			strSql.Append("check_by_name=@check_by_name,");
			strSql.Append("prnt_order=@prnt_order");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@hospnum", SqlDbType.NVarChar,50),
					new SqlParameter("@patname", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NChar,10),
					new SqlParameter("@Age", SqlDbType.NChar,10),
					new SqlParameter("@age_month", SqlDbType.NChar,10),
					new SqlParameter("@ext_mthd", SqlDbType.NVarChar,50),
					new SqlParameter("@chinese", SqlDbType.NVarChar,50),
					new SqlParameter("@result", SqlDbType.NVarChar,-1),
					new SqlParameter("@units", SqlDbType.NChar,10),
					new SqlParameter("@ref_flag", SqlDbType.NChar,10),
					new SqlParameter("@lowvalue", SqlDbType.NVarChar,50),
					new SqlParameter("@highvalue", SqlDbType.NVarChar,50),
					new SqlParameter("@print_ref", SqlDbType.NVarChar,50),
					new SqlParameter("@check_date", SqlDbType.NVarChar,50),
					new SqlParameter("@check_by_name", SqlDbType.NVarChar,50),
					new SqlParameter("@prnt_order", SqlDbType.NVarChar,50),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.hospnum;
			parameters[1].Value = model.patname;
			parameters[2].Value = model.sex;
			parameters[3].Value = model.age;
			parameters[4].Value = model.age_month;
			parameters[5].Value = model.ext_mthd;
			parameters[6].Value = model.chinese;
			parameters[7].Value = model.result;
			parameters[8].Value = model.units;
			parameters[9].Value = model.ref_flag;
			parameters[10].Value = model.lowvalue;
			parameters[11].Value = model.highvalue;
			parameters[12].Value = model.print_ref;
			parameters[13].Value = model.check_date;
			parameters[14].Value = model.check_by_name;
			parameters[15].Value = model.prnt_order;
			parameters[16].Value = model.Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from NormalLisReport ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from NormalLisReport ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public RuRo.Model.NormalLisReport GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,hospnum,patname,Sex,Age,age_month,ext_mthd,chinese,result,units,ref_flag,lowvalue,highvalue,print_ref,check_date,check_by_name,prnt_order from NormalLisReport ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			RuRo.Model.NormalLisReport model=new RuRo.Model.NormalLisReport();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public RuRo.Model.NormalLisReport DataRowToModel(DataRow row)
		{
			RuRo.Model.NormalLisReport model=new RuRo.Model.NormalLisReport();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["hospnum"]!=null)
				{
					model.hospnum=row["hospnum"].ToString();
				}
				if(row["patname"]!=null)
				{
					model.patname=row["patname"].ToString();
				}
				if(row["Sex"]!=null)
				{
					model.sex=row["Sex"].ToString();
				}
				if(row["Age"]!=null)
				{
					model.age=row["Age"].ToString();
				}
				if(row["age_month"]!=null)
				{
					model.age_month=row["age_month"].ToString();
				}
				if(row["ext_mthd"]!=null)
				{
					model.ext_mthd=row["ext_mthd"].ToString();
				}
				if(row["chinese"]!=null)
				{
					model.chinese=row["chinese"].ToString();
				}
				if(row["result"]!=null)
				{
					model.result=row["result"].ToString();
				}
				if(row["units"]!=null)
				{
					model.units=row["units"].ToString();
				}
				if(row["ref_flag"]!=null)
				{
					model.ref_flag=row["ref_flag"].ToString();
				}
				if(row["lowvalue"]!=null)
				{
					model.lowvalue=row["lowvalue"].ToString();
				}
				if(row["highvalue"]!=null)
				{
					model.highvalue=row["highvalue"].ToString();
				}
				if(row["print_ref"]!=null)
				{
					model.print_ref=row["print_ref"].ToString();
				}
				if(row["check_date"]!=null)
				{
					model.check_date=row["check_date"].ToString();
				}
				if(row["check_by_name"]!=null)
				{
					model.check_by_name=row["check_by_name"].ToString();
				}
				if(row["prnt_order"]!=null)
				{
					model.prnt_order=row["prnt_order"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,hospnum,patname,Sex,Age,age_month,ext_mthd,chinese,result,units,ref_flag,lowvalue,highvalue,print_ref,check_date,check_by_name,prnt_order ");
			strSql.Append(" FROM NormalLisReport ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,hospnum,patname,Sex,Age,age_month,ext_mthd,chinese,result,units,ref_flag,lowvalue,highvalue,print_ref,check_date,check_by_name,prnt_order ");
			strSql.Append(" FROM NormalLisReport ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM NormalLisReport ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from NormalLisReport T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "NormalLisReport";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}


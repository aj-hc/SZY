using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace RuRo.DAL
{
	/// <summary>
	/// 数据访问类:NormalLisItems
	/// </summary>
	public partial class NormalLisItems
	{
		public NormalLisItems()
		{}
		#region  BasicMethod


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(RuRo.Model.NormalLisItems model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into NormalLisItems(");
			strSql.Append("hospnum,patname,sex,age,age_month,ext_mthd,location,DOC_NAME0,test_date,check_by_name,check_date,IsDel)");
			strSql.Append(" values (");
			strSql.Append("@hospnum,@patname,@sex,@age,@age_month,@ext_mthd,@location,@DOC_NAME0,@test_date,@check_by_name,@check_date,@IsDel)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@hospnum", SqlDbType.NVarChar,50),
					new SqlParameter("@patname", SqlDbType.NVarChar,50),
					new SqlParameter("@sex", SqlDbType.NChar,10),
					new SqlParameter("@age", SqlDbType.NChar,10),
					new SqlParameter("@age_month", SqlDbType.NChar,10),
					new SqlParameter("@ext_mthd", SqlDbType.NVarChar,50),
					new SqlParameter("@location", SqlDbType.NVarChar,50),
					new SqlParameter("@DOC_NAME0", SqlDbType.NVarChar,50),
					new SqlParameter("@test_date", SqlDbType.DateTime),
					new SqlParameter("@check_by_name", SqlDbType.NVarChar,50),
					new SqlParameter("@check_date", SqlDbType.DateTime),
					new SqlParameter("@IsDel", SqlDbType.Bit,1)};
			parameters[0].Value = model.hospnum;
			parameters[1].Value = model.patname;
			parameters[2].Value = model.sex;
			parameters[3].Value = model.age;
			parameters[4].Value = model.age_month;
			parameters[5].Value = model.ext_mthd;
			parameters[6].Value = model.location;
			parameters[7].Value = model.DOC_NAME0;
			parameters[8].Value = model.test_date;
			parameters[9].Value = model.check_by_name;
			parameters[10].Value = model.check_date;
			parameters[11].Value = model.IsDel;

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
		public bool Update(RuRo.Model.NormalLisItems model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update NormalLisItems set ");
			strSql.Append("hospnum=@hospnum,");
			strSql.Append("patname=@patname,");
			strSql.Append("sex=@sex,");
			strSql.Append("age=@age,");
			strSql.Append("age_month=@age_month,");
			strSql.Append("ext_mthd=@ext_mthd,");
			strSql.Append("location=@location,");
			strSql.Append("DOC_NAME0=@DOC_NAME0,");
			strSql.Append("test_date=@test_date,");
			strSql.Append("check_by_name=@check_by_name,");
			strSql.Append("check_date=@check_date,");
			strSql.Append("IsDel=@IsDel");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@hospnum", SqlDbType.NVarChar,50),
					new SqlParameter("@patname", SqlDbType.NVarChar,50),
					new SqlParameter("@sex", SqlDbType.NChar,10),
					new SqlParameter("@age", SqlDbType.NChar,10),
					new SqlParameter("@age_month", SqlDbType.NChar,10),
					new SqlParameter("@ext_mthd", SqlDbType.NVarChar,50),
					new SqlParameter("@location", SqlDbType.NVarChar,50),
					new SqlParameter("@DOC_NAME0", SqlDbType.NVarChar,50),
					new SqlParameter("@test_date", SqlDbType.DateTime),
					new SqlParameter("@check_by_name", SqlDbType.NVarChar,50),
					new SqlParameter("@check_date", SqlDbType.DateTime),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.hospnum;
			parameters[1].Value = model.patname;
			parameters[2].Value = model.sex;
			parameters[3].Value = model.age;
			parameters[4].Value = model.age_month;
			parameters[5].Value = model.ext_mthd;
			parameters[6].Value = model.location;
			parameters[7].Value = model.DOC_NAME0;
			parameters[8].Value = model.test_date;
			parameters[9].Value = model.check_by_name;
			parameters[10].Value = model.check_date;
			parameters[11].Value = model.IsDel;
			parameters[12].Value = model.Id;

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
			strSql.Append("delete from NormalLisItems ");
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
			strSql.Append("delete from NormalLisItems ");
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
		public RuRo.Model.NormalLisItems GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,hospnum,patname,sex,age,age_month,ext_mthd,location,DOC_NAME0,test_date,check_by_name,check_date,IsDel from NormalLisItems ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

            RuRo.Model.NormalLisItems model = new RuRo.Model.NormalLisItems();
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
		public RuRo.Model.NormalLisItems DataRowToModel(DataRow row)
		{
            RuRo.Model.NormalLisItems model = new RuRo.Model.NormalLisItems();
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
				if(row["sex"]!=null)
				{
					model.sex=row["sex"].ToString();
				}
				if(row["age"]!=null)
				{
					model.age=row["age"].ToString();
				}
				if(row["age_month"]!=null)
				{
					model.age_month=row["age_month"].ToString();
				}
				if(row["ext_mthd"]!=null)
				{
					model.ext_mthd=row["ext_mthd"].ToString();
				}
				if(row["location"]!=null)
				{
					model.location=row["location"].ToString();
				}
				if(row["DOC_NAME0"]!=null)
				{
					model.DOC_NAME0=row["DOC_NAME0"].ToString();
				}
				if(row["test_date"]!=null && row["test_date"].ToString()!="")
				{
					model.test_date=DateTime.Parse(row["test_date"].ToString());
				}
				if(row["check_by_name"]!=null)
				{
					model.check_by_name=row["check_by_name"].ToString();
				}
				if(row["check_date"]!=null && row["check_date"].ToString()!="")
				{
					model.check_date=DateTime.Parse(row["check_date"].ToString());
				}
				if(row["IsDel"]!=null && row["IsDel"].ToString()!="")
				{
					if((row["IsDel"].ToString()=="1")||(row["IsDel"].ToString().ToLower()=="true"))
					{
						model.IsDel=true;
					}
					else
					{
						model.IsDel=false;
					}
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
			strSql.Append("select Id,hospnum,patname,sex,age,age_month,ext_mthd,location,DOC_NAME0,test_date,check_by_name,check_date,IsDel ");
			strSql.Append(" FROM NormalLisItems ");
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
			strSql.Append(" Id,hospnum,patname,sex,age,age_month,ext_mthd,location,DOC_NAME0,test_date,check_by_name,check_date,IsDel ");
			strSql.Append(" FROM NormalLisItems ");
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
			strSql.Append("select count(1) FROM NormalLisItems ");
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
			strSql.Append(")AS Row, T.*  from NormalLisItems T ");
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
			parameters[0].Value = "NormalLisItems";
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


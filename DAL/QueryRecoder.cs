using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace RuRo.DAL
{
	/// <summary>
	/// 数据访问类:QueryRecoder
	/// </summary>
	public partial class QueryRecoder
	{
		public QueryRecoder()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "QueryRecoder"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from QueryRecoder");
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
		public int Add(RuRo.Model.QueryRecoder model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QueryRecoder(");
			strSql.Append("Uname,AddDate,LastQueryDate,Code,CodeType,QueryType,QueryResult,IsDel)");
			strSql.Append(" values (");
			strSql.Append("@Uname,@AddDate,@LastQueryDate,@Code,@CodeType,@QueryType,@QueryResult,@IsDel)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Uname", SqlDbType.NVarChar,50),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@LastQueryDate", SqlDbType.DateTime),
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@CodeType", SqlDbType.NVarChar,50),
					new SqlParameter("@QueryType", SqlDbType.NVarChar,50),
					new SqlParameter("@QueryResult", SqlDbType.NVarChar),
					new SqlParameter("@IsDel", SqlDbType.Bit,1)};
			parameters[0].Value = model.Uname;
			parameters[1].Value = model.AddDate;
			parameters[2].Value = model.LastQueryDate;
			parameters[3].Value = model.Code;
			parameters[4].Value = model.CodeType;
			parameters[5].Value = model.QueryType;
			parameters[6].Value = model.QueryResult;
			parameters[7].Value = model.IsDel;

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
		public bool Update(RuRo.Model.QueryRecoder model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QueryRecoder set ");
			strSql.Append("Uname=@Uname,");
			strSql.Append("AddDate=@AddDate,");
			strSql.Append("LastQueryDate=@LastQueryDate,");
			strSql.Append("Code=@Code,");
			strSql.Append("CodeType=@CodeType,");
			strSql.Append("QueryType=@QueryType,");
			strSql.Append("QueryResult=@QueryResult,");
			strSql.Append("IsDel=@IsDel");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Uname", SqlDbType.NVarChar,50),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@LastQueryDate", SqlDbType.DateTime),
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@CodeType", SqlDbType.NVarChar,50),
					new SqlParameter("@QueryType", SqlDbType.NVarChar,50),
					new SqlParameter("@QueryResult", SqlDbType.NVarChar),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Uname;
			parameters[1].Value = model.AddDate;
			parameters[2].Value = model.LastQueryDate;
			parameters[3].Value = model.Code;
			parameters[4].Value = model.CodeType;
			parameters[5].Value = model.QueryType;
			parameters[6].Value = model.QueryResult;
			parameters[7].Value = model.IsDel;
			parameters[8].Value = model.Id;

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
			strSql.Append("delete from QueryRecoder ");
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
			strSql.Append("delete from QueryRecoder ");
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
		public RuRo.Model.QueryRecoder GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Uname,AddDate,LastQueryDate,Code,CodeType,QueryType,QueryResult,IsDel from QueryRecoder ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			RuRo.Model.QueryRecoder model=new RuRo.Model.QueryRecoder();
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
		public RuRo.Model.QueryRecoder DataRowToModel(DataRow row)
		{
			RuRo.Model.QueryRecoder model=new RuRo.Model.QueryRecoder();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["Uname"]!=null)
				{
					model.Uname=row["Uname"].ToString();
				}
				if(row["AddDate"]!=null && row["AddDate"].ToString()!="")
				{
					model.AddDate=DateTime.Parse(row["AddDate"].ToString());
				}
				if(row["LastQueryDate"]!=null && row["LastQueryDate"].ToString()!="")
				{
					model.LastQueryDate=DateTime.Parse(row["LastQueryDate"].ToString());
				}
				if(row["Code"]!=null)
				{
					model.Code=row["Code"].ToString();
				}
				if(row["CodeType"]!=null)
				{
					model.CodeType=row["CodeType"].ToString();
				}
				if(row["QueryType"]!=null)
				{
					model.QueryType=row["QueryType"].ToString();
				}
				if(row["QueryResult"]!=null)
				{
					model.QueryResult=row["QueryResult"].ToString();
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
			strSql.Append("select Id,Uname,AddDate,LastQueryDate,Code,CodeType,QueryType,QueryResult,IsDel ");
			strSql.Append(" FROM QueryRecoder ");
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
			strSql.Append(" Id,Uname,AddDate,LastQueryDate,Code,CodeType,QueryType,QueryResult,IsDel ");
			strSql.Append(" FROM QueryRecoder ");
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
			strSql.Append("select count(1) FROM QueryRecoder ");
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
			strSql.Append(")AS Row, T.*  from QueryRecoder T ");
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
			parameters[0].Value = "QueryRecoder";
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
        /// <summary>
        /// 读取分页的查询数据
        /// </summary>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="where"></param>
        /// <param name="strorder"></param>
        /// <returns></returns>
        public DataSet GetQueryRecoderTrue(int size,int count,string where,string strorder)
        {
            count = count - 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("select top "+size+" *");
            sb.Append("from (");
            sb.Append("select row_number() over(order by id) as rownumber,* from QueryRecoder) A ");
            sb.Append("where rownumber >" + size * count);
            sb.Append(" and "+where);
            sb.Append("order by " + strorder);
            string sqlstr = sb.ToString();
            return DbHelperSQL.Query(sqlstr);
        }
        /// <summary>
        /// 查询倒数第二条数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetLastSecondData() 
        {
            string sqlstr = "SELECT * FROM QueryRecoder a WHERE(SELECT COUNT(Id) FROM QueryRecoder WHERE a.Id < QueryRecoder.Id) = 1";
            return DbHelperSQL.Query(sqlstr);
        }
        public DataSet GetReciprocalFirstData() 
        {
            string sqlstr = "SELECT * FROM QueryRecoder a WHERE(SELECT COUNT(Id) FROM QueryRecoder WHERE a.Id < QueryRecoder.Id) = 0";
            return DbHelperSQL.Query(sqlstr);
        }

        public int UpdataQueryRecoderIsDel(string uname,string adddate,string code,string queryType)
        {
            string sqlstr = "UPDATE QueryRecoder SET IsDel=" + true + " WHERE Uname='" + uname + "' AND AddDate='" + adddate + "' AND Code='" + code + "' AND QueryType='" + queryType + "'";
            return DbHelperSQL.ExecuteSql(sqlstr);
        }
		#endregion  ExtensionMethod
	}
}


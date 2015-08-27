using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace RuRo.DAL
{
	/// <summary>
	/// 数据访问类:PatientDiagnose
	/// </summary>
	public partial class PatientDiagnose
	{
		public PatientDiagnose()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "PatientDiagnose"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PatientDiagnose");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(RuRo.Model.PatientDiagnose model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PatientDiagnose(");
			strSql.Append("Cardno,Csrq00,PatientName,Sex,Brithday,CardId,Tel,RegisterNo,Icd,Diagnose,Type,Flag,DiagnoseDate,IsDel)");
			strSql.Append(" values (");
			strSql.Append("@Cardno,@Csrq00,@PatientName,@Sex,@Brithday,@CardId,@Tel,@RegisterNo,@Icd,@Diagnose,@Type,@Flag,@DiagnoseDate,@IsDel)");
			SqlParameter[] parameters = {
					new SqlParameter("@Cardno", SqlDbType.NVarChar,50),
					new SqlParameter("@Csrq00", SqlDbType.NVarChar,50),
					new SqlParameter("@PatientName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NChar,10),
					new SqlParameter("@Brithday", SqlDbType.DateTime),
					new SqlParameter("@CardId", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@RegisterNo", SqlDbType.NVarChar,50),
					new SqlParameter("@Icd", SqlDbType.NVarChar,50),
					new SqlParameter("@Diagnose", SqlDbType.NVarChar,50),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Flag", SqlDbType.NVarChar,50),
					new SqlParameter("@DiagnoseDate", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDel", SqlDbType.Bit,1)};
			parameters[0].Value = model.Cardno;
			parameters[1].Value = model.Csrq00;
			parameters[2].Value = model.PatientName;
			parameters[3].Value = model.Sex;
			parameters[4].Value = model.Brithday;
			parameters[5].Value = model.CardId;
			parameters[6].Value = model.Tel;
			parameters[7].Value = model.RegisterNo;
			parameters[8].Value = model.Icd;
			parameters[9].Value = model.Diagnose;
			parameters[10].Value = model.Type;
			parameters[11].Value = model.Flag;
			parameters[12].Value = model.DiagnoseDate;
			parameters[13].Value = model.IsDel;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(RuRo.Model.PatientDiagnose model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PatientDiagnose set ");
			strSql.Append("Cardno=@Cardno,");
			strSql.Append("Csrq00=@Csrq00,");
			strSql.Append("PatientName=@PatientName,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("Brithday=@Brithday,");
			strSql.Append("CardId=@CardId,");
			strSql.Append("Tel=@Tel,");
			strSql.Append("RegisterNo=@RegisterNo,");
			strSql.Append("Icd=@Icd,");
			strSql.Append("Diagnose=@Diagnose,");
			strSql.Append("Type=@Type,");
			strSql.Append("Flag=@Flag,");
			strSql.Append("DiagnoseDate=@DiagnoseDate,");
			strSql.Append("IsDel=@IsDel");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Cardno", SqlDbType.NVarChar,50),
					new SqlParameter("@Csrq00", SqlDbType.NVarChar,50),
					new SqlParameter("@PatientName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NChar,10),
					new SqlParameter("@Brithday", SqlDbType.DateTime),
					new SqlParameter("@CardId", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@RegisterNo", SqlDbType.NVarChar,50),
					new SqlParameter("@Icd", SqlDbType.NVarChar,50),
					new SqlParameter("@Diagnose", SqlDbType.NVarChar,50),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Flag", SqlDbType.NVarChar,50),
					new SqlParameter("@DiagnoseDate", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.Cardno;
			parameters[1].Value = model.Csrq00;
			parameters[2].Value = model.PatientName;
			parameters[3].Value = model.Sex;
			parameters[4].Value = model.Brithday;
			parameters[5].Value = model.CardId;
			parameters[6].Value = model.Tel;
			parameters[7].Value = model.RegisterNo;
			parameters[8].Value = model.Icd;
			parameters[9].Value = model.Diagnose;
			parameters[10].Value = model.Type;
			parameters[11].Value = model.Flag;
			parameters[12].Value = model.DiagnoseDate;
			parameters[13].Value = model.IsDel;
			parameters[14].Value = model.id;

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
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PatientDiagnose ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = id;

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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PatientDiagnose ");
			strSql.Append(" where id in ("+idlist + ")  ");
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
		public RuRo.Model.PatientDiagnose GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,Cardno,Csrq00,PatientName,Sex,Brithday,CardId,Tel,RegisterNo,Icd,Diagnose,Type,Flag,DiagnoseDate,IsDel from PatientDiagnose ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = id;

			RuRo.Model.PatientDiagnose model=new RuRo.Model.PatientDiagnose();
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
		public RuRo.Model.PatientDiagnose DataRowToModel(DataRow row)
		{
			RuRo.Model.PatientDiagnose model=new RuRo.Model.PatientDiagnose();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["Cardno"]!=null)
				{
					model.Cardno=row["Cardno"].ToString();
				}
				if(row["Csrq00"]!=null)
				{
					model.Csrq00=row["Csrq00"].ToString();
				}
				if(row["PatientName"]!=null)
				{
					model.PatientName=row["PatientName"].ToString();
				}
				if(row["Sex"]!=null)
				{
					model.Sex=row["Sex"].ToString();
				}
				if(row["Brithday"]!=null && row["Brithday"].ToString()!="")
				{
					model.Brithday=DateTime.Parse(row["Brithday"].ToString());
				}
				if(row["CardId"]!=null)
				{
					model.CardId=row["CardId"].ToString();
				}
				if(row["Tel"]!=null)
				{
					model.Tel=row["Tel"].ToString();
				}
				if(row["RegisterNo"]!=null)
				{
					model.RegisterNo=row["RegisterNo"].ToString();
				}
				if(row["Icd"]!=null)
				{
					model.Icd=row["Icd"].ToString();
				}
				if(row["Diagnose"]!=null)
				{
					model.Diagnose=row["Diagnose"].ToString();
				}
				if(row["Type"]!=null)
				{
					model.Type=row["Type"].ToString();
				}
				if(row["Flag"]!=null)
				{
					model.Flag=row["Flag"].ToString();
				}
				if(row["DiagnoseDate"]!=null)
				{
					model.DiagnoseDate=row["DiagnoseDate"].ToString();
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
			strSql.Append("select id,Cardno,Csrq00,PatientName,Sex,Brithday,CardId,Tel,RegisterNo,Icd,Diagnose,Type,Flag,DiagnoseDate,IsDel ");
			strSql.Append(" FROM PatientDiagnose ");
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
			strSql.Append(" id,Cardno,Csrq00,PatientName,Sex,Brithday,CardId,Tel,RegisterNo,Icd,Diagnose,Type,Flag,DiagnoseDate,IsDel ");
			strSql.Append(" FROM PatientDiagnose ");
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
			strSql.Append("select count(1) FROM PatientDiagnose ");
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
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from PatientDiagnose T ");
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
			parameters[0].Value = "PatientDiagnose";
			parameters[1].Value = "id";
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


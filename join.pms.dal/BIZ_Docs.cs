using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.dal
{
    public class BIZ_Docs
    {
        #region 字段

        public string CommID;
        public string BizID;
        public string PersonID;
        public string DocsType;
        public string DocsPath;
        public string SourceName;
        public string DocsName;
        public string OprateDate;
        public string SysNo;
        public string IsInnerArea;
        // DocsType,DocsPath,SourceName,DocsName,OprateDate,IsInnerArea SvrsWebPath
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];
        private string m_SvrsPath = System.Configuration.ConfigurationManager.AppSettings["SvrsWebPath"];

        #endregion

        public string[] aryAnalys;

        #region BIZ_Docs相关

        /// <summary>
        /// 向表插入数据
        /// </summary>
        /// <param name="personID">返回ID</param>
        /// <returns></returns>
        public string Insert()
        {
            #region SQL参数
            SqlParameter[] paras = new SqlParameter[9];

            paras[0] = new SqlParameter("@BizID", SqlDbType.VarChar, 4);
            paras[0].Value = BizID;
            paras[1] = new SqlParameter("@PersonID", SqlDbType.VarChar, 4);
            paras[1].Value = PersonID;
            paras[2] = new SqlParameter("@DocsType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(DocsType)) { DocsType = ""; }
            paras[2].Value = DocsType;
            paras[3] = new SqlParameter("@DocsPath", SqlDbType.VarChar, 500);
            if (String.IsNullOrEmpty(DocsPath)) { DocsPath = ""; }
            paras[3].Value = DocsPath;
            paras[4] = new SqlParameter("@SourceName", SqlDbType.VarChar, 100);
            if (String.IsNullOrEmpty(SourceName)) { SourceName = ""; }
            paras[4].Value = SourceName;
            paras[5] = new SqlParameter("@DocsName", SqlDbType.VarChar, 100);
            if (String.IsNullOrEmpty(DocsName)) { DocsName = ""; }
            paras[5].Value = DocsName;
            paras[6] = new SqlParameter("@OprateDate", SqlDbType.SmallDateTime, 4);
            paras[6].Value = DateTime.Now;
            paras[7] = new SqlParameter("@SysNo", SqlDbType.TinyInt, 1);
            paras[7].Value = SysNo;
            paras[8] = new SqlParameter("@IsInnerArea", SqlDbType.TinyInt, 1);
            paras[8].Value = IsInnerArea;

            #endregion
            #region SQL
            string Fields = "BizID,PersonID,DocsType,DocsPath,SourceName,DocsName,OprateDate,SysNo,IsInnerArea";
            aryAnalys = Fields.Split(',');
            string sql = GetInsertSQL(aryAnalys);

            #endregion
            try
            {
                return DbHelperSQL.GetSingle(sql, paras).ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 获取查询的字段
        /// </summary>
        /// <param name="aAnaly"></param>
        /// <returns></returns>
        private string GetSelectSQL(string[] aAnaly)
        {
            string returnVal = string.Empty;
            string returnA = "";
            for (int i = 0; i < aAnaly.Length; i++)
            {
                if (aAnaly[i] != "null")
                {
                    returnA += aAnaly[i] + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);
            returnVal = returnA;

            return returnVal;
        }

        /// <summary>
        /// 获取新增的字段
        /// </summary>
        /// <param name="aAnaly"></param>
        /// <returns></returns>
        private string GetInsertSQL(string[] aAnaly)
        {
            string returnVal = string.Empty;
            string returnA = "";
            string returnB = "";
            string[] values ={ BizID, PersonID, DocsType, DocsPath, SourceName, DocsName, OprateDate, SysNo, IsInnerArea };
            for (int i = 0; i < aAnaly.Length; i++)
            {
                if (!string.IsNullOrEmpty(values[i]) && values[i] != null)
                    if (aAnaly[i] != "null")
                    {
                        returnA += aAnaly[i] + ",";
                        returnB += "@" + aAnaly[i].Trim() + ",";
                    }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);
            if (returnB != "") returnB = returnB.Substring(0, returnB.Length - 1);

            returnVal = "INSERT INTO  [BIZ_Docs](" + returnA + ") Values(" + returnB + ") SELECT SCOPE_IDENTITY();";

            return returnVal;
        }

        /// <summary>
        /// 获取更新的字段信息
        /// </summary>
        /// <param name="aAnaly"></param>
        /// <returns></returns>
        private string GetUpdateSQL(string[] aAnaly)
        {
            string returnVal = string.Empty;
            string returnA = "";
            for (int i = 0; i < aAnaly.Length; i++)
            {
                if (aAnaly[i] != "null")
                {
                    returnA += aAnaly[i] + "=@" + aAnaly[i].Trim() + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);

            returnVal = "Update BIZ_Docs Set " + returnA + " Where 1=1 and CommID  = @CommID";
            return returnVal;
        }

        /// <summary>
        /// 更新表中的指定数据
        /// </summary>
        /// <param name="funcNo">功能编号</param>
        /// <returns></returns>
        public int Update()
        {
            #region SQL参数
            SqlParameter[] paras = new SqlParameter[10];
            paras[0] = new SqlParameter("@CommID", SqlDbType.VarChar, 4);
            paras[0].Value = CommID;
            paras[1] = new SqlParameter("@BizID", SqlDbType.VarChar, 4);
            if (String.IsNullOrEmpty(BizID)) { BizID = ""; }
            paras[1].Value = BizID;
            paras[2] = new SqlParameter("@PersonID", SqlDbType.VarChar, 4);
            if (String.IsNullOrEmpty(PersonID)) { PersonID = ""; }
            paras[2].Value = PersonID;
            paras[3] = new SqlParameter("@DocsType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(DocsType)) { DocsType = ""; }
            paras[3].Value = DocsType;
            paras[4] = new SqlParameter("@DocsPath", SqlDbType.VarChar, 500);
            if (String.IsNullOrEmpty(DocsPath)) { DocsPath = ""; }
            paras[4].Value = DocsPath;
            paras[5] = new SqlParameter("@SourceName", SqlDbType.VarChar, 100);
            if (String.IsNullOrEmpty(SourceName)) { SourceName = ""; }
            paras[5].Value = SourceName;
            paras[6] = new SqlParameter("@DocsName", SqlDbType.VarChar, 100);
            if (String.IsNullOrEmpty(DocsName)) { DocsName = ""; }
            paras[6].Value = DocsName;
            paras[7] = new SqlParameter("@OprateDate", SqlDbType.SmallDateTime, 4);
            paras[7].Value = DateTime.Now;
            paras[8] = new SqlParameter("@SysNo", SqlDbType.TinyInt, 1);
            paras[8].Value = int.Parse(SysNo);
            paras[9] = new SqlParameter("@IsInnerArea", SqlDbType.TinyInt, 1);
            paras[9].Value = int.Parse(IsInnerArea);

            #endregion
            #region SQL

            string sql = GetUpdateSQL(aryAnalys);
            #endregion
            try
            {
                return SqlHelper.ExecuteNonQuery(DbHelperSQL.connectionString, CommandType.Text, sql, paras);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 信息是否需要更新 update 并更新
        /// </summary>
        /// <returns></returns>
        public string IsBizDocsNeedUpdate(string commID)
        {
            CommID = commID;
            int n = 0;
            string retrunVal = string.Empty;
            if (!string.IsNullOrEmpty(commID))
            {

                string fileds = "BizID, PersonID, DocsType, DocsPath, SourceName, DocsName, OprateDate, SysNo, IsInnerArea";
                string[] arrFileds = fileds.Split(',');
                string[] values ={ BizID, PersonID, DocsType, DocsPath, SourceName, DocsName, OprateDate, SysNo, IsInnerArea };
                string sqlParams = "SELECT " + fileds + ",CommID FROM BIZ_Docs WHERE CommID IN (" + commID + ")";
                string sdrVal = string.Empty;
                SqlDataReader sdr = null;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            string updateFileds = string.Empty;
                            for (int i = 0; i < arrFileds.Length; i++)
                            {
                                CommID = sdr["CommID"].ToString();
                                sdrVal = sdr[i].ToString();
                                if (i == 7)
                                {
                                    sdrVal = BIZ_Common.FormatString(sdrVal, "2");
                                    values[i] = BIZ_Common.FormatString(values[i], "2");
                                }
                                if (!string.IsNullOrEmpty(values[i]) && (sdrVal != values[i]))
                                { updateFileds += arrFileds[i] + ","; }
                                if (i == 8) { SysNo = sdr["SysNo"].ToString(); }
                                if (i == 9) { IsInnerArea = sdr["IsInnerArea"].ToString(); }
                            }
                            if (updateFileds != "")
                            {
                                updateFileds = updateFileds.Substring(0, updateFileds.Length - 1);
                                aryAnalys = updateFileds.Split(',');
                                n += Update();
                            }
                        }
                    }
                    sdr.Close();
                }
                catch { if (sdr != null) sdr.Close(); }

            }
            retrunVal = n.ToString();
            return retrunVal;
        }
        #endregion

        #region 获取业务附件材料

        public string GetBizDocs(string bizID)
        {
            string navUrl = "", BizComments = "", DocsType = string.Empty;
            string docPath = "", newFile = string.Empty;
            string sqlParams = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                BizComments = CommPage.GetSingleVal("SELECT Comments FROM BIZ_Contents WHERE BizID=" + bizID);
                if (string.IsNullOrEmpty(BizComments)) BizComments = "……";

                sqlParams = "SELECT DocsType,DocsPath,SourceName,DocsName,OprateDate,IsInnerArea FROM BIZ_Docs WHERE BizID=" + bizID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                s.Append("<table width=\"880\" border=\"0\"  cellpadding=\"3\" cellspacing=\"1\">");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        docPath = sdr["DocsPath"].ToString();
                        DocsType = sdr["DocsType"].ToString();

                        newFile = docPath.Insert(docPath.LastIndexOf("/") + 1, "microBig-");
                        SetMicroImages.GetMicroImage(m_SvrsPath + docPath, m_SvrsPath + newFile, "W", 1000, 600);
                        navUrl = m_SvrsUrl + newFile;

                        s.Append("<tr class=\"zhengwen\">");
                        s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">材料附件：</td>");
                        //s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" target=\"_blank\">" + sdr["DocsName"].ToString() + "</a></td>");
                        if (DocsType == ".jpg" || DocsType == ".gif" || DocsType == ".png" || DocsType == ".bmp")
                        {
                            s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" rel=\"lightbox[zj]\" title=\"证照：" + sdr["DocsName"].ToString() + "\">" + sdr["DocsName"].ToString() + "(" + DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy/MM/dd") + ")</a></td>");
                        }
                        else
                        {
                            s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" target=\"_blank\">" + sdr["DocsName"].ToString() + "</a></td>");
                        }
                        s.Append("</tr>");
                    }
                    sdr.Close();
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">现场提交：</td>");
                    s.Append("<td align=\"left\">" + BizComments + "</td>");
                    s.Append("</tr>");
                }
                else
                {
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">材料附件：</td>");
                    s.Append("<td align=\"left\">当前业务申请事项没有提交任何电子证照材料……</td>");
                    s.Append("</tr>");
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">现场提交：</td>");
                    s.Append("<td align=\"left\">" + BizComments + "</td>");
                    s.Append("</tr>");
                }
                s.Append("</table>");
            }
            catch
            {
                if (sdr != null) sdr.Close();
                s.Append("<br/>操作失败：获取证明材料时出现错误！<br/>"); 
                //(" <script>alert('操作失败：获取证明材料时出现错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
            }

            return s.ToString();
        }

        public string GetBizDocsForView(string bizID)
        {
            string navUrl = "", BizComments = "", DocsType = string.Empty;
            string docPath = "", newFile = string.Empty;
            string sqlParams = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                BizComments = CommPage.GetSingleVal("SELECT Comments FROM BIZ_Contents WHERE BizID=" + bizID);
                if (string.IsNullOrEmpty(BizComments)) BizComments = "……";

                sqlParams = "SELECT DocsType,DocsPath,SourceName,DocsName,OprateDate,IsInnerArea FROM BIZ_Docs WHERE BizID=" + bizID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                s.Append("<br/><table border=\"0\"  cellpadding=\"3\" cellspacing=\"1\">");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        docPath = sdr["DocsPath"].ToString();
                        DocsType = sdr["DocsType"].ToString();

                        newFile = docPath.Insert(docPath.LastIndexOf("/") + 1, "microBig-");
                        SetMicroImages.GetMicroImage(m_SvrsPath + docPath, m_SvrsPath + newFile, "W", 1000, 600);
                        navUrl = m_SvrsUrl + newFile;

                        s.Append("<tr class=\"zhengwen\">");
                        s.Append("<td   height=\"25\" align=\"right\" class=\"zhengwenjiacu\">材料附件：</td>");
                        //s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" target=\"_blank\">" + sdr["DocsName"].ToString() + "</a></td>");
                        if (DocsType == ".jpg" || DocsType == ".gif" || DocsType == ".png" || DocsType == ".bmp")
                        {
                            s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" rel=\"lightbox[zj]\" title=\"证照：" + sdr["DocsName"].ToString() + "\">" + sdr["DocsName"].ToString() + "(" + DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy/MM/dd") + ")</a></td>");
                        }
                        else
                        {
                            s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" target=\"_blank\">" + sdr["DocsName"].ToString() + "</a></td>");
                        }
                        s.Append("</tr>");
                    }
                    sdr.Close();
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">现场提交：</td>");
                    s.Append("<td align=\"left\">" + BizComments + "</td>");
                    s.Append("</tr>");
                }
                else
                {
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">材料附件：</td>");
                    s.Append("<td align=\"left\">当前业务申请事项没有提交任何电子证照材料……</td>");
                    s.Append("</tr>");
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td height=\"25\" align=\"right\" class=\"zhengwenjiacu\">现场提交：</td>");
                    s.Append("<td align=\"left\">" + BizComments + "</td>");
                    s.Append("</tr>");
                }
                s.Append("</table>");
            }
            catch
            {
                if (sdr != null) sdr.Close();
                s.Append("<br/>操作失败：获取证明材料时出现错误！<br/>");
                //(" <script>alert('操作失败：获取证明材料时出现错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
            }

            return s.ToString();
        }

        #endregion 
    }
}

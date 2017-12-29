using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Data.SqlClient;
using System.Text;

namespace join.pms.web.userctrl
{
    public partial class ucDocsList : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataList();
        }

        private void BindDataList()
        {



           string  m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            
            string    m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);


          string  m_ObjID = PageValidate.GetTrim(Request.QueryString["k"]);

          if (string.IsNullOrEmpty(m_ObjID))
          {
             m_ObjID= PageValidate.GetFilterSQL(Request.QueryString["RID"]);
          }

           string     m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");

           string sqlParams = "";
           string DocsPath = string.Empty;
           string SourceName = string.Empty, DocsName = "";
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;

            try
            {
                sqlParams = "SELECT DocsPath,SourceName,DocsName  FROM NHS_Docs WHERE funcCode = '" + m_FuncCode + "' AND NHSID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                
               
                
                sHtml.Append("<fieldset style=\"margin:20px 10px 10px 0; width:850px\">");
                    sHtml.Append("<legend>上传附件列表</legend>");
                    sHtml.Append("<table width=\"850\" border=\"0\" cellspacing=\"5\" cellpadding=\"0\"> ");                    
                                      
                    
                
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td>");
                    while (sdr.Read())
                    {
                        DocsPath = sdr["DocsPath"].ToString();
                        SourceName = sdr["SourceName"].ToString();
                        DocsName = sdr["DocsName"].ToString();

                        sHtml.Append("<div style=\"padding:2px;float:left;width:200px;height:130px;overflow:hidden;\">");
                        sHtml.Append("<div>"+DocsName+"：</div>");
                        sHtml.Append("<div style=\"padding:1px;\"><a href='" + System.Configuration.ConfigurationManager.AppSettings["SvrUrl"] + DocsPath + "' target=_blank><img src=" + System.Configuration.ConfigurationManager.AppSettings["SvrUrl"] + DocsPath + " width=99% /></a></div>");
                      sHtml.Append("</div>");  
                        
                    }
                    sHtml.Append("<div style=\"clear:both\"></div></td></tr>"); 
                }
                else
                {
                    sHtml.Append("<tr><td colspan=\"3\">没有附件</td></tr>");
                }
                sdr.Close();
                sHtml.Append("</table>");
                sHtml.Append("</fieldset>");

            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
           this.litList.Text=  sHtml.ToString();
        }
        
    }
}
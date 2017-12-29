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

using System.IO;
using System.Globalization;

using UNV.Comm.Web;
using UNV.Comm.DataBase;

namespace join.pms.web.userctrl
{
    public partial class SetCmsUploadCompts : System.Web.UI.Page
    {
        private string m_SqlParams;
        private string m_UserID;
        private string m_DocsName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            this.LabelMsg.Text = "�ϴ���ʾ��Ϣ����";
        }

        /// <summary>
        /// �����֤
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["AREWEB_OC_USERID"] != null && !String.IsNullOrEmpty(Session["AREWEB_OC_USERID"].ToString())) { returnVa = true; m_UserID = Session["AREWEB_OC_USERID"].ToString(); }
            }

            if (!returnVa)
            {
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        protected void ButUploadFile_Click(object sender, EventArgs e)
        {
            iSvrs.DALSvrs Svrs = new join.pms.web.iSvrs.DALSvrs();
            iSvrs.ClientInfo CI = new join.pms.web.iSvrs.ClientInfo();
            ///
            string docsType, fileContentType = string.Empty;
            string sourceFile = string.Empty; // ԭʼ�ļ���
            string saveFile = string.Empty; // ������ļ�
            long docsSize = 0;

            string siteimgWidth = System.Configuration.ConfigurationManager.AppSettings["SiteImgWidth"];
            string targetFile = StringProcess.GetCurDateTimeStr(16); // ���ݿⱣ����ļ���
            string fullPath = "/Files/" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "/" + DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "/";
            string dispMsg = string.Empty;
            string sParams = "";

            //�ͻ����ϴ����ļ�
            System.Web.HttpFileCollection _file = System.Web.HttpContext.Current.Request.Files;
            if (_file.Count > 0)
            {
                try
                {
                    docsSize = _file[0].ContentLength;
                    fileContentType = _file[0].ContentType;
                    sourceFile = _file[0].FileName; // "C:\\Users\\ysl\\Pictures\\1-130922034538-lp.jpg"
                    docsType = System.IO.Path.GetExtension(sourceFile); //��չ��

                    if (fileContentType == "image/pjpeg" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "image/png" || fileContentType == "application/x-jpg" || fileContentType == "image/x-png")
                    {
                        long fileSize = docsSize / 1024;
                        if (fileSize < 10240)
                        {
                            CI.ClientID = "1";
                            CI.ClientCode = "c6RFtyTTF4vPnMWJUvuIWlMQZAKD1Ysl";
                            //����ƬתΪbyte
                            //FileStream fs = File.OpenRead("�����ļ�·��");
                            //��ȡ�ļ���
                            System.IO.Stream stream = _file[0].InputStream;
                            byte[] imgByte = ConvertStreamToByteBuffer(stream);
                            //fs.Close();
                            //fs = null;
                            //�ϴ���Ƭ
                            saveFile = fullPath + targetFile + docsType;
                            sParams = "FileName=" + saveFile + ";UID=1;IDC=QJFuGC7vZcdxusUSkQWixNXBUp8jPW0EyheiFyaWhmwe4Zjj0PtwgQ3dMez0dx5d;";
                            Svrs.UpLoadFiles(imgByte, sParams, CI);

                            //���浽���ݿ�
                            SaveFileToDB(docsType, saveFile, sourceFile);

                            this.txtSourceFile.Value = sourceFile;
                            this.txtSaveFile.Value = saveFile;
                            this.txtFileType.Value = docsType;

                            dispMsg = "<div id=\"oprateUpFiles\">�ļ�[ " + sourceFile + " ]�ϴ��ɹ���<br/><b>The file size:</b>" + docsSize.ToString() + "�ֽ�<br/>";
                        }
                        else
                        {
                            dispMsg = "����ʧ�ܣ�ͼƬ�ļ����󣬲��ܳ���10M����ʹ��ͼ���������������ϴ���" + docsSize.ToString();
                        }
                    }
                    else
                    {
                        dispMsg = "��ѡ��ͼƬ�ļ��ϴ���";
                    }
                }
                catch (Exception ex) { dispMsg = ex.Message; }
            }
            else
            {
                dispMsg = "<font color=red>��ѡ��Ҫ�ϴ��ĸ�����</font>";
            }

            this.LabelMsg.Text = dispMsg;
        }

        /// <summary>
        /// ���ϴ��ļ���Ϣ��¼���ݿ�,��������޸�
        /// </summary>
        /// <param name="docsType"></param>
        /// <param name="docsPath"></param>
        /// <param name="sourceName"></param>
        private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        {
            try
            {
                // SELECT [CommID], [CmsID], [DocsType], [DocsPath], [SourceName], [UserID], [OprateDate] FROM [NMG_TLS_KLQ_OC_DB].[dbo].[CMS_Docs]
                m_SqlParams = "INSERT INTO [CMS_Docs](DocsType,DocsPath,SourceName,UserID) ";
                m_SqlParams += "VALUES('" + docsType + "', '" + docsPath + "', '" + sourceName + "'," + m_UserID + ") ";
                m_SqlParams += "SELECT SCOPE_IDENTITY()"; // SQL2005:OUTPUT INSERTED.ID

                this.txtFileID.Value = DbHelperSQL.GetSingle(m_SqlParams).ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        private byte[] ConvertStreamToByteBuffer(System.IO.Stream S)
        {
            int i = -1;
            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
            byte[] ba = new byte[64 * 1024]; //byte[] ba = new byte[S.Length];
            while ((i = S.Read(ba, 0, ba.Length)) != 0)
            {
                tempStream.Write(ba, 0, i);
            }
            return tempStream.ToArray();
        }
    }
}

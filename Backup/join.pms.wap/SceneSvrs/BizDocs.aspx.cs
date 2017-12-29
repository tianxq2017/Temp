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
using System.Globalization;

using System.IO;
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;
namespace join.pms.wap.SceneSvrs
{
    public partial class BizDocs : UNV.Comm.Web.PageBase
    {
        private string m_UserID;

        private string m_BizID;
        protected string m_BizCode;
        private string m_BizName;

        private string m_SqlParams;

        protected SqlDataReader m_sdr;

        private string m_SvrUrl = ConfigurationManager.AppSettings["SvrUrl"];
        private string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            SetPageHeader("�ϴ�֤��");
            this.Uc_PageTop1.GetSysMenu("�ϴ�֤��");
            if (!IsPostBack)
            {
                GetBizCategoryLicense();
            }
        }

        #region ����ҳͷ��Ϣ������\��֤����\��֤�û���
        //����ҳͷ��Ϣ��������
        private void SetPageHeader(string objTitles)
        {
            try
            {
                this.Page.Header.Title = this.m_SiteName;
                base.AddMetaTag("keywords", Server.HtmlEncode(this.m_SiteName + "," + objTitles + "," + this.m_BizName + "," + this.m_SiteKeyWord));
                base.AddMetaTag(this.m_SiteName);
                base.AddMetaTag("description", Server.HtmlEncode(this.m_BizName + "," + m_SiteDescription));
                base.AddMetaTag("copyright", Server.HtmlEncode("��ҳ��Ȩ�� �������ǿƼ���չ���޹�˾ ���С�All Rights Reserved"));

            }
            catch
            {
                Server.Transfer("~/errors.aspx");
            }
        }
        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_BizCode = PageValidate.GetTrim(Request.QueryString["c"]);
            m_BizID = PageValidate.GetTrim(Request.QueryString["b"]);
            if (!string.IsNullOrEmpty(m_BizCode) && PageValidate.IsNumber(m_BizCode) && !string.IsNullOrEmpty(m_BizID) && PageValidate.IsNumber(m_BizID))
            { }
            else
            {
                Server.Transfer("~/errors.aspx");
            }
        }
        /// <summary>
        /// �����֤
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["UserID"].ToString())) { returnVa = true; m_UserID = Session["UserID"].ToString(); }
            }
            if (!returnVa)
            {
                Response.Write("<script language='javascript'>parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
        }

        #endregion

        #region ��ȡ����ҵ������֤��
        /// <summary>
        /// 
        /// </summary>
        private void GetBizCategoryLicense()
        {
            try
            {
                Biz_Categories bizCateg = new Biz_Categories();
                this.LiteralBizCategoryLicense.Text = bizCateg.GetBizCategoryLicenseWap(this.m_BizCode, this.m_BizID);

                this.txtRegAreaCodeA.Value = bizCateg.RegAreaCodeA;
                this.txtRegAreaCodeB.Value = bizCateg.RegAreaCodeB;
                if (bizCateg.IsInnerArea) { this.txtIsInnerArea.Value = "1"; }
                this.txtBizCNum.Value = bizCateg.BizCNum;
                this.txtBizGNum.Value = bizCateg.BizGNum;
                bizCateg = null;
            }
            catch { }
        }
        #endregion

        #region �ύ֤��
        /// <summary>
        /// �ύ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string strSql = string.Empty;
            string Comments = string.Empty;
            string incDocsID = string.Empty;
            string bizGNum = this.txtBizGNum.Value;
            string bizCNum = this.txtBizCNum.Value;

            string IsInnerArea = this.txtIsInnerArea.Value;
            string innerAreaPath = string.Empty;
            //

            #region �ϴ�

            iSvrs.DALSvrs Svrs = new join.pms.wap.iSvrs.DALSvrs();
            iSvrs.ClientInfo CI = new join.pms.wap.iSvrs.ClientInfo();

            string siteimgWidth = System.Configuration.ConfigurationManager.AppSettings["SiteImgWidth"];
            string targetFile = string.Empty; // ���ݿⱣ����ļ���
            string fullPath = "/Files/" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "/" + DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "/";
            string dispMsg = string.Empty;
            string sParams = "";

            //�ͻ����ϴ����ļ�
            System.Web.HttpFileCollection _file = System.Web.HttpContext.Current.Request.Files;
            string fileContentType = string.Empty;
            string[] DocsName = new string[_file.Count];// ��������
            string[] docsType = new string[_file.Count];// �ļ���׺��
            string[] sourceFile = new string[_file.Count]; // ԭʼ�ļ���
            string[] saveFile = new string[_file.Count]; // ������ļ� 
            long docsSize = 0;

            if (_file.Count > 0)
            {
                try
                {
                    for (int i = 0; i < _file.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(_file[i].FileName))
                        {
                            targetFile = StringProcess.GetCurDateTimeStr(16); // ���ݿⱣ����ļ���
                            docsSize = _file[i].ContentLength;
                            fileContentType = _file[i].ContentType;
                            DocsName[i] = HttpUtility.UrlDecode(Request["txtDocsName" + i]);
                            sourceFile[i] = _file[i].FileName; // "C:\\Users\\ysl\\Pictures\\1-130922034538-lp.jpg"
                            docsType[i] = System.IO.Path.GetExtension(sourceFile[i]); //��չ��

                            if (fileContentType == "application/octet-stream" || fileContentType == "image/pjpeg" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "image/png" || fileContentType == "application/x-jpg" || fileContentType == "image/x-png" || fileContentType == "application/x-bmp")
                            {
                                long fileSize = docsSize / 1024;
                                if (fileSize < 10240)
                                {
                                    CI.ClientID = "1";
                                    CI.ClientCode = "c6RFtyTTF4vPnMWJUvuIWlMQZAKD1Ysl";
                                    //����ƬתΪbyte
                                    //FileStream fs = File.OpenRead("�����ļ�·��");
                                    //��ȡ�ļ���
                                    System.IO.Stream stream = _file[i].InputStream;
                                    byte[] imgByte = ConvertStreamToByteBuffer(stream);
                                    //fs.Close();
                                    //fs = null;
                                    //�ϴ���Ƭ
                                    saveFile[i] = fullPath + targetFile + docsType[i];
                                    sParams = "FileName=" + saveFile[i] + ";UID=1;IDC=QJFuGC7vZcdxusUSkQWixNXBUp8jPW0EyheiFyaWhmwe4Zjj0PtwgQ3dMez0dx5d;";
                                    Svrs.UpLoadFiles(imgByte, sParams, CI);
                                    dispMsg = "<div id=\"oprateUpFiles\">�ļ�[ " + sourceFile + " ]�ϴ��ɹ���<br/><b>The file size:</b>" + docsSize.ToString() + "�ֽ�<br/>";
                                }
                                else
                                {
                                    strErr += "����ʧ�ܣ�" + DocsName[i] + "ͼƬ�ļ����󣬲��ܳ���10M����ʹ��ͼ���������������ϴ���" + docsSize.ToString();
                                }
                            }
                            else
                            {
                                strErr += "����ʧ�ܣ�" + DocsName[i] + "ͼƬ��ʽ���������ϴ���Χ�ڣ�";
                            }
                        }
                    }
                }
                catch (Exception ex) { dispMsg = ex.Message; }
            }
            #endregion

            //for (int i = 0; i < int.Parse(bizCNum); i++)
            //{
            //    if (Request["cbBiz" + i] != "on" && String.IsNullOrEmpty(saveFile[i])) 
            //    { 
            //        strErr += "���ϴ�" + HttpUtility.UrlDecode(Request["txtDocsName" + i]) + "֤�����ֳ��ύ��\\n";
            //    }
            //}
            //int n = int.Parse(bizGNum) - 1;
            //if (IsInnerArea == "1")
            //{
            //    innerAreaPath = saveFile[n];
            //    if (String.IsNullOrEmpty(innerAreaPath)) { strErr += "���ϴ�" + HttpUtility.UrlDecode(Request["txtDocsName" + n]) + "��\\n"; }
            //}

            //if (strErr != "")
            //{
            //    //GetBizCategoryLicense();
            //    MessageBox.Show(this, strErr);
            //    return;
            //}
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(int.Parse(bizGNum));
            try
            {
                for (int i = 0; i < int.Parse(bizGNum); i++)
                {
                    //if (String.IsNullOrEmpty(saveFile[i]) && Request["cbBiz" + i] == "on") 
                    //{ 
                    //    Comments += HttpUtility.UrlDecode(Request["txtDocsName" + i]) + "<br/>"; 
                    //}

                    if (i == (int.Parse(bizGNum) - 1) && IsInnerArea == "1" && !String.IsNullOrEmpty(saveFile[i]))
                    {
                        list.Add("INSERT INTO [BIZ_Docs](BizID,PersonID,DocsName,DocsType,DocsPath,SourceName)VALUES(" + m_BizID + "," + m_UserID + ",'" + DocsName[i] + "','" + docsType[i] + "','" + saveFile[i] + "','" + sourceFile[i] + "')");
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(saveFile[i])) { list.Add("INSERT INTO [BIZ_Docs](BizID,PersonID,IsInnerArea,DocsName,DocsType,DocsPath,SourceName)VALUES(" + m_BizID + "," + m_UserID + ",1,'" + DocsName[i] + "','" + docsType[i] + "','" + saveFile[i] + "','" + sourceFile[i] + "')"); }
                    }
                }
                if (DbHelperSQL.ExecuteSqlTran(list) > 0)
                {
                    list = null;
                    if (IsInnerArea == "1")
                    {
                        string RegAreaCodeA = this.txtRegAreaCodeA.Value;
                        string RegAreaCodeB = this.txtRegAreaCodeB.Value;
                        //if (this.m_BizCode == "0104" && !string.IsNullOrEmpty(RegAreaCodeA) && !string.IsNullOrEmpty(RegAreaCodeB))
                        //{
                        //    //����֤ ˫������ ���⴦��
                        //    string SelAreaCode = this.txtAreaCode.Value;
                        //    if (RegAreaCodeA == SelAreaCode)
                        //    {
                        //        //���������з�
                        //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlowsWap(this.m_BizID, "1", innerAreaPath); }
                        //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlowsWap(this.m_BizID, "0", innerAreaPath); }
                        //    }
                        //    else
                        //    {
                        //        //��������Ů��
                        //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlowsWap(this.m_BizID, "0", innerAreaPath); }
                        //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlowsWap(this.m_BizID, "1", innerAreaPath); }
                        //    }
                        //}
                        //else
                        //{
                        if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlowsWap(this.m_BizID, this.m_BizCode, "0", innerAreaPath); }
                        if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlowsWap(this.m_BizID, this.m_BizCode, "1", innerAreaPath); }
                        //}
                    }
                }

                DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Comments='" + Comments + "',Attribs=6 WHERE BizID=" + m_BizID);
                CommPage.SetBizLog(m_BizID, m_UserID, "ҵ��֤���ύ", "Ⱥ���û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_BizName + "��֤���ύ����");

                BIZ_Common.SendMsgToPerson(m_BizID, "0",true);

                MessageBox.ShowAndRedirect(this.Page, "", "/Svrs-BizSucess/" + m_BizCode + "-" + m_BizID + "." + m_FileExt, false, true);

            }
            catch (Exception ex)
            {
                list = null;
                MessageBox.Show(this, ex.Message);
                return;
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
        #endregion

    }
}

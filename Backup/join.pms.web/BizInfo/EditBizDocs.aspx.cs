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

namespace join.pms.web.BizInfo
{
    public partial class EditBizDocs : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserName;

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;


        private string m_PersonIDA;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                GetBizCategoryLicense();
                this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">������ҳ</a> &gt;&gt; ҵ����� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + m_NavTitle + "</a> &gt;&gt; �ϴ�������";
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region
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
            else { GetUserInfo(); }
        }

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            //string sss = Request.Url.ToString();
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = HttpUtility.UrlDecode(StringProcess.AnalysisParas(m_SourceUrlDec, "FuncNa"));
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
            this.txtAction.Value = m_ActionName;
        }

        /// <summary>
        /// ��ȡ��ǰ��¼�û���Ϣ
        /// </summary>
        private void GetUserInfo()
        {
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT UserAccount,UserName,UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        m_UserName = sdr["UserAccount"].ToString() + "(" + sdr["UserName"].ToString() + ")";
                    }
                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }
        /// <summary>
        /// ����ҵ����������֤�Ż�ȡ�û�ID
        /// </summary>
        private string GetPersonID()
        {
            string returnVal = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                string BizCode = "", PersonCidA = "", PersonCidB = "", PersonIDA = "", PersonIDB = "", PersonID = string.Empty;
                sdr = DbHelperSQL.ExecuteReader("SELECT BizCode,PersonCidA,PersonCidB,PersonID,Attribs FROM BIZ_Contents WHERE BizID=" + m_ObjID);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        BizCode = sdr["BizCode"].ToString();
                        PersonCidA = sdr["PersonCidA"].ToString();
                        PersonCidB = sdr["PersonCidB"].ToString();
                        PersonID = sdr["PersonID"].ToString();
                    }
                }
                sdr.Close(); sdr.Dispose();
                if (String.IsNullOrEmpty(PersonID))
                {
                    if (!String.IsNullOrEmpty(PersonCidB)) { PersonIDB = CommPage.GetSingleValue("SELECT PersonID FROM BIZ_Persons WHERE PersonCardID='" + PersonCidB + "'"); }
                    if (!String.IsNullOrEmpty(PersonCidA)) { PersonIDA = CommPage.GetSingleValue("SELECT PersonID FROM BIZ_Persons WHERE PersonCardID='" + PersonCidA + "'"); }
                    if (BizCode == "0101" || BizCode == "0102" || BizCode == "0105" || BizCode == "0107" || BizCode == "0108" || BizCode == "0111" || BizCode == "0122")
                    { returnVal = PersonIDB; }
                    else { returnVal = PersonIDA; }

                }
                else { returnVal = PersonID; }
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } returnVal = ""; }
            return returnVal;
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
                this.LiteralBizCategoryLicense.Text = bizCateg.GetBizCategoryLicenseEdit(this.m_FuncCode, this.m_ObjID, this.m_ActionName, "1");

                this.txtRegAreaCodeA.Value = bizCateg.RegAreaCodeA;
                this.txtRegAreaCodeB.Value = bizCateg.RegAreaCodeB;
                if (bizCateg.IsInnerArea) { this.txtIsInnerArea.Value = "1"; }
                this.txtBizCNum.Value = bizCateg.BizCNum;
                this.txtBizGNum.Value = bizCateg.BizGNum;
                this.txtBizDocsIDOld.Value = bizCateg.BizDocsIDOld;
                this.txtBizDocsIDiOld.Value = bizCateg.BizDocsIDiOld;
                this.txtBizDocsIDIsOld.Value = bizCateg.BizDocsIDIsOld;
                string PersonID = bizCateg.PersonID;
                this.txtAttribs.Value = CommPage.GetSingleVal("SELECT Attribs FROM BIZ_Contents WHERE BizID=" + m_ObjID);
                bizCateg = null;
                if (String.IsNullOrEmpty(PersonID)) { PersonID = GetPersonID(); }
                this.txtPersonID.Value = PersonID;
            }
            catch { }
        }
        #endregion

        #region �ύ������Ϣ
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
            string bizAttribs = this.txtAttribs.Value;
            string bizGNum = this.txtBizGNum.Value;//��Ҫ֤������
            string personPhotos = string.Empty;//������ϵ�֤��
            for (int i = 0; i < int.Parse(bizGNum); i++)
            {
                string DocsName = HttpUtility.UrlDecode(Request["txtDocsName" + i]);
                string DocsID = Request["txtDocsID" + i];
                string LicenseType = Request["txtType" + i];
                string cbBiz = Request["cbBiz" + i];

                if (!String.IsNullOrEmpty(DocsID))
                {
                    incDocsID += DocsID;
                    if (LicenseType == "5") { personPhotos = CommPage.GetSingleVal("SELECT DocsPath FROM BIZ_Docs WHERE CommID=" + DocsID.Substring(0, DocsID.Length - 1)); }
                }
                else
                {
                    if (LicenseType == "0")
                    {
                        //if (string.IsNullOrEmpty(DocsID)) { strErr += "���ϴ�" + DocsName + "֤����\\n"; }
                    }
                    if (this.m_ActionName == "Attach" && DocsName == "�ռ���ִ��") { }
                    else
                    {
                        //string cbBiz = Request["cbBiz" + i];
                        //if (cbBiz == "on" && String.IsNullOrEmpty(DocsID)) { Comments += DocsName + "<br/>"; }
                        if (String.IsNullOrEmpty(DocsID)) { Comments += DocsName + "<br/>"; }
                    }
                }
            }

            string IsInnerArea = this.txtIsInnerArea.Value;
            string DocsIDIs = Request["txtDocsIDIs"];

            if (IsInnerArea == "1")
            {

                if (!string.IsNullOrEmpty(DocsIDIs))
                {
                    strSql = ",IsInnerArea=1";
                    incDocsID = incDocsID + DocsIDIs;
                }
            }


            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(incDocsID))
                {
                    //BIZ_Docs bizDocs = new BIZ_Docs();
                    //bizDocs.BizID = this.m_ObjID;
                    //bizDocs.PersonID = this.m_PersonIDA;
                    //if (IsInnerArea == "1")
                    //{
                    //    if (!string.IsNullOrEmpty(DocsIDIs))
                    //    {
                    //        bizDocs.IsInnerArea = "1";
                    //        incDocsID = incDocsID + DocsIDIs;
                    //    }
                    //}
                    //else
                    //{
                    //    bizDocs.IsInnerArea = "0";
                    //}
                    incDocsID = incDocsID.Substring(0, incDocsID.Length - 1);
                    // string execuN = bizDocs.IsBizDocsNeedUpdate(incDocsID);
                    m_SqlParams = "UPDATE BIZ_Docs SET BizID=" + m_ObjID + ",PersonID = " + this.txtPersonID.Value + "  " + strSql + "  WHERE CommID IN (" + incDocsID + ")";
                    //if (int.Parse(execuN) > 0 && IsInnerArea == "1")
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                    {
                        DelBizDocsOld(incDocsID);
                        if (IsInnerArea == "1" && !string.IsNullOrEmpty(DocsIDIs))
                        {
                            string RegAreaCodeA = this.txtRegAreaCodeA.Value;
                            string RegAreaCodeB = this.txtRegAreaCodeB.Value;
                            //if (this.m_FuncCode == "0104" && !string.IsNullOrEmpty(RegAreaCodeA) && !string.IsNullOrEmpty(RegAreaCodeB))
                            //{
                            //    //����֤ ˫������ ���⴦��
                            //    string SelAreaCode = this.txtAreaCode.Value;
                            //    if (RegAreaCodeA == SelAreaCode)
                            //    {
                            //        //���������з�
                            //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, "1", DocsIDIs); }
                            //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, "0", DocsIDIs); }
                            //    }
                            //    else
                            //    {
                            //        //��������Ů��
                            //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, "0", DocsIDIs); }
                            //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, "1", DocsIDIs); }
                            //    }
                            //}
                            //else
                            //{
                            if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, this.m_FuncCode, "0", DocsIDIs); }
                            if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, this.m_FuncCode, "1", DocsIDIs); }
                            //}
                        }
                    }
                }
                //BIZ_Contents m_BizC = new BIZ_Contents();
                //m_BizC.BizID = this.m_ObjID;
                //m_BizC.Attribs = bizAttribs;
                //m_BizC.Comments = Comments;
                //m_BizC.UpdateAttribs();
                //m_BizC = null;   
                string sql = string.Empty;
                if (!String.IsNullOrEmpty(personPhotos)) { sql = " ,PersonPhotos='" + personPhotos + "' "; }

                if (bizAttribs == "0") { DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Comments='" + Comments + "',Attribs=6 " + sql + " WHERE BizID=" + m_ObjID); }
                else if (bizAttribs == "3")
                {
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                    list.Add("UPDATE BIZ_Contents SET Attribs=1 " + sql + " WHERE BizID=" + this.m_ObjID); // ����ҵ��״̬
                    list.Add("UPDATE BIZ_WorkFlows SET Attribs=9 WHERE BizID=" + this.m_ObjID + " AND Attribs=0"); //��������״̬
                    DbHelperSQL.ExecuteSqlTran(list);
                    list = null;
                }
                else { DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Comments='" + Comments + "' " + sql + " WHERE BizID=" + m_ObjID); }

                CommPage.SetBizLog(m_ObjID, m_UserID, "ҵ��֤���޸�", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_NavTitle + "��֤���޸Ĳ���");
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��<" + this.m_NavTitle + ">������Ϣ�ύ�ɹ���������룡", m_TargetUrl, true, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                //Response.Write(" <script>alert('����ʧ�ܣ�" + ex.Message + "') ;</script>");
                return;
            }
        }
        #endregion

        #region ������ز���

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incDocsID">�����ϴ��ĸ���id</param>
        private void DelBizDocsOld(string incDocsID)
        {
            try
            {
                #region ��ȡ�޸ĵ�ͼƬid

                string BizDocsIDOld = this.txtBizDocsIDOld.Value;//���ϴ���ͼƬID
                string BizDocsIDiOld = this.txtBizDocsIDiOld.Value;//���ϴ���ͼƬ����������
                string BizDocsIDs = string.Empty;//��Ҫɾ����ͼƬID

                if (!String.IsNullOrEmpty(BizDocsIDOld))
                {
                    string[] arrObizDocsID = BizDocsIDOld.Split(',');
                    string[] arrObizDocsiID = BizDocsIDiOld.Split(',');

                    string BizGNum = this.txtBizGNum.Value;
                    for (int i = 0; i < int.Parse(BizGNum); i++)
                    {
                        string DocsID = Request["txtDocsID" + i];

                        if (!String.IsNullOrEmpty(DocsID))
                        {
                            for (int j = 0; j < arrObizDocsiID.Length; j++)
                            {
                                if (i.ToString() == arrObizDocsiID[j])
                                {
                                    if (String.IsNullOrEmpty(BizDocsIDs)) { BizDocsIDs = arrObizDocsID[j]; }
                                    else { BizDocsIDs += "," + arrObizDocsID[j]; }
                                }
                            }
                        }
                    }
                }

                #endregion
                if (!String.IsNullOrEmpty(BizDocsIDs))
                {
                    m_SqlParams = "DELETE FROM BIZ_Docs WHERE CommID IN(" + BizDocsIDs + ") AND BizID=" + m_ObjID + "";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                }
            }
            catch { }
        }
        #endregion

    }
}


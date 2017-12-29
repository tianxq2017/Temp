using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;

using UNV.Comm.Web;
namespace join.pms.wap
{
    public class BIZ_UpFhotos
    {
        #region �ϴ�ͼƬ ֱ��
        /// <summary>
        /// �ϴ�ͼƬ
        /// </summary>
        /// <param name="photoPath">�����ϴ�ͼƬ�����վ��Ŀ¼��·��</param>
        /// <returns></returns>
        public static bool UploadPhotos(FileUpload ctr, ref string[] bizDocsInfo, string n, ref string msg)
        {
            iSvrs.DALSvrs Svrs = new join.pms.wap.iSvrs.DALSvrs();
            iSvrs.ClientInfo CI = new join.pms.wap.iSvrs.ClientInfo();

            string saveFile = "", sParams = "";
            bool returnVa = false;
            if (ctr.HasFile)
            {
                try
                {
                    string fileName = ctr.FileName;
                    string fileType = fileName.Substring(fileName.LastIndexOf("."));

                    string fileContentType = ctr.PostedFile.ContentType;
                    if (IsAllowedFiles(fileContentType))
                    {
                        int fileSize = ctr.PostedFile.ContentLength / 1024;
                        if (fileSize < 10240)
                        {
                            string targetFile = StringProcess.GetCurDateTimeStr(16) + n + fileType; // ���ݿⱣ����ļ���
                            string fullPath = "/Files/" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "/" + DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "/";
                            CI.ClientID = "1";
                            CI.ClientCode = "c6RFtyTTF4vPnMWJUvuIWlMQZAKD1Ysl";
                            byte[] imgByte = ConvertStreamToByteBuffer(ctr.PostedFile.InputStream);
                            saveFile = fullPath + targetFile;
                            sParams = "FileName=" + saveFile + ";UID=1;IDC=QJFuGC7vZcdxusUSkQWixNXBUp8jPW0EyheiFyaWhmwe4Zjj0PtwgQ3dMez0dx5d;";
                            Svrs.UpLoadFiles(imgByte, sParams, CI);

                            bizDocsInfo[0] = fileType;
                            bizDocsInfo[1] = saveFile;
                            bizDocsInfo[2] = fileName;
                            returnVa = true;
                        }
                        else
                        {
                            msg = "����ʧ�ܣ�ͼƬ�ļ����󣬲��ܳ���10M����ʹ��ͼ���������������ϴ���";
                        }
                    }
                    else
                    {
                        msg = "����ʧ�ܣ�����ͼ��ֻ֧��.jpg|.jpeg|.gif��ʽ��ͼƬ�ļ��ϴ��������²�����";
                    }

                }
                catch (Exception ex)
                {
                    msg = "����ʧ�ܣ�" + ex.Message;
                }
            }
            else
            {
                msg = "����ʧ�ܣ�û���ҵ��ϴ����ļ��������²�����";
            }
            return returnVa;
        }
        #endregion

        #region �ϴ�ͼƬ ����ʾ
        /// <summary>
        /// �ϴ�ͼƬ ����ʾ ����¼���ݿ�
        /// </summary>
        /// <param name="photoPath">�����ϴ�ͼƬ�����վ��Ŀ¼��·��</param>
        /// <returns></returns>
        public static bool UploadPhotos(FileUpload ctr, ref string[] bizDocsInfo, ref string msg)
        {
            iSvrs.DALSvrs Svrs = new join.pms.wap.iSvrs.DALSvrs();
            iSvrs.ClientInfo CI = new join.pms.wap.iSvrs.ClientInfo();

            string saveFile = "", sParams = "";
            long docsSize = 0;
            bool returnVa = false;
            if (ctr.HasFile)
            {
                try
                {
                    string fileName = ctr.FileName;
                    string fileType = fileName.Substring(fileName.LastIndexOf("."));

                    string fileContentType = ctr.PostedFile.ContentType;
                    if (IsAllowedFiles(fileContentType))
                    {
                        docsSize = ctr.PostedFile.ContentLength;
                        long fileSize = docsSize / 1024;
                        if (fileSize < 10240)
                        {
                            string targetFile = StringProcess.GetCurDateTimeStr(16) + fileType; // ���ݿⱣ����ļ���
                            string fullPath = "/Files/" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "/" + DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "/";
                            CI.ClientID = "1";
                            CI.ClientCode = "c6RFtyTTF4vPnMWJUvuIWlMQZAKD1Ysl";
                            byte[] imgByte = ConvertStreamToByteBuffer(ctr.PostedFile.InputStream);
                            saveFile = fullPath + targetFile;
                            sParams = "FileName=" + saveFile + ";UID=1;IDC=QJFuGC7vZcdxusUSkQWixNXBUp8jPW0EyheiFyaWhmwe4Zjj0PtwgQ3dMez0dx5d;";
                            Svrs.UpLoadFiles(imgByte, sParams, CI);

                            bizDocsInfo[0] = fileType;
                            bizDocsInfo[1] = saveFile;
                            bizDocsInfo[2] = fileName;

                            msg = "<div id=\"oprateUpFiles\">�ļ�[ " + fileName + " ]�ϴ��ɹ���<br/><b>The file size:</b>" + docsSize.ToString() + "�ֽ�<br/>";
                            returnVa = true;
                        }
                        else
                        {
                            msg = "����ʧ�ܣ�ͼƬ�ļ����󣬲��ܳ���10M����ʹ��ͼ���������������ϴ���" + docsSize.ToString();
                        }
                    }
                    else
                    {
                        msg = "����ʧ�ܣ�����ͼ��ֻ֧��.jpg|.jpeg|.gif��ʽ��ͼƬ�ļ��ϴ��������²�����";
                    }
                }
                catch (Exception ex)
                {
                    msg = "����ʧ�ܣ�" + ex.Message;
                }
            }
            else
            {
                msg = "����ʧ�ܣ�û���ҵ��ϴ����ļ��������²�����";
            }
            return returnVa;
        }
        #endregion

        #region �������
        /// <summary>
        /// 
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        private static byte[] ConvertStreamToByteBuffer(System.IO.Stream S)
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
        /// <summary>
        /// �����Ƿ��������ϴ����ļ�����
        /// </summary>
        /// <param name="fileContentType"></param>
        /// <returns></returns>
        private static bool IsAllowedFiles(string fileContentType)
        {
            bool isValidFile = false;
            string[] allowFiles ={ "image/pjpeg", "image/gif", "image/jpeg", "application/x-jpg", "image/x-png" };
            for (int i = 0; i < allowFiles.Length; i++)
            {
                if (fileContentType == allowFiles[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            return isValidFile;
        }

        #endregion
    }
}

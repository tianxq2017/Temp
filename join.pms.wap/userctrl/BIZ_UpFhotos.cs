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
        #region 上传图片 直接
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="photoPath">返回上传图片相对网站根目录的路径</param>
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
                            string targetFile = StringProcess.GetCurDateTimeStr(16) + n + fileType; // 数据库保存的文件名
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
                            msg = "操作失败：图片文件过大，不能超过10M，请使用图像处理软件处理后再上传！";
                        }
                    }
                    else
                    {
                        msg = "操作失败：链接图标只支持.jpg|.jpeg|.gif格式的图片文件上传，请重新操作！";
                    }

                }
                catch (Exception ex)
                {
                    msg = "操作失败：" + ex.Message;
                }
            }
            else
            {
                msg = "操作失败：没有找到上传的文件，请重新操作！";
            }
            return returnVa;
        }
        #endregion

        #region 上传图片 有提示
        /// <summary>
        /// 上传图片 有提示 并记录数据库
        /// </summary>
        /// <param name="photoPath">返回上传图片相对网站根目录的路径</param>
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
                            string targetFile = StringProcess.GetCurDateTimeStr(16) + fileType; // 数据库保存的文件名
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

                            msg = "<div id=\"oprateUpFiles\">文件[ " + fileName + " ]上传成功！<br/><b>The file size:</b>" + docsSize.ToString() + "字节<br/>";
                            returnVa = true;
                        }
                        else
                        {
                            msg = "操作失败：图片文件过大，不能超过10M，请使用图像处理软件处理后再上传！" + docsSize.ToString();
                        }
                    }
                    else
                    {
                        msg = "操作失败：链接图标只支持.jpg|.jpeg|.gif格式的图片文件上传，请重新操作！";
                    }
                }
                catch (Exception ex)
                {
                    msg = "操作失败：" + ex.Message;
                }
            }
            else
            {
                msg = "操作失败：没有找到上传的文件，请重新操作！";
            }
            return returnVa;
        }
        #endregion

        #region 其他相关
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
        /// 检验是否是允许上传的文件类型
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

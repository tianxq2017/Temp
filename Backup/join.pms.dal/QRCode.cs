using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Drawing;

using UNV.Comm.Web;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using ZXing.Rendering;

namespace join.pms.dal
{
    /// <summary>
    /// ��ά�봦�����
    /// </summary>
    public class QRCode
    {
        ~QRCode() { }

        private QrCodeEncodingOptions _QrOptions = null;
        private BarcodeWriter _Writer = null;

        /// <summary>
        /// ���ã����ɣ�������
        /// </summary>
        /// <param name="qrInfo">��������Ϣ</param>
        /// <param name="serverPath">������·��</param>
        /// <param name="qrFiles">���ɵĶ�ά��ͼƬ</param>
        /// <returns></returns>
        public bool SetBarCode(string qrInfo, string serverPath, ref string qrFiles)
        {
            string savePath = string.Empty;
            string saveFile = string.Empty;
            try
            {
                savePath = "/Files/BarCode/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
                if (!Directory.Exists(serverPath + savePath)) Directory.CreateDirectory(serverPath + savePath);
                saveFile = StringProcess.GetCurDateTimeStr(16) + ".jpg";
                qrFiles = savePath + saveFile;

                _QrOptions = new QrCodeEncodingOptions();
                _QrOptions.DisableECI = true;
                _QrOptions.CharacterSet = "UTF-8";
                _QrOptions.Margin = 1;
                _QrOptions.Width = 100;
                _QrOptions.Height = 150;

                _Writer = new BarcodeWriter();
                _Writer.Format = BarcodeFormat.CODABAR;
                _Writer.Options = _QrOptions;

                Bitmap bitmap = _Writer.Write("A" + qrInfo + "B");
                bitmap.Save(serverPath + qrFiles, System.Drawing.Imaging.ImageFormat.Jpeg);
                bitmap = null;

                _Writer = null;
                _QrOptions = null;

                return true;
            }
            catch (Exception ex)
            {
                qrFiles = ex.Message;

                _Writer = null;
                _QrOptions = null;

                return false;
            }
        }

        /// <summary>
        /// ���ã����ɣ���ά��
        /// </summary>
        /// <param name="qrInfo">��ά����Ϣ</param>
        /// <param name="serverPath">������·��</param>
        /// <param name="qrFiles">���ɵĶ�ά��ͼƬ</param>
        /// <returns></returns>
        public bool SetQrCode(string qrInfo, string serverPath, ref string qrFiles)
        {
            string savePath = string.Empty;
            string saveFile = string.Empty;
            try
            {
                savePath = "/Files/MatrixCode/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
                if (!Directory.Exists(serverPath + savePath)) Directory.CreateDirectory(serverPath + savePath);
                saveFile = StringProcess.GetCurDateTimeStr(16) + ".jpg";
                qrFiles = savePath + saveFile;

                _QrOptions = new QrCodeEncodingOptions();
                _QrOptions.DisableECI = true;
                _QrOptions.CharacterSet = "UTF-8";
                _QrOptions.Margin = 1;
                _QrOptions.Width = 100;
                _QrOptions.Height = 100;

                _Writer = new BarcodeWriter();
                _Writer.Format = BarcodeFormat.QR_CODE;
                _Writer.Options = _QrOptions;

                Bitmap bitmap = _Writer.Write(qrInfo);
                bitmap.Save(serverPath + qrFiles, System.Drawing.Imaging.ImageFormat.Jpeg);
                bitmap = null;

                _Writer = null;
                _QrOptions = null;

                return true;
            }
            catch (Exception ex)
            {
                qrFiles = ex.Message;

                _Writer = null;
                _QrOptions = null;

                return false;
            }
        }

        /// <summary>
        /// ���ã����ɣ���ά��
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="qrInfo"></param>
        /// <param name="serverPath"></param>
        /// <param name="qrFiles"></param>
        /// <returns></returns>
        public bool SetQrCode(int width,int height,string qrInfo, string serverPath, ref string qrFiles)
        {
            string savePath = string.Empty;
            string saveFile = string.Empty;
            try
            {
                savePath = "/Files/MatrixCode/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
                if (!Directory.Exists(serverPath + savePath)) Directory.CreateDirectory(serverPath + savePath);
                saveFile = StringProcess.GetCurDateTimeStr(16) + ".jpg";
                qrFiles = savePath + saveFile;

                _QrOptions = new QrCodeEncodingOptions();
                _QrOptions.DisableECI = true;
                _QrOptions.CharacterSet = "UTF-8";
                _QrOptions.Width = 100;
                _QrOptions.Height = 100;

                _Writer = new BarcodeWriter();
                _Writer.Format = BarcodeFormat.QR_CODE;
                _Writer.Options = _QrOptions;

                Bitmap bitmap = _Writer.Write(qrInfo);
                bitmap.Save(serverPath + qrFiles, System.Drawing.Imaging.ImageFormat.Jpeg);
                bitmap = null;

                _Writer = null;
                _QrOptions = null;

                return true;
            }
            catch (Exception ex)
            {
                qrFiles = ex.Message;
                //qrInfo = ex.Message;

                _Writer = null;
                _QrOptions = null;

                return false;
            }
        }
    }
}

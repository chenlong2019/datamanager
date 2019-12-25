using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using SeasideResearch.LibCurlNet;
using System.Diagnostics;

namespace DataManager
{
    /// <summary>
    /// UploadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UploadWindow : Window
    {
        public UploadWindow(string s)
        {
            InitializeComponent();
            this.s = s;
        }
        private readonly string s;
        FtpWebRequest reqFTP;
        private int per;
        public static bool uploading = false;
        private delegate void lb_Content(string text, Label label);
        private delegate void pb_ProgressBar(int max, int nowValue, ProgressBar PB);


        private void Lb_Content(string text, Label label)
        {
            label.Content = text;
        }
        private void Pb_ProgressBar(int max, int nowValue, ProgressBar PB)
        {
            try
            {
                PB.Maximum = max;
                PB.Value = nowValue;
            }
            catch (Exception) { }
        }
        
        private void EventUpload()
        {
           Upload(AddWindow.localDataPath, AddWindow.localImagePath);
            MessageBox.Show("上传成功");
        }
        private void Connect(string path)
        {
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            reqFTP.UseBinary = true;//二进制传输，false文本传输
            reqFTP.Credentials = new NetworkCredential(AddWindow.serverUser, AddWindow.serverPWD);
            //reqFTP.UsePassive = true;//被动模式
        }
        private void MakeDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + AddWindow.serverIP + dirName;
                Connect(uri);//连接 
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="localData">本地数据路径</param>
        /// <param name="directory1">一级目录</param>
        /// <param name="localImage">本地影像路径</param>
        private void Upload(string localData, string localImage="")
        {
            if (localImage != "")
            {
                try
                {
                    FileInfo file = new FileInfo(localImage);
                    string uri = "ftp://" + AddWindow.serverIP +"/preview.TIF";
                    Connect(uri);
                    reqFTP.KeepAlive = false;
                    reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                    reqFTP.ContentLength = file.Length;
                    int buffLength = 2048;
                    byte[] buff = new byte[buffLength];
                    int contentLen;
                    FileStream fs = file.OpenRead();
                    Stream strm = reqFTP.GetRequestStream();
                    contentLen = fs.Read(buff, 0, buffLength);
                    while (contentLen != 0)
                    {
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }
                    strm.Close();
                    fs.Close();
                }
                catch (Exception)
                { }
            }
            try
            {
                FileInfo file = new FileInfo(localData);
                string uri = "ftp://" + AddWindow.serverIP + "/"+ file.Name;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.UseBinary = true;//二进制传输，false文本传输
                reqFTP.Credentials = new NetworkCredential(AddWindow.serverUser, AddWindow.serverPWD);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.ContentLength = file.Length;
                int fileSize = Convert.ToInt32(file.Length);
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                FileStream fs = file.OpenRead();
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                int nowSize = contentLen;
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    nowSize += contentLen;
                    per = Convert.ToInt32(((nowSize * 100.0 / fileSize) * 1000 + 0.5) / 1000);
                    string kb = nowSize/1024 + "KB/" + fileSize/1024 + "KB\n" + per.ToString()+"%";
                    lb_UploadFileName.Dispatcher.BeginInvoke(new lb_Content(Lb_Content), new object[] { "文件名:"+AddWindow.localDataName, lb_UploadFileName });
                    lb_kbye.Dispatcher.BeginInvoke(new lb_Content(Lb_Content), new object[] { kb, lb_kbye });
                    progressBar_Upload.Dispatcher.BeginInvoke(new pb_ProgressBar(Pb_ProgressBar), new object[] { fileSize, nowSize, progressBar_Upload });
                }
                if (UploadWindow.uploading)
                {
                    SqlConnection conn = new SqlConnection(LoginForm.connString);
                    conn.Open();
                    SqlCommand comm = new SqlCommand(s, conn);
                    try
                    {
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    return;
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //链接CURL

        public static void Get(/*string workspace,*/ string localDataPath, string localDataName)
        {

            Process p = new Process();
            try
            {
                
                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.FileName = "curl.exe";
                string workspace = "MangerData";
                //获得发布图层的工作空间，文件路径，图层名称
                string url = "-u admin:geoserver -XPUT -H \"Content-type:image/tiff\" --data-binary @{0} http://119.3.54.117:8080/geoserver/rest/workspaces/{1}/coveragestores/{2}/file.geotiff";
                Console.WriteLine(url);
                p.StartInfo.Arguments = string.Format(url, localDataPath, workspace, localDataName);
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                p.WaitForExit();
            }
            finally 
            {
                p.Close();
            }

        }


        
        public static Int32 OnWriteData(Byte[] buf, Int32 size, Int32 nmemb, System.Object extraData) 
        {
            Console.Write(System.Text.Encoding.UTF8.GetString(buf));
            return size * nmemb;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (per < 100)
            {
                MessageBoxResult result = MessageBox.Show("下载仍在继续,确定退出吗", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            uploading = false;
        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uploading = true;
            Thread thread = new Thread(new ThreadStart(EventUpload));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}

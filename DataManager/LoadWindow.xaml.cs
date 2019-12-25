using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using System.Data.SqlClient;

namespace DataManager
{
    /// <summary>
    /// LoadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoadWindow : Window
    {
        public LoadWindow(string loadFilePath)
        {
            InitializeComponent();
            this.loadFilePath = loadFilePath;
        }
        private int i;//下载百分比
        FtpWebRequest reqFTP;
        private delegate void updateui(long rowCount, int i, ProgressBar PB);//定义委托：rowCount代表文件总长度，i代表现在所占百分比，pb代表空间名
        private delegate void lb_Content(string text, Label label);
        private string loadFilePath;

        private  void lb_Text(string text, Label label)
        {
            label.Content = text;
        }
        private  void upui(long rowCount, int i, ProgressBar PB)
        {
            try
            {
                PB.Maximum = rowCount;
                PB.Value = i;
            }
            catch { }
        }
        private void loadFile()//创建方法，为开启线程做准备
        {
            Download(MainWindow.saveFilePath, MainWindow.loadFilePath);
            MessageBox.Show("下载完成");
            //UploadWindow upload = new UploadWindow(s);
            //upload.Close();
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="localpath">本地路径（不包含文件名）</param>
        /// <param name="fileName">ftp完整路径(相对根目录而言)</param>
        public void Download(string localpath,string fileName)
        {
            try
            {
                String onlyFileName = System.IO.Path.GetFileName(fileName);
                lb_FileName.Dispatcher.BeginInvoke(new lb_Content(lb_Text), new object[] { "文件名:" + onlyFileName, lb_FileName });
                string uri = "ftp://" + AddWindow.serverIP + "/" + fileName;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                string newFileName = localpath+"/"+onlyFileName;
                if (File.Exists(newFileName))
                {
                    MessageBox.Show("本地文件已存在，无法下载");
                    return;
                }
                
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(AddWindow.serverUser, AddWindow.serverPWD);
                reqFTP.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                //long cl = response.ContentLength;//-1,不对
                long cl = GetFileSize(uri);//获取服务器端有特定的方法
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);

                FileStream outputStream = new FileStream(newFileName, FileMode.Create);//接收服务器的二进制流，一次2kb
                while (readCount > 0)
                {
                    double per = outputStream.Length * 100.0 / cl;
                    i = Convert.ToInt32((per * 1000 + 0.5) / 1000);
                    lb_Speed.Dispatcher.BeginInvoke(new lb_Content(lb_Text), new object[] { i.ToString() + "%", lb_Speed });
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    if(progressBar1!=null)
                    progressBar1.Dispatcher.Invoke(new updateui(upui), new object[] { cl, Convert.ToInt32(outputStream.Length), progressBar1 });//委托
                }
                reqFTP.Abort();
                SqlConnection conn = new SqlConnection(LoginForm.connString);
                conn.Open();
                int time = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                SqlCommand comm = new SqlCommand("insert into Download_Log values('" + LoginForm.staff_Number + "','" + LoginForm.Namer.ToString() + "','" + time.ToString() + "','" + loadFilePath.Trim() + "')", conn);
                comm.ExecuteNonQuery();
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("因" + ex.Message + "无法下载");
            }
        }
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private long GetFileSize(string url)
        {
            long fileSize = 0;
            try
            {
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                ftp.UseBinary = true;
                ftp.Credentials = new NetworkCredential(AddWindow.serverUser,AddWindow.serverPWD);
                ftp.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                fileSize = response.ContentLength;
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fileSize;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread td = new Thread(new ThreadStart(loadFile));
            td.IsBackground = true;
            td.Start();//启动线程
            
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (i < 100)
            {
                MessageBoxResult result = MessageBox.Show("下载仍在继续,确定退出吗","提示",MessageBoxButton.OKCancel,MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel)
                    e.Cancel=true;
            }
        }
    }
}

using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Rasters;
using Esri.ArcGISRuntime.Geometry;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Timers;
using System.Net;
using System.Security.Cryptography;
using Esri.ArcGISRuntime.ArcGISServices;
using SeasideResearch.LibCurlNet;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;

namespace DataManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Win32Native
    {//wpf实现父子窗体
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public extern static IntPtr SetParent(IntPtr childPtr, IntPtr parentPtr);
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (LoginForm.power==1)
            {
                ManagerUserName.Visibility = Visibility.Collapsed;
                Audit.Visibility = Visibility.Collapsed;
                b_Opinion2.Visibility = Visibility.Collapsed;
                b_Opinion1.Visibility = Visibility.Collapsed;
            }
            txt_LoginUserName.Text = "[当前登录用户："+LoginForm.Namer+"]";
            txt_LoginUserName.IsReadOnly = true;
            AddToCmb("select Satellite from SatelliteClass", cmb_Staellite);
            AddToCmb("select Orbit from SatelliteClass", cmb_Orbit);
            if (LoginForm.power == 2)
            {
                timer.Elapsed += new ElapsedEventHandler(theout);//到达设定时间运行程序
                timer.AutoReset = true;//false：执行一次，true：一直执行
                timer.Enabled = true;
            }


        }

        [DllImport("user32.dll")]
        public static extern int MessageBeep(uint uType);//用于发出提示音
        public static string saveFilePath;
        public static string loadFilePath;
        public Timer timer = new Timer(5000);//5s
        public static int x = 0;//判断申请序号的变量
        public int pageIndex=0;//当前页码
        public int dataNumber = 0;//搜索总记录数
        private int pageMax =0;//记录最大页数
        private string sqlstring;//分页时记录sql语句
        FtpWebRequest reqFTP;
        private DataCard card;

        public bool DataRepeat(string s)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlCommand comm = new SqlCommand(s, conn);
            SqlDataReader reader = comm.ExecuteReader();
            bool ifRepetion = reader.Read();
            if (ifRepetion)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }//判断数据是否重复


        //读取geoserver服务，预览显示图层
        public async void AddRaster(string fileName)
        {
           
            string layer = System.IO.Path.GetFileName(fileName).Split('.')[0];
            Esri.ArcGISRuntime.Mapping.Map myMap = new Esri.ArcGISRuntime.Mapping.Map(Basemap.CreateImageryWithLabels());
            string strWMSUrl = "http://119.3.54.117:8080/geoserver/MangerData/wms";
            Uri _wmsUrl = new Uri(strWMSUrl); 
            List<string> _wmsLayerName = new List<string> { layer };
            WmsLayer wmsLayer = new WmsLayer(_wmsUrl, _wmsLayerName);
            wmsLayer.Name = layer;
            myMapView.Map = myMap;
            myMapView.Map.OperationalLayers.Add(wmsLayer);

            //获取图层属性信息
            try
            {
               
                await wmsLayer.LoadAsync();
                Envelope enve1 = wmsLayer.FullExtent;
                Envelope enve2 = new Envelope(enve1.GetCenter(),enve1.Width,enve1.Height);
                await myMapView.SetViewpointGeometryAsync(enve2);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("暂无预览数据");
                //MessageBox.Show(ex.Message,"错误");
            }

            
        }
        public void UpdateSql(string s)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlCommand comm = new SqlCommand(s, conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void AddToCmb(string s, ComboBox cmb)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlDataAdapter sdr = new SqlDataAdapter(s,conn);
            DataSet ds = new DataSet();
            sdr.Fill(ds);
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow myDR = dt.Rows[i];
                cmb.Items.Add(myDR[0].ToString());
            }
            conn.Close();
        }//将数据库内容添加进combobox控件
        private DataTable GetSelectData(string s1)
        {

            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlDataAdapter sdr = new SqlDataAdapter(s1,conn);
            DataSet ds = new DataSet();
            sdr.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            conn.Close();
            return dataTable;
        }
        private void InitPage(string s)
        {
            DataTable myDT = GetSelectData(s);
            dataNumber = myDT.Rows.Count;
            lb_DataNumber.Content = dataNumber + "条数据";
            pageIndex = 1;
            txt_PageIndex.Text = "1";
            if (dataNumber <= 10)
                pageMax = 1;
            else
            {
                if (dataNumber % 10 == 0)
                    pageMax=dataNumber/10;
                else
                    pageMax=dataNumber/10+1;
            }
            lb_PageNumber.Content = "/" + pageMax.ToString();
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="localpath">本地路径（不包含文件名）</param>
        /// <param name="fileName">ftp完整路径(相对于根目录而言)</param>
        public void DownloadMain(string localpath, string fileName)
        {
            try
            {
                String onlyFileName = System.IO.Path.GetFileName(fileName);
                string newFileName = localpath + "/" + onlyFileName;
                if (File.Exists(newFileName))
                {
                    return;
                }
                string uri = "ftp://" + AddWindow.serverIP +"/"+ fileName;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(AddWindow.serverUser, AddWindow.serverPWD);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);

                FileStream outputStream = new FileStream(newFileName, FileMode.Create);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("因" + ex.Message + "无法查询结果");
            }
        }
        private void GetPageData(string s)
        {
            spv_Page.Children.Clear();
            DataTable data = GetSelectData(s);
            string s1 = "D:/DataImage";
            if (data.Rows.Count > 0 && !Directory.Exists(s1))
                Directory.CreateDirectory(s1);
            int i = 0;
            while (i < data.Rows.Count)
            {
                DataRow dataRow = data.Rows[i];
                try
                {
                    if (dataRow[4].ToString() != string.Empty)
                    {
                        string ftpPath = dataRow[4].ToString();
                        string path = s1 + SlipString(dataRow[0].ToString(), "/", true);//数据+影像的路径
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        DownloadMain(path, ftpPath);
                    }
                }
                catch (Exception) { }
                DataCard card = new DataCard();
                card.VerticalAlignment = VerticalAlignment.Top;
                card.Height = 80;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"ImageFile\browse.png", UriKind.RelativeOrAbsolute);//相对路径
                bi.EndInit();
                card.image1.Source = bi;
                card.lb_SatelliteInformation.FontSize = 10;
                card.lb_SatelliteInformation.Items.Add("文件名:  "+SlipString(dataRow[0].ToString(),"/",false));
                card.lb_SatelliteInformation.Items.Add("拍摄卫星:" +dataRow[1].ToString());
                card.lb_SatelliteInformation.Items.Add("卫星轨道:"+dataRow[2].ToString());
                card.lb_SatelliteInformation.Items.Add("拍摄时间:" + dataRow[3].ToString());
                spv_Page.Children.Add(card);
                card.btn_Preview.Click += (sdr, arg) =>
                {


                    AddRaster(SlipString(dataRow[0].ToString(),"/", false));

                    /*this.Dispatcher.Invoke(new Action(delegate()
                    {
                        try
                        {
                            //spv_Page.Children.Remove(card);
                            if (dataRow[4].ToString() != string.Empty)
                                AddRaster(s1 + dataRow[4].ToString());
                            else
                                MessageBox.Show("暂无预览数据");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("暂无预览数据");
                        }
                    }));*/
                };//匿名委托形式
                card.btn_LoadData.Click+=(sdr,arg)=>
                {
                    loadFilePath = dataRow[0].ToString();
                    //MessageBox.Show(loadFilePath);
                    if (LoginForm.power == 2 || DataRepeat("select * from Application where SatelliteData='" + dataRow[0].ToString() + "' and Opinion='同意'"))
                    {
                        System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
                        folder.ShowNewFolderButton = true;
                        folder.Description = "选择保存路径";
                        System.Windows.Forms.DialogResult dialog = folder.ShowDialog();
                        if (dialog == System.Windows.Forms.DialogResult.OK)
                        {
                            saveFilePath = folder.SelectedPath ;//获取文件路径,只到选定的文件那
                            
                            LoadWindow lwForm = new LoadWindow(loadFilePath);
                            lwForm.Show();
                            WindowInteropHelper parentHelper = new WindowInteropHelper(this);
                            WindowInteropHelper childHelper = new WindowInteropHelper(lwForm);
                            Win32Native.SetParent(childHelper.Handle, parentHelper.Handle);
                            
                        }
                       else
                            return;
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("权限不够,是否前往申请？", "权限提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            ApplicationForm aForm = new ApplicationForm();
                            aForm.ShowDialog();
                        }
                    }
                };
                i++;
            }
        }//将数据以卡片显示
        public string SlipString(string s,string s1,bool Front)
        {
            int i = s.LastIndexOf(s1);
            string s2;
            if (Front)
               return  s2 = s.Substring(0, i);
            else
               return s2 = s.Substring(i+1);
        }
        private string[] Director(string strFile)
        {//(递归)得出已知路径下的所有文件
            string[] s = new string[20];
            int i = 0;
            if (strFile.Length == 0)
                return s;
            else
            {
                try
                {
                    DirectoryInfo directory = new DirectoryInfo(strFile);
                    FileSystemInfo[] fileSystemInfo = directory.GetFileSystemInfos();
                    foreach (FileSystemInfo file in fileSystemInfo)
                    {
                        if (file is DirectoryInfo)
                        {//如果是文件夹，递归调用
                            Director(file.FullName);
                        }
                        else
                        {
                            if (SlipString(file.Name,".",false).Equals("tif") || SlipString(file.Name,".",false).Equals("TIF") || SlipString(file.Name,".",false).Equals("gz")|| SlipString(file.Name,".",false).Equals("GZ"))
                            {
                                s[i]=file.FullName;
                                i++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            return s;
            }
        }
        private void Windows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("您确定退出吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            
        }
        private void theout(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(delegate ()
            {
                SqlConnection conn = new SqlConnection(LoginForm.connString);
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from Application where Opinion='待审核'", conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable myDT = ds.Tables[0];
                int aNumber = myDT.Rows.Count;
                int i = 0;
            
                for (int j = 0; j < aNumber; j++)
                {
                    DataRow myDR = myDT.Rows[j];
                    i = Convert.ToInt32(myDR[0]);
                }
                if (i > x)
                {
                    //System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                    //player.SoundLocation = "E:/gis/DataManager/8858.mp3";
                    //player.LoadAsync();
                    //player.PlaySync();
                    uint beep = 0x00000040;
                    MessageBeep(beep);
                    x = i;
                }
            
            if (aNumber > 0 && aNumber < 100)
            {
                this.b_Opinion1.Dispatcher.Invoke(new Action(delegate { b_Opinion1.Visibility = Visibility.Visible; }));
                this.lb_ApplicationNumber.Dispatcher.Invoke(new Action(delegate { lb_ApplicationNumber.Visibility = Visibility.Visible;lb_ApplicationNumber.Content = aNumber; })) ;
                this.b_Opinion2.Dispatcher.Invoke(new Action(delegate { b_Opinion2.Background = Brushes.Red; }));   
            }
            if (aNumber >= 100)
            {
                this.b_Opinion1.Dispatcher.Invoke(new Action(delegate { b_Opinion1.Visibility = Visibility.Visible; }));
                this.lb_ApplicationNumber.Dispatcher.Invoke(new Action(delegate { lb_ApplicationNumber.Visibility = Visibility.Visible; lb_ApplicationNumber.Content = "99+"; }));
                this.b_Opinion2.Dispatcher.Invoke(new Action(delegate { b_Opinion2.Background = Brushes.Red; }));
            }
            if(aNumber==0)
            {
                this.b_Opinion1.Dispatcher.Invoke(new Action(delegate { b_Opinion1.Visibility = Visibility.Collapsed; }));
                this.lb_ApplicationNumber.Dispatcher.Invoke(new Action(delegate { lb_ApplicationNumber.Visibility = Visibility.Collapsed; lb_ApplicationNumber.Content = string.Empty; }));
                this.b_Opinion2.Dispatcher.Invoke(new Action(delegate { b_Opinion2.Background = Brushes.WhiteSmoke; }));
            }
            conn.Close();
            }));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void ManagerUserName_Click(object sender, RoutedEventArgs e)
        {
            AddUserNameForm aunf = new AddUserNameForm();
            aunf.Show();
            WindowInteropHelper parentHelper = new WindowInteropHelper(this);
            WindowInteropHelper childHelper = new WindowInteropHelper(aunf);
            Win32Native.SetParent(childHelper.Handle,parentHelper.Handle);
        }

        private void tb_CloseClumns_Click(object sender, RoutedEventArgs e)
        {
            myGrid_Condition.Visibility = Visibility.Collapsed;
            myGrid_Page.Visibility = Visibility.Collapsed;
        }

        private void BrowseData_Click(object sender, RoutedEventArgs e)
        {
            myGrid_Condition.Visibility = Visibility.Visible;
            myGrid_Page.Visibility = Visibility.Visible;
        }

        private void DataManager_Click(object sender, RoutedEventArgs e)
        {
            AddData adForm = new AddData();//打开DataManagerXaml窗口
            adForm.Show();
            WindowInteropHelper parentHelper = new WindowInteropHelper(this);
            WindowInteropHelper childHelper = new WindowInteropHelper(adForm);
            Win32Native.SetParent(childHelper.Handle, parentHelper.Handle);
        }

        private void CloseBrowse_Click(object sender, RoutedEventArgs e)
        {
            myGrid_Condition.Visibility = Visibility.Collapsed;
            myGrid_Page.Visibility = Visibility.Collapsed;
            //lb_Information.Visibility = Visibility.Collapsed;
        }


        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
            //spv_Page.Children.Clear();
            int orgin = 0, end = 99999999;
            bool time = (dp_OrginTime.Text != string.Empty || dp_EndTime.Text != string.Empty);
            if (time)
            {

                if (dp_OrginTime.Text != string.Empty)
                {
                    int year = Convert.ToDateTime(dp_OrginTime.Text.Trim()).Year;
                    int month = Convert.ToDateTime(dp_OrginTime.Text.Trim()).Month;
                    int day = Convert.ToDateTime(dp_OrginTime.Text.Trim()).Day;
                    orgin = year * 10000 + month * 100 + day;
                }
                if (dp_EndTime.Text != string.Empty)
                {
                    int year = Convert.ToDateTime(dp_EndTime.Text.Trim()).Year;
                    int month = Convert.ToDateTime(dp_EndTime.Text.Trim()).Month;
                    int day = Convert.ToDateTime(dp_EndTime.Text.Trim()).Day;
                    end = year * 10000 + month * 100 + day;
                }
            }
            if (!time && txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && cmb_Staellite.Text.Trim().Length == 0
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim());
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim()+" order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim();
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length == 0
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length == 0
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end);
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end+" order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end;
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length != 0
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length == 0
                && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Orbit='" + cmb_Orbit.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Orbit='" + cmb_Orbit.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Orbit='" + cmb_Orbit.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length == 0
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Staff_Number=" + txt_StaffNumber.Text.Trim());
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Staff_Number=" + txt_StaffNumber.Text.Trim()+" order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Staff_Number=" + txt_StaffNumber.Text.Trim();
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite + "' and Staff_Number=" + txt_StaffNumber.Text.Trim()+" order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite + "' and Staff_Number=" + txt_StaffNumber.Text.Trim();
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and Orbit='" + cmb_Orbit.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and Orbit='" + cmb_Orbit.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and People='" + txt_People.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and People='" + txt_People.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and People='" + txt_People.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite= '" + cmb_Staellite.Text.Trim() + "'and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite= '" + cmb_Staellite.Text.Trim() + "'and Orbit='" + cmb_Orbit.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite= '" + cmb_Staellite.Text.Trim() + "'and Orbit='" + cmb_Orbit.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and People='" + txt_People.Text.Trim() + "' and Staff_Number='" + txt_StaffNumber.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and People='" + txt_People.Text.Trim() + "' and Staff_Number='" + txt_StaffNumber.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and People='" + txt_People.Text.Trim() + "' and Staff_Number='" + txt_StaffNumber.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() );
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() + " order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() ;
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() );
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() + " order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() ;
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() );
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() + " order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() ;
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' order by is desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'";
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim()+" order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim();
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim()+" order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where People='" + txt_People.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim();
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim()+" order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim();
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + dp_OrginTime.Text.Trim() + " and PhotosTime<=" + dp_EndTime.Text.Trim() + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + dp_OrginTime.Text.Trim() + " and PhotosTime<=" + dp_EndTime.Text.Trim() + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim()+" order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + dp_OrginTime.Text.Trim() + " and PhotosTime<=" + dp_EndTime.Text.Trim() + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim();
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                InitPage("select SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() + " and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                GetPageData("select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() + " and Orbit='" + cmb_Orbit.Text.Trim() + "' order by id desc");
                sqlstring = "select top 10 SatelliteData,Satellite,Orbit,PhotosTime,Preview_Image from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim() + " and Orbit='" + cmb_Orbit.Text.Trim() + "'";
                return;
            }
            MessageBox.Show("没有您需要的内容", "提示");
        }

        private void btn_ClearCondition_Click(object sender, RoutedEventArgs e)
        {
            dp_OrginTime.Text = string.Empty;
            dp_EndTime.Text = string.Empty;
            txt_People.Text = string.Empty;
            txt_StaffNumber.Text = string.Empty;
            cmb_Staellite.Text = string.Empty;
            cmb_Orbit.Text = string.Empty;
        }

        private void LoadGZData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Audit_Click(object sender, RoutedEventArgs e)
        {
            AuditWindow aForm = new AuditWindow();
            aForm.Show();
            WindowInteropHelper parentHelper = new WindowInteropHelper(this);
            WindowInteropHelper childHelper = new WindowInteropHelper(aForm);
            Win32Native.SetParent(childHelper.Handle, parentHelper.Handle);
        }
        private void DownloadLog_Click(object sender, RoutedEventArgs e)
        {
            DownloadLogWindow dlwForm = new DownloadLogWindow();
            dlwForm.Show();
            WindowInteropHelper childHelper = new WindowInteropHelper(dlwForm);
            WindowInteropHelper parentHelper = new WindowInteropHelper(this);
            Win32Native.SetParent(childHelper.Handle,parentHelper.Handle);
        }

        private void CloseMainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_FirstPage_Click(object sender, RoutedEventArgs e)
        {
            GetPageData(sqlstring+" order by id desc");
            pageIndex = 1;
            txt_PageIndex.Text = "1";
        }

        private void btn_EndPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageMax == 1)
                return;
            int i = (pageMax - 1) * 10;
            GetPageData(sqlstring+" and id<(select min(id) from (select top "+i.ToString()+ " id from Storage order by id desc)a ) order by id desc");
            pageIndex = pageMax;
            txt_PageIndex.Text = pageMax.ToString();
        }

        private void btn_Prev_Click(object sender, RoutedEventArgs e)
        {
            if (txt_PageIndex.Text.Trim() == "1")
                return;
            else
            {
                if (pageIndex == 2)
                {
                    btn_FirstPage_Click(null, null);
                    pageIndex = 1;
                }
                else
                {
                    int i = (pageIndex - 2) * 10;
                    GetPageData(sqlstring + " and id<(select min(id) from (select top " + i.ToString() + " id from storage order by id desc)a ) order by id desc");
                    pageIndex = pageIndex - 1;
                    txt_PageIndex.Text = (pageIndex - 1).ToString();
                }
            }
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex >= pageMax)
                return;
            else
            {
                int i = pageIndex * 10;
                GetPageData(sqlstring + " and id<(select min(id) from (select top " + i.ToString() + " id from storage order by id desc)a ) order by id desc");
                pageIndex += 1;
                txt_PageIndex.Text = pageIndex.ToString();
            }
        }

        private void btn_Go_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = Convert.ToInt32(txt_PageIndex.Text.Trim());
                if (i > pageMax || i < 1)
                    return;
                else if (i == 1)
                    btn_FirstPage_Click(null, null);
                else
                {
                    int i1 = (i - 1)*10;
                    GetPageData(sqlstring + " and id<(select min(id) from (select top " + i1.ToString() + " id from storage order by id desc)a ) order by id desc");
                    pageIndex = i;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("输入页码不合法！","提示");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
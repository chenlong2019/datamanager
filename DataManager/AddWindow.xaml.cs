using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Rasters;
using Esri.ArcGISRuntime.Mapping;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace DataManager
{
    /// <summary>
    /// AddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            MainWindow main = new MainWindow();
            main.AddToCmb("select Satellite from SatelliteClass", cmb_Satellite);
            main.AddToCmb("select Orbit from SatelliteClass", cmb_Orbit);
            cmb_Orbit.Items.Add("");
            cmb_Satellite.Items.Add("");
            txt_StaffNumber.Text = LoginForm.staff_Number.ToString();
            txt_People.Text = LoginForm.Namer;
        }
        public static string serverIP = ConfigurationManager.ConnectionStrings["serverIP"].ToString();
        public static string serverUser = "jiutian";
        public static string serverPWD = "jiutian";
        public static string localDataPath;
        public static string localDataName;
        public static string localImagePath="";
        public static string localImageName = string.Empty;
        public static string directory;
        private int RasterSaveAs(IRaster raster, string fileName)//将栅格信息保存到指定路径下
        {
            int result = 0;
            try
            {
                IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(fileName), 0);
                ISaveAs pSaveAs = raster as ISaveAs;
                pSaveAs.SaveAs(System.IO.Path.GetFileName(fileName), pWorkspace, "TIFF");
                result = 0;
            }
            catch (Exception)
            {
                result = 1;
            }
            return result;
        }
        public static long GetUnixTime(DateTime nowTime)
        {//获取时间戳
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (UploadWindow.uploading == true)
            {
                MessageBoxResult result = MessageBox.Show("下载器正在运行，确定关闭吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void btn_SelectData_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Title = "选择数据文件";
            openFile.Filter = "All File (*.*)|*.*";
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txt_Data.Text = openFile.FileName;
                localDataPath = openFile.FileName;
                MainWindow main = new MainWindow();
                //localDataName = main.SlipString(openFile.SafeFileName,".",true);
                localDataName = openFile.SafeFileName;
            }
        }

        private void btn_SelectImage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Title = "选择数据文件";
            openFile.Filter = "TIF File (*.TIF)|*.TIF|JP2 File (*.jp2)|*.jp2|HDF File (*.hdf)|*.hdf";
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //txt_Image.Text = openFile.FileName;

                MainWindow main = new MainWindow();
                string fileType = main.SlipString(openFile.FileName, ".", false);
                if (!(fileType.Equals("tif") || fileType.Equals("TIF")))
                {
                    long l1 = GetUnixTime(DateTime.Now);
                    string dirName = "C:/ImageData/" + l1;
                    Directory.CreateDirectory(dirName);

                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromFilePath(openFile.FileName);//创建图层所需的完整路径
                    IRaster raster = rasterLayer.Raster;
                    localImagePath = dirName/* + "/preview.TIF"*/;
                    int i = RasterSaveAs(raster, localImagePath);

                    if (i == 1)
                    {
                        MessageBox.Show("图片转化或压缩过程中出错！");
                        //txt_Image.Text = string.Empty;
                        return;
                    }
                }
                else
                    //localImagePath = txt_Image.Text.Trim();
                localImageName = openFile.SafeFileName;
            }
        }
        

        private void btn_Upload_Click(object sender, RoutedEventArgs e)
        {
            
            DateTime time = DateTime.Now;
            directory = localDataName;
            string sqlData = localDataName;
            string sqlImage = string.Empty;
            //if (txt_Image.Text.Trim() != string.Empty)
                sqlImage = "/" +directory /*+ "/preview.TIF"*/;
            int sqlTime = time.Year * 10000 + time.Month * 100 + time.Day;
            if (txt_Data.Text.Length == 0)
            {
                MessageBox.Show("传入的数据不能为空");
                return;
            }


            string s = "insert into Storage values('" + txt_StaffNumber.Text.Trim() + "', '" + txt_People.Text.Trim() + "', '" + sqlTime + "','" + cmb_Satellite.Text.Trim() + "', '" + cmb_Orbit.Text.Trim() + "', '" + sqlData + "','" + sqlImage + "')";
            UploadWindow upload = new UploadWindow(s);
            upload.Show();
            WindowInteropHelper parentHelper = new WindowInteropHelper(this);
            WindowInteropHelper childHelper = new WindowInteropHelper(upload);
            Win32Native.SetParent(childHelper.Handle, parentHelper.Handle);
            UploadWindow.Get(localDataPath, localDataName.Split('.')[0]);

            IWorkspaceFactory myWorkFact = new RasterWorkspaceFactoryClass();
            string rasterData = txt_Data.Text;
            IRasterWorkspace myRasterWorkspce = myWorkFact.OpenFromFile(System.IO.Path.GetDirectoryName(rasterData), 0) as IRasterWorkspace;
            IRasterDataset3 myRasterDataset3 = myRasterWorkspce.OpenRasterDataset(System.IO.Path.GetFileName(rasterData)) as IRasterDataset3;
            IRasterLayer myRasterLayer = new RasterLayerClass();
            myRasterLayer.CreateFromDataset(myRasterDataset3);
            IRasterProps myRasterProp = myRasterLayer.Raster as IRasterProps;


            string row = myRasterProp.Height.ToString();
            string clonmun = myRasterProp.Width.ToString();
            string pixeltype = myRasterProp.PixelType.ToString();
            string band = (myRasterLayer.Raster as IRasterBandCollection).Count.ToString();
            string compressionType = myRasterDataset3.CompressionType.ToString();
            //四个角点的最大最小坐标
            string XMax = myRasterProp.Extent.XMax.ToString();
            string XMin = myRasterProp.Extent.XMin.ToString();
            string YMax = myRasterProp.Extent.YMax.ToString();
            string YMin = myRasterProp.Extent.YMin.ToString();
            //四个角点的坐标
            string LowerLeftX = myRasterProp.Extent.LowerLeft.X.ToString();
            string LowerLeftY = myRasterProp.Extent.LowerLeft.Y.ToString();
            string UpperLeftX = myRasterProp.Extent.UpperLeft.X.ToString();
            string UpperLeftY = myRasterProp.Extent.UpperLeft.Y.ToString();
            string LowerRightX = myRasterProp.Extent.LowerRight.X.ToString();
            string LowerRightY = myRasterProp.Extent.LowerRight.Y.ToString();
            string UpperRightX = myRasterProp.Extent.UpperRight.X.ToString();
            string UpperRightY = myRasterProp.Extent.UpperRight.Y.ToString();
            //X和Y坐标的格网分辨率
            string pixelX = myRasterProp.MeanCellSize().X.ToString();
            string pixelY = myRasterProp.MeanCellSize().Y.ToString();

            //参考坐标信息

            ISpatialReference pSpatialReference = myRasterProp.SpatialReference;
            string spatial = pSpatialReference.Name.ToString();

            //投影坐标系

            IProjectedCoordinateSystem pcs = pSpatialReference as IProjectedCoordinateSystem;
            string coordinate = pcs.GeographicCoordinateSystem.Name;
            string datumplan = pcs.GeographicCoordinateSystem.Datum.Name;
            string spheroid = pcs.GeographicCoordinateSystem.Datum.Spheroid.Name;
            string projection = pcs.Projection.Name;
            string centralMeridian = pcs.get_CentralMeridian(true).ToString();
            string coordinateUnit = pcs.CoordinateUnit.Name.ToString();
            string scaleFactor = pcs.ScaleFactor.ToString();
            string sensorType = myRasterDataset3.SensorType.ToString();
            string format = myRasterDataset3.Format.ToString();
            string completeName = myRasterDataset3.CompleteName.ToString();



        }
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_RemoveImage_Click(object sender, RoutedEventArgs e)
        {
            //txt_Image.Text = string.Empty;
            localImagePath = string.Empty;
        }
    }
}

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
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace DataManager
{
    /// <summary>
    /// DataCard.xaml 的交互逻辑
    /// </summary>
    public partial class DataCard : UserControl
    {
        public DataCard()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeComponent();
        }

        private async void Btn_Preview_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Btn_Info_Click(object sender, RoutedEventArgs e)
        {
            
           
            datainfo nFrom = new datainfo();
            nFrom.Show();
        }

        private void Btn_Preview_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Interop;
using System.Net;
using System.IO;

namespace DataManager
{
    /// <summary>
    /// AddData.xaml 的交互逻辑
    /// </summary>
    public partial class AddData : Window
    {
        public AddData()
        {
            InitializeComponent();
            MainWindow main = new MainWindow();
            main.AddToCmb("select Satellite from SatelliteClass", cmb_Staellite);
            main.AddToCmb("select Orbit from SatelliteClass", cmb_Orbit);
            cmb_Staellite.Items.Add(" ");
            cmb_Orbit.Items.Add(" ");
            if (LoginForm.power == 1)
            {
                btn_ModifyData.Visibility = Visibility.Collapsed;
                btn_DeleteData.Visibility = Visibility.Collapsed;
            }
            AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage");
        }
        public int rowDataIndex;
        public static int id;
        private string sqlDataName;
        FtpWebRequest reqFTP;
        FtpWebResponse Response;
        private void Connect(string path)
        {
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(AddWindow.serverUser, AddWindow.serverPWD);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">ftp相对根目录下的路径</param>
        private void DeleteFile(string fileName)
        {
            try
            {
                string uri = "ftp://" + AddWindow.serverIP + fileName;//fileName中/开头
                Connect(uri);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                Response = (FtpWebResponse)reqFTP.GetResponse();
            }
            catch (Exception) { }
            finally
            {
                Response.Close();
            }
        }
        private void DeleteDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + AddWindow.serverIP + dirName;
                Connect(uri);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                Response = (FtpWebResponse)reqFTP.GetResponse();            }
            catch (Exception) { }
            finally
            {
                Response.Close();
            }
        }
        public void AddDataTable(string s)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlDataAdapter sdr = new SqlDataAdapter(s, conn);
            DataSet ds = new DataSet();
            sdr.Fill(ds);
            DG2.DataSource = ds.Tables[0];
            //DG2.Columns[0].ReadOnly = true;
            for (int i = 0; i < DG2.ColumnCount; i++)
            {
                DG2.Columns[i].Width = 164;
                DG2.Columns[i].ReadOnly = true;
            }
            conn.Close();
        }
        private void SelectData_Click(object sender, RoutedEventArgs e)
        {//dp初始值是空字符串
            int orgin = 0,end=99999999;
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
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length == 0 
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where People='" + txt_People.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length == 0 
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin+" and PhotosTime<="+end);
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length != 0
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Satellite='" + cmb_Staellite.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length == 0
                && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Orbit='" + cmb_Orbit.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length == 0
                && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='"+txt_People.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end+" and Staff_Number="+txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Satellite='" + cmb_Staellite + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin+ " and PhotosTime<=" + end + " and People='" + txt_People.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where People='" + txt_People.Text.Trim() + "' and Satellite='"+cmb_Staellite.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where People='" + txt_People.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" +end + " and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Satellite= '" + cmb_Staellite.Text.Trim() +"'and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and People='" + txt_People.Text.Trim() + "' and Staff_Number='"+txt_StaffNumber.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' and Satellite='"+cmb_Staellite.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Staff_Number=" + txt_StaffNumber.Text.Trim() + " and People='" + txt_People.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin+ " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number='" + txt_StaffNumber.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Staff_Number='" + txt_StaffNumber.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Satellite='" + cmb_Staellite.Text.Trim() + "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and Staff_Number='" + txt_StaffNumber.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where Satellite='" + cmb_Staellite.Text.Trim()+"' and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='"+txt_People.Text.Trim()+"'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where People='" + txt_People.Text.Trim()+ "' and Orbit='" + cmb_Orbit.Text.Trim() + "' and Satellite='" + cmb_Staellite.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length == 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end+ " and Orbit='" + cmb_Orbit.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length == 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + dp_OrginTime.Text.Trim() + " and PhotosTime<=" + dp_EndTime.Text.Trim() + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && time && cmb_Staellite.Text.Trim().Length != 0
               && cmb_Orbit.Text.Trim().Length != 0)
            {
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage where PhotosTime>=" + orgin + " and PhotosTime<=" + end + " and Satellite='" + cmb_Staellite.Text.Trim() + "' and People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim()+" and Orbit='"+cmb_Orbit.Text.Trim()+"'");
                return;
            }
            MessageBox.Show("没有您需要的内容","提示");
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            //AddForm af = new AddForm();
            //af.ShowDialog();
            AddWindow aw = new AddWindow();
            aw.Show();
            MainWindow main = new MainWindow();
            WindowInteropHelper parentHelper = new WindowInteropHelper(main);
            WindowInteropHelper childHelper = new WindowInteropHelper(aw);
            Win32Native.SetParent(childHelper.Handle, parentHelper.Handle);
        }

        private void btn_Modify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ModifyDataForm mdf = new ModifyDataForm();
                mdf.ShowDialog();
                AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage");
            }
            catch (Exception)
            { }
        }

        private void DG2_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                rowDataIndex = e.RowIndex;
                DataTable myDT = (DataTable)DG2.DataSource;
                DataRow myDR = myDT.Rows[rowDataIndex];
                id = Convert.ToInt32(myDR[0].ToString());
                sqlDataName = myDR[5].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("此处没有相关索引","提示");
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        { 
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            try
            {
                MessageBoxResult result = MessageBox.Show("确定删除这条数据吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SqlCommand comm = new SqlCommand("delete from Storage where id='" + id+"'", conn);
                    comm.ExecuteNonQuery();
                    AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage");
                    MainWindow main = new MainWindow();
                    string sqlDir = main.SlipString(sqlDataName, "/", true);
                    string sqlImageName = sqlDir + "/preview.TIF";
                    DeleteFile(sqlDataName);
                    DeleteFile(sqlImageName);
                    DeleteDir(sqlDir);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("索引处无数据，请重新选择", "提示");
            }
            finally
            {
                conn.Close();
            }
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            AddDataTable("select id as 序列号,Staff_Number as 职员编号,People as 入库人,PhotosTime as 拍摄时间,Satellite as 拍摄卫星,SatelliteData as 数据 from Storage");
        }
    }
}

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

namespace DataManager
{
    /// <summary>
    /// DownloadLogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadLogWindow : Window
    {
        public DownloadLogWindow()
        {
            InitializeComponent();
            if (LoginForm.power == 2)
                AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log ");
            else
            {
                AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where Staff_Number=" + LoginForm.staff_Number);
                txt_StaffNumber.Text = LoginForm.staff_Number.ToString();
                txt_People.Text = LoginForm.Namer;
                txt_StaffNumber.IsReadOnly = true;
                txt_People.IsReadOnly = true;
            }
        }
        private void AddLogTable(string s)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(s,conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DG4.DataSource = ds.Tables[0];
            for (int i = 0; i < DG4.ColumnCount; i++)
            {
                DG4.Columns[i].Width = 197;
                DG4.Columns[i].ReadOnly = true;
            }
            conn.Close();
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
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
            if (LoginForm.power == 2)
            {
                if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && !time)
                {
                    AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where Staff_Number="+txt_StaffNumber.Text.Trim());
                    return;
                }
                if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && !time)
                {
                    AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where People='" + txt_People.Text.Trim()+"'");
                    return;
                }
                if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && time)
                {
                    AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where Download_Time>=" + orgin+" and Download_Time<="+end);
                    return;
                }
                if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && !time)
                {
                    AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where People='" + txt_People.Text.Trim() + "' and Staff_Number="+txt_StaffNumber.Text.Trim());
                    return;
                }
                if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && time)
                {
                    AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where Download_Time>=" + orgin + " and Download_Time<=" + end+" and Staff_Number="+txt_StaffNumber.Text.Trim());
                    return;
                }
                if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && time)
                {
                    AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where Download_Time>=" + orgin + " and Download_Time<=" + end+" and People='"+txt_People.Text.Trim()+"'");
                    return;
                }
                if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && time)
                {
                    AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where Download_Time>=" + orgin + " and Download_Time<=" + end + " and People='" + txt_People.Text.Trim() + "' and Staff_Number="+txt_StaffNumber.Text.Trim());
                    return;
                }
                MessageBox.Show("已知条件下无下载记录","检索提示");
            }
            else
            {
                if(time)
                {
                    AddLogTable("select Id as 序号,Staff_Number as 职员编号,People as 下载人,Download_Time as 下载时间,SatelliteData as 下载数据 from Download_Log where Staff_Number=" + LoginForm.staff_Number+" and Download_Time>="+orgin+" and Download_Time<="+end);
                }
            }
        }
    }
}

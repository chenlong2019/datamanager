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
using System.Timers;

namespace DataManager
{
    /// <summary>
    /// AuditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AuditWindow : Window
    {
        public AuditWindow()
        {
            InitializeComponent();
            cmb_Opinion.Items.Add("同意");
            cmb_Opinion.Items.Add("待审核");
            cmb_Opinion.Items.Add("拒绝");
            cmb_Opinion.Items.Add("");
            AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Opinion='待审核'");
            timer.Elapsed += new ElapsedEventHandler(theout);//到达设定时间运行程序
            timer.AutoReset = true;//false：执行一次，true：一直执行
            timer.Enabled = true;
        }
        private int rowIndex;
        private Timer timer = new Timer(5000);//5s访问一次数据库
        private void AddOpinion(string s)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlDataAdapter sdr = new SqlDataAdapter(s, conn);
            DataSet ds = new DataSet();
            sdr.Fill(ds);
            DG3.DataSource = ds.Tables[0];
            for (int i = 0; i < DG3.Columns.Count; i++)
            {
                DG3.Columns[i].Width = 140;
                DG3.Columns[i].ReadOnly = true;
            }
            conn.Close();
        }
        private void theout(object sender, ElapsedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from Application where Opinion='待审核'",conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int aNumber = ds.Tables[0].Rows.Count;
            if (aNumber > 0 && aNumber < 100)
            {
                this.b_Opinion.Dispatcher.Invoke(new Action(delegate { b_Opinion.Visibility = Visibility.Visible; }));
                this.lb_ApplicationNumber.Dispatcher.Invoke(new Action(delegate
                {
                    lb_ApplicationNumber.Visibility = Visibility.Visible;
                    lb_ApplicationNumber.Content = aNumber;
                }));
            }
            else if (aNumber >= 100)
            {
                this.b_Opinion.Dispatcher.Invoke(new Action(delegate { b_Opinion.Visibility = Visibility.Visible; }));
                this.lb_ApplicationNumber.Dispatcher.Invoke(new Action(delegate
                {
                    lb_ApplicationNumber.Visibility = Visibility.Visible;
                    lb_ApplicationNumber.Content = "99+";
                }));
            }
            else
            {
                this.b_Opinion.Dispatcher.Invoke(new Action(delegate { b_Opinion.Visibility = Visibility.Collapsed; }));
                this.lb_ApplicationNumber.Dispatcher.Invoke(new Action(delegate
                {
                    lb_ApplicationNumber.Visibility = Visibility.Collapsed;
                    lb_ApplicationNumber.Content = string.Empty;
                }));
            }
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
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && cmb_Opinion.Text.Trim().Length == 0&&!time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && cmb_Opinion.Text.Trim().Length == 0&&!time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where People='" + txt_People.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && cmb_Opinion.Text.Trim().Length != 0&&!time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Opinion='" + cmb_Opinion.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && cmb_Opinion.Text.Trim().Length == 0 && !time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Application_Time>=" + orgin + " and Application_Time<="+end);
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && cmb_Opinion.Text.Trim().Length == 0&&!time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where People='" + txt_People.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && cmb_Opinion.Text.Trim().Length != 0&&!time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Opinion='" + cmb_Opinion.Text.Trim() + "' Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length == 0 && cmb_Opinion.Text.Trim().Length == 0 && time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Applivation_Time>=" + orgin+ " and Application_Time<="+end+" Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && cmb_Opinion.Text.Trim().Length != 0&&!time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where People='" + txt_People.Text.Trim() + "' and Opinion='" + cmb_Opinion.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && cmb_Opinion.Text.Trim().Length == 0 && time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where People='" + txt_People.Text.Trim() + "' and Application_Time>=" + orgin + " and Application_Time<="+end);
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length == 0 && cmb_Opinion.Text.Trim().Length != 0 && time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Application_Time>=" + orgin+" and Application_Time<="+end + " and Opinion='" + cmb_Opinion.Text.Trim() + "'");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && cmb_Opinion.Text.Trim().Length != 0&&!time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where People='" + txt_People.Text.Trim() + "' and Opinion='" + cmb_Opinion.Text.Trim() + "' and Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && cmb_Opinion.Text.Trim().Length == 0 && time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where People='" + txt_People.Text.Trim() + "' and Application_Time>="+orgin + " and Application_Time<="+end + " and Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0 && txt_People.Text.Trim().Length != 0 && cmb_Opinion.Text.Trim().Length != 0 && time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where People='" + txt_People.Text.Trim() + "' and Application_Time>=" + orgin + " and Application_Time<=" + end + " and Opinion='" +cmb_Opinion.Text.Trim()+"'" );
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length != 0 && txt_People.Text.Trim().Length != 0 && cmb_Opinion.Text.Trim().Length != 0 && time)
            {
                AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where People='" + txt_People.Text.Trim() + "' and Application_Time>=" + orgin + " and Application_Time<=" + end + " and Staff_Number=" + txt_StaffNumber.Text.Trim()+" and Opinion='"+cmb_Opinion.Text.Trim()+"'");
                return;
            }
            MessageBox.Show("查询结果不存在！", "查询提示");
        }
        private void DG3_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }
        private void btn_Agree_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable data = (DataTable)DG3.DataSource;
                DataRow row = data.Rows[rowIndex];
                int xh = Convert.ToInt32(row[0]);
                string s = "update Application set Opinion='同意' where Id=" + xh;
                SqlConnection conn = new SqlConnection(LoginForm.connString);
                conn.Open();
                SqlCommand comm = new SqlCommand(s, conn);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("没有要审核的内容！", "提示");
            }
            AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Opinion='待审核'");
        }
        private void btn_Disagree_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                DataTable data = (DataTable)DG3.DataSource;
                DataRow row = data.Rows[rowIndex];
                int xh = Convert.ToInt32(row[0]);
                string s = "update Application set Opinion='拒绝' where Id=" + xh;
                SqlConnection conn = new SqlConnection(LoginForm.connString);
                conn.Open();
                SqlCommand comm = new SqlCommand(s, conn);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("没有要审核的内容！", "提示");
            }
            AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Opinion='待审核'");
        }
        private void btn_Refersh_Click(object sender, RoutedEventArgs e)
        {
            AddOpinion("select Id as 序号,Staff_Number as 职员编号,People as 申请人,Application_Time as 申请时间,SatelliteData as 申请数据,Cause as 申请理由,Opinion as 审核意见 from Application where Opinion='待审核'");
        }
    }
}

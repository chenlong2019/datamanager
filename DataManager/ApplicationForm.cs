using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataManager
{
	// 更新测试
    public partial class ApplicationForm : Form
    {
        public ApplicationForm()
        {
            InitializeComponent();
        }

        private void Btn_Application_Click(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            if (main.DataRepeat("select * from Application where Staff_Number=" + LoginForm.staff_Number + " and SatelliteData='" + @MainWindow.loadFilePath.Trim()+ "' and Opinion='待审核'"))
            {
                MessageBox.Show("您的申请正在审核,请耐心等待","提示");
                return;
            }
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            string y = "待审核";
            int time = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
            SqlCommand comm = new SqlCommand("insert into Application values('" + LoginForm.staff_Number + "','" + time.ToString() + "','" +MainWindow.loadFilePath.Trim() + "','" + rtb_Opinion.Text.Trim() + "','" + y.Trim() + "','" + LoginForm.Namer + "')", conn);
            try
            {
                comm.ExecuteNonQuery();
                MessageBox.Show("发送成功,待审核！", "提示");
            }
            catch (Exception)
            {
                MessageBox.Show("发送失败！", "提示");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

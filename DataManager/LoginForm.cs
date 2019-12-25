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
using System.Configuration;

namespace DataManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        public static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString(),Namer;
        public bool ifom = false;
        public static int power = 1,staff_Number;
        private void Txt_Pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (txt_Pwd.Text.Trim().Length == 0)
            {
                return;
            }
            if (e.KeyValue == 13)
                Btn_Login_Click(null, null);
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            if (txt_UserName.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户名不能为空！", "登录提示");
                txt_UserName.Focus();
                return;
            }
            if (txt_Pwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("密码不能为空！", "登录提示");
                txt_Pwd.Focus();
                return;
            }
            if (txt_UserName.Text.Trim().Length != 0 && txt_Pwd.Text.Trim().Length != 0)
            {

                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                try
                {
                    string s = "select * from [User] where UserName='" + txt_UserName.Text.Trim() + "' and Pwd='" + txt_Pwd.Text.Trim() + "'";
                    SqlCommand com = new SqlCommand(s, conn);
                    SqlDataReader sdr = com.ExecuteReader();
                    bool ll = sdr.Read();
                    power = Convert.ToInt32(sdr[2].ToString().Trim());//读权限
                    staff_Number = Convert.ToInt32(sdr[3].ToString().Trim());//读取职员编号
                    Namer = sdr[4].ToString().Trim();
                    conn.Close();
                    if (ll)
                    {
                        ifom = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("账号或者密码不正确！", "登录提示");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("账号或者密码不正确！", "登录提示");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}

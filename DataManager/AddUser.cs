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
// 增加用户
namespace DataManager
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
            cmb_Power.Items.Add(1);
            cmb_Power.Items.Add(2);
        }

        private void Btn_Join_Click(object sender, EventArgs e)
        {
            if (txt_UserName.Text.Trim().Length == 0 || txt_UserName.Text.Trim().Length > 20)
            {
                MessageBox.Show("用户名设置不合规范（请保证在1-20字符区间）！", "注册提示");
                txt_UserName.Focus();
                return;
            }
            if (txt_Pwd.Text.Trim().Length == 0 || txt_Pwd.Text.Trim().Length > 20)
            {
                MessageBox.Show("密码设置不合规范（请保证在1-20字符区间）！", "注册提示");
                txt_Pwd.Focus();
                return;
            }
            if (!(txt_Pwd.Text.Trim().Equals(txt_Pwd1.Text.Trim())))
            {
                MessageBox.Show("二次密码不相同", "注册提示");
                txt_Pwd1.Focus();
                txt_Pwd1.SelectAll();
                return;
            }
            if (cmb_Power.Text.Trim().Length == 0)
            {
                MessageBox.Show("权限不能为空", "注册提示");
                return;
            }
            if (txt_StaffNumber.Text.Trim().Length == 0)
            {
                MessageBox.Show("职员编号不能为空", "注册提示");
                txt_StaffNumber.Focus();
                return;
            }
            if (txt_Name.Text.Trim().Length == 0 || txt_Name.Text.Trim().Length > 10)
            {
                MessageBox.Show("职员姓名不合规范（请保证字符在1-10字符区间）", "注册提示");
                txt_Name.Focus();
                return;
            }
            if (txt_UserName.Text.Trim().Length > 0 || txt_UserName.Text.Trim().Length > 20)
            {
                MainWindow main = new MainWindow();
                if (main.DataRepeat("select * from [User] where UserName='" + txt_UserName.Text.Trim() + "' or Staff_Number=" + txt_StaffNumber.Text.Trim()))
                {
                    MessageBox.Show("用户名已存在,或者该职员已有账号", "注册提示");
                    txt_UserName.Focus();
                    txt_UserName.SelectAll();
                    return;
                }
            }
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlCommand comm = new SqlCommand("insert into [User] values('" + txt_UserName.Text.Trim() + "','" + txt_Pwd.Text.Trim() + "','" + cmb_Power.Text.Trim() + "','" + txt_StaffNumber.Text.Trim() + "','" + txt_Name.Text.Trim() + "')", conn);
            try
            {
                comm.ExecuteNonQuery();
                //MessageBox.Show("注册成功！", "注册提示");
                DialogResult result = MessageBox.Show("注册成功，是否继续添加？", "注册提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    txt_UserName.Text = string.Empty;
                    txt_Pwd.Text = string.Empty;
                    txt_Pwd1.Text = string.Empty;
                    txt_StaffNumber.Text = string.Empty;
                    txt_Name.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class ModifyForm : Form
    {
        public ModifyForm()
        {
            InitializeComponent();
            cmb_Power.Items.Add(1);
            cmb_Power.Items.Add(2);
            txt_UserName.Text = AddUserNameForm.userName;
            txt_UserName.ReadOnly = true;
            txt_Pwd.Text = AddUserNameForm.pwd;
            cmb_Power.Text = AddUserNameForm.power.ToString();
            txt_StaffNumber.Text = AddUserNameForm.staffNumber.ToString();
            txt_Name.Text = AddUserNameForm.name;
        }
        public void AddToCmbForm(string s, ComboBox cmb)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlDataAdapter sdr = new SqlDataAdapter(s, conn);
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
        private void Btn_Modify_Click(object sender, EventArgs e)
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.UpdateSql("update [User] set Pwd='" + txt_Pwd.Text.Trim() + "',Power=" + cmb_Power.Text.Trim() + ",Staff_Number=" + txt_StaffNumber.Text.Trim() + ",Name='" + txt_Name.Text.Trim() + "' where UserName='" + AddUserNameForm.userName + "'");
                MessageBox.Show("修改成功","提示");
            }
            catch (Exception)
            {
                MessageBox.Show("修改失败！", "修改提示");
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

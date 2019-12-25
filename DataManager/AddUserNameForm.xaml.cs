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
    /// AddUserNameForm.xaml 的交互逻辑
    /// </summary>
    public partial class AddUserNameForm : Window
    {
        public AddUserNameForm()
        {
            InitializeComponent();
            AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User]");
        }
        private int rowIndex;
        public static string userName;
        public static string pwd;
        public static int power;
        public static int staffNumber;
        public static string name;
        private void AddUserTable(string s)
        {
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(s, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DG1.DataSource = ds.Tables[0];
            //DG1.RowHeadersVisible = false;
            for (int i = 0; i < DG1.ColumnCount; i++)
            {
                DG1.Columns[i].Width = 194;
                DG1.Columns[i].ReadOnly = true;
            }
            conn.Close();
        }
        private void btn_SelectUser_Click(object sender, RoutedEventArgs e)
        {
            if (txt_UserName.Text.Trim().Length != 0 && txt_Name.Text.Trim().Length == 0 && txt_StaffNumber.Text.Trim().Length == 0)
            {
                AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User] where UserName='"+txt_UserName.Text.Trim()+"'");
                return;
            }
            if (txt_UserName.Text.Trim().Length != 0 && txt_Name.Text.Trim().Length != 0 && txt_StaffNumber.Text.Trim().Length == 0)
            {
                AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User] where UserName='"+txt_UserName+"' and Name='"+txt_Name.Text.Trim()+"'");
                return;
            }
            if (txt_UserName.Text.Trim().Length != 0 && txt_Name.Text.Trim().Length != 0 && txt_StaffNumber.Text.Trim().Length != 0)
            {
                AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User] where UserName='" + txt_UserName + "' and Name='" + txt_Name.Text.Trim() + "' and Staff_Number="+txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_UserName.Text.Trim().Length == 0 && txt_Name.Text.Trim().Length != 0 && txt_StaffNumber.Text.Trim().Length == 0)
            {
                AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User] where Name='" + txt_Name.Text.Trim() + "'");
                return;
            }
            if (txt_UserName.Text.Trim().Length == 0 && txt_Name.Text.Trim().Length == 0 && txt_StaffNumber.Text.Trim().Length != 0)
            {
                AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User] where Staff_Number=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_UserName.Text.Trim().Length != 0 && txt_Name.Text.Trim().Length == 0 && txt_StaffNumber.Text.Trim().Length != 0)
            {
                AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User] where UserName='" + txt_UserName.Text.Trim() + "' and StaffNumber=" + txt_StaffNumber.Text.Trim());
                return;
            }
            if (txt_UserName.Text.Trim().Length == 0 && txt_Name.Text.Trim().Length != 0 && txt_StaffNumber.Text.Trim().Length != 0)
            {
                AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User] where Name='" + txt_Name.Text.Trim() + "' and StaffNamber=" + txt_StaffNumber.Text.Trim());
                return;
            }
            MessageBox.Show("查询结果不存在！","查询提示");
        }

        private void DG1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void btn_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable myDT = (DataTable)DG1.DataSource;
                DataRow myDR = myDT.Rows[rowIndex];
                MessageBoxResult result = MessageBox.Show("确定删除用户：" + myDR[0].ToString() + " 吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(LoginForm.connString);
                    conn.Open();
                    SqlCommand comm = new SqlCommand("delete from [User] where UserName='" + myDR[0].ToString().Trim() + "'", conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User]");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("索引处无数据，请重新选择", "提示");
            }
        }

        private void btn_JoinUser(object sender, RoutedEventArgs e)
        {
            AddUser adForm = new AddUser();
            adForm.ShowDialog();
            AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User]");
        }

        private void btn_Modify_Click(object sender, RoutedEventArgs e)
        {
            DataTable myDT = (DataTable)DG1.DataSource;
            DataRow myDR = myDT.Rows[rowIndex];
            userName = myDR[0].ToString().Trim();
            pwd = myDR[1].ToString().Trim();
            power = Convert.ToInt32(myDR[2].ToString().Trim());
            staffNumber = Convert.ToInt32(myDR[3].ToString().Trim());
            name = myDR[4].ToString().Trim();
            ModifyForm mfForm = new ModifyForm();
            mfForm.ShowDialog();
            AddUserTable("select UserName as 用户名,Pwd as 密码,Power as 权限,Staff_Number as 职员编号,People as 姓名 from [User]");
        }
    }
}

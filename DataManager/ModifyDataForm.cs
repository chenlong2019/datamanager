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
    public partial class ModifyDataForm : Form
    {
        public ModifyDataForm()
        {
            InitializeComponent();
            ModifyForm mf = new ModifyForm();
            mf.AddToCmbForm("select Satellite from SatelliteClass ",cmb_Satellite);
            mf.AddToCmbForm("select Orbit from SatelliteClass ", cmb_Orbit);
            SqlConnection conn = new SqlConnection(LoginForm.connString);
            conn.Open();
            try
            {
                string s = "select * from Storage where id=" +AddData.id ;
                SqlDataAdapter sda = new SqlDataAdapter(s, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable myDT = ds.Tables[0];
                DataRow myDR = myDT.Rows[0];
                conn.Close();
                txt_StaffNumber.Text = myDR[1].ToString();
                txt_People.Text = myDR[2].ToString();
                txt_Time.Text = myDR[3].ToString();
                cmb_Satellite.Text = myDR[4].ToString();
                cmb_Orbit.Text = myDR[5].ToString();
                txt_DataPath.Text = myDR[6].ToString();
            }
            catch (Exception)
            {
                conn.Close();
                MessageBox.Show("尚未选择需要修改的内容","提示");
                this.Close();
            }
            conn.Close();
        }
        private void Btn_Modify_Click(object sender, EventArgs e)
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.UpdateSql("update Storage set Staff_Number=" + txt_StaffNumber.Text.Trim() + ",People='" + txt_People.Text.Trim() + "',PhotosTime=" + txt_Time.Text.Trim() + ",Satellite='" + cmb_Satellite.Text.Trim() +"',Orbit='"+cmb_Orbit.Text.Trim()+"',SatelliteData='"+txt_DataPath.Text.Trim() +"' where id=" + AddData.id);
                MessageBox.Show("修改成功", "提示");
            }
            catch (Exception )
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

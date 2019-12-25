namespace DataManager
{
    partial class ModifyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.txt_Pwd = new System.Windows.Forms.TextBox();
            this.cmb_Power = new System.Windows.Forms.ComboBox();
            this.txt_StaffNumber = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "权限：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "职员编号：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "姓名：";
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(102, 21);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(241, 21);
            this.txt_UserName.TabIndex = 6;
            this.txt_UserName.Tag = "1";
            // 
            // txt_Pwd
            // 
            this.txt_Pwd.Location = new System.Drawing.Point(102, 85);
            this.txt_Pwd.Name = "txt_Pwd";
            this.txt_Pwd.Size = new System.Drawing.Size(241, 21);
            this.txt_Pwd.TabIndex = 7;
            this.txt_Pwd.Tag = "2";
            // 
            // cmb_Power
            // 
            this.cmb_Power.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Power.FormattingEnabled = true;
            this.cmb_Power.Location = new System.Drawing.Point(102, 149);
            this.cmb_Power.Name = "cmb_Power";
            this.cmb_Power.Size = new System.Drawing.Size(241, 20);
            this.cmb_Power.TabIndex = 8;
            this.cmb_Power.Tag = "3";
            // 
            // txt_StaffNumber
            // 
            this.txt_StaffNumber.Location = new System.Drawing.Point(102, 213);
            this.txt_StaffNumber.Name = "txt_StaffNumber";
            this.txt_StaffNumber.Size = new System.Drawing.Size(241, 21);
            this.txt_StaffNumber.TabIndex = 9;
            this.txt_StaffNumber.Tag = "4";
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(102, 277);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(241, 21);
            this.txt_Name.TabIndex = 10;
            this.txt_Name.Tag = "5";
            // 
            // btn_Modify
            // 
            this.btn_Modify.Location = new System.Drawing.Point(102, 315);
            this.btn_Modify.Name = "btn_Modify";
            this.btn_Modify.Size = new System.Drawing.Size(75, 33);
            this.btn_Modify.TabIndex = 11;
            this.btn_Modify.Tag = "6";
            this.btn_Modify.Text = "修改";
            this.btn_Modify.UseVisualStyleBackColor = true;
            this.btn_Modify.Click += new System.EventHandler(this.Btn_Modify_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(268, 315);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 33);
            this.btn_Close.TabIndex = 12;
            this.btn_Close.Tag = "7";
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // ModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 360);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Modify);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.txt_StaffNumber);
            this.Controls.Add(this.cmb_Power);
            this.Controls.Add(this.txt_Pwd);
            this.Controls.Add(this.txt_UserName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改用户信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.TextBox txt_Pwd;
        private System.Windows.Forms.ComboBox cmb_Power;
        private System.Windows.Forms.TextBox txt_StaffNumber;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_Close;
    }
}
namespace DataManager
{
    partial class AddUser
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.txt_Pwd = new System.Windows.Forms.TextBox();
            this.txt_StaffNumber = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.cmb_Power = new System.Windows.Forms.ComboBox();
            this.btn_Join = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Pwd1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(171, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "注册！";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 99;
            this.label2.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 100;
            this.label3.Text = "密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 103;
            this.label4.Text = "权限：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 104;
            this.label5.Text = "职员编号：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(72, 297);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 105;
            this.label6.Text = "姓名：";
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(109, 54);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(235, 21);
            this.txt_UserName.TabIndex = 1;
            this.txt_UserName.Tag = "";
            // 
            // txt_Pwd
            // 
            this.txt_Pwd.Location = new System.Drawing.Point(109, 102);
            this.txt_Pwd.Name = "txt_Pwd";
            this.txt_Pwd.PasswordChar = '*';
            this.txt_Pwd.Size = new System.Drawing.Size(235, 21);
            this.txt_Pwd.TabIndex = 2;
            this.txt_Pwd.Tag = "";
            // 
            // txt_StaffNumber
            // 
            this.txt_StaffNumber.Location = new System.Drawing.Point(109, 246);
            this.txt_StaffNumber.Name = "txt_StaffNumber";
            this.txt_StaffNumber.Size = new System.Drawing.Size(235, 21);
            this.txt_StaffNumber.TabIndex = 5;
            this.txt_StaffNumber.Tag = "";
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(109, 294);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(234, 21);
            this.txt_Name.TabIndex = 6;
            this.txt_Name.Tag = "";
            // 
            // cmb_Power
            // 
            this.cmb_Power.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Power.FormattingEnabled = true;
            this.cmb_Power.Location = new System.Drawing.Point(110, 198);
            this.cmb_Power.Name = "cmb_Power";
            this.cmb_Power.Size = new System.Drawing.Size(235, 20);
            this.cmb_Power.TabIndex = 4;
            this.cmb_Power.Tag = "";
            // 
            // btn_Join
            // 
            this.btn_Join.Location = new System.Drawing.Point(109, 354);
            this.btn_Join.Name = "btn_Join";
            this.btn_Join.Size = new System.Drawing.Size(75, 37);
            this.btn_Join.TabIndex = 7;
            this.btn_Join.Tag = "";
            this.btn_Join.Text = "注册";
            this.btn_Join.UseVisualStyleBackColor = true;
            this.btn_Join.Click += new System.EventHandler(this.Btn_Join_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(268, 354);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 37);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.Tag = "";
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 101;
            this.label7.Text = "确认密码：";
            // 
            // txt_Pwd1
            // 
            this.txt_Pwd1.Location = new System.Drawing.Point(109, 150);
            this.txt_Pwd1.Name = "txt_Pwd1";
            this.txt_Pwd1.PasswordChar = '*';
            this.txt_Pwd1.Size = new System.Drawing.Size(235, 21);
            this.txt_Pwd1.TabIndex = 3;
            this.txt_Pwd1.Tag = "";
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 424);
            this.Controls.Add(this.txt_Pwd1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Join);
            this.Controls.Add(this.cmb_Power);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.txt_StaffNumber);
            this.Controls.Add(this.txt_Pwd);
            this.Controls.Add(this.txt_UserName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册用户";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.TextBox txt_Pwd;
        private System.Windows.Forms.TextBox txt_StaffNumber;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.ComboBox cmb_Power;
        private System.Windows.Forms.Button btn_Join;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Pwd1;
    }
}
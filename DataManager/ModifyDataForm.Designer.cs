namespace DataManager
{
    partial class ModifyDataForm
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
            this.txt_StaffNumber = new System.Windows.Forms.TextBox();
            this.txt_People = new System.Windows.Forms.TextBox();
            this.txt_Time = new System.Windows.Forms.TextBox();
            this.cmb_Satellite = new System.Windows.Forms.ComboBox();
            this.txt_DataPath = new System.Windows.Forms.TextBox();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.cmb_Orbit = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "职员编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "入库人：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "拍摄时间：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "拍摄卫星：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "轨道：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 348);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "数据：";
            // 
            // txt_StaffNumber
            // 
            this.txt_StaffNumber.Location = new System.Drawing.Point(97, 30);
            this.txt_StaffNumber.Name = "txt_StaffNumber";
            this.txt_StaffNumber.Size = new System.Drawing.Size(270, 21);
            this.txt_StaffNumber.TabIndex = 6;
            // 
            // txt_People
            // 
            this.txt_People.Location = new System.Drawing.Point(97, 93);
            this.txt_People.Name = "txt_People";
            this.txt_People.Size = new System.Drawing.Size(270, 21);
            this.txt_People.TabIndex = 7;
            // 
            // txt_Time
            // 
            this.txt_Time.Location = new System.Drawing.Point(97, 156);
            this.txt_Time.Name = "txt_Time";
            this.txt_Time.Size = new System.Drawing.Size(270, 21);
            this.txt_Time.TabIndex = 8;
            // 
            // cmb_Satellite
            // 
            this.cmb_Satellite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Satellite.FormattingEnabled = true;
            this.cmb_Satellite.Location = new System.Drawing.Point(97, 219);
            this.cmb_Satellite.Name = "cmb_Satellite";
            this.cmb_Satellite.Size = new System.Drawing.Size(270, 20);
            this.cmb_Satellite.TabIndex = 9;
            // 
            // txt_DataPath
            // 
            this.txt_DataPath.Location = new System.Drawing.Point(97, 345);
            this.txt_DataPath.Name = "txt_DataPath";
            this.txt_DataPath.Size = new System.Drawing.Size(270, 21);
            this.txt_DataPath.TabIndex = 11;
            // 
            // btn_Modify
            // 
            this.btn_Modify.Location = new System.Drawing.Point(97, 390);
            this.btn_Modify.Name = "btn_Modify";
            this.btn_Modify.Size = new System.Drawing.Size(75, 37);
            this.btn_Modify.TabIndex = 12;
            this.btn_Modify.Text = "修改";
            this.btn_Modify.UseVisualStyleBackColor = true;
            this.btn_Modify.Click += new System.EventHandler(this.Btn_Modify_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(292, 390);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 37);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // cmb_Orbit
            // 
            this.cmb_Orbit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Orbit.FormattingEnabled = true;
            this.cmb_Orbit.Location = new System.Drawing.Point(97, 282);
            this.cmb_Orbit.Name = "cmb_Orbit";
            this.cmb_Orbit.Size = new System.Drawing.Size(270, 20);
            this.cmb_Orbit.TabIndex = 14;
            // 
            // ModifyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 455);
            this.Controls.Add(this.cmb_Orbit);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Modify);
            this.Controls.Add(this.txt_DataPath);
            this.Controls.Add(this.cmb_Satellite);
            this.Controls.Add(this.txt_Time);
            this.Controls.Add(this.txt_People);
            this.Controls.Add(this.txt_StaffNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifyDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改";
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
        private System.Windows.Forms.TextBox txt_StaffNumber;
        private System.Windows.Forms.TextBox txt_People;
        private System.Windows.Forms.TextBox txt_Time;
        private System.Windows.Forms.ComboBox cmb_Satellite;
        private System.Windows.Forms.TextBox txt_DataPath;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.ComboBox cmb_Orbit;
    }
}
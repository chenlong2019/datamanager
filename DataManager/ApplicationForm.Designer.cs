namespace DataManager
{
    partial class ApplicationForm
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
            this.rtb_Opinion = new System.Windows.Forms.RichTextBox();
            this.btn_Application = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入申请下载该数据的原因：";
            // 
            // rtb_Opinion
            // 
            this.rtb_Opinion.Location = new System.Drawing.Point(12, 39);
            this.rtb_Opinion.Name = "rtb_Opinion";
            this.rtb_Opinion.Size = new System.Drawing.Size(473, 386);
            this.rtb_Opinion.TabIndex = 1;
            this.rtb_Opinion.Text = "";
            // 
            // btn_Application
            // 
            this.btn_Application.Location = new System.Drawing.Point(184, 443);
            this.btn_Application.Name = "btn_Application";
            this.btn_Application.Size = new System.Drawing.Size(89, 31);
            this.btn_Application.TabIndex = 2;
            this.btn_Application.Text = "提交申请";
            this.btn_Application.UseVisualStyleBackColor = true;
            this.btn_Application.Click += new System.EventHandler(this.Btn_Application_Click);
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 500);
            this.Controls.Add(this.btn_Application);
            this.Controls.Add(this.rtb_Opinion);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "申请";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtb_Opinion;
        private System.Windows.Forms.Button btn_Application;
    }
}
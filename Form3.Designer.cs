namespace ScollDemo
{
    partial class Form3
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
            this.cbxGraphType = new System.Windows.Forms.ComboBox();
            this.customPanel1 = new ScollDemo.CustomPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择类型：";
            // 
            // cbxGraphType
            // 
            this.cbxGraphType.FormattingEnabled = true;
            this.cbxGraphType.Location = new System.Drawing.Point(95, 27);
            this.cbxGraphType.Name = "cbxGraphType";
            this.cbxGraphType.Size = new System.Drawing.Size(121, 20);
            this.cbxGraphType.TabIndex = 4;
            // 
            // customPanel1
            // 
            this.customPanel1.Location = new System.Drawing.Point(17, 75);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(953, 415);
            this.customPanel1.TabIndex = 3;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(816, 24);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(99, 23);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "开始编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 510);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxGraphType);
            this.Controls.Add(this.customPanel1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxGraphType;
        private CustomPanel customPanel1;
        private System.Windows.Forms.Button btnEdit;
    }
}
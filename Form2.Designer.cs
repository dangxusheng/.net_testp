namespace ScollDemo
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.cbxGraphType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.customPanel1 = new ScollDemo.CustomPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxGraphType
            // 
            this.cbxGraphType.FormattingEnabled = true;
            this.cbxGraphType.Location = new System.Drawing.Point(106, 27);
            this.cbxGraphType.Name = "cbxGraphType";
            this.cbxGraphType.Size = new System.Drawing.Size(121, 20);
            this.cbxGraphType.TabIndex = 1;
            this.cbxGraphType.SelectedIndexChanged += new System.EventHandler(this.cbxGraphType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择类型：";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // customPanel1
            // 
            this.customPanel1.Location = new System.Drawing.Point(28, 75);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(953, 415);
            this.customPanel1.TabIndex = 0;
            this.customPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.customPanel1_Paint);
            this.customPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.customPanel1_MouseDown);
            this.customPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.customPanel1_MouseMove);
            this.customPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.customPanel1_MouseUp);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(779, 25);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(99, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "开始编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 511);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxGraphType);
            this.Controls.Add(this.customPanel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomPanel customPanel1;
        private System.Windows.Forms.ComboBox cbxGraphType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnEdit;
    }
}
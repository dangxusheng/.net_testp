using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScollDemo
{
    public partial class Form1 : Form
    {
        object lockObject = new object();

        //GDI+实现
        static List<CustomClass> scollTxtList2 = new List<CustomClass>();
        static List<CustomClass> hasScollOutList2 = new List<CustomClass>();
        Font strFont = new Font("Courier New", 12, FontStyle.Regular);
        Brush strBrush = new SolidBrush(Color.White);

        //label实现
        static List<Label> scollTxtList = new List<Label>();
        static List<Label> hasScollOutList = new List<Label>();
        int height = 20;

        int n = 1;

        protected Color[] colorArr = new Color[] { Color.Red, Color.PowderBlue, Color.SeaShell, Color.SlateGray };


        public Form1()
        {
            InitializeComponent();
            this.customPanel1.BorderStyle = BorderStyle.None;
            this.customPanel1.BackColor = Color.Black;
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //label实现滚动
            //lock (lockObject)
            //{
            //    if (scollTxtList.Count() == 0) n = 0;

            //    n++;
            //    string sendTxt = this.textBox1.Text.Trim();
            //    Label showLb = new Label()
            //    {
            //        Text = sendTxt,
            //        Height = height,
            //        AutoSize = true,
            //        TextAlign = ContentAlignment.MiddleCenter,
            //        FlatStyle = FlatStyle.Flat,
            //        BackColor = this.customPanel1.BackColor,
            //        ForeColor = Color.White,
            //        Location = new System.Drawing.Point(this.customPanel1.Left, this.customPanel1.Top + height * (n - 1) + 5)
            //    };
            //    scollTxtList.Add(showLb);
            //}
            //if (scollTxtList.Count() > 0)
            //    this.timer1.Start();


            //GDI+实现滚动
            lock (lockObject)
            {
                if (scollTxtList2.Count() == 0) n = 0;

                n++;
                string sendTxt = this.textBox1.Text.Trim();
                var locationY = this.customPanel1.Top + height * (n - 1) + 5;
                if (locationY + strFont.Height > this.customPanel1.Height) n = 1;
                scollTxtList2.Add(new CustomClass()
                {
                    txt = sendTxt,
                    location = new System.Drawing.Point(this.customPanel1.Left, this.customPanel1.Top + height * (n - 1) + 5)
                });
            }
            if (scollTxtList2.Count() > 0)
                this.timer1.Start();
        }

        protected void beginChangeBorderColor(Graphics gp)
        {
            Random dm = new Random();
            int n = dm.Next(0, 3);
            gp.DrawRectangle(new Pen(colorArr[n], 10), 0, 0, this.customPanel1.Width, this.customPanel1.Height);
        }

        private void beginScoll()
        {
            var list = scollTxtList.ToList();
            if (list.Count() == 0)
            {
                this.timer1.Stop();
                MessageBox.Show("没有可滚动的文字了...");
                return;
            }
            foreach (var lb in list)
            {
                if (!this.Controls.Contains(lb))
                {
                    this.Controls.Add(lb);
                    lb.BringToFront();
                }

                lb.Left += 1;
                if (lb.Left == this.customPanel1.Right - this.customPanel1.Left)
                {
                    lb.SendToBack();
                    this.Controls.Remove(lb);
                    scollTxtList.Remove(lb);
                    hasScollOutList.Add(lb);
                }
            }
        }
        /// <summary>
        /// 计时器--开始滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //beginScoll();

            //this.customPanel1.Invalidate(new Rectangle(0, 0, areaWidth, areaHeight));
            this.customPanel1.Invalidate();
        }

        private void customPanel1_Paint(object sender, PaintEventArgs e)
        {
            //beginChangeBorderColor(e.Graphics);
            if (this.timer1.Enabled && scollTxtList2.Count() == 0)
            {
                this.timer1.Stop();
                MessageBox.Show("没有可滚动的文字了...");
                return;
            }
            for (int i = 0; i < scollTxtList2.Count(); i++)
            {
                var item = scollTxtList2[i];
                item.location = new System.Drawing.Point(item.location.X + 2, item.location.Y);
                if (item.location.X >= this.customPanel1.Width)
                {
                    scollTxtList2.Remove(item);
                }
                e.Graphics.DrawString(item.txt, strFont, strBrush, item.location);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.customPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customPanel1.Refresh();
        }
    }


    public class CustomClass
    {
        public string txt { get; set; }
        public Point location { get; set; }
    }
}

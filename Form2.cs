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
    public partial class Form2 : Form
    {
        bool isDrawing = false;
        bool isEditStatus = false;
        bool selectedStartPoint = false;
        bool selectedEndPoint = false;
        bool selectedLine = false;
        bool selectedRect = false;
        bool selectedPolygon = false;
        bool selectedVertex = false;

        Point mouseDownPoint = Point.Empty;

        Point startPoint = Point.Empty;
        Point endPoint = Point.Empty;
        List<Point> pointList = new List<Point>();

        List<GraphicTypeClass> GraphicTypeList = new List<GraphicTypeClass>();
        GraphicTypeClass selectedType;

        public Form2()
        {
            InitializeComponent();

            GraphicTypeList.Add(new GraphicTypeClass() { type = "直线", val = 0 });
            GraphicTypeList.Add(new GraphicTypeClass() { type = "矩形", val = 1 });
            GraphicTypeList.Add(new GraphicTypeClass() { type = "多边形", val = 2 });
            GraphicTypeList.Add(new GraphicTypeClass() { type = "园", val = 3 });

            this.cbxGraphType.Items.AddRange(GraphicTypeList.ToArray());
            this.cbxGraphType.SelectedIndex = 0;

            this.customPanel1.BackColor = Color.White;
            this.customPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            this.timer1.Start();
        }

        public void draw(Graphics g)
        {
            if ((startPoint != Point.Empty && endPoint != Point.Empty)
                            || pointList.Count() > 2)
                switch (selectedType.val)
                {
                    case 0: //直线
                        g.DrawLine(new Pen(Color.Red, 2), startPoint, endPoint);
                        break;
                    case 1: //矩形
                        g.DrawRectangle(new Pen(Color.Red, 2), startPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                        break;
                    case 3: //园
                        g.DrawEllipse(new Pen(Color.Red, 2), new Rectangle(startPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y)));
                        break;
                    case 2: //多边形
                        g.DrawPolygon(new Pen(Color.Red, 2), pointList.ToArray());
                        break;
                    default:
                        break;
                }
        }
        private void customPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isDrawing && !isEditStatus)
                {
                    switch (selectedType.val)
                    {
                        case 0: //直线
                        case 1: //矩形
                        case 3: //园
                            startPoint = new Point(e.X, e.Y);
                            break;
                        case 2:
                            pointList.Add(new Point(e.X, e.Y));
                            break;
                        default:
                            break;
                    }
                }
                if (isEditStatus)
                {
                    resetOperation();
                    mouseDownPoint = new Point(e.X, e.Y);
                    switch (selectedType.val)
                    {
                        case 0: //直线

                            if (GraphicHelper.GetPointIsInLine(mouseDownPoint, startPoint, endPoint, 1))
                            {
                                selectedLine = true;
                                //MessageBox.Show("选中线了");
                            }
                            else if (GraphicHelper.IsThisPoint(mouseDownPoint, startPoint, 1)) selectedStartPoint = true;
                            else if (GraphicHelper.IsThisPoint(mouseDownPoint, endPoint, 1)) selectedEndPoint = true;
                            break;
                        case 1: //矩形
                            pointList.Add(startPoint);
                            pointList.Add(new Point(endPoint.X, startPoint.Y));
                            pointList.Add(endPoint);
                            pointList.Add(new Point(startPoint.X, endPoint.Y));
                            if (GraphicHelper.IsPointInPolygon(mouseDownPoint, pointList.ToArray()))
                            {
                                selectedRect = true;
                                //MessageBox.Show("选中面了");
                                pointList.Clear();
                            }
                            break;
                        case 3: //园
                            break;
                        case 2:  //多边形
                            if (GraphicHelper.IsPointInPolygon(mouseDownPoint, pointList.ToArray()))
                            {
                                selectedPolygon = true;
                                //MessageBox.Show("选中面了");
                            }
                            else if (GraphicHelper.IsGraphVertex(ref pointList, mouseDownPoint))
                            {
                                selectedVertex = true;
                                //MessageBox.Show("选中顶点了");
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void customPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isDrawing && !isEditStatus)
                {
                    switch (selectedType.val)
                    {
                        case 0: //直线
                        case 1: //矩形
                        case 3: //园
                            endPoint = new Point(e.X, e.Y);
                            break;
                        case 2:
                            pointList.Add(new Point(e.X, e.Y));
                            break;
                        default:
                            break;
                    }
                }
                if (isEditStatus)
                {
                    if (selectedLine)
                        GraphicHelper.moveLine(ref startPoint, ref endPoint, ref mouseDownPoint, new Point(e.X, e.Y));
                    else if (selectedStartPoint) startPoint = new Point(e.X, e.Y);
                    else if (selectedEndPoint) endPoint = new Point(e.X, e.Y);
                    else if (selectedRect) GraphicHelper.moveRect(ref startPoint, ref endPoint, ref mouseDownPoint, new Point(e.X, e.Y));
                    else if (selectedVertex) GraphicHelper.modifyVertex(ref pointList, ref mouseDownPoint, new Point(e.X, e.Y));
                    else if (selectedPolygon) GraphicHelper.movePolygon(ref pointList, ref mouseDownPoint, new Point(e.X, e.Y));
                }
            }
        }

        private void customPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isDrawing && !isEditStatus)
                {
                    switch (selectedType.val)
                    {
                        case 0: //直线
                        case 1: //矩形
                        case 3: //园
                            endPoint = new Point(e.X, e.Y);
                            break;
                        case 2: //多边形
                            pointList.Add(new Point(e.X, e.Y));
                            break;
                        default:
                            break;
                    }
                }
                if (isEditStatus)
                {
                    if (selectedLine)
                        GraphicHelper.moveLine(ref startPoint, ref endPoint, ref mouseDownPoint, new Point(e.X, e.Y));

                    else if (selectedStartPoint) startPoint = new Point(e.X, e.Y);
                    else if (selectedEndPoint) endPoint = new Point(e.X, e.Y);
                    else if (selectedRect) GraphicHelper.moveRect(ref startPoint, ref endPoint, ref mouseDownPoint, new Point(e.X, e.Y));
                    else if (selectedVertex) GraphicHelper.modifyVertex(ref pointList, ref mouseDownPoint, new Point(e.X, e.Y));
                    else if (selectedPolygon) GraphicHelper.movePolygon(ref pointList, ref mouseDownPoint, new Point(e.X, e.Y));
                }
            }
        }

        private void customPanel1_Paint(object sender, PaintEventArgs e)
        {
            draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.customPanel1.Invalidate();
        }

        /// <summary>
        /// 选择图形类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxGraphType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedType = (GraphicTypeClass)this.cbxGraphType.SelectedItem;
            reset();
            isDrawing = true;
        }

        private void resetOperation()
        {
            selectedStartPoint = false;
            selectedEndPoint = false;
            selectedLine = false;
            selectedRect = false;
            selectedPolygon = false;
            selectedVertex = false;
        }

        private void reset()
        {
            startPoint = Point.Empty;
            endPoint = Point.Empty;
            pointList.Clear();
            isDrawing = false;
            isEditStatus = false;
            selectedStartPoint = false;
            selectedEndPoint = false;
            selectedLine = false;
            selectedPolygon = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isDrawing = false;
            isEditStatus = true;
        }
    }
}

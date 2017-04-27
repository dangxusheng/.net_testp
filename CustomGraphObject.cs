using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScollDemo
{
    /// <summary>
    /// 数学方法帮助对象
    /// </summary>
    public class MathHelper
    {
        /// <summary>
        /// 计算2点间距离
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static int Math_CalcSDistance(int x1, int y1, int x2, int y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }
        /// <summary>
        /// 判断点是否在直线上
        /// </summary>
        /// <param name="lx1"></param>
        /// <param name="ly1"></param>
        /// <param name="lx2"></param>
        /// <param name="ly2"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <returns></returns>
        public static bool Math_IsPointInLine(int lx1, int ly1, int lx2, int ly2, int px, int py)
        {
            int dx1 = lx2 - lx1;
            int dy1 = ly2 - ly1;
            int dx2 = px - lx1;
            int dy2 = py - ly1;
            double cosAng = (dx1 * dx2 + dy1 * dy2) * 1.0 / (Math.Sqrt(dx1 * dx1 + dy1 * dy1 * 1.0) * Math.Sqrt(dx2 * dx2 + dy2 * dy2 * 1.0));
            if ((cosAng >= 0.99 && cosAng <= 1.01) || (cosAng >= -1.01 && cosAng <= -0.99))
            {
                int minx = lx1 > lx2 ? lx2 : lx1;
                int maxx = lx1 + lx2 - minx;
                if (px > minx && px < maxx)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断点是否在多边形内部
        /// </summary>
        /// <param name="pxs"></param>
        /// <param name="pys"></param>
        /// <param name="pnum"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <returns></returns>
        public static bool Math_IsPointInPolygon(int[] pxs, int[] pys, int pnum, int px, int py)
        {
            int nCross = 0;
            for (int i = 0; i < pnum; i++)
            {
                int lx1 = pxs[i], ly1 = pys[i];
                int lx2 = pxs[(i + 1) % pnum], ly2 = pys[(i + 1) % pnum];

                if (ly1 == ly2)
                    continue;
                if (py <= Math.Min(ly1, ly2))
                    continue;
                if (py >= Math.Max(ly1, ly2))
                    continue;

                double x = (double)(py - ly1) * (double)(lx2 - lx1) / (double)(ly2 - ly1) + lx1;
                if (x > px)
                    nCross++;
            }
            return (nCross % 2 == 1);
        }
    }

    /*
     * 名称：自定义图形宏/常量声明
     */
    public class CustomGraphMacro
    {
        public const Int32 MaxVertexNum = 16;        // 自定义图形顶点最大数量
    }
    /*
     * 名称：自定义图形绘制类型枚举
     */
    public enum eCustomGraphDrawType
    {
        Line,           // 一条线
        DoubleLine,     // 两条线
        DirectLine,     // 带方向的线
        Rectangle,      // 矩形
        Quadrangle,     // 四边形
        Polygon,        // 多边形
        MaxEnum
    }
    /*
     * 名称：自定义图形修改方式枚举
     */
    public enum eCustomGraphModifyType
    {
        ModifyPoint,    // 修改顶点位置
        InsertPoint,    // 插入点
        DeletePoint,    // 删除点
        DrawMove,       // 全部移动
        MaxEnum
    }
    /*
     * 名称：自定义图形状态枚举
     */
    public enum eCustomGraphStatus
    {
        BeFree,         // 空闲状态
        BeDrawing,      // 正在绘制
        BeComplete,     // 绘制完成
    }
    /*
     * 名称：自定义点结构
     */
    public struct strCustomPoint
    {
        public int X;
        public int Y;
        public strCustomPoint(int _x, int _y)
        {
            X = _x; Y = _y;
        }
    }
    /*
     * 名称：自定义图形帮助对象
     */
    public class CustomGraphsHelper
    {
        public static string GraphTypeTransToChinese(eCustomGraphDrawType _type)
        {
            string revalue = null;
            switch (_type)
            {
                case eCustomGraphDrawType.Line:
                    revalue = "线段";
                    break;
                case eCustomGraphDrawType.DoubleLine:
                    revalue = "两条线段";
                    break;
                case eCustomGraphDrawType.DirectLine:
                    revalue = "方向线段";
                    break;
                case eCustomGraphDrawType.Rectangle:
                    revalue = "矩形";
                    break;
                case eCustomGraphDrawType.Quadrangle:
                    revalue = "四边形";
                    break;
                case eCustomGraphDrawType.Polygon:
                    revalue = "多边形";
                    break;
                default:
                    break;
            }
            return revalue;
        }
        public static eCustomGraphDrawType ChineseTransToGraphType(string _type)
        {
            if (String.IsNullOrEmpty(_type))
            {
                return eCustomGraphDrawType.MaxEnum;
            }
            else if (String.Equals(_type, "线段"))
            {
                return eCustomGraphDrawType.Line;
            }
            else if (String.Equals(_type, "两条线段"))
            {
                return eCustomGraphDrawType.DoubleLine;
            }
            else if (String.Equals(_type, "方向线段"))
            {
                return eCustomGraphDrawType.DirectLine;
            }
            else if (String.Equals(_type, "矩形"))
            {
                return eCustomGraphDrawType.Rectangle;
            }
            else if (String.Equals(_type, "四边形"))
            {
                return eCustomGraphDrawType.Quadrangle;
            }
            else if (String.Equals(_type, "多边形"))
            {
                return eCustomGraphDrawType.Polygon;
            }
            else
            {
                return eCustomGraphDrawType.MaxEnum;
            }
        }
    }

    public class CustomGraphObject
    {
        #region <<构造/析构函数>>
        public CustomGraphObject()
        {
            m_drawType = eCustomGraphDrawType.MaxEnum;
            m_status = eCustomGraphStatus.BeFree;
            m_graphVertex = new List<strCustomPoint>();

            m_left = Int32.MaxValue;
            m_right = Int32.MinValue;
            m_top = Int32.MaxValue;
            m_bottom = Int32.MinValue;

            m_bModifyInfo = false;
            m_modifyId = -1;
            m_modifyType = eCustomGraphModifyType.MaxEnum;

            m_iCanvasWidth = 704;
            m_iCanvasHeight = 576;
        }
        public CustomGraphObject(int _canvasWidth, int _canvasHeight)
            : this()
        {
            m_iCanvasWidth = _canvasWidth;
            m_iCanvasHeight = _canvasHeight;
        }
        public CustomGraphObject(CustomGraphObject _object)
            : this()
        {
            if (null != _object)
            {
                foreach (var _point in _object.GraphVertex)
                {
                    m_graphVertex.Add(new strCustomPoint(_point.X, _point.Y));
                }
                m_drawType = _object.GraphType;
                m_status = _object.GraphStatus;

                m_left = _object.GraphLeft;
                m_right = _object.GraphRight;
                m_top = _object.GraphTop;
                m_bottom = _object.GraphBottom;

                m_bModifyInfo = _object.bModifyInfo;
                m_modifyId = _object.ModifyId;
                m_modifyType = _object.ModifyType;

                m_iCanvasWidth = _object.CanvasWidth;
                m_iCanvasHeight = _object.CanvasHeight;
            }
        }
        #endregion

        #region <<接口属性>>
        /*
         * 获取一个值，该值标志图形对象类型
         */
        public eCustomGraphDrawType GraphType
        {
            get { return m_drawType; }
        }
        /*
         * 获取一个值，该值标志图形对象状态
         */
        public eCustomGraphStatus GraphStatus
        {
            get { return m_status; }
        }
        /*
         * 获取一个值，该值标志图形对象的顶点列表
         */
        public List<strCustomPoint> GraphVertex
        {
            get
            {
                return m_graphVertex;
            }
        }
        /*
         * 获取一个值，该值标志图形对象顶点数量
         */
        public Int32 GraphVertexNum
        {
            get
            {
                if (null != m_graphVertex)
                {
                    return m_graphVertex.Count;
                }
                else
                {
                    return 0;
                }
            }
        }
        /*
         * 获取一个值，该值标志图形外接矩形左边界
         */
        public Int32 GraphLeft
        {
            get { return m_left; }
        }
        /*
         * 获取一个值，该值标志图形外接矩形右边界
         */
        public Int32 GraphRight
        {
            get { return m_right; }
        }
        /*
         * 获取一个值，该值标志图形外接矩形上边界
         */
        public Int32 GraphTop
        {
            get { return m_top; }
        }
        /*
         * 获取一个值，该值标志图形外接矩形下边界
         */
        public Int32 GraphBottom
        {
            get { return m_bottom; }
        }
        /*
         * 获取一个值，该值标志是否正在修改图形
         */
        public bool bModifyInfo
        {
            get { return m_bModifyInfo; }
        }
        /*
         * 获取一个值，该值标志正在修改的顶点编号
         */
        public Int32 ModifyId
        {
            get { return m_modifyId; }
        }
        /*
         * 获取一个值，该值标志图形修改方案（插入/删除/移动顶点；全部顶点移动）
         */
        public eCustomGraphModifyType ModifyType
        {
            get { return m_modifyType; }
        }
        /*
         * 获取一个值，该值标志图形信息所基于的画布宽度
         * 注：
         *   该值需在图形创建时传入
         */
        public Int32 CanvasWidth
        {
            get { return m_iCanvasWidth; }
        }
        /*
         * 获取一个值，该值标志图形信息所基于的画布高度
         * 注：
         *   该值需在图形创建时传入
         */
        public Int32 CanvasHeight
        {
            get { return m_iCanvasHeight; }
        }
        #endregion

        #region <<接口函数-基本操作>>
        /*
         * 名称：拷贝赋值
         */
        public bool Copy(CustomGraphObject _src)
        {
            if (null == _src)
            {
                return false;
            }
            lock (mLockObject)
            {
                m_graphVertex.Clear();
                foreach (var pt in _src.m_graphVertex)
                {
                    m_graphVertex.Add(new strCustomPoint(pt.X, pt.Y));
                }
                m_drawType = _src.GraphType;
                m_status = _src.GraphStatus;

                m_left = _src.GraphLeft;
                m_right = _src.GraphRight;
                m_top = _src.GraphTop;
                m_bottom = _src.GraphBottom;

                m_bModifyInfo = _src.bModifyInfo;
                m_modifyId = _src.ModifyId;
                m_modifyType = _src.ModifyType;
                return true;
            }
        }
        /*
         * 名称：开始绘制图形
         */
        public bool StartDraw(eCustomGraphDrawType _type)
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeDrawing == m_status)
                {
                    return false;
                }
                else
                {
                    m_graphVertex.Clear();
                    m_drawType = _type;
                    m_status = eCustomGraphStatus.BeDrawing;
                    return true;
                }
            }
        }
        /*
         * 名称：完成图形绘制
         * 摘要：
         *   1、该函数对顶点数目明确的图形无效
         */
        public eCustomGraphStatus CompleteDraw()
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeDrawing == m_status)
                {
                    switch (m_drawType)
                    {
                        case eCustomGraphDrawType.Polygon:
                            if (m_graphVertex.Count >= 3)
                            {
                                m_status = eCustomGraphStatus.BeComplete;
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (m_status == eCustomGraphStatus.BeComplete)
                {
                    m_bModifyInfo = false;
                    m_modifyId = -1;
                    m_modifyType = eCustomGraphModifyType.MaxEnum;
                }
                return m_status;
            }
        }
        /*
         * 名称：取消图形绘制
         * 摘要：
         *   1、调用该函数后，图形将恢复空闲状态（无效图形）
         */
        public bool CancelDraw()
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeFree != m_status)
                {
                    m_drawType = eCustomGraphDrawType.MaxEnum;
                    m_status = eCustomGraphStatus.BeFree;
                    m_graphVertex.Clear();

                    m_left = Int32.MaxValue;
                    m_right = Int32.MinValue;
                    m_top = Int32.MaxValue;
                    m_bottom = Int32.MinValue;

                    m_bModifyInfo = false;
                    m_modifyId = -1;
                    m_modifyType = eCustomGraphModifyType.MaxEnum;
                }
                return true;
            }
        }
        /*
         * 名称：添加顶点信息
         * 摘要：
         *   1、向正在绘制的图形添加一个顶点；
         *   2、若顶点数量达到图形所允许的最大值，则自动完成绘制
         */
        public eCustomGraphStatus AddVertex(int _x, int _y)
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeDrawing == m_status)
                {
                    if (!m_bModifyInfo)
                    {
                        m_graphVertex.Add(new strCustomPoint(_x, _y));
                        int vertexCount = m_graphVertex.Count;
                        switch (m_drawType)
                        {
                            case eCustomGraphDrawType.Line:
                            case eCustomGraphDrawType.DirectLine:
                                if (2 == vertexCount)
                                {
                                    m_status = eCustomGraphStatus.BeComplete;
                                }
                                break;
                            case eCustomGraphDrawType.Rectangle:
                                if (2 == vertexCount)
                                {
                                    int x = m_graphVertex[0].X <= m_graphVertex[1].X ? m_graphVertex[0].X : m_graphVertex[1].X;
                                    int y = m_graphVertex[0].Y <= m_graphVertex[1].Y ? m_graphVertex[0].Y : m_graphVertex[1].Y;
                                    int w = m_graphVertex[0].X + m_graphVertex[1].X - 2 * x;
                                    int h = m_graphVertex[0].Y + m_graphVertex[1].Y - 2 * y;
                                    m_graphVertex[0] = new strCustomPoint(x, y);
                                    m_graphVertex[1] = new strCustomPoint(w, h);
                                    m_status = eCustomGraphStatus.BeComplete;
                                }
                                break;
                            case eCustomGraphDrawType.DoubleLine:
                            case eCustomGraphDrawType.Quadrangle:
                                if (4 == vertexCount)
                                {
                                    m_status = eCustomGraphStatus.BeComplete;
                                }
                                break;
                            case eCustomGraphDrawType.Polygon:
                                if (CustomGraphMacro.MaxVertexNum == vertexCount)
                                {
                                    m_status = eCustomGraphStatus.BeComplete;
                                }
                                break;
                            default:
                                break;
                        }
                        if (eCustomGraphStatus.BeComplete == m_status)
                        {
                            calculateGraphBorder();
                        }
                    }
                }
                return m_status;
            }
        }
        /*
         * 名称：开始修改图形
         * 摘要：
         *   1、只有已存在且完成绘制的图形才允许被修改哦
         *   2、参数：_type，修改方案；_modifyId，将要被修改的点的编号
         */
        public bool StartModify(eCustomGraphModifyType _type, int _modifyId)
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeComplete != m_status)
                {
                    return false;
                }
                else
                {
                    m_modifyType = _type;
                    m_modifyId = _modifyId;
                    m_bModifyInfo = true;
                    m_status = eCustomGraphStatus.BeDrawing;
                    return true;
                }
            }
        }
        /*
         * 名称：更新修改信息
         * 摘要：
         *   1、调用该函数后将结束图形的修改状态
         */
        public eCustomGraphStatus CompleteModify(Int32 _xInfo, Int32 _yInfo)
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeDrawing == m_status)
                {
                    if (m_bModifyInfo)
                    {
                        switch (m_modifyType)
                        {
                            case eCustomGraphModifyType.ModifyPoint:
                                modifyGraphVertex(_xInfo, _yInfo);
                                break;
                            case eCustomGraphModifyType.InsertPoint:
                                insertGraphVertex(_xInfo, _yInfo);
                                break;
                            case eCustomGraphModifyType.DeletePoint:
                                deleteGraphVertex();
                                break;
                            case eCustomGraphModifyType.DrawMove:
                                moveGraph(_xInfo, _yInfo);
                                break;
                            default:
                                break;
                        }
                        m_bModifyInfo = false;
                        m_modifyId = -1;
                        m_modifyType = eCustomGraphModifyType.MaxEnum;
                        m_status = eCustomGraphStatus.BeComplete;
                        calculateGraphBorder();
                    }
                }
                return m_status;
            }
        }
        #endregion

        #region <<接口函数-图形呈现/GDI效果>>
        /*
         * 名称：显示图形
         * 摘要：
         *   1、调用该函数可分别实现完成状态和绘图状态的图形；
         *   2、共同参数：_graph，图形绘制设备；
         *   3、完成图形参数：_cpen，绘图画笔；
         *   4、未完成图形参数：_mpen，绘图画笔；_x,_y，不修改下的图形未确定的最新顶点；_dx,_dy，修改状态下的图形顶点未确定移动量
         */
        public void DrawGraph(Graphics _graph, Pen _cpen, Pen _mpen, bool _bIgnoreCanvasSize, int _x, int _y, int _dx, int _dy)
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeComplete == m_status)
                {
                    drawCompleteGraph(_graph, _cpen, _bIgnoreCanvasSize);
                }
                else if (eCustomGraphStatus.BeDrawing == m_status)
                {
                    drawDrawingGraph(_graph, _mpen, _x, _y, _dx, _dy);
                }
            }
        }
        #endregion

        #region <<接口函数-图形关系判断>>
        /*
         * 名称：判断点是否是图形顶点
         * 摘要：
         *   1、参数值：_minDis点之间的最小距离，若两点之间距离小于该值，则认为是同一点；
         *   2、返回值：>=0，表示匹配到的顶点编号，反之，未匹配到顶点
         */
        public int IsGraphVertex(int _xInfo, int _yInfo)
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeComplete == m_status)
                {
                    int minDis = Int32.MaxValue;
                    int minDisId = -1;
                    int _minDis = 8;
                    if (m_drawType != eCustomGraphDrawType.Rectangle)
                    {
                        int vertexCount = m_graphVertex.Count;
                        for (int i = 0; i < vertexCount; i++)
                        {
                            int dis = MathHelper.Math_CalcSDistance(m_graphVertex[i].X, m_graphVertex[i].Y, _xInfo, _yInfo);
                            if (dis < minDis)
                            {
                                minDis = dis;
                                minDisId = i;
                            }
                        }
                        if (minDis <= _minDis)
                        {
                            return minDisId;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        int[] xAng = new int[4];
                        int[] yAng = new int[4];
                        xAng[0] = m_graphVertex[0].X;
                        yAng[0] = m_graphVertex[0].Y;
                        xAng[1] = m_graphVertex[0].X + m_graphVertex[1].X;
                        yAng[1] = m_graphVertex[0].Y;
                        xAng[2] = m_graphVertex[0].X + m_graphVertex[1].X;
                        yAng[2] = m_graphVertex[0].Y + m_graphVertex[1].Y;
                        xAng[3] = m_graphVertex[0].X;
                        yAng[3] = m_graphVertex[0].Y + m_graphVertex[1].Y;
                        for (int i = 0; i < 4; i++)
                        {
                            int dis = MathHelper.Math_CalcSDistance(xAng[i], yAng[i], _xInfo, _yInfo);
                            if (dis < minDis)
                            {
                                minDis = dis;
                                minDisId = i;
                            }
                        }
                        if (minDis <= _minDis)
                        {
                            return minDisId;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }
        }
        /*
         * 名称：判断点是否在图形边上
         * 摘要：
         *   1、该函数对任意图形有效；
         *   2、参数值：_minDis点到线段的最小距离，若两点之间距离小于该值，则认为是点在线上；
         *   3、返回值：>=0，表示匹配到的线段的第一个(除多边形外)或二个点(多边形)的编号，反之，点不在线上
         */
        public int IsInGraphSide(int _xInfo, int _yInfo)
        {
            lock (mLockObject)
            {
                int revalue = -1;
                if (eCustomGraphStatus.BeComplete == m_status)
                {
                    if (eCustomGraphDrawType.Polygon == m_drawType)
                    {
                        int vertexCount = m_graphVertex.Count;
                        for (int i = 1; i < vertexCount; i++)
                        {
                            if (MathHelper.Math_IsPointInLine(m_graphVertex[i - 1].X, m_graphVertex[i - 1].Y, m_graphVertex[i].X, m_graphVertex[i].Y, _xInfo, _yInfo))
                            {
                                revalue = i;
                                break;
                            }
                        }
                        if (-1 == revalue)
                        {
                            if (MathHelper.Math_IsPointInLine(m_graphVertex[vertexCount - 1].X, m_graphVertex[vertexCount - 1].Y, m_graphVertex[0].X, m_graphVertex[0].Y, _xInfo, _yInfo))
                            {
                                revalue = 0;
                            }
                        }
                    }
                    else if (eCustomGraphDrawType.Line == m_drawType
                        || eCustomGraphDrawType.DirectLine == m_drawType)
                    {
                        if (MathHelper.Math_IsPointInLine(m_graphVertex[0].X, m_graphVertex[0].Y, m_graphVertex[1].X, m_graphVertex[1].Y, _xInfo, _yInfo))
                        {
                            revalue = 0;
                        }
                    }
                    else if (eCustomGraphDrawType.DoubleLine == m_drawType)
                    {
                        if (MathHelper.Math_IsPointInLine(m_graphVertex[0].X, m_graphVertex[0].Y, m_graphVertex[1].X, m_graphVertex[1].Y, _xInfo, _yInfo))
                        {
                            revalue = 0;
                        }
                        else if (MathHelper.Math_IsPointInLine(m_graphVertex[2].X, m_graphVertex[2].Y, m_graphVertex[3].X, m_graphVertex[3].Y, _xInfo, _yInfo))
                        {
                            revalue = 2;
                        }
                    }
                }
                return revalue;
            }
        }
        /*
         * 名称：判断点是否在图形内部
         * 摘要：
         *   1、仅对多边形有效；
         */
        public bool IsInGraphInside(int _xInfo, int _yInfo)
        {
            lock (mLockObject)
            {
                if (eCustomGraphStatus.BeComplete == m_status)
                {
                    if (eCustomGraphDrawType.Rectangle == m_drawType)
                    {
                        if (_xInfo > m_graphVertex[0].X && _xInfo < m_graphVertex[0].X + m_graphVertex[1].X
                            && _yInfo > m_graphVertex[0].Y && _yInfo < m_graphVertex[0].Y + m_graphVertex[1].Y)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (eCustomGraphDrawType.Quadrangle == m_drawType
                        || eCustomGraphDrawType.Polygon == m_drawType)
                    {
                        int ptNum = m_graphVertex.Count;
                        int[] ptx = new int[ptNum];
                        int[] pty = new int[ptNum];
                        for (int i = 0; i < ptNum; i++)
                        {
                            ptx[i] = m_graphVertex[i].X;
                            pty[i] = m_graphVertex[i].Y;
                        }
                        if (MathHelper.Math_IsPointInPolygon(ptx, pty, ptNum, _xInfo, _yInfo))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region <<业务字段>>
        protected List<strCustomPoint> m_graphVertex = null;
        private eCustomGraphDrawType m_drawType = eCustomGraphDrawType.MaxEnum;
        private eCustomGraphStatus m_status = eCustomGraphStatus.BeFree;

        private bool m_bModifyInfo = false;
        private Int32 m_modifyId = -1;
        private eCustomGraphModifyType m_modifyType = eCustomGraphModifyType.MaxEnum;

        private Int32 m_left = Int32.MaxValue;
        private Int32 m_right = Int32.MinValue;
        private Int32 m_top = Int32.MaxValue;
        private Int32 m_bottom = Int32.MinValue;

        protected Int32 m_iCanvasWidth = 0;
        protected Int32 m_iCanvasHeight = 0;

        private readonly object mLockObject = new object();
        #endregion

        #region <<业务函数>>
        /*
         * 名称：计算图形外接矩形边界
         */
        private void calculateGraphBorder()
        {
            if (eCustomGraphStatus.BeComplete == m_status)
            {
                m_left = Int32.MaxValue;
                m_right = Int32.MinValue;
                m_top = Int32.MaxValue;
                m_bottom = Int32.MinValue;
                if (eCustomGraphDrawType.Rectangle == m_drawType)
                {
                    m_left = m_graphVertex[0].X;
                    m_right = m_graphVertex[0].X + m_graphVertex[1].X;
                    m_top = m_graphVertex[0].Y;
                    m_bottom = m_graphVertex[0].Y + m_graphVertex[1].Y;
                }
                else
                {
                    foreach (var pt in m_graphVertex)
                    {
                        if (pt.X > m_right)
                        {
                            m_right = pt.X;
                        }
                        if (pt.X < m_left)
                        {
                            m_left = pt.X;
                        }
                        if (pt.Y > m_bottom)
                        {
                            m_bottom = pt.Y;
                        }
                        if (pt.Y < m_top)
                        {
                            m_top = pt.Y;
                        }
                    }
                }
            }
        }
        /*
         * 名称：修改图形指定顶点坐标
         * 摘要：
         *   1、该函数对所有类型的图形有效；
         *   2、修改矩形顶点时，请注意矩形存储的格式(x,y)(w,h)
         */
        private bool modifyGraphVertex(Int32 _xInfo, Int32 _yInfo)
        {
            if (eCustomGraphDrawType.Rectangle == m_drawType)
            {
                int[] xAng = new int[4];
                int[] yAng = new int[4];
                xAng[0] = m_graphVertex[0].X;
                yAng[0] = m_graphVertex[0].Y;
                xAng[1] = m_graphVertex[0].X + m_graphVertex[1].X;
                yAng[1] = m_graphVertex[0].Y;
                xAng[2] = m_graphVertex[0].X + m_graphVertex[1].X;
                yAng[2] = m_graphVertex[0].Y + m_graphVertex[1].Y;
                xAng[3] = m_graphVertex[0].X;
                yAng[3] = m_graphVertex[0].Y + m_graphVertex[1].Y;

                int orgId = m_modifyId + 2;
                if (orgId >= 4)
                    orgId -= 4;
                int orgX = xAng[orgId];
                int orgY = yAng[orgId];
                int x = orgX <= _xInfo ? orgX : _xInfo;
                int y = orgY <= _yInfo ? orgY : _yInfo;
                int w = orgX + _xInfo - 2 * x;
                int h = orgY + _yInfo - 2 * y;
                m_graphVertex[0] = new strCustomPoint(x, y);
                m_graphVertex[1] = new strCustomPoint(w, h);
                return true;
            }
            else
            {
                if (m_modifyId >= 0 && m_modifyId < m_graphVertex.Count)
                {
                    m_graphVertex[m_modifyId] = new strCustomPoint(_xInfo, _yInfo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /*
         * 名称：修改图形时，插入顶点
         * 摘要：
         *   1、该函数仅对顶点数不固定的图形（如多边形等）有效
         */
        public bool insertGraphVertex(Int32 _xInfo, Int32 _yInfo)
        {
            if (eCustomGraphDrawType.Polygon == m_drawType)
            {
                if (m_graphVertex.Count >= CustomGraphMacro.MaxVertexNum || m_modifyId < 0 || m_modifyId >= m_graphVertex.Count)
                {
                    return false;
                }
                m_graphVertex.Insert(m_modifyId, new strCustomPoint(_xInfo, _yInfo));
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * 名称：修改图形时，删除顶点
         * 摘要：
         *   1、该函数仅对顶点数不固定的图形（如多边形等）有效
         */
        public bool deleteGraphVertex()
        {
            if (eCustomGraphDrawType.Polygon == m_drawType)
            {
                if (m_graphVertex.Count <= 3 || m_modifyId < 0 || m_modifyId >= m_graphVertex.Count)
                {
                    return false;
                }
                m_graphVertex.RemoveAt(m_modifyId);
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * 名称：移动图形
         * 摘要：
         *   1、该函数对所有图形有效；
         *   2、请注意矩形的存储格式(x,y)(w,h)，只需移动(x,y)即可；
         */
        private bool moveGraph(Int32 _xInfo, Int32 _yInfo)
        {
            if (eCustomGraphDrawType.Rectangle == m_drawType)
            {
                m_graphVertex[0] = new strCustomPoint(m_graphVertex[0].X + _xInfo, m_graphVertex[0].Y + _yInfo);
            }
            else
            {
                int vertexNum = m_graphVertex.Count;
                for (int i = 0; i < vertexNum; i++)
                {
                    m_graphVertex[i] = new strCustomPoint(m_graphVertex[i].X + _xInfo, m_graphVertex[i].Y + _yInfo);
                }
            }
            return true;
        }
        /*
         * 名称：显示图形
         * 摘要：
         *   1、该函数仅对已经完成绘图状态的图形对象有效；
         *   2、参数：_bIgnoreCanvasSize，是否忽略画布信息，若为true,则无缩放，可能出现绘图错误
         */
        private void drawCompleteGraph(Graphics _graph, Pen _pen, bool _bIgnoreCanvasSize)
        {
            float xScale = 1.0f;
            float yScale = 1.0f;
            if (!_bIgnoreCanvasSize && m_iCanvasWidth * m_iCanvasHeight > 0)
            {
                xScale = _graph.VisibleClipBounds.Width / m_iCanvasWidth;
                yScale = _graph.VisibleClipBounds.Height / m_iCanvasHeight;
            }
            switch (m_drawType)
            {
                case eCustomGraphDrawType.Line:
                    _graph.DrawRectangle(_pen, (int)((m_graphVertex[0].X - 2) * xScale), (int)((m_graphVertex[0].Y - 2) * yScale), 5, 5);
                    _graph.DrawRectangle(_pen, (int)((m_graphVertex[1].X - 2) * xScale), (int)((m_graphVertex[1].Y - 2) * yScale), 5, 5);
                    _graph.DrawLine(_pen, (int)(m_graphVertex[0].X * xScale), (int)(m_graphVertex[0].Y * yScale), (int)(m_graphVertex[1].X * xScale), (int)(m_graphVertex[1].Y * yScale));
                    break;
                case eCustomGraphDrawType.DoubleLine:
                    for (int i = 0; i < 4; i += 2)
                    {
                        _graph.DrawRectangle(_pen, (int)((m_graphVertex[i].X - 2) * xScale), (int)((m_graphVertex[i].Y - 2) * yScale), 5, 5);
                        _graph.DrawRectangle(_pen, (int)((m_graphVertex[i + 1].X - 2) * xScale), (int)((m_graphVertex[i + 1].Y - 2) * yScale), 5, 5);
                        _graph.DrawLine(_pen, (int)(m_graphVertex[i].X * xScale), (int)(m_graphVertex[i].Y * yScale), (int)(m_graphVertex[i + 1].X * xScale), (int)(m_graphVertex[i + 1].Y * yScale));

                    }
                    break;
                case eCustomGraphDrawType.DirectLine:
                    _graph.DrawRectangle(_pen, (int)((m_graphVertex[0].X - 2) * xScale), (int)((m_graphVertex[0].Y - 2) * yScale), 5, 5);
                    _graph.DrawLine(_pen, (int)(m_graphVertex[0].X * xScale), (int)(m_graphVertex[0].Y * yScale), (int)(m_graphVertex[1].X * xScale), (int)(m_graphVertex[1].Y * yScale));
                    break;
                case eCustomGraphDrawType.Rectangle:
                    _graph.DrawRectangle(_pen, (int)(m_graphVertex[0].X * xScale), (int)(m_graphVertex[0].Y * yScale), (int)(m_graphVertex[1].X * xScale), (int)(m_graphVertex[1].Y * yScale));
                    break;
                case eCustomGraphDrawType.Quadrangle:
                case eCustomGraphDrawType.Polygon:
                    int vertexNum = m_graphVertex.Count;
                    for (int i = 0; i < vertexNum - 1; i++)
                    {
                        _graph.DrawRectangle(_pen, (int)((m_graphVertex[i].X - 2) * xScale), (int)((m_graphVertex[i].Y - 2) * yScale), 5, 5);
                        _graph.DrawLine(_pen, (int)(m_graphVertex[i].X * xScale), (int)(m_graphVertex[i].Y * yScale), (int)(m_graphVertex[i + 1].X * xScale), (int)(m_graphVertex[i + 1].Y * yScale));
                    }
                    _graph.DrawRectangle(_pen, (int)((m_graphVertex[vertexNum - 1].X - 2) * xScale), (int)((m_graphVertex[vertexNum - 1].Y - 2) * yScale), 5, 5);
                    _graph.DrawLine(_pen, (int)(m_graphVertex[vertexNum - 1].X * xScale), (int)(m_graphVertex[vertexNum - 1].Y * yScale), (int)(m_graphVertex[0].X * xScale), (int)(m_graphVertex[0].Y * yScale));
                    break;
                default:
                    break;
            }
        }
        /*
         * 名称：显示正在绘制或修改的图形
         */
        private void drawDrawingGraph(Graphics _graph, Pen _pen, int _x, int _y, int _dx, int _dy)
        {
            int vertexNum = m_graphVertex.Count;
            if (0 == vertexNum)
            {
                return;
            }
            switch (m_drawType)
            {
                case eCustomGraphDrawType.Line:
                    {
                        #region 绘图状态下的线段的显示
                        if (m_bModifyInfo)
                        {
                            if (eCustomGraphModifyType.ModifyPoint == m_modifyType)
                            {
                                int sureId = 1 - m_modifyId;
                                _graph.DrawRectangle(_pen, m_graphVertex[sureId].X - 2, m_graphVertex[sureId].Y - 2, 5, 5);
                                _graph.DrawLine(_pen, m_graphVertex[sureId].X, m_graphVertex[sureId].Y, _x, _y);
                            }
                            else if (eCustomGraphModifyType.DrawMove == m_modifyType)
                            {
                                _graph.DrawLine(_pen, m_graphVertex[0].X + _dx, m_graphVertex[0].Y + _dy, m_graphVertex[1].X + _dx, m_graphVertex[1].Y + _dy);
                            }
                        }
                        else
                        {
                            _graph.DrawRectangle(_pen, m_graphVertex[0].X - 2, m_graphVertex[0].Y - 2, 5, 5);
                            _graph.DrawLine(_pen, m_graphVertex[0].X, m_graphVertex[0].Y, _x, _y);
                        }
                        #endregion
                    }
                    break;
                case eCustomGraphDrawType.DoubleLine:
                    {
                        #region 绘图状态下的两条线段的显示
                        if (m_bModifyInfo)
                        {
                            if (eCustomGraphModifyType.ModifyPoint == m_modifyType)
                            {
                                int i = 0;
                                while (i < vertexNum)
                                {
                                    if (m_modifyId == i)
                                    {
                                        _graph.DrawRectangle(_pen, _x - 2, _y - 2, 5, 5);
                                        _graph.DrawLine(_pen, _x, _y, m_graphVertex[i + 1].X, m_graphVertex[i + 1].Y);
                                        _graph.DrawRectangle(_pen, m_graphVertex[i + 1].X - 2, m_graphVertex[i + 1].Y - 2, 5, 5);
                                    }
                                    else if (m_modifyId == i + 1)
                                    {
                                        _graph.DrawRectangle(_pen, m_graphVertex[i].X - 2, m_graphVertex[i].Y - 2, 5, 5);
                                        _graph.DrawLine(_pen, m_graphVertex[i].X, m_graphVertex[i].Y, _x, _y);
                                        _graph.DrawRectangle(_pen, _x - 2, _y - 2, 5, 5);
                                    }
                                    else
                                    {
                                        _graph.DrawRectangle(_pen, m_graphVertex[i].X - 2, m_graphVertex[i].Y - 2, 5, 5);
                                        _graph.DrawLine(_pen, m_graphVertex[i].X, m_graphVertex[i].Y, m_graphVertex[i + 1].X, m_graphVertex[i + 1].Y);
                                        _graph.DrawRectangle(_pen, m_graphVertex[i + 1].X - 2, m_graphVertex[i + 1].Y - 2, 5, 5);

                                    }
                                    i += 2;
                                }
                            }
                            else if (eCustomGraphModifyType.DrawMove == m_modifyType)
                            {
                                _graph.DrawLine(_pen, m_graphVertex[0].X + _dx, m_graphVertex[0].Y + _dy, m_graphVertex[1].X + _dx, m_graphVertex[1].Y + _dy);
                                _graph.DrawLine(_pen, m_graphVertex[2].X + _dx, m_graphVertex[2].Y + _dy, m_graphVertex[3].X + _dx, m_graphVertex[3].Y + _dy);
                            }
                        }
                        else
                        {
                            int i = 0;
                            while (i < vertexNum)
                            {
                                _graph.DrawRectangle(_pen, m_graphVertex[i].X - 2, m_graphVertex[i].Y - 2, 5, 5);
                                if (i + 1 < vertexNum)
                                {
                                    _graph.DrawLine(_pen, m_graphVertex[i].X, m_graphVertex[i].Y, m_graphVertex[i + 1].X, m_graphVertex[i + 1].Y);
                                    _graph.DrawRectangle(_pen, m_graphVertex[i + 1].X - 2, m_graphVertex[i + 1].Y - 2, 5, 5);
                                }
                                else
                                {
                                    _graph.DrawLine(_pen, m_graphVertex[i].X, m_graphVertex[i].Y, _x, _y);
                                }
                                i += 2;
                            }
                        }
                        #endregion
                    }
                    break;
                case eCustomGraphDrawType.DirectLine:
                    {
                        #region 绘图状态下的方向标识的显示
                        if (m_bModifyInfo)
                        {
                            if (eCustomGraphModifyType.ModifyPoint == m_modifyType)
                            {
                                if (m_modifyId == 0)
                                {
                                    _graph.DrawRectangle(_pen, _x - 2, _y - 2, 5, 5);
                                    _graph.DrawLine(_pen, _x, _y, m_graphVertex[1].X, m_graphVertex[1].Y);
                                }
                                else
                                {
                                    _graph.DrawRectangle(_pen, m_graphVertex[0].X - 2, m_graphVertex[0].Y - 2, 5, 5);
                                    _graph.DrawLine(_pen, m_graphVertex[0].X, m_graphVertex[0].Y, _x, _y);
                                }
                            }
                            else if (eCustomGraphModifyType.DrawMove == m_modifyType)
                            {
                                _graph.DrawLine(_pen, m_graphVertex[0].X + _dx, m_graphVertex[0].Y + _dy, m_graphVertex[1].X + _dx, m_graphVertex[1].Y + _dy);
                            }
                        }
                        else
                        {
                            _graph.DrawRectangle(_pen, m_graphVertex[0].X - 2, m_graphVertex[0].Y - 2, 5, 5);
                            _graph.DrawLine(_pen, m_graphVertex[0].X, m_graphVertex[0].Y, _x, _y);
                        }
                        #endregion
                    }
                    break;
                case eCustomGraphDrawType.Rectangle:
                    {
                        #region 绘图状态下的矩形的显示
                        if (m_bModifyInfo)
                        {
                            if (eCustomGraphModifyType.ModifyPoint == m_modifyType)
                            {
                                int[] xAng = new int[4];
                                int[] yAng = new int[4];
                                xAng[0] = m_graphVertex[0].X;
                                yAng[0] = m_graphVertex[0].Y;
                                xAng[1] = m_graphVertex[0].X + m_graphVertex[1].X;
                                yAng[1] = m_graphVertex[0].Y;
                                xAng[2] = m_graphVertex[0].X + m_graphVertex[1].X;
                                yAng[2] = m_graphVertex[0].Y + m_graphVertex[1].Y;
                                xAng[3] = m_graphVertex[0].X;
                                yAng[3] = m_graphVertex[0].Y + m_graphVertex[1].Y;

                                int orgId = m_modifyId + 2;
                                if (orgId >= 4)
                                    orgId -= 4;
                                int orgX = xAng[orgId];
                                int orgY = yAng[orgId];
                                int x = orgX <= _x ? orgX : _x;
                                int y = orgY <= _y ? orgY : _y;
                                int w = orgX + _x - 2 * x;
                                int h = orgY + _y - 2 * y;
                                _graph.DrawRectangle(_pen, x, y, w, h);
                            }
                            else if (eCustomGraphModifyType.DrawMove == m_modifyType)
                            {
                                _graph.DrawRectangle(_pen, m_graphVertex[0].X + _dx, m_graphVertex[0].Y + _dy, m_graphVertex[1].X, m_graphVertex[1].Y);
                            }
                        }
                        else
                        {
                            int x = m_graphVertex[0].X <= _x ? m_graphVertex[0].X : _x;
                            int y = m_graphVertex[0].Y <= _y ? m_graphVertex[0].Y : _y;
                            int w = m_graphVertex[0].X + _x - 2 * x;
                            int h = m_graphVertex[0].Y + _y - 2 * y;
                            _graph.DrawRectangle(_pen, x, y, w, h);
                        }
                        #endregion
                    }
                    break;
                case eCustomGraphDrawType.Quadrangle:
                case eCustomGraphDrawType.Polygon:
                    {
                        #region 绘图状态下多边形的显示
                        if (m_bModifyInfo)
                        {
                            if (eCustomGraphModifyType.ModifyPoint == m_modifyType)
                            {
                                for (int i = 0; i < vertexNum - 1; i++)
                                {
                                    if (i == m_modifyId)
                                    {
                                        _graph.DrawRectangle(_pen, _x - 2, _y - 2, 5, 5);
                                        _graph.DrawLine(_pen, _x, _y, m_graphVertex[i + 1].X, m_graphVertex[i + 1].Y);
                                    }
                                    else
                                    {
                                        _graph.DrawRectangle(_pen, m_graphVertex[i].X - 2, m_graphVertex[i].Y - 2, 5, 5);
                                        if (i + 1 == m_modifyId)
                                        {
                                            _graph.DrawLine(_pen, m_graphVertex[i].X, m_graphVertex[i].Y, _x, _y);
                                        }
                                        else
                                        {
                                            _graph.DrawLine(_pen, m_graphVertex[i].X, m_graphVertex[i].Y, m_graphVertex[i + 1].X, m_graphVertex[i + 1].Y);
                                        }
                                    }
                                }
                                if (m_modifyId == vertexNum - 1)
                                {
                                    _graph.DrawRectangle(_pen, _x, _y, 5, 5);
                                    _graph.DrawLine(_pen, _x, _y, m_graphVertex[0].X, m_graphVertex[0].Y);
                                }
                                else if (m_modifyId == 0)
                                {
                                    _graph.DrawRectangle(_pen, m_graphVertex[vertexNum - 1].X, m_graphVertex[vertexNum - 1].Y, 5, 5);
                                    _graph.DrawLine(_pen, m_graphVertex[vertexNum - 1].X, m_graphVertex[vertexNum - 1].Y, _x, _y);
                                }
                                else
                                {
                                    _graph.DrawRectangle(_pen, m_graphVertex[vertexNum - 1].X, m_graphVertex[vertexNum - 1].Y, 5, 5);
                                    _graph.DrawLine(_pen, m_graphVertex[vertexNum - 1].X, m_graphVertex[vertexNum - 1].Y, m_graphVertex[0].X, m_graphVertex[0].Y);
                                }
                            }
                            else if (eCustomGraphModifyType.DrawMove == m_modifyType)
                            {
                                for (int i = 0; i < vertexNum - 1; i++)
                                {
                                    _graph.DrawLine(_pen, m_graphVertex[i].X + _dx, m_graphVertex[i].Y + _dy, m_graphVertex[i + 1].X + _dx, m_graphVertex[i + 1].Y + _dy);
                                }
                                _graph.DrawLine(_pen, m_graphVertex[vertexNum - 1].X + _dx, m_graphVertex[vertexNum - 1].Y + _dy, m_graphVertex[0].X + _dx, m_graphVertex[0].Y + _dy);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < vertexNum - 1; i++)
                            {
                                _graph.DrawRectangle(_pen, m_graphVertex[i].X - 2, m_graphVertex[i].Y - 2, 5, 5);
                                _graph.DrawLine(_pen, m_graphVertex[i].X, m_graphVertex[i].Y, m_graphVertex[i + 1].X, m_graphVertex[i + 1].Y);
                            }
                            _graph.DrawRectangle(_pen, m_graphVertex[vertexNum - 1].X - 2, m_graphVertex[vertexNum - 1].Y - 2, 5, 5);
                            _graph.DrawLine(_pen, m_graphVertex[vertexNum - 1].X, m_graphVertex[vertexNum - 1].Y, _x, _y);
                        }
                        #endregion
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}

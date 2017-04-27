using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScollDemo
{
    public class GraphicHelper
    {
        public static readonly object locker = new object();

        public static bool IsThisPoint(Point p1, Point p2, double range)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2)) <= range;
        }

        public static bool GetPointIsInLine(Point pf, Point p1, Point p2, double range)
        {

            //range 判断的的误差，不需要误差则赋值0
            //点在线段首尾两端之外则return false

            double cross = (p2.X - p1.X) * (pf.X - p1.X) + (p2.Y - p1.Y) * (pf.Y - p1.Y);
            if (cross <= 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross >= d2) return false;

            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;

            //判断距离是否小于误差
            return Math.Sqrt((pf.X - px) * (pf.X - px) + (py - pf.Y) * (py - pf.Y)) <= range;
        }

        public static bool IsPointInPolygon(Point curPoint, Point[] points)
        {
            var counter = 0;
            for (var i = 0; i < points.Length; i++)
            {
                var p1 = points[i];
                var p2 = points[(i + 1) % points.Length];
                if (p1.Y == p2.Y)
                {
                    continue;
                }
                if (curPoint.Y <= Math.Min(p1.Y, p2.Y))
                {
                    continue;
                }
                if (curPoint.Y >= Math.Max(p1.Y, p2.Y))
                {
                    continue;
                }
                var x = (curPoint.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
                if (x > curPoint.X) counter++;
            }
            return counter % 2 > 0;
        }
        /// <summary>
        /// 是否选中了顶点
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static bool IsGraphVertex(ref List<Point> list, Point pt)
        {
            lock (locker)
            {
                bool isVertex = false;

                if (list.Count() > 2)
                {
                    foreach (var p in list)
                    {
                        if (IsThisPoint(p, pt, 3))
                        {
                            isVertex = true;
                        }
                    }
                }

                return isVertex;
            }
        }

        public static void modifyVertex(ref List<Point> list, ref Point modifyPoint, Point pt)
        {
            lock (locker)
            {
                var pts = list.ToArray();
                for (var i = 0; i < pts.Length; i++)
                {
                    if (IsThisPoint(pts[i], modifyPoint, 3))
                    {
                        pts[i] = pt;
                        modifyPoint = pt;
                        break;
                    }
                }
                list.Clear();
                list = pts.ToList();
            }
        }


        /// <summary>
        /// 是否在图形内部
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static bool IsInPolygon(ref GraphicObject gp, ref Point pt)
        {
            lock (locker)
            {
                bool isIn = false;
                if (gp != null && gp.type != null)
                {
                    switch (gp.type.val)
                    {
                        case 2: //多边形
                            if (gp.pointList.Count() > 2)
                            {
                                if (IsPointInPolygon(pt, gp.pointList.ToArray()))
                                {
                                    isIn = true;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                return isIn;
            }
        }
        /// <summary>
        /// 移动直线
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public static void moveLine(ref Point startPoint, ref Point endPoint, ref Point p1, Point p2)
        {
            var offsetX = p2.X - p1.X;
            var offsetY = p2.Y - p1.Y;
            startPoint.Offset(offsetX, offsetY);
            endPoint.Offset(offsetX, offsetY);
            p1.Offset(offsetX, offsetY);
        }
        //移动矩形
        public static void moveRect(ref Point startPoint, ref Point endPoint, ref Point p1, Point p2)
        {
            var offsetX = p2.X - p1.X;
            var offsetY = p2.Y - p1.Y;
            startPoint.Offset(offsetX, offsetY);
            endPoint.Offset(offsetX, offsetY);
            p1.Offset(offsetX, offsetY);
        }
        //移动多边形
        public static void movePolygon(ref List<Point> list, ref Point p1, Point p2)
        {
            var offsetX = p2.X - p1.X;
            var offsetY = p2.Y - p1.Y;
            var pts = list.ToArray();
            for (var i = 0; i < pts.Length; i++)
            {
                pts[i].X += offsetX;
                pts[i].Y += offsetY;
            }
            list.Clear();
            list = pts.ToList();
            p1.Offset(offsetX, offsetY);
        }



    }
}

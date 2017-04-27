using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScollDemo
{
    //图形类型对象
    public class GraphicTypeClass
    {
        public string type { get; set; }
        public int val { get; set; }

        public override string ToString()
        {
            return type;
        }
    }
    public class GraphicObject {
        public GraphicTypeClass type { get; set; }

        public Point startPoint { get; set; }
        public Point endPoint { get; set; }

        public List<Point> pointList { get; set; }
    }
}

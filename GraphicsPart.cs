//Dilara Ademoglu
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dilara_Ademoglu_AI_hw2
{
    public class GraphicsPart
    {
        private static Graphics gObj;

        public GraphicsPart(Graphics g)
        {
            gObj = g;
            setUpCanvas();
        }
        public static void setUpCanvas()
        {
            Brush br = new SolidBrush(Color.WhiteSmoke);
            Pen lines = new Pen(Color.Black, 5);

            gObj.FillRectangle(br, new Rectangle(0, 0, 498, 498));
            //We need 4 lines
            gObj.DrawLine(lines, new Point(166, 0), new Point(166, 498));
            gObj.DrawLine(lines, new Point(332, 0), new Point(332, 498));
            gObj.DrawLine(lines, new Point(0, 166), new Point(498, 166));
            gObj.DrawLine(lines, new Point(0, 332), new Point(498, 332));
        }
        public static void drawX(Point loc)
        {
            Pen xPen = new Pen(Color.Tomato, 5);
            int xLoc = loc.X * 166;
            int yLoc = loc.Y * 166;
            gObj.DrawLine(xPen, (xLoc + 10), (yLoc + 10), (xLoc + 156), (yLoc + 156));
            gObj.DrawLine(xPen, (xLoc + 156), (yLoc + 10), (xLoc + 10), (yLoc + 156));

        }
        public static void drawO(Point loc)
        {
            Pen oPen = new Pen(Color.RoyalBlue, 5);
            int xLoc = loc.X * 166;
            int yLoc = loc.Y * 166;
            gObj.DrawEllipse(oPen, xLoc + 10, yLoc + 10, 146, 146);
        }
    }
}

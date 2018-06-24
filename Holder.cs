//Dilara Ademoglu
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dilara_Ademoglu_AI_hw2
{
    public class Holder //class for board structure
    { 
        private Point loc;
        private int value = Board.B;
        
        public Holder()
        {
            loc = new Point(-1,-1);
            value = Board.B;
        }
               
        public void setLoc(Point p) //setter
        {
            loc = p;
        }
        public Point getLoc() //getter 
        {
            return loc;
        }
        public void setValue(int i)
        {
            value = i;
        }
        public int getValue()
        {
            return value;
        }
    }
}

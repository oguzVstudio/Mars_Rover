using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Rover
{
    public class Plateau
    {
        private int _x;
        private int _y;
        public int X
        {
            get { return this._x; }
            set 
            {
                if (value < 0)
                    throw new Exception("X coordinate must be >=0");
                
                else
                    this._x = value;             
            }
        }
        public int Y
        {
            get { return this._y; }
            set
            {
                if (value < 0)
                    throw new Exception("Y coordinate must be >=0");
                else
                    this._y = value;
            }
        }
    }
}

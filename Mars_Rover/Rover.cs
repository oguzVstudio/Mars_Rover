using Mars_Rover.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Rover
{
    public class Rover
    {
        private int _x;
        private int _y;

        private Plateau _plateau;
        public Rover(Plateau plateau)
        {
            //_plateau = plateau ?? throw new ArgumentNullException(nameof(plateau));
            _plateau = plateau ?? new Plateau();
        }

        public int X 
        {
            get { return _x; }
            set
            {
                if (value > this._plateau.X || value < 0)
                    throw new Exception("Rover X coordinate out of the plateau!");
                else
                    _x = value;
            }
        }
        public int Y
        {
            get { return _y; }
            set
            {
                if (value > this._plateau.Y || value < 0)
                    throw new Exception("Rover Y coordinate out of the plateau!");
                else
                    _y = value;
            }
        }
        public NavigationFace NavigationFace { get; set; }
        public string NavigationLetter { get; set; }
    }
}

using Mars_Rover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mars_Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            ICalculable calculable = new Calculate();
            calculable.CommandForConsole();
        }
    }
}

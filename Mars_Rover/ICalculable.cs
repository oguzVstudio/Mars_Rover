using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Rover
{
    public interface ICalculable
    {
        IList<Rover> CommandForConsole();
        IList<Rover> Command(string plateauCoordinates, string qty, string coordinatesRover, string moveLetters);
    }
}

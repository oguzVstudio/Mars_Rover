using Mars_Rover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mars_Rover
{
    public class Calculate : ICalculable //Eğer bu bir asp.net web applicationda kullanılmak istenirse dependency injection olarak kullanılabilmesi için yazıldı.Burada dependecy injection kullanılmaya gerek duyulmadı.
    {
        public IList<Rover> CommandForConsole()
        {
            try
            {
                Console.WriteLine("Please enter plateau co-ordinates (X,Y)");

                string plateauCoordinates = Console.ReadLine();

                string[] coordinates = Regex.Split(plateauCoordinates.Trim(), @"\D+", RegexOptions.IgnorePatternWhitespace);

                if (coordinates.Length < 2 || coordinates.Length > 2)
                    throw new Exception("Invalid Coordinates");

                var plateau = new Plateau
                {
                    X = Convert.ToInt32(coordinates[0]),
                    Y = Convert.ToInt32(coordinates[1])
                };

                Console.WriteLine("Please enter rover quantity");

                string qty = Console.ReadLine();

                uint roverQty;

                if (!uint.TryParse(qty, out roverQty))
                    throw new Exception("Please enter valid number");

                var rovers = new List<Rover>();

                for (int i = 0; i < roverQty; i++)
                {
                    Console.WriteLine($"Please enter Rover{i + 1} position (X,Y,N) {{N--> N,E,S,W}}");

                    string coordinatesRover = Console.ReadLine();

                    var validRoverCoordinates = CheckRoverCoordinate(coordinatesRover);

                    rovers.Add(new Rover(plateau)
                    {
                        NavigationFace = (NavigationFace)Enum.Parse(typeof(NavigationFace), validRoverCoordinates[2]),
                        X = Convert.ToInt32(validRoverCoordinates[0]),
                        Y = Convert.ToInt32(validRoverCoordinates[1])
                    });
                }

                foreach (var rover in rovers.Select((rover, index) => new { rover, index }))
                {
                    Console.WriteLine($"Please enter rover{rover.index + 1} navigation letters {{M->Move, L->Left, R->Right}}");
                    string moveLetters = Console.ReadLine();
                    string[] moveLetterArray = CheckRoverMoveLetter(moveLetters);
                    rover.rover.NavigationLetter = moveLetters;
                }

                var outputRovers = new List<Rover>();

                foreach (var rover in rovers.Select((rover, index) => new { rover, index }))
                {
                    string[] moveLetterArray = rover.rover.NavigationLetter.ToCharArray().Select(c => c.ToString()).ToArray();
                    var outputRover = GetRoverPosition(rover.rover, moveLetterArray);
                    outputRovers.Add(outputRover);
                    Console.WriteLine($"Rover{rover.index + 1} position: {outputRover.X},{outputRover.Y},{outputRover.NavigationFace}");
                }

                Console.WriteLine("Please enter any key to exit");
                Console.ReadKey();
                return outputRovers;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        public IList<Rover> Command(string plateauCoordinates, string qty, string coordinatesRover, string moveLetters)
        {
            try
            {                
                

                string[] coordinates = Regex.Split(plateauCoordinates.Trim(), @"\D+", RegexOptions.IgnorePatternWhitespace);

                if (coordinates.Length < 2 || coordinates.Length > 2)
                    throw new Exception("Invalid Coordinates");

                var plateau = new Plateau
                {
                    X = Convert.ToInt32(coordinates[0]),
                    Y = Convert.ToInt32(coordinates[1])
                };               

                uint roverQty;

                if (!uint.TryParse(qty, out roverQty))
                    throw new Exception("Please enter valid number");

                var rovers = new List<Rover>();

                for (int i = 0; i < roverQty; i++)
                {
                    var validRoverCoordinates = CheckRoverCoordinate(coordinatesRover);

                    rovers.Add(new Rover(plateau)
                    {
                        NavigationFace = (NavigationFace)Enum.Parse(typeof(NavigationFace), validRoverCoordinates[2]),
                        X = Convert.ToInt32(validRoverCoordinates[0]),
                        Y = Convert.ToInt32(validRoverCoordinates[1])
                    });
                }

                foreach (var rover in rovers.Select((rover, index) => new { rover, index }))
                {
                    string[] moveLetterArray = CheckRoverMoveLetter(moveLetters);
                    rover.rover.NavigationLetter = moveLetters;
                }

                var outputRovers = new List<Rover>();

                foreach (var rover in rovers.Select((rover, index) => new { rover, index }))
                {
                    string[] moveLetterArray = rover.rover.NavigationLetter.ToCharArray().Select(c => c.ToString()).ToArray();
                    var outputRover = GetRoverPosition(rover.rover, moveLetterArray);
                    outputRovers.Add(outputRover);                   
                }
                return outputRovers;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }


        #region private utility methods 
        private static Rover GetRoverPosition(Rover rover, params string[] moveLetters)
        {
            foreach (var letter in moveLetters)
            {
                var navigationLetter = (NavigationLetters)Enum.Parse(typeof(NavigationLetters), letter);

                if (navigationLetter == NavigationLetters.L || navigationLetter == NavigationLetters.R)
                {
                    rover.NavigationFace = (int)navigationLetter + (int)rover.NavigationFace >= 360
                        ? (NavigationFace)((int)navigationLetter + (int)rover.NavigationFace - 360)
                        : (
                            (int)navigationLetter + (int)rover.NavigationFace < 0
                            ? (NavigationFace)((int)navigationLetter + (int)rover.NavigationFace + 360)
                            : (NavigationFace)((int)navigationLetter + (int)rover.NavigationFace)
                        );
                }
                else
                {
                    switch (rover.NavigationFace)
                    {
                        case NavigationFace.N:
                            rover.Y++;
                            break;
                        case NavigationFace.E:
                            rover.X++;
                            break;
                        case NavigationFace.S:
                            rover.Y--;
                            break;
                        case NavigationFace.W:
                            rover.X--;
                            break;
                        default:
                            break;
                    }
                }
            }

            return rover;
        }

        private static string[] CheckRoverCoordinate(string coordinateInput)
        {
            var coordinatesRover1Array = Regex.Split(coordinateInput.Trim(), @"\W+").Select((value, index) => new { value, index });

            if (coordinatesRover1Array.Count() > 3)
                throw new Exception("Invalid Coordinates");


            foreach (var item in coordinatesRover1Array)
            {
                if (item.index == 0)
                {
                    int coordinateX;

                    bool isInt = int.TryParse(item.value.ToString(), out coordinateX);

                    if (!isInt)
                        throw new Exception("X coordinate must be an integer and >=0");

                }
                else if (item.index == 1)
                {
                    int coordinateY;

                    bool isInt = int.TryParse(item.value.ToString(), out coordinateY);

                    if (!isInt)
                        throw new Exception("Y coordinate must be an integer and >=0");
                }
                else if (item.index == 2)
                {
                    var values = Enum.GetValues(typeof(NavigationFace)).Cast<NavigationFace>();

                    if (!values.Any(u => u.ToString() == item.value.ToString()))
                    {
                        throw new Exception("Navigation Face must be valid");
                    }
                }
            }
            return coordinatesRover1Array.Select(u => u.value).ToArray();
        }

        private static string[] CheckRoverMoveLetter(string moveLetters)
        {
            string[] moveLetterArray = moveLetters.ToCharArray().Select(c => c.ToString()).ToArray();

            var values = Enum.GetValues(typeof(NavigationLetters)).Cast<NavigationLetters>();

            foreach (var item in moveLetterArray)
            {
                if (!values.Any(u => u.ToString() == item))
                    throw new Exception("Navigation letter must be valid");
            }

            return moveLetterArray;
        }

      
        #endregion
    }
}

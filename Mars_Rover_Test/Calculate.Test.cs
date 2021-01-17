using Mars_Rover;
using Mars_Rover.Enums;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mars_Rover_Test
{
    public class Calculate_Test
    {

        private ICalculable _calculate;

        [SetUp]
        public void Setup()
        {
            _calculate = new Calculate();
        }



        [TestCase("5 5", "1", "1 2 N", "LMLMLMLMM", "1 3 N")]
        [TestCase("5 5", "1", "3 3 E", "MMRMMRMRRM", "5 1 E")]
        [TestCase("10 10", "1", "4 3 S", "MLMLMRM", "6 3 E")]
        public void IsValid_GivenCommand_ReturnsExpectedResult(string plateauCoordinates, string qty, string coordinatesRover, string moveLetters, string output)
        {
            var result = _calculate.Command(plateauCoordinates, qty, coordinatesRover, moveLetters).FirstOrDefault();

            var resultText = result.X + " " + result.Y + " " + result.NavigationFace.ToString();
            
            Assert.AreEqual(output, resultText);
        }



        [TestCase("5 5", "1", "1 2 N", "LMLMLMLMM", "1 5 N")]
        [TestCase("5 5", "1", "3 3 E", "MMRMMRMRRM", "5 1 N")]
        [TestCase("10 10", "1", "6 3 S", "MLMLMRM", "6 3 E")]
        public void IsNotValid_GivenCommand_ReturnsExpectedResult(string plateauCoordinates, string qty, string coordinatesRover, string moveLetters, string output)
        {
            var result = _calculate.Command(plateauCoordinates, qty, coordinatesRover, moveLetters).FirstOrDefault();

            var resultText = result.X + " " + result.Y + " " + result.NavigationFace.ToString();

            Assert.AreNotEqual(output, resultText);
        }



        [TestCase("5 5 7", "1", "1 2 N", "LMLMLMLMM", "1 5 N")]
        [TestCase("5", "1", "1 2 N", "LMLMLMLMM", "1 5 N")]
        public void InvalidCoordinates_GivenCommand_ReturnsExpectedResult(string plateauCoordinates, string qty, string coordinatesRover, string moveLetters, string output)
        {
            Action testCode = () => { _calculate.Command(plateauCoordinates, qty, coordinatesRover, moveLetters); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);

            Assert.AreSame("Invalid Coordinates", ex.Message);
        }




        [TestCase("5 5", "-1", "1 2 N", "LMLMLMLMM", "1 5 N")]
        [TestCase("5 5", "g", "1 2 N", "LMLMLMLMM", "1 5 N")]
        public void RoverQuantityInvalid_GivenCommand_ReturnsExpectedResult(string plateauCoordinates, string qty, string coordinatesRover, string moveLetters, string output)
        {
            Action testCode = () => { _calculate.Command(plateauCoordinates, qty, coordinatesRover, moveLetters); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);

            Assert.AreSame("Please enter valid number", ex.Message);
        }



        [TestCase("10 10", "1", "4 3 S", "MLMLMRM", "6 3 E")]
        [TestCase("5 5", "1", "1 2 N", "LMLMLMLMM", "1 3 N")]
        public void RoverQuantityValid_GivenCommand_ReturnsExpectedResult(string plateauCoordinates, string qty, string coordinatesRover, string moveLetters, string output)
        {
            Action testCode = () => { _calculate.Command(plateauCoordinates, qty, coordinatesRover, moveLetters); };

            var ex = Record.Exception(testCode);

            Assert.Null(ex);
        }
    }
}
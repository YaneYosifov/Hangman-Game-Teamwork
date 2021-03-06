﻿namespace Hangman.Logic.Factories.Tests
{
    using NUnit.Framework;
    using Factories;
    using Contracts;
    using Commands;

    [TestFixture()]
    public class CommandFactoryTests
    {
        [TestCase(null)]
        [TestCase("Lizzard")]
        [TestCase("")]
        [TestCase(" ")]
        [ExpectedException]
        public void CreateCommandInvalidCommandTest(string commandString)
        {
            var factory = new CommandFactory();
            ICommand command=factory.CreateCommand(commandString);
        }

        [TestCase("exit")]
        [TestCase("Exit")]
        [TestCase("EXIT")]
        public void CreateCommandExitSteingShouldCreateExitCommandTest(string input)
        {
            var factory = new CommandFactory();
            ICommand command=factory.CreateCommand(input);
            Assert.AreNotSame(command, new Exit());
        }

        [TestCase("help")]
        [TestCase("Help")]
        [TestCase("HELP")]
        public void CreateCommandHelpInputShouldCreateHelpCommandTest(string input)
        {
            var factory = new CommandFactory();
            ICommand command=factory.CreateCommand(input);
            Assert.AreNotSame(command, new Help());
        }

        [TestCase("a")]
        [TestCase("A")]
        [TestCase("h")]
        public void CreateCommandCharInputShouldCreateGuessCommandTest(string input)
        {
            var factory = new CommandFactory();
            ICommand command=factory.CreateCommand(input);
            Assert.AreNotSame(command, new LetterGuess(input));
        }

        [TestCase("restart")]
        [TestCase("Restart")]
        [TestCase("RESTART")]
        public void CreateCommandRestartInputShouldCreateRestartCommandTest(string input)
        {
            var factory = new CommandFactory();
            ICommand command=factory.CreateCommand(input);
            Assert.AreNotSame(command, new Restart());
        }

        [TestCase("top")]
        [TestCase("Top")]
        [TestCase("TOP")]
        public void CreateCommandTopInputShouldCreateTopCommandTest(string input)
        {
            var factory = new CommandFactory();
            ICommand command=factory.CreateCommand(input);
            Assert.AreNotSame(command, new Top());
        }
    }
}
﻿namespace Hangman.Tests.CommandFactory
{
    using Logic;
    using Logic.Commands;
    using Logic.Engines;
    using Logic.Factories;
    using Logic.Games;
    using Logic.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandFactoryTests
    {
        [TestMethod]
        public void CommandFactoryCreatesHelp()
        {
            var engine = new HangmanEngine(new ConsolePrinter(), new ConsoleReader(), new CommandFactory(),
                             new Validator(), new HangmanGame(new WordInitializer()));
            var help = engine.CommandFactory.CreateCommand("help");

            Assert.IsNotNull(help);
        }

        [TestMethod]
        public void CommandFactoryCreatesExit()
        {
            var engine = new HangmanEngine(new ConsolePrinter(), new ConsoleReader(), new CommandFactory(),
                             new Validator(), new HangmanGame(new WordInitializer()));
            var exit = engine.CommandFactory.CreateCommand("exit");

            Assert.IsNotNull(exit);
        }

        [TestMethod]
        public void CommandFactoryCreatesTop()
        {
            var engine = new HangmanEngine(new ConsolePrinter(), new ConsoleReader(), new CommandFactory(),
                             new Validator(), new HangmanGame(new WordInitializer()));
            var top = engine.CommandFactory.CreateCommand("top");

            Assert.IsNotNull(top);
        }

        [TestMethod]
        public void CommandFactoryCreatesRestart()
        {
            var engine = new HangmanEngine(new ConsolePrinter(), new ConsoleReader(), new CommandFactory(),
                             new Validator(), new HangmanGame(new WordInitializer()));
            var restart = engine.CommandFactory.CreateCommand("restart");

            Assert.IsNotNull(restart);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Incorrect guess or command!")]

        public void CommandFactoryCreatesLetterGuess()
        {
            var engine = new HangmanEngine(new ConsolePrinter(), new ConsoleReader(), new CommandFactory(),
                             new Validator(), new HangmanGame(new WordInitializer()));
            var letterGuess = engine.CommandFactory.CreateCommand("LetterGuess");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Incorrect guess or command!")]
        public void CommandFactoryCreatesLetterGuessIfAWrongCommandIsPassed() // TODO: get test to pass
        {
            var engine = new HangmanEngine(new ConsolePrinter(), new ConsoleReader(), new CommandFactory(),
                             new Validator(), new HangmanGame(new WordInitializer()));
            var defaultLetterGuessCase = engine.CommandFactory.CreateCommand("brum");
        }
    }
}
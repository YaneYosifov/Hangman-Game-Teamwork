﻿namespace Hangman.Logic
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Commands;
    using Utils;
    using Common;
    using Contracts;
    using Factories;

    internal class HangmanEngine : WordInitializer, IEngine
    {
        private readonly Scoreboard scoreboard;

        private int mistakes;
        private bool haveAllGamesEnded;
        private bool hasCurrentGameEnded;
        private bool isHelpUsed;
        private Validator validator;

        private ConsolePrinter printer;
        private ConsoleReader inputReader;

        internal HangmanEngine()
        {
            this.Mistakes = this.mistakes;
            this.HaveAllGamesEnded = this.haveAllGamesEnded;
            this.HasCurrentGameEnded = this.hasCurrentGameEnded;
            this.IsHelpUsed = this.isHelpUsed;
            this.scoreboard = Scoreboard.Instance;
            this.CommandFactory = new CommandFactory();
            this.printer = new ConsolePrinter();
            this.validator = new Validator();
            this.inputReader = new ConsoleReader();
        }

        public bool HaveAllGamesEnded
        {
            get 
            {
                return this.haveAllGamesEnded;
            }

            set
            {
                this.haveAllGamesEnded = value; 
            }
        }

        public bool HasCurrentGameEnded
        {
            get 
            { 
                return this.hasCurrentGameEnded;
            }

            set
            { 
                this.hasCurrentGameEnded = value; 
            }
        }

        internal bool IsHelpUsed
        {
            get 
            {
                return this.isHelpUsed; 
            }

            set
            {
                this.isHelpUsed = value;
            }
        }

        private CommandFactory CommandFactory { get; set; }

        private int Mistakes
        {
            get 
            {
                return this.mistakes; 
            }

            set 
            {
                this.mistakes = value;
            }
        }

        public ConsolePrinter Printer
        {
            get 
            {
                return this.printer; 
            }

            set 
            {
                this.printer = value;
            }
        }

        public bool CheckIfGameIsWon()
        {
            bool isWordRevealed = this.CheckIfWordIsRevealed(this.WordOfUnderscores);
            if (isWordRevealed)
            {
                this.printer.PrintWinMessage(this.Mistakes, this.isHelpUsed, this.scoreboard);
                string currentPlayerName = this.AskForPlayerName();
                var player = new Player(currentPlayerName, this.Mistakes);
                this.scoreboard.AddNewRecord(player);
                this.printer.PrintAllRecords(this.scoreboard.GetAllRecords());
                this.printer.PrintWordToGuess(this.WordOfUnderscores);
            }

            return isWordRevealed;
        }

        internal void ProcessUserGuess(char suggestedLetter)
        {
            int numberOfRevealedLetters = this.CheckUserGuess(suggestedLetter, this.Word, this.WordOfUnderscores);
            if (numberOfRevealedLetters > 0)
            {
                bool isWordRevealed = this.CheckIfWordIsRevealed(this.WordOfUnderscores);
                if (!isWordRevealed)
                {
                    this.printer.PrintNumberOfRevealedLetters(numberOfRevealedLetters);
                }
            }
            else
            {
                this.printer.PrintNoRevealedLettersMessage(suggestedLetter);
                this.Mistakes++;
            }
        }

        public void GetUserInput()
        {
            bool isInputValid = false;
            ICommand command;

            while (!isInputValid)
            {
                this.printer.PrintEnterLetterOrCommandMessage();
                string inputCommand = inputReader.ReadLine();
                inputCommand = inputCommand.ToLower();

                if(validator.InputCommandValidator(inputCommand))
                {
                    isInputValid = true;
                    command = CommandFactory.CreateCommand(inputCommand, this, this.scoreboard.TopFiveRecords);
                    command.Execute();
                }
            }
        }

        internal void RevealLetter(string secretWord, char[] wordToGuess)
        {
            int nextUnrevealedLetterIndex = 0;
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == '_')
                {
                    nextUnrevealedLetterIndex = i;
                    break;
                }
            }

            char letterToBeRevealed = secretWord[nextUnrevealedLetterIndex];
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (letterToBeRevealed == secretWord[i])
                {
                    wordToGuess[i] = letterToBeRevealed;
                }
            }

            this.printer.PrintRevealLetterMessage(letterToBeRevealed);
        }

        private bool CheckIfWordIsRevealed(char[] wordToGuess)
        {
            return wordToGuess.All(ch => ch != '_');
        }

        // TODO if user enter already revealed letter, don't count mistake and print proper message.

        private int CheckUserGuess(char suggestedLetter, string secretWord, char[] wordToGuess)
        {
            int numberOfRevealedLetters = 0;
            bool isLetterAlreadyRevealed = this.CheckIfLetterIsAlreadyRevealed(suggestedLetter, wordToGuess);
            if (!isLetterAlreadyRevealed)
            {
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (suggestedLetter == secretWord[i])
                    {
                        wordToGuess[i] = suggestedLetter;
                        numberOfRevealedLetters++;
                    }
                }
            }

            return numberOfRevealedLetters;
        }

        private bool CheckIfLetterIsAlreadyRevealed(char suggestedLetter, char[] wordToGuess)
        {
            bool isLetterRevealed = false;
            foreach (char letter in wordToGuess)
            {
                if (letter == suggestedLetter)
                {
                    isLetterRevealed = true;
                }
            }

            return isLetterRevealed;
        }

        private string AskForPlayerName()
        {
            string name = null;
            bool isInputValid = false;
            this.printer.Write(GlobalMessages.EnterNameForScoreBoard);
            while (!isInputValid)
            {
                string inputName = inputReader.ReadLine();

                if (validator.PlayerNameValidator(inputName))
                {
                    name = inputName;
                    isInputValid = true;
                }
            }

            return name;
        }
    }
}
﻿namespace Hangman.Logic
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Contracts;
    using Utils;

    /// <summary>
    /// Class for rendering on the console
    /// </summary>
    public class ConsolePrinter : IPrinter
    {
        public void PrintWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(GlobalMessages.Welcome);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(GlobalMessages.CommandOptions);
            Console.ResetColor();
        }

        public void PrintWordToGuess(char[] wordToGuess)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(GlobalMessages.SecretWord);
            foreach (var letter in wordToGuess)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("{0} ", letter);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
        }

        public void PrintInvalidEntryMessage()
        {
            Console.WriteLine(GlobalMessages.IncorrectGuessOrCommand);
        }

        public void PrintWinMessage(uint mistakesCount, bool isHelpUsed, IScoreboard scoreboard, char[] wordToGuess)
        {
            this.PrintWordToGuess(wordToGuess);

            if (isHelpUsed)
            {
                Console.WriteLine(GlobalMessages.WinWithHelp, mistakesCount);
            }
            else
            {
                Console.WriteLine(GlobalMessages.Win, mistakesCount);
            }
        }

        public void PrintNumberOfRevealedLetters(int numberOfRevealedLetters)
        {
            if (numberOfRevealedLetters == 1)
            {
                Console.WriteLine(GlobalMessages.OneLetterRevealed, numberOfRevealedLetters);
            }
            else
            {
                Console.WriteLine(GlobalMessages.MultipleLettersRevealed, numberOfRevealedLetters);
            }
        }

        public void PrintNoRevealedLettersMessage(char suggestedLetter)
        {
            var uppercaseSuggestedLetter = char.ToUpper(suggestedLetter);
            Console.WriteLine(GlobalMessages.LetterNotRevealed, uppercaseSuggestedLetter);
        }

        public void PrintEnterLetterOrCommandMessage()
        {
            Console.Write(GlobalMessages.EnterLetterOrCommand);
        }

        public void PrintRevealLetterMessage(char letterToBeRevealed)
        {
            Console.WriteLine(GlobalMessages.HelpRevealLetter, letterToBeRevealed);
        }

        public void PrintAllRecords(List<Player> topFiveRecords)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(GlobalMessages.HighScores);
            if (topFiveRecords.Count != 0)
            {
                for (int i = 0; i < topFiveRecords.Count; i++)
                {
                    string name = topFiveRecords[i].PlayerName;
                    uint mistakes = topFiveRecords[i].Score;
                    Console.WriteLine(GlobalMessages.ScoreFormat, i + 1, name, mistakes);
                }
            }
            else
            {
                Console.WriteLine(GlobalMessages.NoScoresYet);
            }
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintLetterAlreadyRevealedMessage()
        {
            Console.WriteLine("The letter you have entered is already revealed!");
        }
    }
}

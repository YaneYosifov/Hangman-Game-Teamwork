﻿namespace Hangman.Logic.Contracts
{
    using System.Collections.Generic;
    using Utils;

    /// <summary>
    /// Abstraction for printing messages on the UI
    /// </summary>
    public interface IPrinter
    {
        /// <summary>
        ///     Display five players with highest score.
        /// </summary>
        /// <param name="topFiveRecords"></param>
        void PrintAllRecords(List<Player> topFiveRecords);

        void PrintEnterLetterOrCommandMessage();

        void PrintInvalidEntryMessage();

        void PrintNoRevealedLettersMessage(char suggestedLetter);

        void PrintNumberOfRevealedLetters(int numberOfRevealedLetters);

        void PrintRevealLetterMessage(char letterToBeRevealed);

        void PrintWelcomeMessage();

        void PrintWinMessage(uint mistakesCount, bool isHelpUsed, IScoreboard scoreboard, char[] wordToGuess);

        void PrintWordToGuess(char[] wordToGuess);

        void PrintLetterAlreadyRevealedMessage();

        void Write(string message);
    }
}
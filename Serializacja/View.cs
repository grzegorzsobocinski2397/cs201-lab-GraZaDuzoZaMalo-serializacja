using Serializacja.Models;
using Serializacja.Resources;
using System;
using static System.Console;

namespace Serializacja
{
    /// <summary>
    /// Displays messages in Console Window.
    /// </summary>
    internal class View
    {
        #region Public Constant Fields

        /// <summary>
        /// Character that when typed will close the application or cause it to save game.
        /// </summary>
        public const char CLOSE_APPLICATION_CHAR = 'X';

        #endregion Public Constant Fields

        #region Private Fields

        /// <summary>
        /// Instance of the <see cref="Controller"/>.
        /// </summary>
        private readonly Controller controller;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initialize the View with Controller.
        /// </summary>
        /// <param name="controller">Controller of the game.</param>
        public View(Controller controller)
        {
            this.controller = controller;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Clears console.
        /// </summary>
        public void ClearConsole() => Clear();

        /// <summary>
        /// Shows the welcome screen for user.
        /// </summary>
        public void DisplayWelcomeScreen() => WriteLine(ConsoleMessages.ComputerStart);

        /// <summary>
        /// Load the player's input from Console.
        /// </summary>
        /// <returns></returns>
        public int LoadInput()
        {
            int score = 0;
            bool isInputCorrect = false;
            while (!isInputCorrect)
            {
                Write($"Try making a guess (or type {CLOSE_APPLICATION_CHAR} to stop the game): ");
                try
                {
                    string value = ReadLine().TrimStart().ToUpper();
                    if (value.Length > 0 && value[0].Equals(CLOSE_APPLICATION_CHAR))
                    {
                        if (AskUserForInput(ConsoleMessages.SaveGame))
                        {
                            controller.SaveGame();
                            controller.CloseApplication();
                        }
                        else
                        {
                            controller.CloseApplication();
                        }
                    }

                    score = int.Parse(value);
                    isInputCorrect = true;
                }
                catch (FormatException)
                {
                    WriteLine(ConsoleMessages.NotNumber);
                    continue;
                }
                catch (OverflowException)
                {
                    WriteLine(ConsoleMessages.NumberTooBig);
                    continue;
                }
                catch (Exception)
                {
                    WriteLine(ConsoleMessages.UnknownException);
                    continue;
                }
                ClearConsole();
            }
            return score;
        }

        /// <summary>
        /// Display game description for the player.
        /// </summary>
        public void DisplayGameDescription()
        {
            WriteLine(ConsoleMessages.GameRules);
        }

        /// <summary>
        /// Ask the user a question and wait for input.
        /// </summary>
        /// <returns>User's input.</returns>
        public bool AskUserForInput(string message)
        {
            Write(message);
            char answer = ReadKey().KeyChar;
            WriteLine();
            return answer == 'y' || answer == 'Y';
        }

        /// <summary>
        /// Shows the previous rounds information.
        /// </summary>
        public void DisplayGameHistory()
        {
            if (controller.Rounds.Count == 0)
            {
                WriteLine("--- empty ---");
                return;
            }

            WriteLine("Nr    Guess     Answer     Time    Status");
            WriteLine("=================================================");
            int i = 1;
            foreach (Round ruch in controller.Rounds)
            {
                WriteLine($"{i}     {ruch.Number}      {ruch.Answer}       {ruch.Time.Second}        {ruch.Status}");
                i++;
            }
        }

        /// <summary>
        /// Informs the user that the value was too high.
        /// </summary>
        public void DisplayTooMuchMessage()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(ConsoleMessages.TooMuch);
            ResetColor();
        }

        /// <summary>
        /// Informs the user that the value was too low.
        /// </summary>
        public void DisplayTooLittleMessage()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(ConsoleMessages.TooLittle);
            ResetColor();
        }

        /// <summary>
        /// Informs the user that he/she won.
        /// </summary>
        public void DisplayWinMessage()
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine(ConsoleMessages.CorrectGuess);
            ResetColor();
        }

        /// <summary>
        /// Displays information about previous game.
        /// </summary>
        /// <param name="numberOfRounds">Number of rounds that user has played.</param>
        /// <param name="startDate">Start date of the game.</param>
        /// <param name="duration">Total game duration.</param>
        public void DisplayLoadGameInformation(int numberOfRounds, DateTime startDate, TimeSpan duration)
        {
            WriteLine($"Rounds played: {numberOfRounds}. Start date: {startDate.ToString("yyyy-MM-dd HH:mm")}. Duration: {duration.ToString()}");
        }

        /// <summary>
        /// Informs the user about previous game.
        /// </summary>
        public void DisplayInformationAboutPreviousGame()
        {
            WriteLine($"\n\n{ConsoleMessages.SaveFileInformation}");
        }

        #endregion Public Methods
    }
}
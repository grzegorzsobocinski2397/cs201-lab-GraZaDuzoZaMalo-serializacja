using Serializacja.Models;
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
        public void DisplayWelcomeScreen() => WriteLine("Wylosowałem liczbę z zakresu ");

        /// <summary>
        /// Load the player's input from Console.
        /// </summary>
        /// <returns></returns>
        public int LoadInput()
        {
            int score = 0;
            bool isCorrectAnswer = false;
            while (!isCorrectAnswer)
            {
                Write("Podaj swoją propozycję (lub " + Controller.CLOSE_APPLICATION_CHAR + " aby przerwać): ");
                try
                {
                    string value = ReadLine().TrimStart().ToUpper();
                    if (value.Length > 0 && value[0].Equals(CLOSE_APPLICATION_CHAR))
                        throw new GameEndException();

                    //UWAGA: ponizej może zostać zgłoszony wyjątek
                    score = int.Parse(value);
                    isCorrectAnswer = true;
                }
                catch (FormatException)
                {
                    WriteLine("Podana przez Ciebie wartość nie przypomina liczby! Spróbuj raz jeszcze.");
                    continue;
                }
                catch (OverflowException)
                {
                    WriteLine("Przesadziłeś. Podana przez Ciebie wartość jest zła! Spróbuj raz jeszcze.");
                    continue;
                }
                catch (Exception)
                {
                    WriteLine("Nieznany błąd! Spróbuj raz jeszcze.");
                    continue;
                }
            }
            return score;
        }

        /// <summary>
        /// Display game description for the player.
        /// </summary>
        public void DisplayGameDescription()
        {
            WriteLine("Gra w \"Za dużo za mało\"." + Environment.NewLine
                + "Twoimm zadaniem jest odgadnąć liczbę, którą wylosował komputer." + Environment.NewLine + "Na twoje propozycje komputer odpowiada: za dużo, za mało albo trafiłeś");
        }

        /// <summary>
        /// Ask the user if he/she wants to continue the game.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>User's input.</returns>
        public bool DisplayContinueGameDialog(string prompt)
        {
            Write(prompt);
            char answer = ReadKey().KeyChar;
            WriteLine();
            return (answer == 't' || answer == 'T');
        }

        /// <summary>
        /// Shows the previous rounds information.
        /// </summary>
        public void DisplayGameHistory()
        {
            if (controller.Rounds.Count == 0)
            {
                WriteLine("--- pusto ---");
                return;
            }

            WriteLine("Nr    Propozycja     Odpowiedź     Czas    Status");
            WriteLine("=================================================");
            int i = 1;
            foreach (var ruch in controller.Rounds)
            {
                WriteLine($"{i}     {ruch.Number}      {ruch.Answer}  {ruch.Time.Second}   {ruch.Status}");
                i++;
            }
        }

        /// <summary>
        /// Informs the user that the value was too high.
        /// </summary>
        public void DisplayTooMuchMessage()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Za dużo!");
            ResetColor();
        }

        /// <summary>
        /// Informs the user that the value was too low.
        /// </summary>
        public void DisplayTooLittleMessage()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Za mało!");
            ResetColor();
        }

        /// <summary>
        /// Informs the user that he/she won.
        /// </summary>
        public void DisplayWinMessage()
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Trafiono!");
            ResetColor();
        }

        #endregion Public Methods
    }
}
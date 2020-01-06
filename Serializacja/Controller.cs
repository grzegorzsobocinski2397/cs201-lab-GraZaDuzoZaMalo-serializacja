using Serializacja.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serializacja
{
    /// <summary>
    /// Controller for the game.
    /// </summary>
    public class Controller
    {
        #region Public Constant Values

        /// <summary>
        /// Character that when typed will close the application.
        /// </summary>
        public const char CLOSE_APPLICATION_CHAR = 'X';

        #endregion Public Constant Values

        #region Public Properties

        /// <summary>
        /// The maximum value user can try to guess and the computer can generate.
        /// </summary>
        public int MaximumNumber { get; private set; } = 100;

        /// <summary>
        /// The minimum value user can try to guess and the computer can generate.
        /// </summary>
        public int MinimumNumber { get; private set; } = 1;

        /// <summary>
        /// List of rounds played by the user.
        /// </summary>
        public IReadOnlyList<Round> Rounds
        {
            get
            { return game.Rounds; }
        }

        #endregion Public Properties

        #region Private Fields

        /// <summary>
        /// Instance of the <see cref="Game"/>. Contains logic of the game.
        /// </summary>
        private Game game;

        /// <summary>
        /// Instance of the <see cref="View"/>. Displays messages in Console Window.
        /// </summary>
        private View view;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Default constructor. Start new game and view.
        /// </summary>
        public Controller()
        {
            game = new Game();
            view = new View(this);
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Display the rules to the player and start game.
        /// </summary>
        public void StartApplication()
        {
            view.DisplayGameDescription();
            while (view.DisplayStartGameMessage())
            {
                StartGame();
            }
        }

        /// <summary>
        /// Create new instance of the game resulting with new secret number.
        /// </summary>
        public void StartGame()
        {
            view.ClearConsole();

            game = new Game(MinimumNumber, MaximumNumber); // may throw an exception.

            do
            {
                int propozycja = 0;
                try
                {
                    propozycja = view.LoadInput();
                }
                catch (GameEndException)
                {
                    game.StopGame();
                }

                Console.WriteLine(propozycja);

                if (game.Status == GameStatus.Lost)
                {
                    break;
                }

                //Console.WriteLine( gra.Ocena(propozycja) );
                //oceń propozycję, break
                switch (game.Score(propozycja))
                {
                    case Answer.TooBig:
                        view.DisplayTooMuchMessage();
                        break;

                    case Answer.TooSmall:
                        view.DisplayTooLittleMessage();
                        break;

                    case Answer.Correct:
                        view.DisplayWinMessage();
                        break;

                    default:
                        break;
                }
                view.DisplayGameHistory();
            }
            while (game.Status == GameStatus.OnGoing);

            //if StatusGry == Przerwana wypisz poprawną odpowiedź
            //if StatusGry == Zakończona wypisz statystyki gry
        }

        /// <summary>
        /// Parameterize the game by asking the user for new <see cref="MinimumNumber"/> and <see cref="MaximumNumber"/>.
        /// </summary>
        /// <param name="min">The minimum value user can try to guess and the computer can generate.</param>
        /// <param name="max">The maximum value user can try to guess and the computer can generate.</param>
        public void SetRangeOfValues(ref int min, ref int max)
        {
        }

        /// <summary>
        /// Return the amount of rounds user have played and tried to guess the number.
        /// </summary>
        /// <returns>Amount of rounds.</returns>
        public int AmountOfTries() => game.Rounds.Count();

        /// <summary>
        /// Save the progress and close application.
        /// </summary>
        public void CloseApplication()
        {
            game = null;
            view.ClearConsole();
            view = null;
            Environment.Exit(0);
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        public void EndGame()
        {
            game.StopGame();
        }

        public int LoadGameOrEnd(string value, int defaultValue)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            value = value.TrimStart().ToUpper();
            if (value.Length > 0 && value[0].Equals(CLOSE_APPLICATION_CHAR))
                throw new GameEndException();

            //UWAGA: ponizej może zostać zgłoszony wyjątek
            return int.Parse(value);
        }

        #endregion Public Methods
    }
}
using Serializacja.Models;
using Serializacja.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

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

            if (File.Exists($"{Environment.CurrentDirectory}\\SaveFile.xml"))
            {
                view.DisplayInformationAboutPreviousGame();

                SaveFile save = LoadGame();

                if (save != null)
                {
                    view.DisplayLoadGameInformation(save.Rounds.Count, save.StartDate, save.TotalGameDuration);
                    RemoveSaveFile();

                    while (view.AskUserForInput(ConsoleMessages.LoadGame))
                    {
                        game = new Game(save);
                        StartGame(game);
                    }
                }
            }

            while (view.AskUserForInput(ConsoleMessages.StartGame))
            {
                game = new Game(MinimumNumber, MaximumNumber);
                StartGame(game);
            }
        }

        /// <summary>
        /// Create new instance of the game resulting with new secret number.
        /// </summary>
        public void StartGame(Game game)
        {
            view.ClearConsole();

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
        /// Saves current game information. Remove save file if the saving process was incorrect.
        /// </summary>
        public void SaveGame()
        {
            if (!game.SaveGame())
            {
                RemoveSaveFile();
            }
            CloseApplication();
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        public void EndGame()
        {
            game.StopGame();
        }

        /// <summary>
        /// Removes the save file.
        /// </summary>
        public void RemoveSaveFile()
        {
            try
            {
                File.Delete($"{Environment.CurrentDirectory}\\SaveFile.xml");
            }
            catch (Exception ex)
            {
                view.AskUserForInput($"Something went wrong while deleting a save file. {ex.Message} ");
            }
        }
        
        /// <summary>
        /// Loads the file.
        /// </summary>
        public SaveFile LoadGame()
        {
            Stream stream = new FileStream("SaveFile.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
            DataContractSerializer serializer = new DataContractSerializer(typeof(SaveFile));


            try
            {
                using (stream)
                {
                    using (reader)
                    {
                        SaveFile saveFile = (SaveFile)serializer.ReadObject(reader, true);
                        return saveFile;
                    }
                }
            }
            catch (Exception ex)
            {
                view.AskUserForInput($"There was an error during game load. A new game will start instead. Please click anything. Error: {ex.Message}.");
                RemoveSaveFile();
                game = new Game();
            }


            return null;
        }

        #endregion Public Methods
    }
}
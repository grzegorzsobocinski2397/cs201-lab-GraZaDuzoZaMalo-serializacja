using System;
using System.Collections.Generic;

namespace Serializacja.Models
{
    /// <summary>
    /// Contains all the information about saved game.
    /// </summary>
    [Serializable]
    public class SaveFile
    {
        #region Public Properties

        /// <summary>
        /// List of rounds played by the user.
        /// </summary>
        public List<Round> Rounds { get; }

        /// <summary>
        /// The minimum value user can try to guess and the computer can generate.
        /// </summary>
        public int MinimumValue { get; }

        /// <summary>
        /// The maximum value user can try to guess and the computer can generate.
        /// </summary>
        public int MaximumValue { get; }

        /// <summary>
        /// Currently generated secret number. User has to guess it.
        /// </summary>
        public int Secret { get; }

        /// <summary>
        /// Start date of the initialization of this game.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Returns current game duration from constructor initialization till this time.
        /// </summary>
        public TimeSpan GameDuration { get; }

        /// <summary>
        /// Returns total current game duration if the game wasn't started.
        /// </summary>
        public TimeSpan TotalGameDuration { get; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Creates a save file.
        /// </summary>
        /// <param name="game">Game instance. Take all public properties from here.</param>
        /// <param name="rounds">List of rounds played by the user.</param>
        /// <param name="secret">Secret number.</param>
        public SaveFile(Game game, List<Round> rounds, int secret)
        {
            Rounds = rounds;
            MinimumValue = game.MinimumNumber;
            MaximumValue = game.MaximumNumber;
            StartDate = game.StartDate;
            GameDuration = game.GameDuration;
            TotalGameDuration = game.TotalGameDuration;
            Secret = secret;
        }

        #endregion Constructor
    }
}
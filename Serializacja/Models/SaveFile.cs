using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Serializacja.Models
{
    /// <summary>
    /// Contains all the information about saved game.
    /// </summary>
    [DataContract(Name = "SaveFile")]
    public class SaveFile
    {
        #region Public Properties

        /// <summary>
        /// List of rounds played by the user.
        /// </summary>
        [DataMember()]
        public List<Round> Rounds { get; set; }

        /// <summary>
        /// The minimum value user can try to guess and the computer can generate.
        /// </summary>
        [DataMember()]
        public int MinimumValue { get; set; }

        /// <summary>
        /// The maximum value user can try to guess and the computer can generate.
        /// </summary>
        [DataMember()]
        public int MaximumValue { get; set; }

        /// <summary>
        /// Currently generated secret number. User has to guess it.
        /// </summary>
        [DataMember()]
        public int Secret { get; set; }

        /// <summary>
        /// Start date of the initialization of this game.
        /// </summary>
        [DataMember()]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Returns total current game duration.
        /// </summary>
        [DataMember()]
        public TimeSpan TotalGameDuration { get; set; }

        /// <summary>
        /// Current game status.
        /// </summary>
        [DataMember()]
        public GameStatus GameStatus { get; set; }

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
            GameStatus = GameStatus.Halted;
            Rounds = rounds;
            MinimumValue = game.MinimumNumber;
            MaximumValue = game.MaximumNumber;
            StartDate = game.StartDate;
            TotalGameDuration = game.TotalGameDuration;
            Secret = secret;
        }

        #endregion Constructor
    }
}
using System;
using System.Runtime.Serialization;

namespace Serializacja.Models
{
    /// <summary>
    /// One round that user have played.
    /// </summary>
    [DataContract(Name = "Round")]
    public class Round
    {
        #region Public Properties

        /// <summary>
        /// Number that user wrote.
        /// </summary>
        [DataMember()]
        public int? Number { get; set; }

        /// <summary>
        /// Computer's answer to the <see cref="Number"/>.
        /// </summary>
        [DataMember()]
        public Answer? Answer { get; set; }

        /// <summary>
        /// Contains information about this game status.
        /// </summary>
        [DataMember()]
        public GameStatus Status { get; set; }

        /// <summary>
        /// Saves the date user tried to make that guess.
        /// </summary>
        [DataMember()]
        public DateTime Time { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// One round which is one player's guess.
        /// </summary>
        /// <param name="guess">Number that user wrote.</param>
        /// <param name="answer">Computer's answer to the <see cref="Number"/>.</param>
        /// <param name="status">Contains information about this game status.</param>
        public Round(int? guess, Answer? answer, GameStatus status)
        {
            Number = guess;
            Answer = answer;
            Status = status;
            Time = DateTime.Now;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Overriden method that returns string implementation of this class.
        /// </summary>
        /// <returns>String implementation of this class</returns>
        public override string ToString()
        {
            return $"({Number}, {Answer}, {Time}, {Status})";
        }

        #endregion Public Methods
    }
}
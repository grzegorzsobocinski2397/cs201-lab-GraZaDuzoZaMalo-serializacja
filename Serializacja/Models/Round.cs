using System;

namespace Serializacja.Models
{
    /// <summary>
    /// One round that user have played.
    /// </summary>
    public class Round
    {
        #region Public Properties

        /// <summary>
        /// Number that user wrote.
        /// </summary>
        public int? Number { get; }

        /// <summary>
        /// Computer's answer to the <see cref="Number"/>.
        /// </summary>
        public Answer? Answer { get; }

        /// <summary>
        /// Contains information about this game status.
        /// </summary>
        public GameStatus Status { get; }

        /// <summary>
        /// Saves the date user tried to make that guess.
        /// </summary>
        public DateTime Time { get; }

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
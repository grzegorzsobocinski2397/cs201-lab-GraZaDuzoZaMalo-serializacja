namespace Serializacja.Models
{
    /// <summary>
    /// Possible game statuses.
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// User is currently trying to guess the answer.
        /// </summary>
        OnGoing,
        /// <summary>
        /// User correctly guessed the answer.
        /// </summary>
        Ended,
        /// <summary>
        /// User gave up.
        /// </summary>
        Lost
    };
}

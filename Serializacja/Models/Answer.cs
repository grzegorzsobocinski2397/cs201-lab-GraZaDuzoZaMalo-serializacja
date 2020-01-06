namespace Serializacja.Models
{
    /// <summary>
    /// Contains information about user guess and secret number.
    /// </summary>
    public enum Answer
    {
        /// <summary>
        /// User's guess was too small.
        /// </summary>
        TooSmall = -1,
        /// <summary>
        /// User's guess was correct.
        /// </summary>
        Correct = 0,
        /// <summary>
        /// User's guess was too big.
        /// </summary>
        TooBig = 1
    };
}

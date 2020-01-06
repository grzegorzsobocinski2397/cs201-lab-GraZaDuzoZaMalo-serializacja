﻿using Serializacja.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializacja
{
    /// <summary>
    /// Contains logic of the game.
    /// </summary>
    public class Game
    {
        #region Private Fields

        /// <summary>
        /// Currently generated secret number. User has to guess it.
        /// </summary>
        readonly private int secretNumber;

        /// <summary>
        /// List of rounds played by the user.
        /// </summary>
        private List<Round> rounds;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// The maximum value user can try to guess and the computer can generate.
        /// </summary>
        public int MaximumNumber { get; } = 100;

        /// <summary>
        /// The minimum value user can try to guess and the computer can generate.
        /// </summary>
        public int MinimumNumber { get; } = 1;

        /// <summary>
        /// Contains information about current status of the game.
        /// </summary>
        public GameStatus Status { get; private set; }

        /// <summary>
        /// List of rounds played by the user.
        /// </summary>
        public IReadOnlyList<Round> Rounds { get { return rounds.AsReadOnly(); } }

        /// <summary>
        /// Start date of the initialization of this game.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// End date of the game.
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Returns current game duration from constructor initialization till this time.
        /// </summary>
        public TimeSpan GameDuration => DateTime.Now - StartDate;

        /// <summary>
        /// Returns total current game duration if the game wasn't started.
        /// </summary>
        public TimeSpan TotalGameDuration => (Status == GameStatus.OnGoing) ? GameDuration : (TimeSpan)(EndDate - StartDate);

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Creates game from previously saved game.
        /// </summary>
        /// <param name="game">Game instance. Take all public properties from here.</param>
        /// <param name="rounds">List of rounds played by the user.</param>
        /// <param name="secret">Secret number.</param>
        public Game(SaveFile save)
        {
            MaximumNumber = save.MaximumValue;
            MinimumNumber = save.MinimumValue;

            secretNumber = save.Secret;
            StartDate = save.StartDate;
            Status = GameStatus.OnGoing;
            EndDate = null;

            rounds = save.Rounds;
        }

        /// <summary>
        /// Parameterized constructor that sets the <see cref="MinimumNumber"/> and <see cref="MaximumNumber"/>.
        /// </summary>
        /// <param name="min">The maximum value user can try to guess and the computer can generate.</param>
        /// <param name="max">The minimum value user can try to guess and the computer can generate.</param>
        public Game(int min, int max)
        {
            if (min >= max)
            {
                throw new ArgumentException();
            }

            MinimumNumber = min;
            MaximumNumber = max;

            secretNumber = (new Random()).Next(MinimumNumber, MaximumNumber + 1);
            StartDate = DateTime.Now;
            EndDate = null;
            Status = GameStatus.OnGoing;

            rounds = new List<Round>();
        }

        /// <summary>
        /// Default constructor that will set the <see cref="MinimumNumber"/> and <see cref="MaximumNumber"/> to 1 and 100.
        /// </summary>
        public Game() : this(1, 100) { }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Check the user's score and add it to <see cref="Rounds"/>.
        /// </summary>
        /// <param name="guess">User's input.</param>
        /// <returns>An enum value informing about correctness of the answer.</returns>
        public Answer Score(int guess)
        {
            Answer answer;
            if (guess == secretNumber)
            {
                answer = Answer.Correct;
                Status = GameStatus.Ended;
                EndDate = DateTime.Now;
                rounds.Add(new Round(guess, answer, GameStatus.Ended));
            }
            else
            {
                answer = guess < secretNumber ? Answer.TooSmall : Answer.TooBig;
            }

            if (Status == GameStatus.OnGoing)
            {
                rounds.Add(new Round(guess, answer, GameStatus.OnGoing));
            }

            return answer;
        }

        /// <summary>
        /// Stop the game and return secret number that user was trying to guess.
        /// </summary>
        /// <returns>Currently generated secret number.</returns>
        public int StopGame()
        {
            if (Status == GameStatus.OnGoing)
            {
                Status = GameStatus.Lost;
                EndDate = DateTime.Now;
                rounds.Add(new Round(null, null, GameStatus.OnGoing));
            }

            return secretNumber;
        }

        /// <summary>
        /// Saves current game information.
        /// </summary>
        public void SaveGame()
        {
            SaveFile save = new SaveFile(this, rounds, secretNumber);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("SaveFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            using (stream)
            {
                try
                {
                    formatter.Serialize(stream, save);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }
        /// <summary>
        /// Loads the file.
        /// </summary>
        public SaveFile LoadGame()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("SaveFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            using (stream)
            {
                try
                {
                    return (SaveFile)formatter.Deserialize(stream);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }

            return null;
        }

        #endregion Public Methods
    }
}
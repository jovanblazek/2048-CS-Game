using System;
using game1024Core.Core;
using game1024Core.Entities;
using game1024Core.Services;

namespace game1024Console
{
    public class ConsoleUI
    {
        private Game game;
        private Field field;
        private readonly IScoreService _scoreService = new ScoreServiceFile();

        public ConsoleUI(Game game)
        {
            this.game = game;
            field = game.GetField();
            game.OnFieldChange += Print;
            game.OnGameStateChange += PrintGameEnd;
        }

        /// <summary>
        /// Main game loop. 
        /// </summary>
        public void Run()
        {
            Print();
            do
            {
                ProcessInput();
            } while (game.GameState == GameState.Playing);

            _scoreService.AddScore(new Score
                {Player = Environment.UserName, Points = field.Score, PlayedAt = DateTime.Now});
        }

        /// <summary>
        /// Prints the game field
        /// </summary>
        public void Print()
        {
            Console.Clear();
            Console.WriteLine("Score: {0}", field.Score);
            Console.WriteLine();

            for (var row = 0; row < field.RowCount; row++)
            {
                for (var column = 0; column < field.ColumnCount; column++)
                {
                    if (field.GetTile(row, column) != null)
                    {
                        CalculateTileColor(field.GetTile(row, column).Value);
                        Console.Write("{0,3} ", field.GetTile(row, column).Value);

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write("{0,3} ", "x");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Set background and foreground color of console based on tile value
        /// </summary>
        /// <param name="value">Value of tile</param>
        private static void CalculateTileColor(int value)
        {
            switch (value)
            {
                case 1:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 4:
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case 8:
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 16:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case 32:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case 64:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case 128:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case 256:
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 512:
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case 1024:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
            }
        }

        /// <summary>
        /// Process player input and update the game accordingly.
        /// </summary>
        private void ProcessInput()
        {
            ConsoleKeyInfo move = Console.ReadKey();

            if (move.Key == ConsoleKey.UpArrow)
                game.Update(Direction.Up);
            else if (move.Key == ConsoleKey.DownArrow)
                game.Update(Direction.Down);
            else if (move.Key == ConsoleKey.LeftArrow)
                game.Update(Direction.Left);
            else if (move.Key == ConsoleKey.RightArrow)
                game.Update(Direction.Right);
        }

        /// <summary>
        /// Prints summary at the end of the game
        /// </summary>
        private void PrintGameEnd()
        {
            Print();
            if (game.GameState == GameState.Won)
                Console.WriteLine("\nCongratulations, you won!\n");
            else
                Console.WriteLine("\nGAME OVER!\n");
            PrintTopScores();
        }

        /// <summary>
        /// Prints top 3 scores
        /// </summary>
        private void PrintTopScores()
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("----- TOP SCORES -----");
            Console.WriteLine("----------------------");
            foreach (var score in _scoreService.GetTopScores())
            {
                Console.WriteLine("{0} {1,5}", score.Player.PadRight(10), score.Points);
            }

            Console.WriteLine("----------------------");
        }
    }
}
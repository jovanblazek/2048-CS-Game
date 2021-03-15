using System;
using System.Collections.Generic;
using System.Text;
using game1024Core.Core;

namespace game1024Console
{
    public class ConsoleUI
    {
        private Game game;
        private Field field;

        public ConsoleUI(Game game)
        {
            this.game = game;
            field = game.GetField();
        }

        /// <summary>
        /// Main game loop. 
        /// </summary>
        public void Run()
        {
            do
            {
                Print();
                ProcessInput();
            } while (game.GameState == GameState.Playing);

            Print();
            if (game.GameState == GameState.Won)
                Console.WriteLine("Congratulations, you won!");
            else
                Console.WriteLine("GAME OVER!");
        }

        /// <summary>
        /// Prints the game field
        /// </summary>
        public void Print()
        {
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
            ConsoleKeyInfo move = Console.ReadKey(); ;
            Console.Clear();

            if (move.Key == ConsoleKey.UpArrow)
                game.Update(Direction.Up);
            else if (move.Key == ConsoleKey.DownArrow)
                game.Update(Direction.Down);
            else if (move.Key == ConsoleKey.LeftArrow)
                game.Update(Direction.Left);
            else if (move.Key == ConsoleKey.RightArrow)
                game.Update(Direction.Right);
        }
    }
}
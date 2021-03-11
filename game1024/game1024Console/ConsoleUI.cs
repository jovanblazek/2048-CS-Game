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

        public void Play()
        {
            do
            {
                PrintField();
                //process input
            } while (game.gameState == GameState.Playing);
        }

        public void PrintField()
        {
            for (var row = 0; row < field.RowCount; row++)
            {
                for (var column = 0; column < field.ColumnCount; column++)
                {

                    if (field.GetTile(row, column) != null)
                        Console.Write(" {0}", field.GetTile(row, column).Value);
                    else
                        Console.Write(" x");
                }

                Console.WriteLine();
            }

        }

    }
}

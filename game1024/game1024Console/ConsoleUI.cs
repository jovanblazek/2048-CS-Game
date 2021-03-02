using System;
using System.Collections.Generic;
using System.Text;
using game1024Core.Core;

namespace game1024Console
{
    public class ConsoleUI
    {
        private Field field;

        public ConsoleUI(Field field)
        {
            this.field = field;
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

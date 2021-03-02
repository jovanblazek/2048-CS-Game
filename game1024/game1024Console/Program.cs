using System;
using game1024Core.Core;

namespace game1024Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var field = new Field(4, 4);
            var ui = new ConsoleUI(field);
            ui.PrintField();
        }
    }
}

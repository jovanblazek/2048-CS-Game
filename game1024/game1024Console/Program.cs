﻿using game1024Core.Core;

namespace game1024Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(4);
            var ui = new ConsoleUI(game);
            ui.Run();
        }
    }
}
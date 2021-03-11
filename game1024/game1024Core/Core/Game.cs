using System;
using System.Collections.Generic;
using System.Text;

namespace game1024Core.Core
{
    public enum GameState
    {
        Playing, Won, Lost
    }

    public class Game
    {
        private Field field;
        public GameState gameState;

        public Game(int gridSize)
        {
            field = new Field(gridSize, gridSize);
            gameState = GameState.Playing;
        }

        public void Update(Direction direction)
        {
            if (field.isSolved())
            {
                gameState = GameState.Won;
                return;
            }
            else if (!field.IsMovePossible())
            {
                gameState = GameState.Lost;
                return;
            }

            field.Move(direction);

            if (field.didSomething)
                field.CreateNewTile();
        }

        public Field GetField()
        {
            return this.field;
        }
    }
}

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
        public GameState GameState { get; private set; }

        public Game(int gridSize)
        {
            field = new Field(gridSize, gridSize);
            GameState = GameState.Playing;
        }

        public void Update(Direction direction)
        {
            field.Move(direction);

            if (field.DidSomething)
                field.CreateNewTile();

            if (field.isSolved())
            {
                GameState = GameState.Won;
                return;
            }
            else if (!field.IsMovePossible())
            {
                GameState = GameState.Lost;
                return;
            }
            
        }

        public Field GetField()
        {
            return this.field;
        }
    }
}

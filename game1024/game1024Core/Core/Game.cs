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

        /// <summary>
        /// Updates the game based on player input and checks if the game has been won/lost.
        /// </summary>
        /// <param name="direction">Direction of player move</param>
        public void Update(Direction direction)
        {
            field.Move(direction);

            if (field.DidSomething)
                field.CreateNewTile();

            if (field.isSolved())
                GameState = GameState.Won;
            else if (!field.IsMovePossible())
                GameState = GameState.Lost;
        }

        public Field GetField()
        {
            return this.field;
        }
    }
}

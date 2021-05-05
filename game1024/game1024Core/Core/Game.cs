using System;

namespace game1024Core.Core
{
    public enum GameState
    {
        Playing,
        Won,
        Lost
    }

    public delegate void OnFieldChange();

    public delegate void OnGameStateChange();

    [Serializable]
    public class Game
    {
        public event OnFieldChange OnFieldChange;
        public event OnGameStateChange OnGameStateChange;
        public int GainedScore { get; private set; }
        private Field field;

        private GameState _gameState;

        public GameState GameState
        {
            get => _gameState;
            private set
            {
                _gameState = value;
                OnGameStateChange?.Invoke();
            }
        }

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
            field.ClearTileAnimations();
            GainedScore = 0;
            int previousScore = field.Score;

            field.Move(direction);

            if (field.DidSomething)
            {
                GainedScore = field.Score - previousScore;
                if (field.IsSolved())
                    GameState = GameState.Won;
                else if (!field.IsMovePossible())
                    GameState = GameState.Lost;

                field.CreateNewTile();

                if (OnFieldChange != null)
                    OnFieldChange();
            }
        }

        public Field GetField()
        {
            return this.field;
        }
    }
}
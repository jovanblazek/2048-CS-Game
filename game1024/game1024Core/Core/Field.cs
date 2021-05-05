using System;

namespace game1024Core.Core
{
    [Serializable]
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [Serializable]
    public class Field
    {
        private Tile[,] tiles;
        public int Score { get; private set; }
        public bool DidSomething { get; private set; }

        private bool hasMovedSomething = false;
        private int foundRow = 0;
        private int foundColumn = 0;

        public int RowCount { get; }
        public int ColumnCount { get; }

        public Field(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            tiles = new Tile[rowCount, columnCount];
            Score = 0;
            Initialize();
        }

        public Tile GetTile(int row, int column)
        {
            return tiles[row, column];
        }

        /// <summary>
        /// Initialize the game, spawn first two tiles.
        /// </summary>
        private void Initialize()
        {
            CreateNewTile();
            CreateNewTile();
        }

        /// <summary>
        /// Create a new tile and place it into random empty place in field.
        /// </summary>
        public void CreateNewTile()
        {
            if (!HasEmptyTile())
                return;

            int row, column;
            do
            {
                row = new Random().Next(0, RowCount);
                column = new Random().Next(0, ColumnCount);
            } while (tiles[row, column] != null);

            var value = (new Random().NextDouble() < 0.9) ? 1 : 2;
            tiles[row, column] = new Tile(value, true);
        }

        /// <summary>
        /// Traverse the field and look for tiles equal to null - empty spaces.
        /// </summary>
        /// <returns>True if found, false otherwise</returns>
        private bool HasEmptyTile()
        {
            for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColumnCount; j++)
                if (tiles[i, j] == null)
                    return true;

            return false;
        }

        /// <summary>
        /// Traverse the field and look for adjacent tiles with equal values.
        /// </summary>
        /// <returns>True if found, false otherwise</returns>
        public bool IsMovePossible()
        {
            if (HasEmptyTile())
                return true;

            //check tile to the right
            for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColumnCount - 1; j++)
                if (tiles[i, j].Value == tiles[i, j + 1].Value)
                    return true;

            //check tile below
            for (var i = 0; i < RowCount - 1; i++)
            for (var j = 0; j < ColumnCount; j++)
                if (tiles[i, j].Value == tiles[i + 1, j].Value)
                    return true;

            return false;
        }


        /* ----------- VERTICAL ----------- */

        /// <summary>
        /// Merge the tiles in column. Has to be called in the for loop iterating rows.
        /// </summary>
        /// <param name="row">Current row</param>
        /// <param name="column">Current column</param>
        /// <param name="hasFirstTile">True, if the first tile for merging has been found</param>
        /// <returns>True, if the first tile for merging has been found, false otherwise</returns>
        private bool MergeColumn(int row, int column, bool hasFirstTile)
        {
            this.foundColumn = column;

            if (!hasFirstTile && tiles[row, column] != null)
            {
                this.foundRow = row;
                hasFirstTile = true;
            }
            else if (hasFirstTile && tiles[row, column] != null)
            {
                if (tiles[row, column].Value == tiles[this.foundRow, this.foundColumn].Value)
                {
                    tiles[this.foundRow, this.foundColumn].Value *= 2;
                    tiles[this.foundRow, this.foundColumn].IsMerged = true;
                    tiles[row, column] = null;
                    Score += tiles[this.foundRow, this.foundColumn].Value;
                    hasFirstTile = false;
                    this.DidSomething = true;
                }
                else
                {
                    this.foundRow = row;
                    hasFirstTile = true;
                }
            }

            return hasFirstTile;
        }

        /// <summary>
        /// Moves the tiles in column. Has to be called in the for loop iterating rows.
        /// </summary>
        /// <param name="row">Current row</param>
        /// <param name="column">Current column</param>
        /// <param name="hasFirstTile">True, if the first empty tile has been found</param>
        /// <returns>True, if the first empty tile has been found, false otherwise</returns>
        private bool MoveColumn(int row, int column, bool hasFirstTile)
        {
            this.foundColumn = column;
            if (!hasFirstTile && tiles[row, column] == null)
            {
                hasFirstTile = true;
                this.foundRow = row;
            }
            else if (hasFirstTile && tiles[row, column] != null)
            {
                tiles[this.foundRow, this.foundColumn] = new Tile(tiles[row, column].Value, false, tiles[row, column].IsMerged);
                tiles[row, column] = null;
                this.hasMovedSomething = true;
                hasFirstTile = false;
                this.DidSomething = true;
            }

            return hasFirstTile;
        }

        /// <summary>
        /// Merge and move the tiles in the column. Iterates through column and uses helper functions to do the merging and moving.
        /// </summary>
        /// <param name="column">Column to merge and move</param>
        /// <param name="direction">Direction in which to merge and move the column</param>
        private void MoveVertical(int column, Direction direction)
        {
            var hasFirstTile = false;
            if (direction == Direction.Down) //3,2,1,0
            {
                for (var row = RowCount - 1; row >= 0; row--)
                    hasFirstTile = MergeColumn(row, column, hasFirstTile);

                hasFirstTile = false;

                for (var row = RowCount - 1; row >= 0; row--)
                {
                    hasFirstTile = MoveColumn(row, column, hasFirstTile);
                    if (this.hasMovedSomething)
                        row = this.foundRow;
                    this.hasMovedSomething = false;
                }
            }
            else //0,1,2,3
            {
                for (var row = 0; row < RowCount; row++)
                    hasFirstTile = MergeColumn(row, column, hasFirstTile);

                hasFirstTile = false;

                for (var row = 0; row < RowCount; row++)
                {
                    hasFirstTile = MoveColumn(row, column, hasFirstTile);
                    if (this.hasMovedSomething)
                        row = this.foundRow;
                    this.hasMovedSomething = false;
                }
            }
        }


        /* ----------- HORIZONTAL ----------- */
        /// <summary>
        /// Merge the tiles in row. Has to be called in the for loop iterating columns.
        /// </summary>
        /// <param name="row">Current row</param>
        /// <param name="column">Current column</param>
        /// <param name="hasFirstTile">True, if the first tile for merging has been found</param>
        /// <returns>True, if the first tile for merging has been found, false otherwise</returns>
        private bool MergeRow(int row, int column, bool hasFirstTile)
        {
            this.foundRow = row;

            if (!hasFirstTile && tiles[row, column] != null)
            {
                this.foundColumn = column;
                hasFirstTile = true;
            }
            else if (hasFirstTile && tiles[row, column] != null)
            {
                if (tiles[row, column].Value == tiles[this.foundRow, this.foundColumn].Value)
                {
                    tiles[this.foundRow, this.foundColumn].Value *= 2;
                    tiles[this.foundRow, this.foundColumn].IsMerged = true;
                    tiles[row, column] = null;
                    Score += tiles[this.foundRow, this.foundColumn].Value;
                    hasFirstTile = false;
                    this.DidSomething = true;
                }
                else
                {
                    this.foundColumn = column;
                    hasFirstTile = true;
                }
            }

            return hasFirstTile;
        }

        /// <summary>
        /// Moves the tiles in row. Has to be called in the for loop iterating columns.
        /// </summary>
        /// <param name="row">Current row</param>
        /// <param name="column">Current column</param>
        /// <param name="hasFirstTile">True, if the first empty tile has been found</param>
        /// <returns>True, if the first empty tile has been found, false otherwise</returns>
        private bool MoveRow(int row, int column, bool hasFirstTile)
        {
            this.foundRow = row;

            if (!hasFirstTile && tiles[row, column] == null)
            {
                hasFirstTile = true;
                this.foundColumn = column;
            }
            else if (hasFirstTile && tiles[row, column] != null)
            {
                tiles[this.foundRow, this.foundColumn] = new Tile(tiles[row, column].Value, false, tiles[row, column].IsMerged);
                tiles[row, column] = null;
                this.hasMovedSomething = true;
                hasFirstTile = false;
                this.DidSomething = true;
            }

            return hasFirstTile;
        }

        /// <summary>
        /// Merge and move the tiles in the row. Iterates through row and uses helper functions to do the merging and moving.
        /// </summary>
        /// <param name="row">Row to merge and move</param>
        /// <param name="direction">Direction in which to merge and move the row</param>
        private void MoveHorizontal(int row, Direction direction)
        {
            var hasFirstTile = false;
            if (direction == Direction.Right) //3,2,1,0
            {
                for (var column = ColumnCount - 1; column >= 0; column--)
                    hasFirstTile = MergeRow(row, column, hasFirstTile);

                hasFirstTile = false;

                for (var column = ColumnCount - 1; column >= 0; column--)
                {
                    hasFirstTile = MoveRow(row, column, hasFirstTile);
                    if (this.hasMovedSomething)
                        column = this.foundColumn;
                    this.hasMovedSomething = false;
                }
            }
            else //0,1,2,3
            {
                for (var column = 0; column < ColumnCount; column++)
                    hasFirstTile = MergeRow(row, column, hasFirstTile);

                hasFirstTile = false;

                for (var column = 0; column < ColumnCount; column++)
                {
                    hasFirstTile = MoveRow(row, column, hasFirstTile);
                    if (this.hasMovedSomething)
                        column = this.foundColumn;
                    this.hasMovedSomething = false;
                }
            }
        }

        /// <summary>
        /// Perform game move in specified direction.
        /// </summary>
        /// <param name="direction">Direction of the move</param>
        public void Move(Direction direction)
        {
            this.DidSomething = false;
            if (!IsMovePossible())
                return;

            switch (direction)
            {
                case Direction.Up:
                    for (var column = 0; column < ColumnCount; column++)
                        MoveVertical(column, Direction.Up);
                    break;
                case Direction.Down:
                    for (var column = 0; column < ColumnCount; column++)
                        MoveVertical(column, Direction.Down);
                    break;
                case Direction.Left:
                    for (var row = 0; row < RowCount; row++)
                        MoveHorizontal(row, Direction.Left);
                    break;
                default:
                    for (var row = 0; row < RowCount; row++)
                        MoveHorizontal(row, Direction.Right);
                    break;
            }
        }

        /// <summary>
        /// Traverse the field and look for tiles which value is equal to 1024.
        /// </summary>
        /// <returns>True if found, false otherwise</returns>
        public bool IsSolved()
        {
            for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColumnCount; j++)
                if (tiles[i, j] != null && tiles[i, j].Value == 16)
                        return true;

            return false;
        }

        /// <summary>
        /// Sets boolean values, used for animations, on tiles to false. Called before every update.
        /// </summary>
        public void ClearTileAnimations()
        {
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    if (tiles[i, j] != null)
                    {
                        if (tiles[i, j].IsMerged) 
                            tiles[i, j].IsMerged = false;
                        if (tiles[i, j].IsNew)
                            tiles[i, j].IsNew = false;
                    }
                }
            }

        }
    }
}
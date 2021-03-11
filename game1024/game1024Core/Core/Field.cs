using System;
using System.Collections.Generic;
using System.Text;

namespace game1024Core.Core
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Field
    {
        private Tile[,] tiles;
        public int Score { get; private set; }
        public bool DidSomething { get; private set; }

        private bool hasMovedSomething = false;
        private int foundRow = 0;
        private int foundColumn = 0;
        private Random rnd = new Random();

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

        /* Initialize the game, spawn first two tiles */
        private void Initialize()
        {
            CreateNewTile();
            CreateNewTile();

            //for (int i = 0; i < 10; i++)
            //  CreateNewTile();
        }

        /* Create a new tile and place it into random empty place in field */
        public void CreateNewTile()
        {
            //if empty space then continue
            //pick random empty spot
            //pick random value
            if (!HasEmptyTile())
            {
                Console.WriteLine("Field.cs - CreateNewTile: No empty tile available");
                return;
            }

            int row, column;
            do
            {
                row = rnd.Next(0, RowCount);
                column = rnd.Next(0, ColumnCount);
            } while (tiles[row, column] != null);

            int value = (rnd.NextDouble() < 0.9) ? 1 : 2;

            tiles[row, column] = new Tile(value);
        }

        /* Traverse the field and look for empty spaces without tiles => null */
        private bool HasEmptyTile()
        {
            for (int i = 0; i < RowCount; i++)
            for (int j = 0; j < ColumnCount; j++)
                if (tiles[i, j] == null)
                    return true;

            return false;
        }

        public bool IsMovePossible()
        {
            if (HasEmptyTile())
                return true;

            //check tile to the right
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount - 1; j++)
                    if (tiles[i, j].Value == tiles[i, j + 1].Value)
                        return true;

            //check tile below
            for (int i = 0; i < RowCount - 1; i++)
                for (int j = 0; j < ColumnCount; j++)
                    if (tiles[i, j].Value == tiles[i + 1, j].Value)
                        return true;

            return false;
        }

        public Tile GetTile(int row, int column)
        {
            return tiles[row, column];
        }


        /* VERTICAL */

        private bool MergeColumn(int row, int column, bool hasFirstTile)
        {
            this.foundColumn = column;

            if (!hasFirstTile && tiles[row, column] != null) //prvy tile
            {
                this.foundRow = row;
                hasFirstTile = true;
            }
            else if (hasFirstTile && tiles[row, column] != null) //druhy tile
            {
                if (tiles[row, column].Value == tiles[this.foundRow, this.foundColumn].Value)
                {
                    tiles[this.foundRow, this.foundColumn].Value *= 2;
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
                tiles[this.foundRow, this.foundColumn] = new Tile(tiles[row, column].Value);
                tiles[row, column] = null;
                this.hasMovedSomething = true;
                hasFirstTile = false;
                this.DidSomething = true;
            }

            return hasFirstTile;
        }

        private void MoveVertical(int column, Direction direction)
        {
            bool hasFirstTile = false;
            if (direction == Direction.Down)
            {
                for (int row = RowCount - 1; row >= 0; row--) //3,2,1,0
                    hasFirstTile = MergeColumn(row, column, hasFirstTile);

                hasFirstTile = false;

                for (int row = RowCount - 1; row >= 0; row--) //3,2,1,0
                {
                    hasFirstTile = MoveColumn(row, column, hasFirstTile);
                    if (this.hasMovedSomething)
                        row = this.foundRow;
                    this.hasMovedSomething = false;
                }
            }
            else
            {
                for (int row = 0; row < RowCount; row++) //0,1,2,3
                    hasFirstTile = MergeColumn(row, column, hasFirstTile);

                hasFirstTile = false;

                for (int row = 0; row < RowCount; row++) //0,1,2,3
                {
                    hasFirstTile = MoveColumn(row, column, hasFirstTile);
                    if (this.hasMovedSomething)
                        row = this.foundRow;
                    this.hasMovedSomething = false;
                }
            }
        }


        /* HORIZONTAL */
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
                tiles[this.foundRow, this.foundColumn] = new Tile(tiles[row, column].Value);
                tiles[row, column] = null;
                this.hasMovedSomething = true;
                hasFirstTile = false;
                this.DidSomething = true;
            }

            return hasFirstTile;
        }

        private void MoveHorizontal(int row, Direction direction)
        {
            bool hasFirstTile = false;
            if (direction == Direction.Right)
            {
                for (int column = ColumnCount - 1; column >= 0; column--) //3,2,1,0
                    hasFirstTile = MergeRow(row, column, hasFirstTile);

                hasFirstTile = false;

                for (int column = ColumnCount - 1; column >= 0; column--) //3,2,1,0
                {
                    hasFirstTile = MoveRow(row, column, hasFirstTile);
                    if (this.hasMovedSomething)
                        column = this.foundColumn;
                    this.hasMovedSomething = false;
                }
            }
            else
            {
                for (int column = 0; column < ColumnCount; column++) //0,1,2,3
                    hasFirstTile = MergeRow(row, column, hasFirstTile);

                hasFirstTile = false;

                for (int column = 0; column < ColumnCount; column++) //0,1,2,3
                {
                    hasFirstTile = MoveRow(row, column, hasFirstTile);
                    if (this.hasMovedSomething)
                        column = this.foundColumn;
                    this.hasMovedSomething = false;
                }
            }
        }


        public void Move(Direction direction)
        {
            this.DidSomething = false;
            if (!IsMovePossible())
                return;

            int startingRow = 0, startingColumn = 0;

            switch (direction)
            {
                case Direction.Up:
                    startingRow = 0;
                    startingColumn = 0;

                    for (int column = startingColumn; column < ColumnCount; column++) //0,1,2,3
                        MoveVertical(column, Direction.Up);

                    break;
                case Direction.Down:
                    startingRow = RowCount;
                    startingColumn = 0;

                    for (int column = startingColumn; column < ColumnCount; column++) //0,1,2,3
                        MoveVertical(column, Direction.Down);

                    break;
                case Direction.Left:
                    startingRow = 0;
                    startingColumn = 0;

                    for (int row = startingRow; row < RowCount; row++)
                        MoveHorizontal(row, Direction.Left);

                    break;
                case Direction.Right:
                    startingRow = 0;
                    startingColumn = ColumnCount;

                    for (int row = startingRow; row < RowCount; row++)
                        MoveHorizontal(row, Direction.Right);

                    break;
            }
        }


        /* Traverse the field and look for tiles that exist and their value is equal to 1024 */
        public bool isSolved()
        {
            for (int i = 0; i < RowCount; i++)
            for (int j = 0; j < ColumnCount; j++)
                if (tiles[i, j] != null && tiles[i, j].Value == 1024)
                    return true;

            return false;
        }
    }
}
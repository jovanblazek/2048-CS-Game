using System;
using System.Collections.Generic;
using System.Text;

namespace game1024Core.Core
{
    public class Field
    {
        private readonly Tile[,] _tiles;
        private Random rnd = new Random();

        public int RowCount { get; }
        public int ColumnCount { get; }

        public Field(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            _tiles = new Tile[rowCount, columnCount];
            Initialize();
        }

        /* Initialize the game, spawn first two tiles */
        private void Initialize()
        {
            CreateNewTile();
            CreateNewTile();
        }

        /* Create a new tile and place it into random empty place in field */
        private void CreateNewTile()
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
            } while (_tiles[row, column] != null);

            int value = (rnd.NextDouble() < 0.7) ? 1 : 2;

            _tiles[row, column] = new Tile(value);
        }

        /* Traverse the field and look for empty spaces without tiles => null */
        private bool HasEmptyTile()
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++)
                    if (_tiles[i, j] == null)
                        return true;

            return false;
        }

        public Tile GetTile(int row, int column)
        {
            return _tiles[row, column];
        }


        /* Traverse the field and look for tiles that exist and their value is equal to 1024 */
        public bool isSolved()
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++)
                    if (_tiles[i, j] != null && _tiles[i,j].Value == 1024)
                        return true;
            
            return false;
        }
    }
}
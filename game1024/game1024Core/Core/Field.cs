using System;
using System.Collections.Generic;
using System.Text;

namespace game1024Core.Core
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public class Field
    {
        private Tile[,] tiles;
        private Random rnd = new Random();

        public int RowCount { get; }
        public int ColumnCount { get; }

        public Field(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            tiles = new Tile[rowCount, columnCount];
            Initialize();
        }

        /* Initialize the game, spawn first two tiles */
        private void Initialize()
        {
            //CreateNewTile();
            //CreateNewTile();
            for (int i = 0; i < 10; i++)
            {
                CreateNewTile();
            }
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
            } while (tiles[row, column] != null);

            int value = (rnd.NextDouble() < 1) ? 1 : 2;

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

        private bool IsMovePossible()
        {
            if (HasEmptyTile())
                return true;

            //check tile to the right
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount-1; j++)
                    if (tiles[i, j] == tiles[i, j + 1])
                            return true;

            //check tile to the left
            for (int i = 0; i < RowCount-1; i++)
                for (int j = 0; j < ColumnCount; j++)
                    if (tiles[i, j] == tiles[i + 1, j])
                        return true;

            return false;
        }

        public Tile GetTile(int row, int column)
        {
            return tiles[row, column];
        }

        public bool MoveAndMerge(int id, int iterator, Direction direction)
        {
            Console.WriteLine("\n");
            if (direction == Direction.Down || direction == Direction.Right)
            {
                /*
                for (int i = iterator-1; i > 0; i--)    // i = 3,2,1
                {
                    //Console.WriteLine("{0} i= {1} id= {2}", tiles[i, id].Value, i, id); //move down
                    Console.WriteLine("{0} i= {1} id= {2}", tiles[id, i].Value, i, id); //move right
                }*/
            }
            else
            {
                for (int i = iterator; i < ColumnCount; i++)
                {
                    Console.WriteLine(" {0}", tiles[i, id].Value); //move up
                    //Console.WriteLine(" {0}", tiles[id, i].Value); //move left
                }
            }
            

            return false;
        }

        public void Move(Direction direction)
        {
            /*if (!IsMovePossible())
                return;*/

            int startingRow = 0, startingColumn = 0;
            switch (direction)
            {
                case Direction.Up:
                    startingRow = 0;
                    startingColumn = 0;
                    break;
                case Direction.Down:
                    startingRow = RowCount;
                    startingColumn = 0;
                    break;
                case Direction.Left:
                    startingRow = 0;
                    startingColumn = 0;
                    break;
                case Direction.Right:
                    startingRow = 0;
                    startingColumn = ColumnCount;
                    break;
            }

            if (false)
            {
                /*
                if (direction == Direction.Up || direction == Direction.Down)
                {
                    for (int i = startingColumn; i < ColumnCount; i++)
                    {
                        //MoveAndMerge(i, startingRow, direction);
                        if (direction == Direction.Down)
                        {
                            for (int j = startingRow - 1; j > 0; j--)    // i = 3,2,1
                            {
                                if (tiles[j, i] != null && tiles[j - 1, i] != null && (tiles[j, i].Value == tiles[j - 1, i].Value))
                                {
                                    tiles[j, i].Value *= 2;
                                    tiles[j - 1, i] = null;
                                }
                                //Console.WriteLine("{0} i= {1} id= {2}", tiles[i, id].Value, i, id); //move down
                            }
                        }
                        else
                        {
                            for (int j = startingRow; j < RowCount-1; j++)    // i = 0,1,2
                            {
                                if (tiles[j, i] != null && tiles[j + 1, i] != null && (tiles[j, i].Value == tiles[j + 1, i].Value))
                                {
                                    tiles[j, i].Value *= 2;
                                    tiles[j + 1, i] = null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = startingRow; i < RowCount; i++)
                    {
                        //MoveAndMerge(i, startingColumn, direction);
                    }
                }

            */
            }

            int firstRow = 0, firstColumn = 0;
            bool hasFirstTile = false;
            bool didSomething = false;

            //down
            for (int column = startingColumn; column < ColumnCount; column++) //0,1,2,3
            {
                firstColumn = column;
                for (int row = RowCount - 1; row >= 0; row--) //3,2,1,0
                {
                    if (!hasFirstTile && tiles[row, column] != null) //prvy tile
                    {
                        firstRow = row;
                        hasFirstTile = true;
                    }
                    else if (hasFirstTile && tiles[row, column] != null) //druhy tile
                    {
                        //Console.WriteLine("{0} {1} ?= {2} {3}", row, column, firstRow, firstColumn);
                        if (tiles[row, column].Value == tiles[firstRow, firstColumn].Value)
                        {
                            tiles[firstRow, firstColumn].Value *= 2;
                            tiles[row, column] = null;
                            hasFirstTile = false;
                            didSomething = true;
                            //Console.WriteLine("{0} {1} ==== {2} {3}", row, column, firstRow, firstColumn);
                        }
                        else
                        {
                            firstRow = row;
                            hasFirstTile = true;
                        }
                    }
                }

                hasFirstTile = false; //cleanup kvoli prechodu medzi stlpcami
            }

            for (int column = startingColumn; column < ColumnCount; column++) //0,1,2,3
            {
                firstColumn = column;
                for (int row = RowCount - 1; row >= 0; row--) //3,2,1,0
                {
                    //Console.WriteLine("{0} {1}", row, column);
                    if (!hasFirstTile && tiles[row, column] == null)
                    {
                        hasFirstTile = true;
                        firstRow = row;
                        //Console.WriteLine("{0} {1} null", row, column);
                    }
                    else if (hasFirstTile && tiles[row, column] != null)
                    {
                        //Console.WriteLine("{0} {1} <== {2} {3}", firstRow, firstColumn, row, column);
                        tiles[firstRow, firstColumn] = new Tile(tiles[row, column].Value);
                        tiles[row, column] = null;
                        row = firstRow;
                        hasFirstTile = false;
                        didSomething = true;
                    }

                }
                hasFirstTile = false; //cleanup kvoli prechodu medzi stlpcami
            }
        }


        /* Traverse the field and look for tiles that exist and their value is equal to 1024 */
        public bool isSolved()
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++)
                    if (tiles[i, j] != null && tiles[i,j].Value == 1024)
                        return true;
            
            return false;
        }
    }
}
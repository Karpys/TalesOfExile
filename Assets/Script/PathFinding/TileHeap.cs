using System;
using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.PathFinding
{
    public class TileHeap
    {
        Tile[] tiles;
        int currentTilesCount;

        public TileHeap(int maxHeapSize)
        {
            tiles = new Tile[maxHeapSize];
        }
        
        public int Count => currentTilesCount;

        public void UpdateItem(Tile tile)
        {
            SortUp(tile);
        }

        public void Add(Tile tile)
        {
            tile.HeapIndex = currentTilesCount;
            tiles[currentTilesCount] = tile;
            SortUp(tile);
            currentTilesCount++;
        }

        public Tile RemoveFirst()
        {
            Tile firstTile = tiles[0];
            currentTilesCount--;
            tiles[0] = tiles[currentTilesCount];
            tiles[0].HeapIndex = 0;
            SortDown(tiles[0]);
            return firstTile;
        }


        public bool Contains(Tile tile)
        {
            if (tile.HeapIndex < currentTilesCount)
            {
                return Equals(tiles[tile.HeapIndex], tile);
            }
            else
            {
                return false;
            }
        }

        void SortDown(Tile tile)
        {
            while (true)
            {
                int childIndexLeft = tile.HeapIndex * 2 + 1;
                int childIndexRight = tile.HeapIndex * 2 + 2;
                int swapIndex = 0;

                if (childIndexLeft < currentTilesCount)
                {
                    swapIndex = childIndexLeft;

                    if (childIndexRight < currentTilesCount)
                    {
                        if (tiles[childIndexLeft].CompareTo(tiles[childIndexRight]) <  0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }

                    if (tile.CompareTo(tiles[swapIndex]) < 0)
                    {
                        Swap(tile,tiles[swapIndex]);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        
        public void Clear()
        { 
            currentTilesCount = 0;
        }
        
        void SortUp(Tile tile)
        {
            int parentIndex = (tile.HeapIndex - 1) / 2;

            while (true)
            {
                Tile parentTile = tiles[parentIndex];
                if (tile.CompareTo(parentTile) > 0)
                {
                    Swap(tile,parentTile);
                }
                else
                {
                    break;
                }
            }
        }

        void Swap(Tile tileA, Tile tileB)
        {
            tiles[tileA.HeapIndex] = tileB;
            tiles[tileB.HeapIndex] = tileA;
            (tileA.HeapIndex, tileB.HeapIndex) = (tileB.HeapIndex, tileA.HeapIndex);
        }
    }
}
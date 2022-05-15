using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Utility
{
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed);

        for (int i = 1; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }
    public static int Abs(int v)
    {
        if (v < 0)
            return v * -1;
        return v;
    }

    public static void FindCells(Cell c, int MaxDist, List<Cell> cells,List <Cell> nPieces=null)
    {
        Queue<Cell> queue = new Queue<Cell>();
        queue.Enqueue(c);
        queue.Enqueue(null);
        bool[] boardFlag = new bool[Board.Instance.nbCell];
        int CDist = 0;
        while (queue.Count > 0)
        {
            Cell cell = queue.Dequeue();
            if (cell == null)
            {
                if (queue.Count == 0)
                    break;
                ++CDist;
                if (CDist >= MaxDist)
                    break;
                queue.Enqueue(null);
                continue;
            }
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int nx = cell.x + x;
                    int ny = cell.y + y;
                    if ((y == 0 || x == 0) && nx >= 0 && nx < Board.Instance.width && ny >= 0 && ny < Board.Instance.height)
                    {
                        int index = nx + ny * Board.Instance.width;
                        Cell cl = Board.Instance.cellList[index].GetComponent<Cell>();
                        if (!boardFlag[index])
                        {
                            if(cl.IsFree())
                                queue.Enqueue(cl.GetComponent<Cell>());
                            boardFlag[index] = true;
                            if (cl != c)
                            {
                                if(nPieces!=null && cl.occupier!=null && cl.occupier.TryGetComponent<Piece>(out _))
                                    nPieces.Add(cl);
                                if(cells!=null && cl.IsFree())
                                    cells.Add(cl);
                            }
                        }
                    }
                }
            }
        }
        return;
    }

}

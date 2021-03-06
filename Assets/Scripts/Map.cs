﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Cell[] cells;
    int width = 0;
    int height = 0;

    public void SetDimensions(int widthParam, int heightParam)
    {
        width = widthParam;
        height = heightParam;
        cells = new Cell[width * height];
    }

    public int CellIndex(int x, int y)
    {
        return x + y * height;
    }

    public Vector3 CellPosition(int x, int y)
    {
        float xPos = -((float)(width / 2)) + x;
        float yPos = -((float)(height / 2)) - y + height;
        return new Vector3(xPos, yPos);
    }

    public Cell GetCell(int x, int y)
    {
        return cells[CellIndex(x, y)];
    }

    public Cell GetCell(int index)
    {
        return cells[index];
    }

    /*
     *  Static functions
     */

    public static bool IsAdjacent(Cell cell1, Cell cell2)
    {
        if (cell1.x == cell2.x && cell1.y == cell2.y) throw new System.Exception("IsAdjacent : same position");
        int distance = Mathf.Abs(cell1.x - cell2.x) + Mathf.Abs(cell1.y - cell2.y);
        return (distance == 1);
    }
}

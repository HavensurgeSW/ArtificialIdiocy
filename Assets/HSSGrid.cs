using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;


public class HSSGrid<TGridObject>
{
    int width;
    int height;
    float cellSize;
    GridNode[,] gridarray;
    TextMesh[,] debugTextArray;
    public HSSGrid(int w, int h)
    {
        this.width = w;
        this.height = h;
        this.cellSize = 12f;

        gridarray = new GridNode[w, h];

        Debug.Log(w + " " +h);

        for (int x = 0; x < gridarray.GetLength(0); x++) { 
            for (int y = 0; y < gridarray.GetLength(1); y++){

                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y),Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(w, h), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(w, h), Color.white, 100f);

        //SetValue(1, 1, 1);
    }

    Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize;
    }

    //public void SetValue(int x, int y, TGridObject value) {
    //    if(x>=0 && y>=0 && x<width&&y<height)
    //        gridarray[x, y] = value;   
    //}

    //public void SetValue(Vector3 worldPosition, TGridObject value) { 
    //    int x, y;
    //    GetXY(worldPosition, out x, out y);
    //    SetValue(x, y, value);
    //}

    //private void GetXY(Vector3 worldPosition, out int x, out int y) { 
    //    x = Mathf.FloorToInt(worldPosition.x / cellSize);
    //    y = Mathf.FloorToInt(worldPosition.y / cellSize);
    //}

    //public TGridObject GetValue(int x, int y) {
    //    if (x >= 0 && y >= 0 && x < width && y < height)
    //        return gridarray[x, y];
    //    else
    //        return default(TGridObject);
    //}

    //public TGridObject GetValue(Vector3 worldPosition) {
    //    int x, y;
    //    GetXY(worldPosition, out x, out y);
    //    return GetValue(x, y);
    //}
}

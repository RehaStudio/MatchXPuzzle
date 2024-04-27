using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridTable : MonoBehaviour
{
    private Grid.GridFactory _GridFactory;
    public float ScreenWidth => Screen.width;
    public float ScreenHeight => Screen.height;

    public int LineCount = 5;
    public Vector2 GridSpacing;

    private float _GridSize;
    [Inject]
    public void Constructor(Grid.GridFactory gridFactory)
    {
        _GridFactory = gridFactory;
    }
    private void Awake()
    {
        CreateTable();
    }
    private void CreateTable()
    {
        _GridSize = CalculateGridSize();
        for (int x = 0; x < LineCount; x++)
        {
            for (int y = 0; y < LineCount; y++)
            {
                Grid grid = CreateGrid();
                grid.SetSize(_GridSize);
                grid.SetPosition(CalculateGridPosition(x, y));
            }
        }
    }
    private Grid CreateGrid()
    {
        return _GridFactory.Create();
    }
    public float CalculateGridSize()
    {
        return Mathf.Min(ScreenWidth-GridSpacing.x*(LineCount-1), ScreenHeight- GridSpacing.y * (LineCount - 1)) / LineCount;
    }
    public Vector3 CalculateGridPosition(int x, int y)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(x * _GridSize + _GridSize / 2 + GridSpacing.x * x
            ,y * _GridSize + _GridSize / 2 + GridSpacing.y * y, 0));
        position.z = 0;
        return position;
    }
}

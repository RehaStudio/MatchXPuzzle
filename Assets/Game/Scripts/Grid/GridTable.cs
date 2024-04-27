using UnityEngine;
using Zenject;

public class GridTable : MonoBehaviour
{
    #region Fields
    public Grid[,] Grids;

    public int LineCount = 5;
    public Vector2 GridSpacing;

    private Grid.GridFactory _GridFactory;
    private float _GridSize;
    #endregion
    #region Properties
    public float ScreenWidth => Screen.width;
    public float ScreenHeight => Screen.height;
    #endregion

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
        Grids = new Grid[LineCount, LineCount];
        _GridSize = CalculateGridSize();
        for (int x = 0; x < LineCount; x++)
        {
            for (int y = 0; y < LineCount; y++)
            {
                Grid grid = CreateGrid();
                grid.SetPosition(x, y);
                grid.SetSize(_GridSize);
                grid.SetScreenPosition(CalculateGridPosition(x, y));
                Grids[x, y] = grid;
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
            ,ScreenHeight-  (y * _GridSize + _GridSize / 2 + GridSpacing.y * y)
            , 0));
        position.z = 0;
        return position;
    }
    public Grid GetGrid(Int2 position) 
    {
        if(position.X< 0 || position.Y< 0)
            return null;
        if (position.X >= LineCount || position.Y >= LineCount)
            return null;
        return Grids[position.X, position.Y];
    }
}

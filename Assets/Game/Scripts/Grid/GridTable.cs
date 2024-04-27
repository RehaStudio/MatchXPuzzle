using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class GridTable : MonoBehaviour
{
    #region Fields

    [SerializeField] private Vector2 GridSpacing;

    private Grid[,] _Grids;

    private Grid.Pool _GridPool;
    private float _GridSize;

    private Camera _MainCamera;
    #endregion
    #region Properties
    [field: SerializeField] public int LineCount { get; private set; } = 5;
    public float ScreenWidth => Screen.width;
    public float ScreenHeight => Screen.height;
    #endregion

    [Inject]
    public void Constructor(Grid.Pool gridPool)
    {
        _GridPool = gridPool;
    }

    public void SetLineCount(int count)
    {
        LineCount = count;
    }
    public void Rebuild()
    {
        Clear();
        CreateTable();
    }
    private void Clear()
    {
        foreach (var item in _Grids)
        {
            _GridPool.Despawn(item);
        }
    }
    public void CreateTable()
    {
        _Grids = new Grid[LineCount, LineCount];
        CalculateGridSize();
        float localScale = GetGridLocalScale();
        for (int x = 0; x < LineCount; x++)
        {
            for (int y = 0; y < LineCount; y++)
            {
                Grid grid = CreateGrid();
                grid.transform.SetParent(transform);
                grid.SetPosition(x, y);
                grid.SetScale(localScale);
                grid.SetScreenPosition(GetGridPosition(x, y));
                _Grids[x, y] = grid;
            }
        }
    }
    private Grid CreateGrid()
    {
        return _GridPool.Spawn();
    }

    public void CalculateGridSize()
    {
        _GridSize = Mathf.Min(ScreenWidth - GridSpacing.x * (LineCount - 1), ScreenHeight - GridSpacing.y * (LineCount - 1)) / LineCount;
    }
    public float GetGridLocalScale()
    {
        float worldScreenHeight = _MainCamera.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / (ScreenHeight - GridSpacing.y * (LineCount - 1)) * (ScreenWidth - GridSpacing.x * (LineCount - 1));
        return Mathf.Min(worldScreenHeight, worldScreenWidth) / LineCount;
    }
    public Vector3 GetGridPosition(int x, int y)
    {
        Vector3 position = _MainCamera.ScreenToWorldPoint(new Vector3(x * _GridSize + _GridSize / 2 + GridSpacing.x * x
            ,ScreenHeight-  (y * _GridSize + _GridSize / 2 + GridSpacing.y * y)
            , 0));
        position.z = 0;
        return position;
    }

    public Grid GetGrid(Int2 position) 
    {
        if(position.X < 0 || position.Y < 0)
            return null;
        if (position.X >= LineCount || position.Y >= LineCount)
            return null;
        return _Grids[position.X, position.Y];
    }
    #region Unity_Functions
    private void Awake()
    {
        _MainCamera = Camera.main;
        CreateTable();
    }
    #endregion
}

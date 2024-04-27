using System;
using System.Collections.Generic;
using Zenject;

public class GridManager 
{
    #region Events
    public event Action OnMatchSuccessed;
    #endregion
    #region Fields
    private GameManager _GameManager; 
    private HashSet<Grid> _Matches = new HashSet<Grid>();
    #endregion
    #region Properties
    public GridTable GridTable { get; private set; }
    #endregion
    [Inject]
    public void Constructor (GameManager gameManager, GridTable gridTable)
    {
        this._GameManager = gameManager;
        this.GridTable = gridTable;

        _GameManager.OnGameRebuild += OnGameRebuild;
    }
 
    private void OnGameRebuild(int lineCount)
    {
        GridTable.SetLineCount(lineCount);
        GridTable.Rebuild();
    }

    public void CheckMatchesOfGrid(Grid grid)
    {
        _Matches.Clear();
        AddMatchedGrid(grid);
        if (_Matches.Count < 3)
            return;
        MatchesSuccess();
    }
    private void AddMatchedGrid(Grid grid)
    {
        _Matches.Add(grid);
        CheckNeighbourGrids(grid);
    }
    private void CheckNeighbourGrids(Grid grid)
    {
        CheckMatchNextGrid(grid, Int2.UP);
        CheckMatchNextGrid(grid, Int2.DOWN);
        CheckMatchNextGrid(grid, Int2.LEFT);
        CheckMatchNextGrid(grid, Int2.RIGHT);
    }
    private void CheckMatchNextGrid(Grid grid, Int2 direction)
    {
        Int2 position = grid.Position + direction;
        Grid nextGrid = GridTable.GetGrid(position);
        if (nextGrid == null)
            return;
        if (nextGrid.IsSelected == false)
            return;
        if (_Matches.Contains(nextGrid))
            return;
        AddMatchedGrid(nextGrid);
    }
    private void MatchesSuccess()
    {
        foreach (var item in _Matches)
        {
            item.SetSelected(false);
        }
        OnMatchSuccessed?.Invoke();
    }
}

using System.Collections.Generic;
using Zenject;

public class GridManager 
{
    #region Fields
    private GridTable _GridTable;
    private HashSet<Grid> _Matches = new HashSet<Grid>();
    #endregion
    [Inject]
    public void Constructor (GridTable gridTable)
    {
        this._GridTable = gridTable;
    }
    public void CheckMatches(Grid grid)
    {
        _Matches.Clear();
        AddMatch(grid);
        if (_Matches.Count < 3)
            return;
        MatchesSuccess();
    }
    private void AddMatch(Grid grid)
    {
        _Matches.Add(grid);
        CheckNextGridMatch(grid, Int2.UP);
        CheckNextGridMatch(grid, Int2.DOWN);
        CheckNextGridMatch(grid, Int2.LEFT);
        CheckNextGridMatch(grid, Int2.RIGHT);
    }
    private void CheckNextGridMatch(Grid grid, Int2 turn)
    {
        Int2 position = grid.Position + turn;
        Grid nextGrid = _GridTable.GetGrid(position);
        if (nextGrid == null)
            return;
        if (nextGrid.IsSelected == false)
            return;
        if (_Matches.Contains(nextGrid))
            return;
        AddMatch(nextGrid);
    }
    private void MatchesSuccess()
    {
        foreach (var item in _Matches)
        {
            item.SetSelected(false);
        }
    }
}

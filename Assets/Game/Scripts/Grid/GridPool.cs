using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPool 
{
    private Grid.Pool _Pool;
    public GridPool(Grid.Pool pool)
    {
        _Pool = pool;
    }
    public Grid Spawn()
    {
        return _Pool.Spawn();
    }
    public void Despawn(Grid grid)
    {
        _Pool.Despawn(grid);
    }
}

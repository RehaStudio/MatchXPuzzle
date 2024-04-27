using UnityEngine;
using Zenject;

public class Grid : MonoBehaviour
{
    private GridManager _GridManager;
    private float GridSpriteSize = 128f;
    [Inject]
    private void Constructor(GridManager gridManager)
    {
        this._GridManager = gridManager;
    }
    public void SetSize(float size)
    {
        transform.localScale = Vector3.one * size / (GridSpriteSize*2);
    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public class GridFactory : PlaceholderFactory<Grid>
    { 
    
    }
}

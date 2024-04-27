using UnityEngine;
using Zenject;

public class Grid : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _SelectGameObject;
    private GridManager _GridManager;

    #endregion

    #region Properties
    public bool IsSelected { get; private set; }
    public Int2 Position { get; private set; }
    #endregion

    [Inject]
    private void Constructor(GridManager gridManager)
    {
        this._GridManager = gridManager;
    }
    public void SetSize(float size)
    {
        transform.localScale = Vector3.one * size / (Constants.GridSpriteSize * 2);
    }
    public void SetPosition(int x,int y)
    {
        Position = new Int2(x, y);
    }
    public void SetScreenPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void SetSelected(bool isSelected)
    {
        this.IsSelected = isSelected;
        _SelectGameObject.SetActive(isSelected);
    }
    public class GridFactory : PlaceholderFactory<Grid>
    { 
    
    }
    #region Unity_Functions
    private void OnMouseDown()
    {
        SetSelected(true);
        _GridManager.CheckMatches(this);
    }
    #endregion
}

using UnityEngine;
using Zenject;

public class Grid : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _SelectGameObject;
    private GridManager _GridManager;
    private SpriteRenderer _SpriteRenderer;
    #endregion

    #region Properties
    public bool IsSelected { get; private set; }
    public Int2 Position { get; private set; }
    #endregion

    [Inject]
    private void Constructor(GridManager gridManager)
    {
        this._GridManager = gridManager;
        this._SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetScale(float size)
    {
        float spriteSize = _SpriteRenderer.sprite.bounds.size.x;
        Vector3 localScale = Vector3.one * (size / spriteSize);
        transform.localScale = localScale;
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

    #region Unity_Functions
    private void OnMouseDown()
    {
        SetSelected(true);
        _GridManager.CheckMatchesOfGrid(this);
    }
    #endregion

    public class Pool : MemoryPool<Grid>
    {
        protected override void OnDespawned(Grid item)
        {
            item.gameObject.SetActive(false);
            item.SetSelected(false);
            base.OnDespawned(item);
        }
        protected override void OnSpawned(Grid item)
        {
            item.gameObject.SetActive(true);
            base.OnSpawned(item);
        }
    }
}

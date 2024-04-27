using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private HudPanel _HudPanel;
    #endregion
    private void Awake()
    {
        _HudPanel.Show();
    }
}

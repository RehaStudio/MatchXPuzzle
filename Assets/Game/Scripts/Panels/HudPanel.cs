using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
public class HudPanel : UIPanel
{

    #region Fields
    [SerializeField] private Button _RebuildButton;

    [SerializeField] private TMP_Text _MatchCountText;
    [SerializeField] private TMP_InputField _LineCountInputField;

    private GameManager _GameManager;
    private GridManager _GridManager;

    private int _MatchCount;
    #endregion
    [Inject]
    public void Constructor(GameManager gameManager,GridManager gridManager)
    {
        _GameManager = gameManager;
        _GridManager = gridManager;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _RebuildButton.onClick.AddListener(Rebuild);
        _GridManager.OnMatchSuccessed += OnMatchSuccessed;
    }
    private void Rebuild()
    {
        ResetMatchCount();
        _GameManager.GameRebuild(int.Parse(_LineCountInputField.text));
    }
    private void OnMatchSuccessed()
    {
        _MatchCount++;
        UpdateMatchCountText();
    }
    private void ResetMatchCount()
    {
        _MatchCount = 0;
        UpdateMatchCountText();
    }
    private void UpdateMatchCountText()
    {
        _MatchCountText.SetText("Match Count: " + _MatchCount);
    }
    public override void Show()
    {
        base.Show();
        _LineCountInputField.text = _GridManager.GridTable.LineCount.ToString();
    }
    public override void Hide()
    {
        base.Hide();
    }

    private void OnDestroy()
    {
        if (_RebuildButton != null)
            _RebuildButton.onClick.RemoveListener(Rebuild);
        if(_GridManager != null)
            _GridManager.OnMatchSuccessed -= OnMatchSuccessed;
    }
}

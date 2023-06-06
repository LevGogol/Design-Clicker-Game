using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelInfo _levelInfoPrefab;
    [SerializeField] private RectTransform _contentRoot;
    [SerializeField] private float _infoWidth = 600;

    private Levels _levels;
    private LevelInfo[] _levelsInfo;

    public void Initialize(Levels levels)
    {
        _levelsInfo = new LevelInfo[levels.Count];
        _levels = levels;
        for (int i = 0; i < _levels.Count; i++)
        {
            _levelsInfo[i] = Instantiate(_levelInfoPrefab, _contentRoot);
            _levelsInfo[i].StartDowned += LevelInfoOnStartDowned;
        }

        RedrawInfo();

        _contentRoot.sizeDelta = new Vector2(_levels.Count * _infoWidth, _contentRoot.sizeDelta.y);
    }

    public void RedrawInfo()
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            var level = _levels.GetPrefab(i);
            _levelsInfo[i].ID = i;
            _levelsInfo[i].Title = level.Title;
            var sprite = level.IsComplete ? level.OpenSprite : level.CloseSprite;
            _levelsInfo[i].SetComplete(level.IsComplete);
            _levelsInfo[i].Sprite = sprite;
        }

        SetAutoOffset();
    }

    private void SetAutoOffset()
    {
        int completeCount = 0;
        for (int i = 0; i < _levels.Count; i++)
        {
            if (_levels.GetPrefab(i).IsComplete)
                completeCount++;
            else
                break;
        }

        if (completeCount > 0)
        {
            completeCount--;
        }
        
        var position = _contentRoot.anchoredPosition;
        position.x = -_infoWidth * completeCount + _infoWidth / 2f;
        _contentRoot.anchoredPosition = position;
    }

    public void Show()
    {
        RedrawInfo();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            _levelsInfo[i].StartDowned -= LevelInfoOnStartDowned;
        }
    }

    private void LevelInfoOnStartDowned(int index)
    {
        _levels.LoadLevel(index);
        Hide();
    }
}
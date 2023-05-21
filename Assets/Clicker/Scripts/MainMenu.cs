using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelInfo[] _levelsInfo;
    
    private Levels _levels;
    
    public void Initialize(Levels levels)
    {
        _levels = levels;
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnButtonClicked(int index)
    {
        _levels.LoadLevel(index);
        Hide();
    }
}

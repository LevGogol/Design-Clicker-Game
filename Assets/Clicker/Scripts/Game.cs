using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private InputFacade _input;
    [SerializeField] private Screens _screens;
    [SerializeField] private CameraFacade _cameraFacade;
    [SerializeField] private Levels _levels;
    
    private float _startZoom;
    private bool IsFirstLevelComplete => PlayerPrefs.GetInt(_levels.GetPrefab(0).name, 0) == 1;

    private void Awake()
    {
        _levels.Initialize(_input, _screens, _cameraFacade);
        
        for (int i = 0; i < _levels.Count; i++)
        {
            var isComplete = PlayerPrefs.GetInt(_levels.GetPrefab(i).name, 0) == 1;
            _levels.GetPrefab(i).IsComplete = isComplete;
        }
        
        _screens.MainMenu.Initialize(_levels);

        if (IsFirstLevelComplete)
        {
            _screens.MainMenu.Show();
            _screens.HideHomeButton();
        }
        else
        {
            _screens.MainMenu.Hide();
            _screens.HideHomeButton();
            _levels.LoadLevel(0);
        }

        _startZoom = _cameraFacade.Camera.orthographicSize;
    }

    private void OnEnable()
    {
        _levels.LevelStarted += OnLevelStarted;
        _levels.LevelWon += OnLevelEnded;
    }

    private void OnLevelStarted(Level obj)
    {
        _cameraFacade.SetZoom(_startZoom);
        if(IsFirstLevelComplete)
            _screens.ShowHomeButton(ReturnToMenu);
    }

    private void OnLevelEnded(Level currentLevel)
    {
        PlayerPrefs.SetInt(_levels.GetPrefab(currentLevel.Index).name, 1);
        SetDefault();
    }

    private void ReturnToMenu()
    {
        _levels.FinishCurrent();
        SetDefault();
    }

    private void SetDefault()
    {
        _screens.MainMenu.Show();
        _screens.HideHomeButton();
        _cameraFacade.SetZoom(_startZoom);
        _cameraFacade.MoveTo(Vector3.zero);
    }
}
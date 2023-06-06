using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private InputFacade _input;
    [SerializeField] private Screens _screens;
    [SerializeField] private CameraFacade _cameraFacade;
    [SerializeField] private Levels _levels;

    private void Awake()
    {
        _levels.Initialize(_input, _screens, _cameraFacade);
        
        for (int i = 0; i < _levels.Count; i++)
        {
            var isComplete = PlayerPrefs.GetInt(_levels.GetPrefab(i).name, 0) == 1;
            _levels.GetPrefab(i).IsComplete = isComplete;
        }
        
        _screens.MainMenu.Initialize(_levels);

        bool isFirstLevelComplete = PlayerPrefs.GetInt(_levels.GetPrefab(0).name, 0) == 1;
        if (isFirstLevelComplete)
        {
            _screens.MainMenu.Show();
        }
        else
        {
            _screens.MainMenu.Hide();
            _levels.LoadLevel(0);
        }
    }

    private void OnEnable()
    {
        _levels.LevelEnded += OnLevelEnded;
    }

    private void OnLevelEnded(Level currentLevel)
    {
        PlayerPrefs.SetInt(_levels.GetPrefab(currentLevel.Index).name, 1);
        _screens.MainMenu.Show();
    }
}
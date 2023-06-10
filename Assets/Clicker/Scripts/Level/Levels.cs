using System;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private Level[] _levels;

    private InputFacade _input;
    private Screens _screens;
    private CameraFacade _cameraFacade;
    private Level _currentLevel;

    public event Action<Level> LevelStarted;
    public event Action<Level> LevelWon;
    public int Count => _levels.Length;

    public void Initialize(InputFacade input, Screens screens, CameraFacade cameraFacade)
    {
        _input = input;
        _screens = screens;
        _cameraFacade = cameraFacade;

        for (int i = 0; i < Count; i++)
        {
            _levels[i].Index = i;
        }
    }

    public void LoadLevel(int index)
    {
        _levels[index].gameObject.SetActive(false);
        var level = Instantiate(_levels[index], transform);
        level.Initialize(_input, _screens, _cameraFacade);
        level.gameObject.SetActive(true);
        level.Ended += LevelOnEnded;
        _currentLevel = level;
        
        LevelStarted?.Invoke(level);
    }

    public Level GetPrefab(int i)
    {
        return _levels[i];
    }

    public void FinishCurrent()
    {
        _currentLevel.Ended -= LevelOnEnded;
        Destroy(_currentLevel.gameObject);
        _screens.HideNextButton();
    }

    private void LevelOnEnded()
    {
        FinishCurrent();
        _levels[_currentLevel.Index].IsComplete = true;

        LevelWon.Invoke(_currentLevel);
    }
}
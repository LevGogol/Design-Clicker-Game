using System;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private Level[] _levels;
    
    private InputFacade _input;
    private Screens _screens;
    private CameraFacade _cameraFacade;
    private Level _currentLevel;
    
    public event Action LevelEnded = delegate {  };

    public void Initialize(InputFacade input, Screens screens, CameraFacade cameraFacade)
    {
        _input = input;
        _screens = screens;
        _cameraFacade = cameraFacade;
    }

    public void LoadLevel(int index)
    {
        _levels[index].gameObject.SetActive(false);
        var level = Instantiate(_levels[index], transform);
        level.Initialize(_input, _screens, _cameraFacade);
        level.gameObject.SetActive(true);
        level.Ended += LevelOnEnded;
        _currentLevel = level;
    }

    private void LevelOnEnded()
    {
        _currentLevel.Ended -= LevelOnEnded;
        Destroy(_currentLevel.gameObject);
        _screens.HideNextButton();
        LevelEnded.Invoke();
    }
}
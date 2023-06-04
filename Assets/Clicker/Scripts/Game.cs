using System;
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
        _screens.MainMenu.Initialize(_levels);
        _screens.MainMenu.Hide();
        
        _levels.LoadLevel(0);
    }

    private void OnEnable()
    {
        _levels.LevelEnded += OnLevelEnded;
    }

    private void OnLevelEnded()
    {
        _screens.MainMenu.Show();
    }
}
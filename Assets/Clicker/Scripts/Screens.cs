using System;
using UnityEngine;
using UnityEngine.UI;

public class Screens : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private MainMenu _mainMenu;

    public MainMenu MainMenu => _mainMenu;

    public void ShowNextButton(Action callback)
    {
        _nextButton.gameObject.SetActive(true);
        _nextButton.onClick.AddListener(() =>
        {
            callback.Invoke();
            _nextButton.onClick.RemoveAllListeners();
        });
    }

    public void HideNextButton()
    {
        _nextButton.gameObject.SetActive(false);
        _nextButton.onClick.RemoveAllListeners();
    }
}
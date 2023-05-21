using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Screens : MonoBehaviour
{
    [SerializeField] private CoinsUI _coinsUI;
    [SerializeField] private CoinsPerSecondUI _coinsPerSecondUI;
    [SerializeField] private CoinsPerClickUI _coinsPerClickUI;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Shop _shop;
    [SerializeField] private Popap _popap;
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
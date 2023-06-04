using System;
using DG.Tweening;
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
        _nextButton.transform.DOScale(1f, 0.4f).From(new Vector3(1, 0, 1));
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
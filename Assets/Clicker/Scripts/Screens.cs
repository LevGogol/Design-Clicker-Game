using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Screens : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Image _SoundButtonImage;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    public MainMenu MainMenu => _mainMenu;

    public void ShowHomeButton(Action callback)
    {
        _homeButton.gameObject.SetActive(true);
        _homeButton.onClick.RemoveAllListeners();
        _homeButton.onClick.AddListener(() =>
        {
            callback.Invoke();
        });
    }

    public void HideHomeButton()
    {
        _homeButton.gameObject.SetActive(false);
    }

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

    public void SoundOn()
    {
        _SoundButtonImage.sprite = _soundOn;
    }

    public void SoundOff()
    {
        _SoundButtonImage.sprite = _soundOff;
    }
}
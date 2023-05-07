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

    public CoinsUI CoinsUI => _coinsUI;
    public CoinsPerSecondUI CoinsPerSecondUI => _coinsPerSecondUI;
    public CoinsPerClickUI CoinsPerClickUI => _coinsPerClickUI;
    public Shop Shop => _shop;
    public Canvas Canvas => _canvas;
    public Popap Popap => _popap;

    public void ShowNextButton()
    {
        _nextButton.gameObject.SetActive(true);
        _nextButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }
}
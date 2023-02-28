using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    
    private int _oldValue;
    private int _currentValue;
    private Coroutine _coroutine;

    public void ForceDrawCoins(int value)
    {
        _textMesh.text = value + "₽";
        _oldValue = value;
        _currentValue = value;
    }

    public void DrawCoins(int value)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _oldValue = _currentValue;
            _coroutine = null;
        }
        
        _coroutine = StartCoroutine(SoftChangeTo(value));
    }

    private IEnumerator SoftChangeTo(int value)
    {
        var delay = 0.1f;
        var timer = delay;

        while (timer > 0f)
        {
            _currentValue = Mathf.FloorToInt(Mathf.Lerp(_oldValue, value, 1 - timer/delay));
            _textMesh.text = _currentValue + "₽";

            yield return null;
            timer -= Time.deltaTime;
        }
        
        _textMesh.text = value + "₽";
        _oldValue = value;
        _coroutine = null;
    }
}
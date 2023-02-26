using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinsPerSecondUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    
    public void DrawCoinsPerSecond(int value)
    {
        _textMesh.text = value.ToString() + "₽/сек";
    }

}
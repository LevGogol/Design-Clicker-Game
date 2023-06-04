using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    [HideInInspector] public int ID;

    [SerializeField] private TextMeshProUGUI _titleMesh;
    [SerializeField] private Button _startButton;
    [SerializeField] private Image _image;
    [SerializeField] private Image _background;
    [SerializeField] private Color _open;
    [SerializeField] private Color _close;

    public event Action<int> StartDowned;

    public string Title
    {
        set { _titleMesh.text = value; }
    }

    public Sprite Sprite
    {
        set { _image.sprite = value; }
    }

    public void SetComplete(bool isComplete)
    {
        var color = isComplete ? _close : _open;
        _background.color = color;
    }

    private void Awake()
    {
        _startButton.onClick.AddListener(() => { StartDowned?.Invoke(ID); });
    }
}
using System;
using System.Collections.Generic;
using Azur.PlayableTemplate.Sound;
using Lean.Localization;
using TMPro;
using UnityEngine;

[Serializable]
public class Flag
{
    public string Name;
    public string ViewName;
    public Sprite Image;
}

public class LanguageSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private Flag[] _flags;
    [SerializeField] private ClipSource _clipSource;

    private void Start()
    {
        var languages = LeanLocalization.CurrentLanguages;
        var list = new List<TMP_Dropdown.OptionData>();
        foreach (var language in languages)
        {
            var flag = FindFlag(language.Key);
            list.Add(new TMP_Dropdown.OptionData(flag.ViewName, flag.Image));
        }

        _dropdown.ClearOptions();
        _dropdown.AddOptions(list);

        for (int i = 0; i < _dropdown.options.Count; i++)
        {
            var flag = FindFlag(LeanLocalization.GetFirstCurrentLanguage());
            if (_dropdown.options[i].text == flag.ViewName)
            {
                _dropdown.value = i;
                break;
            }
        }
        
        _dropdown.onValueChanged.AddListener(OnValueChange);
    }

    private Flag FindFlag(string name)
    {
        foreach (var flag in _flags)
        {
            if (flag.Name == name)
                return flag;
        }

        return null;
    }

    private void OnValueChange(int value)
    {
        _clipSource.Play();
        
        foreach (var flag in _flags)
        {
            if (flag.ViewName == _dropdown.options[value].text)
            {
                LeanLocalization.SetCurrentLanguageAll(flag.Name);
                return;
            }
        }

        Debug.LogError("LostData");
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _levelNumberLabel;

    private ILevelLoader _levelLoader;
    private Action _closeParentWindowFunc;
    private int _levelNumber;

    [Inject]
    private void Construct(ILevelLoader levelLoader)
    {
        _levelLoader = levelLoader;
    }
    private void Awake()
    {
        _button.onClick.AddListener(() => SelectLevel());
    }

    public void Initialize(int levelNumber, Action closeParentWindowFunc)
    {
        _levelNumber = levelNumber;
        _levelNumberLabel.text = levelNumber.ToString();
        _closeParentWindowFunc = closeParentWindowFunc;
    }

    private async void SelectLevel()
    {
        await _levelLoader.LoadLevel(_levelNumber);
        _closeParentWindowFunc.Invoke();
    }

 
}

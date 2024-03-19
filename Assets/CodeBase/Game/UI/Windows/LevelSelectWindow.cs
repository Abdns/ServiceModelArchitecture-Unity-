using UnityEngine;

public sealed class LevelSelectWindow : WindowBase
{
    [SerializeField] private GameObject _levelButtonContainer;

    public void SetLevelButtonToContainer(LevelButton button)
    {
        button.transform.SetParent(_levelButtonContainer.transform);
    }
}

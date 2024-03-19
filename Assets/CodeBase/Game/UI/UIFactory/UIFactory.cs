using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class UIFactory : IUIFactory
{
    private readonly IAsssetProvider _assets;
    private readonly DiContainer _container;
    private Transform _uiRoot;
    public UIFactory(DiContainer containers, IAsssetProvider assets)
    {
        _container = containers;
        _assets = assets;
    }

    public async Task CreateUIRoot()
    {
        GameObject prefab = await _assets.Load<GameObject>(AssetsAdress.UiRoot);
        _uiRoot = Object.Instantiate(prefab).transform;
    }

    public async Task CreateLevelsWindow()
    {
        GameObject levelWindowPrefab = await _assets.Load<GameObject>(AssetsAdress.LevelsWindow);
        LevelSelectWindow LevelSelectWindow =  Object.Instantiate(levelWindowPrefab, _uiRoot).GetComponent<LevelSelectWindow>();
    
        int levelsCount = await _assets.GetObjectsCountByLabel("Level");
        await CreateLevelButtons(levelsCount, LevelSelectWindow);

    }
    private async Task CreateLevelButtons(int levelCount, LevelSelectWindow levelsWindow)
    {
        GameObject levelButtonPrefab = await _assets.Load<GameObject>(AssetsAdress.LevelButton);

        for (int levelNumber = 1; levelNumber <= levelCount; levelNumber++)
        {
            LevelButton buttonObj = _container.InstantiatePrefabForComponent<LevelButton>(levelButtonPrefab, Vector3.zero, Quaternion.identity, null);
      
            buttonObj.Initialize(levelNumber, () => Object.Destroy(levelsWindow.gameObject));

            levelsWindow.SetLevelButtonToContainer(buttonObj);
            buttonObj.transform.localScale = Vector3.one;
        }

    }

}


using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private readonly IAsssetProvider _assets;
    private readonly DiContainer _container;
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameFactory(DiContainer containers, IAsssetProvider assets)
    {
        _container = containers;
        _assets = assets;
    }

    public void WarmUp()
    {

    }

    public async Task CreateHero(Vector3 position)
    {
        GameObject playerPrefab = await _assets.Load<GameObject>(AssetsAdress.HeroPath);
        Player player = _container.InstantiatePrefabForComponent<Player>(playerPrefab, position, Quaternion.identity, null);

        _container.Bind<Player>().FromInstance(player).AsSingle();

        RegisterProgressWatcchers(playerPrefab);
    }
    public async Task CreateHud()
    {
        GameObject prefab = await _assets.Load<GameObject>(AssetsAdress.HudPath);  
        Object.Instantiate(prefab);
    }

    public async Task<GameObject> CreateLevel(int lvlNumber)
    {
        GameObject prefab = await _assets.Load<GameObject>(AssetsAdress.LevelsPath + lvlNumber.ToString());
        GameObject instPrefab = Object.Instantiate(prefab);

        InjectInChilds(instPrefab);

        return instPrefab;
    }

    public void Cleanup()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();
        _assets.CleanUp();
    }

    private void InjectInChilds(GameObject parentPrefab)
    {
        List<Transform> prefabChilds = parentPrefab.GetComponentsInChildren<Transform>().ToList();

        foreach (Transform child in prefabChilds)
        {
            _container.InjectGameObject(child.gameObject);
        }
    }

    private void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
        {
            ProgressWriters.Add(progressWriter);
        }
        ProgressReaders.Add(progressReader);

    }
    private void RegisterProgressWatcchers(GameObject gameObject)
    {
        foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        {
            Register(progressReader);
        }
    }


}


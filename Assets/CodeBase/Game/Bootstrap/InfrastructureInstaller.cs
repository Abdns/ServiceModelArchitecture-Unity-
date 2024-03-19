using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
    [SerializeField] private Coroutines _coroutinesRunner;
    public override void InstallBindings()
    {
        RegisterServices();
        RegisterFabrics();
        RegisterGamePlayServices();
    }

    private void RegisterServices()
    {
        Coroutines coroutineRunner = Container.InstantiatePrefabForComponent<Coroutines>(_coroutinesRunner);
        Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner).AsSingle().NonLazy();

        AssetProvider assetProvider = new AssetProvider();
        assetProvider.Initialize();
        Container.Bind<IAsssetProvider>().FromInstance(assetProvider).AsSingle().NonLazy();

        Container.Bind<SceneLoader>().AsSingle().NonLazy();
    
        Container.Bind<IInputService>().To<DesktopInput>().AsSingle().NonLazy();
        Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle().NonLazy();   
        Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle().NonLazy();

    }

    private void RegisterFabrics()
    {
        Container.Bind<StateFactory>().AsSingle().NonLazy();

        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle().NonLazy();
        Container.Bind<IUIFactory>().To<UIFactory>().AsSingle().NonLazy();
    }

    private void RegisterGamePlayServices()
    {
        Container.Bind<ILevelLoader>().To<LevelLoader>().AsSingle().NonLazy();
    }

 

     
}

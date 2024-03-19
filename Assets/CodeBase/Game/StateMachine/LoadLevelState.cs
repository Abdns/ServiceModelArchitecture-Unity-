using System.Threading.Tasks;
using UnityEngine;

public class LoadLevelState : IPayLoadState<Scenes>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IPersistentProgressService _progressService;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IUIFactory uiFactory, IPersistentProgressService persistentProgress)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _uiFactory = uiFactory;
        _progressService = persistentProgress;
    }

    public void Enter(Scenes scene)
    {
        _gameFactory.Cleanup();        
        _sceneLoader.Load(scene, OnLoaded);
    }

    public void Exit()
    {
    }

    private async void OnLoaded()
    {
        await InitUI();
        await InitGameWorld();

        InformProgressReaders();

        _stateMachine.Enter<GameLoopState>();
    }

    private async Task InitGameWorld()
    {
        await _gameFactory.CreateHero(new Vector3(1, 0, 1));
    }

    private async Task InitUI()
    {
        await _uiFactory.CreateUIRoot();
        await _uiFactory.CreateLevelsWindow();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgres(_progressService.Progress);
        }
    }
}


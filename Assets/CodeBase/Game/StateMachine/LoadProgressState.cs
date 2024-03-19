public class LoadProgressState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService )
    {
        _stateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
    }
    public void Enter()
    {
        LoadProgressOrInitNew();
        _stateMachine.Enter<LoadLevelState, Scenes>(Scenes.Game);
    }
    public void Exit()
    {
       
    }
    public void LoadProgressOrInitNew()
    {
        _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
    }

    private PlayerProgress NewProgress() => new PlayerProgress(0);
   
}


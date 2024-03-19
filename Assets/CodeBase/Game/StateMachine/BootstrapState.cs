using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        _sceneLoader.Load(Scenes.Initial, EnterLoadLeveL);
    }
    public void Exit()
    {

    }
    private void EnterLoadLeveL()
    {
        _stateMachine.Enter<LoadProgressState>();
    }
  
    
}


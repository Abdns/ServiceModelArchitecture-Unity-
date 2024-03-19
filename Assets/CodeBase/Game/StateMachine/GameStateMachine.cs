using System;
using System.Collections.Generic;
using Zenject;

public class GameStateMachine : IInitializable
{
    private readonly StateFactory _stateFactory;

    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(StateFactory stateFactory)
    {
        _stateFactory = stateFactory;       
    }

    public void Initialize()
    {
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = _stateFactory.CreateState<BootstrapState>(),
            [typeof(LoadProgressState)] = _stateFactory.CreateState<LoadProgressState>(),
            [typeof(LoadLevelState)] = _stateFactory.CreateState<LoadLevelState>(),
            [typeof(GameLoopState)] = _stateFactory.CreateState<GameLoopState>(),
        };

        Enter<BootstrapState>();

    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadState<TPayLoad>
    {
        TState state = ChangeState<TState>();
        state.Enter(payLoad);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();
        TState state = GetState<TState>();
        _activeState = state;
        return state;

    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
        return _states[typeof(TState)] as TState;
    }
}

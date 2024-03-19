using Zenject;

public class StateMachineInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        RegisterStates();

        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
    }

    private void RegisterStates()
    {
        Container.Bind<BootstrapState>().AsSingle().NonLazy();
        Container.Bind<LoadProgressState>().AsSingle().NonLazy();
        Container.Bind<LoadLevelState>().AsSingle().NonLazy();
        Container.Bind<GameLoopState>().AsSingle().NonLazy();
    }

}


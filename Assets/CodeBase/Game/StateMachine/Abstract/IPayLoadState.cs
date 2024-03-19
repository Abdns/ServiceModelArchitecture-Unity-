public interface IPayLoadState<TPayLoad> : IExitableState
{
    void Enter(TPayLoad payload);
}


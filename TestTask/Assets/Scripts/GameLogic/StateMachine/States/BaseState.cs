public abstract class BaseState
{
    protected readonly IStateSwitcher StateSwitcher;

    public BaseState(IStateSwitcher stateSwitcher) => StateSwitcher = stateSwitcher;

    public abstract void Start();

    public abstract void Stop();
}

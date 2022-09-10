
public class StartState : BaseState
{
    private readonly InputManager _inputManager;


    public StartState(IStateSwitcher stateSwitcher, InputManager inputManager) : base(stateSwitcher)
    {
        _inputManager = inputManager;
    }

    public override void Start()
    {
        _inputManager.OnClick = Stop;
    }

    public override void Stop()
    {
        _inputManager.OnClick = null;
        StateSwitcher.SwitchState<RunState>();
    }
}

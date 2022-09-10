public class ShootingState : BaseState
{
    private readonly TargetsSpotter _targetsSpotter;
    private readonly Animatable _animatable;

    public ShootingState(IStateSwitcher stateSwitcher, TargetsSpotter targetsSpotter, Animatable animatable)
	: base(stateSwitcher)
	{
        _targetsSpotter = targetsSpotter;
        _animatable = animatable;
    }

	public override void Start()
	{
        _targetsSpotter.OnTargetsCleared = Stop;
        _animatable.PlayIdle();
	}

	public override void Stop()
	{
        _targetsSpotter.OnTargetsCleared = null;
        StateSwitcher.SwitchState<RunState>();
	}
}

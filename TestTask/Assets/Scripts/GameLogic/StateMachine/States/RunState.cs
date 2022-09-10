public class RunState : BaseState
{
    private readonly PathDirector _pathDirector;
    private readonly Animatable _animatable;


    public RunState(IStateSwitcher stateSwitcher, PathDirector pathDirector, Animatable animatable) 
        : base(stateSwitcher)
    {
        _pathDirector = pathDirector;
        _animatable = animatable;
    }

    public override void Start()
    {
        _pathDirector.WaypointReached = StopByWaypointType;
        _pathDirector.MoveNextWaypoint();
        _animatable.PlayRun();
    }

    public override void Stop()
    {
        _pathDirector.WaypointReached = null;
        StateSwitcher.SwitchState<ShootingState>();
    }

    private void StopByWaypointType(WaypointType waypointType)
    {
        _pathDirector.WaypointReached = null;
        switch (waypointType)
        {
            case WaypointType.START:
                StateSwitcher.SwitchState<RunState>();
                break;
            case WaypointType.MAIN:
                StateSwitcher.SwitchState<ShootingState>();
                break;
            case WaypointType.FINAL:
                StateSwitcher.SwitchState<FinishState>();
                break;
            default:
                throw new System.ArgumentException("No such waypoint type");
        }
    }
}

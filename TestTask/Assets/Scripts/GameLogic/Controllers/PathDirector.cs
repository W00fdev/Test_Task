using UnityEngine.Events;
using UnityEngine;


public class PathDirector : MonoBehaviour
{
    [Tooltip("Player's start position/rotation is first waypoint!")]
    [SerializeField] private Waypoint[] _waypoints;
    private int _waypointIndex = 0;

    [SerializeField] private Movable _movablePlayer;

    public UnityAction<WaypointType> WaypointReached;

    private void Awake() => _movablePlayer.OnDestinationReached = OnWaypointReached;

    private void Start()
    {
        if (_waypoints.Length == 0)
            throw new System.Exception("Waypoints aren't set");

        _movablePlayer.ForceNewPositionRotation(_waypoints[_waypointIndex].Position,
            _waypoints[_waypointIndex].Rotation);
        _waypointIndex++;
    }

    public void MoveNextWaypoint()
    {
        if (_waypointIndex >= _waypoints.Length)
            throw new System.OverflowException("Trying to get unexisted waypoint catched");

        _movablePlayer.SetNewDestination(_waypoints[_waypointIndex].Position);
    }

    private void OnWaypointReached()
    {
        // Rotate player for shooting
        _movablePlayer.SetNewRotation(_waypoints[_waypointIndex].Rotation);
        WaypointReached?.Invoke(_waypoints[_waypointIndex++].Type);
    }
}

/// <summary>
/// START - for start waypoint to place player
/// MAIN - for breakpoints (we took their rotation for player)
/// FINAL - for final breakpoint
/// </summary>
public enum WaypointType { START, MAIN, FINAL }

[System.Serializable]
public class Waypoint
{
    [SerializeField] private Transform _waypoint;
    [SerializeField] private WaypointType _type;

    public WaypointType Type => _type;
    public Vector3 Position => _waypoint.position;
    public Vector3 Rotation => _waypoint.rotation.eulerAngles;
}
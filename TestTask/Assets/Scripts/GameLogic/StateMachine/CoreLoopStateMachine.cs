using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CoreLoopStateMachine : MonoBehaviour, IStateSwitcher
{
    [SerializeField] private PathDirector _pathDirector;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private TargetsSpotter _targetsSpotter;
    [SerializeField] private Animatable _animatablePlayer;

    // Disable shooting when not in shooting state
    [SerializeField] private PlayerShooting _playerShooting;

    private BaseState _currentState;
    private List<BaseState> _states;

    private void Start()
    {
        _states = new List<BaseState>()
        {
            new StartState(this, _inputManager),
            new RunState(this, _pathDirector, _animatablePlayer),
            new ShootingState(this, _targetsSpotter, _animatablePlayer),
            new FinishState(this, _animatablePlayer)
        };

        _currentState = _states[0];
        _currentState.Start();
    }

    public void SwitchState<T>() where T: BaseState
    {
        _currentState = _states.FirstOrDefault((stateType) => stateType is T);
        _playerShooting.CanShoot = _currentState is ShootingState;
        _currentState.Start();
    }
}
